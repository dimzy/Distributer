using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using distributer;

namespace distributer_server
{
    public partial class WorkQueue : Form
    {
        public class Listener : NetworkListener
        {
            WorkQueue mForm;

            public Listener(WorkQueue form)
            {
                mForm = form;
            }

            public override string HandleCommand(Command cmd)
            {
                if (cmd.ip.Length > 0)
                {
                    mForm.AddClient(cmd);

                    switch (cmd.command)
                    {
                        case Protocol.LOG:
                            mForm.Log(cmd.ip + " : " + cmd.message);
                            break;
                        case Protocol.WORK_REQUESTED:
                            String job = mForm.SendJob(cmd);
                            return Protocol.WORK_RESPONSE + Protocol.SEPARATOR + job;
                        case Protocol.WORK_FINISHED:
                            mForm.FinishJob(cmd);
                            break;
                        case Protocol.STATUS_RESPONSE:
                            mForm.HandleStatus(cmd);
                            break;
                    }
                }
                return "";
            }
            public override void HandleFile(string file)
            {
                if (!Directory.Exists(mForm.getSettings().getString("results_directory")))
                    Directory.CreateDirectory(mForm.getSettings().getString("results_directory"));
                if (File.Exists(NetworkServer.TempFileDirectory + file))
                    File.Move(NetworkServer.TempFileDirectory + file, mForm.getSettings().getString("results_directory") + file);
            }
        }

        Settings mSettings;
        String mSettingsFile;
        NetworkServer mServer;
        Listener mListener;
        String mLogFile = @".\logs\";

        public WorkQueue()
        {
            InitializeComponent();

            String stamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            mLogFile = mLogFile + stamp + ".log";
            mSettingsFile = @".\settings.xml";
            mListener = new Listener(this);
        }

        #region Generic Functions
        public void Start()
        {
            mSettings = new Settings(mSettingsFile);
            mServer = new NetworkServer(
                mSettings.getString("ip_mask"),
                mSettings.getInteger("server_port"),
                mListener);
            mServer.StartListening();
            lstQueue.Items.Clear();
            lstWorking.Items.Clear();
            lstComplete.Items.Clear();
            lstFailed.Items.Clear();
            lstQueue.Items.AddRange(Persistance.LoadJobs("queue.xml").ToArray());
            lstWorking.Items.AddRange(Persistance.LoadJobs("working.xml").ToArray());
            lstComplete.Items.AddRange(Persistance.LoadJobs("complete.xml").ToArray());
            lstFailed.Items.AddRange(Persistance.LoadJobs("failed.xml").ToArray());

            cmbIP.Items.Clear();
            cmbIP.Items.Add("127.0.0.1");
            cmbIP.Items.AddRange(mServer.GetIPList().ToArray());
            cmbIP.SelectedIndex = cmbIP.Items.IndexOf(mServer.GetMyIP());
        }
        public void Stop()
        {
            mServer.StopListening();
            mSettings.Save();
            Persistance.StoreJobs(lstQueue.Items.Cast<Job>().ToList(), "queue.xml");
            Persistance.StoreJobs(lstWorking.Items.Cast<Job>().ToList(), "working.xml");
            Persistance.StoreJobs(lstComplete.Items.Cast<Job>().ToList(), "complete.xml");
            Persistance.StoreJobs(lstFailed.Items.Cast<Job>().ToList(), "failed.xml");
            lstClients.Items.Clear();
        }
        public void Restart()
        {
            mSettings.setValue("ip_mask", cmbIP.Text);
            Stop();
            Start();
        }

