using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading;

using distributer;

namespace distributer_client
{
    public enum DistributerStatus
    {
        STOPPED = 0,
        WAITING,
        WORKING,
        COMPRESSING,
        UPLOADING,
        PATCHING
    }

    public class DistributerClient : NetworkListener
    {
        NetworkServer mServer;
        DistributerStatus mStatus;
        Settings mSettings;
        String mSettingsFile;

        List<Job> mJobs;
        Process mProcess;
        bool mStop;
        String mPatchFile;

        public DistributerClient(String settingsFile)
        {
            mPatchFile = "";
            mSettingsFile = settingsFile;
            mSettings = new Settings(mSettingsFile);
            mStatus = DistributerStatus.STOPPED;
            mJobs = Persistance.LoadJobs("jobs.xml");
            mServer = new NetworkServer(mSettings.getString("ip_mask"), mSettings.getInteger("client_port"), this);
            mServer.StartListening();
        }

        public void Start()
        {
            mStop = false;
            while (!mStop)
            {
                if (ApplyPatch())
                {
                    mSettings = new Settings(mSettingsFile);
                    mServer.StopListening();
                    mServer = new NetworkServer(mSettings.getString("ip_mask"), mSettings.getInteger("client_port"), this);
                    mServer.StartListening();
                }

                Command response = NetworkClient.SendCommand(
                        mSettings.getString("server_ip"),
                        mSettings.getInteger("server_port"),
                        Protocol.WORK_REQUESTED,
                        "",
                        true);

                if (response.command == Protocol.WORK_RESPONSE
                    && response.message.Length > 0
                    && response.message != "none")
                {
                    RunApplication(Job.Deserialize(response.message));
                }
                else
                {
                    Thread.Sleep(1000);
                }

                Upload();

                Thread.Sleep(1000);
            }
        }
        public void Stop()
        {
            mStop = true;
            mProcess.Kill();
            Persistance.StoreJobs(mJobs, "jobs.xml");
            mServer.StopListening();
        }
        public void Log(String str)
        {
            Console.WriteLine(str);
            NetworkClient.SendCommand(mSettings.getString("server_ip"), mSettings.getInteger("server_port"), Protocol.LOG, str, false);
        }

