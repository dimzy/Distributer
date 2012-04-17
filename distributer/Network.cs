using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace distributer
{
    public struct Protocol
    {
        public const String BASE_CMD = "c";
        public const String BASE_FILE = "f";
        public const String WORK_REQUESTED = "cl_work_r";
        public const String WORK_RESPONSE = "sv_work_r";
        public const String WORK_FINISHED = "cl_work_d";
        public const String WORK_STOP = "sv_work_stop";
        public const String STATUS_REQUEST = "sv_status";
        public const String STATUS_RESPONSE = "cl_status";
        public const String LOG = "cl_log";
        public const Char   SEPARATOR = ':';
        public const Int32  BUFFER_SIZE = 1024;
    }
    public struct Command
    {
        public String ip;
        public String command;
        public String message;
    }

    public abstract class NetworkListener
    {
        public abstract String HandleCommand(Command cmd);
        public abstract void HandleFile(String file);
    }

    public class NetworkServer
    {
        public static String TempFileDirectory = @".\netfiles\";
        Boolean mStop;
        Int32 mPort;
        IPAddress mAddress;
        List<String> mAddresses;

        TcpListener mListener;
        Thread mListenThread;

        NetworkListener mCallback;

        public NetworkServer(String ip_mask, Int32 port, NetworkListener callback)
        {
            mStop = true;
            mPort = port;
            mListenThread = null;
            mCallback = callback;
            mAddress = IPAddress.Parse("127.0.0.1");
            SetIPByMask(ip_mask);

            mAddresses = new List<string>();
            IPHostEntry ip = Dns.GetHostEntry(Dns.GetHostName());
            for (int i = 0; i < ip.AddressList.Length; i++)
                 mAddresses.Add(ip.AddressList[i].ToString());
        }

        public String GetMyIP()
        {
            return mAddress.ToString();
        }
        public List<String> GetIPList()
        {
            return mAddresses;
        }

        public void StartListening()
        {
            if (mStop && mListenThread == null)
            {
                mStop = false;
                mListenThread = new Thread(new ThreadStart(this.Listen));
                mListenThread.Start();
            }
            else
            {
                Log("Network already listening.");
            }
        }
        public void StopListening()
        {
            mStop = true;
            if (mListener != null) 
                mListener.Stop();
            if(mListenThread != null)
                mListenThread.Join();
            mListenThread = null;
        }
        private void Listen()
        {
            try
            {
                mListener = new TcpListener(mAddress, mPort);
                mListener.Start();
            }
            catch
            {
                Log("Couldn't start TcpListener.");
            }

            while (!mStop)
            {
                TcpClient tcp = null;
                NetworkStream stream = null;
                try
                {
                    tcp = mListener.AcceptTcpClient();
                    stream = tcp.GetStream();
                }
                catch (ThreadAbortException e)
                {
                    // thread was killed to restart
                    return;
                }
                catch (Exception e)
                {
                    //Log(e.StackTrace);
                    // i dunno
                }
                Int32 bytesReceived = 0;

                do
                {
                    byte[] buffer = new byte[Protocol.BUFFER_SIZE];
                    try
                    {
                        bytesReceived = stream.Read(buffer, 0, Protocol.BUFFER_SIZE);
                    }
                    catch (Exception e)
                    {
                        //Log(e.StackTrace);
                        Log("Unable to read from stream.");
                        break;
                    }
                    String encode = Encoding.UTF8.GetString(buffer, 0, bytesReceived);

                    if(encode.Length > 0)
                        switch (encode.Substring(0, 1))
                        {
                            case Protocol.BASE_CMD:
                                ProcessCommand(stream, encode.Substring(1), tcp.Client.RemoteEndPoint.ToString().Split(':')[0]);
                                break;
                            case Protocol.BASE_FILE:
                                ProcessFile(stream, buffer, bytesReceived);
                                break;
                        }
                } while (bytesReceived > 0);

                try { stream.Close(); } catch { /* was null */ }
                try { tcp.Close(); } catch { /* was null */ }
                    
            }
            try { mListener.Stop(); } catch { /* was null */ }
        }
        private void ProcessFile(NetworkStream stream, byte[] current, Int32 received)
        {
            if (!Directory.Exists(NetworkServer.TempFileDirectory))
                Directory.CreateDirectory(NetworkServer.TempFileDirectory);

            Int32 startOffset = Encoding.UTF8.GetByteCount(Protocol.BASE_FILE);
            Int32 bytesReceived = 0;
            Int32 filenamelen = BitConverter.ToInt32(current, startOffset);
            String filename = Encoding.UTF8.GetString(current, 4 + startOffset, filenamelen);
            BinaryWriter writer = new BinaryWriter(File.Open(NetworkServer.TempFileDirectory + filename, FileMode.Create));
            writer.Write(current, 4 + startOffset + filenamelen, received - (4 + startOffset + filenamelen));

            do
            {
                byte[] buffer = new byte[Protocol.BUFFER_SIZE];
                bytesReceived = stream.Read(buffer, 0, Protocol.BUFFER_SIZE);
                writer.Write(buffer, 0, bytesReceived);
            } while (bytesReceived > 0);

            writer.Close();
            mCallback.HandleFile(filename);
        }
        private void ProcessCommand(NetworkStream stream, String message, String ip)
        {
            Command cmd = new Command();
            cmd.ip = ip;
            String[] spl = message.Split(Protocol.SEPARATOR);
            if (spl.Length > 0)
                cmd.command = spl[0];
            if (spl.Length > 1)
                cmd.message = message.Substring(spl[0].Length + 1);

            String response = mCallback.HandleCommand(cmd);

            if (response.Length > 0)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(response);
                stream.Write(buffer, 0, buffer.Length);
            }
        }


        public void SetIPByMask(String mask)
        {
            IPHostEntry ip = Dns.GetHostEntry(Dns.GetHostName());
            for (int i = 0; i < ip.AddressList.Length; i++)
                if (ip.AddressList[i].ToString().StartsWith(mask))
                    mAddress = ip.AddressList[i];
        }
        public void Log(String message)
        {
            Console.WriteLine(message);
        }
    }

    public class NetworkClient
    {
        public static Command SendCommand(String ip, Int32 port, String command, String data, Boolean expectReturn)
        {
            Command cmd = new Command();
            cmd.ip = "";
            cmd.message = "";
            cmd.command = "";

            TcpClient tcp = new TcpClient();
            try
            {
                tcp.Connect(ip, port);
            }
            catch
            {
                Console.WriteLine("Error connecting to send command.");
                return cmd;
            }
            NetworkStream stream = tcp.GetStream();

            byte[] buffer = Encoding.UTF8.GetBytes(Protocol.BASE_CMD + command + ":" + data);
            stream.Write(buffer, 0, buffer.Length);

            if (expectReturn)
            {
                try
                {
                    byte[] response = new byte[Protocol.BUFFER_SIZE];
                    int bytesReceived = stream.Read(response, 0, Protocol.BUFFER_SIZE);
                    String encoded = Encoding.UTF8.GetString(response);
                    String[] spl = encoded.Split(Protocol.SEPARATOR);
                    if (spl.Length > 0)
                        cmd.command = spl[0];
                    if (spl.Length > 1)
                        cmd.message = encoded.Substring(spl[0].Length + 1, bytesReceived - cmd.command.Length - 1);
                }
                catch
                {
                    Console.WriteLine("Error receiving data");
                }
            }

            stream.Close();
            tcp.Close();
            return cmd;
        }
        public static bool SendFile(String ip, Int32 port, String file)
        {
            try
            {
                TcpClient tcp = new TcpClient();
                tcp.Connect(ip, port);
                NetworkStream stream = tcp.GetStream();

                byte[] cmd = Encoding.UTF8.GetBytes(Protocol.BASE_FILE);
                byte[] filename = Encoding.UTF8.GetBytes(file);
                byte[] filedata = File.ReadAllBytes(@".\" + file);
                byte[] filenamelen = BitConverter.GetBytes(filename.Length);
                byte[] clientdata = new byte[4 + cmd.Length + filename.Length + filedata.Length];

                cmd.CopyTo(clientdata, 0);
                filenamelen.CopyTo(clientdata, cmd.Length);
                filename.CopyTo(clientdata, 4 + cmd.Length);
                filedata.CopyTo(clientdata, 4 + cmd.Length + filename.Length);

                stream.Write(clientdata, 0, clientdata.Length);
                stream.Close();
                tcp.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