        public Settings getSettings() { return mSettings; }
        public void Log(String str)
        {
            String stamped = DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + str;
            //Console.WriteLine(stamped + "\n");
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    txtOutput.AppendText(stamped + "\n");
                }));
            }
            else
            {
                txtOutput.AppendText(stamped + "\n");
            }
            Persistance.Log(mLogFile, stamped + "\n");
        }
        #endregion

        #region Asynchronous Network Commands
        public String SendJob(Command cmd)
        {
            if (lstQueue.Items.Count > 0)
            {
                Job j = (Job)lstQueue.Items[0];
                    
                if (InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        lstQueue.Items.RemoveAt(0);
                        j.Start();
                        j.IP = cmd.ip;
                        lstWorking.Items.Add(j);
                    }));
                }

                return Job.Serialize(j);
            }
            else
            {
                return "none";
            }
        }
        public void FinishJob(Command cmd)
        {
            Job job = Job.Deserialize(cmd.message);

            if (job.Successful)
            {
                if (InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        lstComplete.Items.Add(job);
                    }));
                }
                else
                {
                    lstComplete.Items.Add(job);
                }
            }
            else
            {
                if (InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        lstFailed.Items.Add(job);
                    }));
                }
                else
                {
                    lstFailed.Items.Add(job);
                }
            }

            for (int i = 0; i < lstWorking.Items.Count; i++)
            {
                if (lstWorking.Items[i].Equals(job))
                {
                    if (InvokeRequired)
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {
                            lstWorking.Items.RemoveAt(i);
                        }));
                    }
                    else
                    {
                        lstWorking.Items.RemoveAt(i);
                    }
                    break;
                }
            }
        }
        public void HandleStatus(Command cmd)
        {
            Job job = null;
            String[] spl = cmd.message.Split(Protocol.SEPARATOR);

            if (spl.Length >= 2)
            {
                job = Job.Deserialize(cmd.message.Substring(spl[0].Length + 1));
            }

            switch (spl[0])
            {
                case "Stopped":
                case "Waiting":
                    {
                        if (job != null)
                        {
                            for (int i = 0; i < lstWorking.Items.Count; i++)
                            {
                                Job tmp = (Job)lstWorking.Items[i];
                                if (tmp.Equals(job))
                                {
                                    lstWorking.Items.RemoveAt(i);
                                    Log("Worker " + job.IP + " finished or crashed when you were offline");
                                }
                            }
                        }
                    }
                    break;
                case "Working":
                    if (job != null)
                    {
                        for (int i = 0; i < lstWorking.Items.Count; i++)
                        {
                            if (lstWorking.Items[i].Equals(job))
                            {
                                lstWorking.Items[i] = job;
                            }
                        }
                    }
                    break;
                case "Running":
                    break;
                case "Patching":
                    break;
                case "Uploading":
                    break;
                case "Unknown":
                    {
                        if (job != null)
                            Log("Worker " + job.IP + " has returned and unknown status");
                    }
                    break;
            }
        }
        public void AddClient(Command cmd)
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    int i = lstClients.Items.IndexOf(cmd.ip);
                    if (i < 0)
                        lstClients.Items.Add(cmd.ip);
                }));
            }
        }
        #endregion

        #region Form Loading / Unloading
        private void WorkQueue_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
        }

        private void WorkQueue_Load(object sender, EventArgs e)
        {
            Start();
        }