        private void RunApplication(Job job)
        {
            mJobs.Add(job);
            mJobs[mJobs.Count - 1].IP = mServer.GetMyIP();
            mJobs[mJobs.Count - 1].Start();
                
            try
            {
                mStatus = DistributerStatus.WORKING;
                Log("Running application.");
                ProcessStartInfo p = new ProcessStartInfo();
                p.FileName = mSettings.getString("process_application");
                p.Arguments = mJobs[mJobs.Count - 1].Parameters;
                p.WorkingDirectory = mSettings.getString("process_application_path");
                p.WindowStyle = ProcessWindowStyle.Hidden;
                mProcess = Process.Start(p);
                mProcess.WaitForExit();
                Log("Application stopped.");

                mJobs[mJobs.Count - 1].Finish(true);
                mJobs[mJobs.Count - 1] = PrepareResults(mJobs[mJobs.Count - 1]);
            }
            catch(Exception e)
            {
                Log("Application failed.");
                mJobs[mJobs.Count - 1].Finish(false);
                NetworkClient.SendCommand(
                    mSettings.getString("server_ip"),
                    mSettings.getInteger("server_port"),
                    Protocol.WORK_FINISHED,
                    Job.Serialize(mJobs[mJobs.Count - 1]),
                    false);
                mStatus = DistributerStatus.STOPPED;
                mJobs.RemoveAt(mJobs.Count - 1);
                return;
            }

            mStatus = DistributerStatus.STOPPED;
        }
        private Job PrepareResults(Job job)
        {
            Log("Preparing Results File.");
            bool copy = true, delete = true;
            mStatus = DistributerStatus.COMPRESSING;
            if (!Directory.Exists(mSettings.getString("process_tempdata_path")))
                Directory.CreateDirectory(mSettings.getString("process_tempdata_path"));
            else
            {
                Directory.Delete(mSettings.getString("process_tempdata_path"), true);
                Directory.CreateDirectory(mSettings.getString("process_tempdata_path"));
            }

            // save job meta information
            job.Save(mSettings.getString("process_tempdata_path") + job.Title + ".xml");

            // copy results to temp folder
            String[] datafiles = mSettings.getString("process_datafiles").Split(',');
            for (int i = 0; i < datafiles.Length; i++)
                if (File.Exists(mSettings.getString("process_application_path") + datafiles[i]))
                    File.Copy(mSettings.getString("process_application_path") + datafiles[i], mSettings.getString("process_tempdata_path") + datafiles[i]);
                else
                    copy = false;

            if (!copy) Log("Error copying files, some didn't exist.");

            // delete certain data files
            String[] deletefiles = mSettings.getString("process_deletefiles").Split(',');
            for (int i = 0; i < deletefiles.Length; i++)
                if (File.Exists(mSettings.getString("process_application_path") + deletefiles[i]))
                    File.Delete(mSettings.getString("process_application_path") + deletefiles[i]);
                else
                    delete = false;

            if (!delete) Log("Error deleting files, some didn't exist.");

            job.ResultsFile = mSettings.getString("distributer_path") +  DateTime.Now.ToString("yyyyMMdd_hhmmss_") + job.Title + mSettings.getString("zip_file_extension");
            Compressor.Compress(mSettings, mSettings.getString("process_tempdata_path"), job.ResultsFile);

            // delete temp folder
            if (Directory.Exists(mSettings.getString("process_tempdata_path")))
                Directory.Delete(mSettings.getString("process_tempdata_path"), true);

            mStatus = DistributerStatus.STOPPED;
            return job;
        }
        private void Upload()
        {
            mStatus = DistributerStatus.UPLOADING;
            // upload all previous jobs that failed to upload before
            for (int i = mJobs.Count - 1; i >= 0; i--)
            {
                // upload zip to server
                if (NetworkClient.SendFile(mSettings.getString("server_ip"), mSettings.getInteger("server_port"), mJobs[i].ResultsFile))
                {
                    Log("File Uploaded.");
                    File.Delete(mJobs[i].ResultsFile);
                    mJobs[i].Successful = true;
                    NetworkClient.SendCommand(
                        mSettings.getString("server_ip"),
                        mSettings.getInteger("server_port"),
                        Protocol.WORK_FINISHED,
                        Job.Serialize(mJobs[i]),
                        false);
                    mJobs.RemoveAt(i);
                }
            }
            mStatus = DistributerStatus.STOPPED;
        }
        private bool ApplyPatch()
        {
            if (mPatchFile.Length > 0)
            {
                Log("Patching Application.");
                if (!Directory.Exists(mSettings.getString("applications_path")))
                    Directory.CreateDirectory(mSettings.getString("applications_path"));
                if (!Directory.Exists(mSettings.getString("process_application_path")))
                    Directory.CreateDirectory(mSettings.getString("process_application_path"));

                if (File.Exists(mPatchFile))
                {
                    Compressor.Decompress(mSettings, mPatchFile, mSettings.getString("distributer_path"));
                    File.Delete(mPatchFile);
                    mPatchFile = "";
                    return true;
                }

                Log("Done Patching.");
            }
            return false;
        }

        private string GetStatus()
        {
            switch (mStatus)
            {
                case DistributerStatus.STOPPED: return "Stopped";
                case DistributerStatus.WAITING: return "Waiting";
                case DistributerStatus.WORKING:
                    {
                        try
                        {
                            Results res = ResultsFile.GetResults(mSettings.getString("process_application_path") + "results.csv");
                            mJobs[mJobs.Count - 1].Generation = res.generation;
                            mJobs[mJobs.Count - 1].Best = res.best;
                            mJobs[mJobs.Count - 1].Average = res.avg;
                            mJobs[mJobs.Count - 1].Valid = res.valid;
                        }
                        catch
                        {
                            Log("Cannot open results file for status.");
                        }
                        return "Working" + Protocol.SEPARATOR + Job.Serialize(mJobs[mJobs.Count - 1]);
                    }
                case DistributerStatus.COMPRESSING: return "Compressing";
                case DistributerStatus.UPLOADING: return "Uploading";
                case DistributerStatus.PATCHING: return "Patching";
            }
            return "Unknown";
        }

        public override string HandleCommand(Command cmd)
        {
            switch (cmd.command)
            {
                case Protocol.STATUS_REQUEST:
                    return Protocol.STATUS_RESPONSE + Protocol.SEPARATOR + GetStatus();
                case Protocol.WORK_STOP:
                    if (mProcess != null && !mProcess.HasExited)
                    {
                        Log("Killing Application.");
                        mProcess.Kill();
                        return Protocol.LOG + Protocol.SEPARATOR + "Application killed.";
                    }
                    return Protocol.LOG + Protocol.SEPARATOR + "Application not running.";
            }
            return "Unknown";
        }
        public override void HandleFile(string file)
        {
            if (File.Exists(NetworkServer.TempFileDirectory + file))
            {
                if (File.Exists(mSettings.getString("distributer_path") + file))
                    File.Delete(mSettings.getString("distributer_path") + file);
                File.Move(NetworkServer.TempFileDirectory + file, mSettings.getString("distributer_path") + file);
                mPatchFile = file;
                Log("File Received.");
            }
        }
    }
}
