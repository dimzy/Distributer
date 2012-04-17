namespace distributer_server
{
    partial class WorkQueue
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lstQueue = new System.Windows.Forms.ListBox();
            this.lstWorking = new System.Windows.Forms.ListBox();
            this.lstComplete = new System.Windows.Forms.ListBox();
            this.lstFailed = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lstClients = new System.Windows.Forms.ListBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtParameters = new System.Windows.Forms.TextBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnPatch = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRestart = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSuccess = new System.Windows.Forms.TextBox();
            this.txtFinished = new System.Windows.Forms.TextBox();
            this.txtViewWorker = new System.Windows.Forms.TextBox();
            this.txtStarted = new System.Windows.Forms.TextBox();
            this.txtViewFiles = new System.Windows.Forms.TextBox();
            this.txtAdded = new System.Windows.Forms.TextBox();
            this.txtViewParameters = new System.Windows.Forms.TextBox();
            this.txtViewTitle = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbIP = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtGen = new System.Windows.Forms.TextBox();
            this.txtBest = new System.Windows.Forms.TextBox();
            this.txtAvg = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.tmrStatus = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstQueue
            // 
            this.lstQueue.FormattingEnabled = true;
            this.lstQueue.Location = new System.Drawing.Point(459, 17);
            this.lstQueue.Name = "lstQueue";
            this.lstQueue.Size = new System.Drawing.Size(155, 342);
            this.lstQueue.TabIndex = 0;
            this.lstQueue.SelectedIndexChanged += new System.EventHandler(this.JobSelected);
            // 
            // lstWorking
            // 
            this.lstWorking.FormattingEnabled = true;
            this.lstWorking.Location = new System.Drawing.Point(620, 17);
            this.lstWorking.Name = "lstWorking";
            this.lstWorking.Size = new System.Drawing.Size(155, 173);
            this.lstWorking.TabIndex = 0;
            this.lstWorking.SelectedIndexChanged += new System.EventHandler(this.JobSelected);
            // 
            // lstComplete
            // 
            this.lstComplete.FormattingEnabled = true;
            this.lstComplete.Location = new System.Drawing.Point(781, 17);
            this.lstComplete.Name = "lstComplete";
            this.lstComplete.Size = new System.Drawing.Size(155, 342);
            this.lstComplete.TabIndex = 0;
            this.lstComplete.SelectedIndexChanged += new System.EventHandler(this.JobSelected);
            // 
            // lstFailed
            // 
            this.lstFailed.FormattingEnabled = true;
            this.lstFailed.Location = new System.Drawing.Point(620, 212);
            this.lstFailed.Name = "lstFailed";
            this.lstFailed.Size = new System.Drawing.Size(155, 147);
            this.lstFailed.TabIndex = 0;
            this.lstFailed.SelectedIndexChanged += new System.EventHandler(this.JobSelected);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(459, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Queue";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(620, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Working";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(781, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Complete";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(620, 196);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Failed";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(348, 71);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lstClients
            // 
            this.lstClients.FormattingEnabled = true;
            this.lstClients.Location = new System.Drawing.Point(13, 19);
            this.lstClients.Name = "lstClients";
            this.lstClients.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstClients.Size = new System.Drawing.Size(157, 277);
            this.lstClients.TabIndex = 3;
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(95, 19);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(328, 20);
            this.txtTitle.TabIndex = 4;
            this.txtTitle.Text = "JobTitle";
            // 
            // txtParameters
            // 
            this.txtParameters.Location = new System.Drawing.Point(95, 45);
            this.txtParameters.Name = "txtParameters";
            this.txtParameters.Size = new System.Drawing.Size(328, 20);
            this.txtParameters.TabIndex = 5;
            this.txtParameters.Text = "-pop 500 -depth 12 -crossover 0.85 -mutation 0.05 -generations 50";
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(158, 315);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Font = new System.Drawing.Font("Arial Narrow", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutput.Location = new System.Drawing.Point(459, 365);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(477, 134);
            this.txtOutput.TabIndex = 6;
            // 
            // btnPatch
            // 
            this.btnPatch.Location = new System.Drawing.Point(13, 300);
            this.btnPatch.Name = "btnPatch";
            this.btnPatch.Size = new System.Drawing.Size(157, 38);
            this.btnPatch.TabIndex = 7;
            this.btnPatch.Text = "Patch";
            this.btnPatch.UseVisualStyleBackColor = true;
            this.btnPatch.Click += new System.EventHandler(this.btnPatch_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(6, 315);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(107, 10);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(75, 23);
            this.btnRestart.TabIndex = 9;
            this.btnRestart.Text = "Restart";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtAvg);
            this.groupBox1.Controls.Add(this.txtBest);
            this.groupBox1.Controls.Add(this.txtGen);
            this.groupBox1.Controls.Add(this.txtSuccess);
            this.groupBox1.Controls.Add(this.txtFinished);
            this.groupBox1.Controls.Add(this.txtViewWorker);
            this.groupBox1.Controls.Add(this.txtStarted);
            this.groupBox1.Controls.Add(this.txtViewFiles);
            this.groupBox1.Controls.Add(this.txtAdded);
            this.groupBox1.Controls.Add(this.txtViewParameters);
            this.groupBox1.Controls.Add(this.txtViewTitle);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnRemove);
            this.groupBox1.Location = new System.Drawing.Point(202, 151);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(251, 348);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selected Item";
            // 
            // txtSuccess
            // 
            this.txtSuccess.Location = new System.Drawing.Point(95, 207);
            this.txtSuccess.Name = "txtSuccess";
            this.txtSuccess.ReadOnly = true;
            this.txtSuccess.Size = new System.Drawing.Size(138, 20);
            this.txtSuccess.TabIndex = 7;
            // 
            // txtFinished
            // 
            this.txtFinished.Location = new System.Drawing.Point(95, 181);
            this.txtFinished.Name = "txtFinished";
            this.txtFinished.ReadOnly = true;
            this.txtFinished.Size = new System.Drawing.Size(138, 20);
            this.txtFinished.TabIndex = 6;
            // 
            // txtViewWorker
            // 
            this.txtViewWorker.Location = new System.Drawing.Point(95, 103);
            this.txtViewWorker.Name = "txtViewWorker";
            this.txtViewWorker.ReadOnly = true;
            this.txtViewWorker.Size = new System.Drawing.Size(138, 20);
            this.txtViewWorker.TabIndex = 7;
            // 
            // txtStarted
            // 
            this.txtStarted.Location = new System.Drawing.Point(95, 155);
            this.txtStarted.Name = "txtStarted";
            this.txtStarted.ReadOnly = true;
            this.txtStarted.Size = new System.Drawing.Size(138, 20);
            this.txtStarted.TabIndex = 5;
            // 
            // txtViewFiles
            // 
            this.txtViewFiles.Location = new System.Drawing.Point(95, 77);
            this.txtViewFiles.Name = "txtViewFiles";
            this.txtViewFiles.ReadOnly = true;
            this.txtViewFiles.Size = new System.Drawing.Size(138, 20);
            this.txtViewFiles.TabIndex = 6;
            // 
            // txtAdded
            // 
            this.txtAdded.Location = new System.Drawing.Point(95, 129);
            this.txtAdded.Name = "txtAdded";
            this.txtAdded.ReadOnly = true;
            this.txtAdded.Size = new System.Drawing.Size(138, 20);
            this.txtAdded.TabIndex = 4;
            // 
            // txtViewParameters
            // 
            this.txtViewParameters.Location = new System.Drawing.Point(95, 51);
            this.txtViewParameters.Name = "txtViewParameters";
            this.txtViewParameters.ReadOnly = true;
            this.txtViewParameters.Size = new System.Drawing.Size(138, 20);
            this.txtViewParameters.TabIndex = 5;
            // 
            // txtViewTitle
            // 
            this.txtViewTitle.Location = new System.Drawing.Point(95, 25);
            this.txtViewTitle.Name = "txtViewTitle";
            this.txtViewTitle.ReadOnly = true;
            this.txtViewTitle.Size = new System.Drawing.Size(138, 20);
            this.txtViewTitle.TabIndex = 4;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 184);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(49, 13);
            this.label14.TabIndex = 3;
            this.label14.Text = "Finished:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 80);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "File:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 210);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(51, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "Success:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 106);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Worker:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 158);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 13);
            this.label12.TabIndex = 1;
            this.label12.Text = "Started:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Parameters:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Added:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Title:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtTitle);
            this.groupBox2.Controls.Add(this.txtParameters);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Location = new System.Drawing.Point(12, 39);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(441, 106);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "New Item";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 48);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "Parameters:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Title:";
            // 
            // cmbIP
            // 
            this.cmbIP.FormattingEnabled = true;
            this.cmbIP.Location = new System.Drawing.Point(12, 10);
            this.cmbIP.Name = "cmbIP";
            this.cmbIP.Size = new System.Drawing.Size(89, 21);
            this.cmbIP.TabIndex = 13;
            this.cmbIP.Text = "127.0.0.1";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lstClients);
            this.groupBox3.Controls.Add(this.btnPatch);
            this.groupBox3.Location = new System.Drawing.Point(12, 151);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(184, 348);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Clients";
            // 
            // txtGen
            // 
            this.txtGen.Location = new System.Drawing.Point(95, 233);
            this.txtGen.Name = "txtGen";
            this.txtGen.ReadOnly = true;
            this.txtGen.Size = new System.Drawing.Size(138, 20);
            this.txtGen.TabIndex = 7;
            // 
            // txtBest
            // 
            this.txtBest.Location = new System.Drawing.Point(95, 259);
            this.txtBest.Name = "txtBest";
            this.txtBest.ReadOnly = true;
            this.txtBest.Size = new System.Drawing.Size(138, 20);
            this.txtBest.TabIndex = 7;
            // 
            // txtAvg
            // 
            this.txtAvg.Location = new System.Drawing.Point(95, 285);
            this.txtAvg.Name = "txtAvg";
            this.txtAvg.ReadOnly = true;
            this.txtAvg.Size = new System.Drawing.Size(138, 20);
            this.txtAvg.TabIndex = 7;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 236);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(62, 13);
            this.label15.TabIndex = 2;
            this.label15.Text = "Generation:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 262);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(31, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "Best:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 288);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(50, 13);
            this.label17.TabIndex = 2;
            this.label17.Text = "Average:";
            // 
            // tmrStatus
            // 
            this.tmrStatus.Enabled = true;
            this.tmrStatus.Interval = 5000;
            this.tmrStatus.Tick += new System.EventHandler(this.tmrStatus_Tick);
            // 
            // WorkQueue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 510);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.cmbIP);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstFailed);
            this.Controls.Add(this.lstComplete);
            this.Controls.Add(this.lstWorking);
            this.Controls.Add(this.lstQueue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WorkQueue";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Distributer Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WorkQueue_FormClosing);
            this.Load += new System.EventHandler(this.WorkQueue_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstQueue;
        private System.Windows.Forms.ListBox lstWorking;
        private System.Windows.Forms.ListBox lstComplete;
        private System.Windows.Forms.ListBox lstFailed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox lstClients;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtParameters;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnPatch;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtViewWorker;
        private System.Windows.Forms.TextBox txtViewFiles;
        private System.Windows.Forms.TextBox txtViewParameters;
        private System.Windows.Forms.TextBox txtViewTitle;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbIP;
        private System.Windows.Forms.TextBox txtSuccess;
        private System.Windows.Forms.TextBox txtFinished;
        private System.Windows.Forms.TextBox txtStarted;
        private System.Windows.Forms.TextBox txtAdded;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtAvg;
        private System.Windows.Forms.TextBox txtBest;
        private System.Windows.Forms.TextBox txtGen;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Timer tmrStatus;
    }
}