#endregion

        // Add / Remove / Stop / Patch / Restart
        #region Action Commands
        // Add a new job to the queue.
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text.Length > 0)
            {
                Job j = new Job();
                j.Title = txtTitle.Text;
                j.Parameters = txtParameters.Text;
                j.Added = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                bool exists = false;
                for (int i = 0; i < lstQueue.Items.Count; i++)
                    if (j.Equals(lstQueue.Items[i]))
                        exists = true;
                if(!exists) lstQueue.Items.Add(j);
            }
        }

        // Remove a job from the queue or complete or failed lists
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstQueue.SelectedIndex >= 0)
            {
                if (lstQueue.SelectedIndex < lstQueue.Items.Count)
                {
                    lstQueue.Items.RemoveAt(lstQueue.SelectedIndex);
                }
            }

            if (lstFailed.SelectedIndex >= 0)
            {
                if (lstFailed.SelectedIndex < lstFailed.Items.Count)
                {
                    lstFailed.Items.RemoveAt(lstFailed.SelectedIndex);
                }
            }

            if (lstComplete.SelectedIndex >= 0)
            {
                if (lstComplete.SelectedIndex < lstComplete.Items.Count)
                {
                    lstComplete.Items.RemoveAt(lstComplete.SelectedIndex);
                }
            }
        }
      
        //TODO: implement
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (lstWorking.SelectedIndex >= 0)
                if (lstWorking.SelectedIndex < lstWorking.Items.Count)
                {
                    Job j = (Job)lstWorking.Items[lstWorking.SelectedIndex];
                    NetworkClient.SendCommand(j.IP, mSettings.getInteger("client_port"), Protocol.WORK_STOP, "", false);
                    lstWorking.Items.RemoveAt(lstWorking.SelectedIndex);
                }
        }

        // Zips up the patch folder, and sends the zip to each client
        private void btnPatch_Click(object sender, EventArgs e)
        {
            btnPatch.Enabled = false;
            if (lstClients.Items.Count > 0)
            {
                Log("Compressing Patch.");
                Compressor.Compress(mSettings, mSettings.getString("patch_directory") + "*", @".\" + mSettings.getString("patch_file") + mSettings.getString("zip_file_extension"));
                Log("Sending Patch.");
            }
            else
                Log("No clients registered.");

            for (int i = 0; i < lstClients.Items.Count; i++)
            {
                NetworkClient.SendFile(lstClients.Items[i].ToString(), mSettings.getInteger("client_port"), mSettings.getString("patch_file") + mSettings.getString("zip_file_extension"));
            }

            if (File.Exists(@".\" + mSettings.getString("patch_file") + mSettings.getString("zip_file_extension")))
                File.Delete(@".\" + mSettings.getString("patch_file") + mSettings.getString("zip_file_extension"));

            btnPatch.Enabled = true;
        }

        // Restart the network with the newly selected IP
        private void btnRestart_Click(object sender, EventArgs e)
        {
            Restart();
        }
        #endregion

// Selecting a job in a list box will deselect all other boxes
// and show the job's information in the information window
#region ViewItemInformation
        private void JobSelected(object sender, EventArgs e)
        {
            ListBox current = (ListBox)sender;
            if (!current.Equals(lstQueue)) lstQueue.SelectedIndex = -1;
            if (!current.Equals(lstFailed)) lstFailed.SelectedIndex = -1;
            if (!current.Equals(lstWorking)) lstWorking.SelectedIndex = -1;
            if (!current.Equals(lstComplete)) lstComplete.SelectedIndex = -1;

            if (current.SelectedIndex > -1)
            {
                SetViewItem((Job)current.Items[current.SelectedIndex]);
                btnRemove.Enabled = true;
                btnStop.Enabled = false;
                if (current.Equals(lstWorking)) { btnRemove.Enabled = false; btnStop.Enabled = true; }
            }
            else
            {
                SetViewItem(new Job());
                btnRemove.Enabled = false;
                btnStop.Enabled = false;
            }

        }
        private void SetViewItem(Job w)
        {
            txtViewTitle.Text = w.Title;
            txtViewParameters.Text = w.Parameters;
            txtViewWorker.Text = w.IP;
            txtViewFiles.Text = w.ResultsFile;
            txtAdded.Text = w.Added;
            txtStarted.Text = w.Started;
            txtFinished.Text = w.Finished;
            txtGen.Text = w.Generation.ToString();
            txtBest.Text = w.Best.ToString();
            txtAvg.Text = w.Average.ToString();
            txtSuccess.Text = (w.Finished.Length > 0) ? w.Successful.ToString() : "";
        }
#endregion

        private void tmrStatus_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < lstClients.Items.Count; i++)
            {
                Command cmd = NetworkClient.SendCommand(lstClients.Items[i].ToString(), mSettings.getInteger("client_port"), Protocol.STATUS_REQUEST, "", true);
                HandleStatus(cmd);
            }
        }

   }
}
