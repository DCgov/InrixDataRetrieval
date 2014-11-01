namespace InRixDataManager
{
    partial class frmMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbc_maintab = new System.Windows.Forms.TabControl();
            this.tb_realtime = new System.Windows.Forms.TabPage();
            this.tb_connlog = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_Start = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.lbl_loading = new System.Windows.Forms.Label();
            this.timer_effect = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txt_debug_pass = new System.Windows.Forms.TextBox();
            this.real_time_monitor = new System.Windows.Forms.DataGridView();
            this.lbl_wo = new System.Windows.Forms.Label();
            this.lbl_workingth = new System.Windows.Forms.Label();
            this.lbl_la = new System.Windows.Forms.Label();
            this.lbl_latestupdate = new System.Windows.Forms.Label();
            this.timer_next = new System.Windows.Forms.Timer(this.components);
            this.lbl_closing = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbl_ne = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_nextup = new System.Windows.Forms.ToolStripStatusLabel();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rtx_log = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.tbc_maintab.SuspendLayout();
            this.tb_realtime.SuspendLayout();
            this.tb_connlog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.real_time_monitor)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(723, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // tbc_maintab
            // 
            this.tbc_maintab.Controls.Add(this.tb_realtime);
            this.tbc_maintab.Controls.Add(this.tb_connlog);
            this.tbc_maintab.Location = new System.Drawing.Point(12, 27);
            this.tbc_maintab.Name = "tbc_maintab";
            this.tbc_maintab.SelectedIndex = 0;
            this.tbc_maintab.Size = new System.Drawing.Size(699, 303);
            this.tbc_maintab.TabIndex = 1;
            // 
            // tb_realtime
            // 
            this.tb_realtime.Controls.Add(this.lbl_latestupdate);
            this.tb_realtime.Controls.Add(this.lbl_la);
            this.tb_realtime.Controls.Add(this.lbl_workingth);
            this.tb_realtime.Controls.Add(this.lbl_wo);
            this.tb_realtime.Controls.Add(this.real_time_monitor);
            this.tb_realtime.Location = new System.Drawing.Point(4, 22);
            this.tb_realtime.Name = "tb_realtime";
            this.tb_realtime.Padding = new System.Windows.Forms.Padding(3);
            this.tb_realtime.Size = new System.Drawing.Size(691, 277);
            this.tb_realtime.TabIndex = 0;
            this.tb_realtime.Text = "Realtime Monitor";
            this.tb_realtime.UseVisualStyleBackColor = true;
            // 
            // tb_connlog
            // 
            this.tb_connlog.Controls.Add(this.rtx_log);
            this.tb_connlog.Location = new System.Drawing.Point(4, 22);
            this.tb_connlog.Name = "tb_connlog";
            this.tb_connlog.Padding = new System.Windows.Forms.Padding(3);
            this.tb_connlog.Size = new System.Drawing.Size(691, 277);
            this.tb_connlog.TabIndex = 1;
            this.tb_connlog.Text = "Log";
            this.tb_connlog.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(147, 340);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 32);
            this.button1.TabIndex = 2;
            this.button1.Text = "debug";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(551, 336);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(77, 35);
            this.btn_Start.TabIndex = 0;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(634, 336);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(77, 35);
            this.btn_stop.TabIndex = 1;
            this.btn_stop.Text = "Stop";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // lbl_loading
            // 
            this.lbl_loading.AutoSize = true;
            this.lbl_loading.Location = new System.Drawing.Point(374, 347);
            this.lbl_loading.Name = "lbl_loading";
            this.lbl_loading.Size = new System.Drawing.Size(107, 13);
            this.lbl_loading.TabIndex = 0;
            this.lbl_loading.Text = "Starting data retrieval";
            this.lbl_loading.Visible = false;
            // 
            // timer_effect
            // 
            this.timer_effect.Interval = 800;
            this.timer_effect.Tick += new System.EventHandler(this.timer_effect_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 359);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "            ";
            this.label1.DoubleClick += new System.EventHandler(this.label1_DoubleClick);
            // 
            // txt_debug_pass
            // 
            this.txt_debug_pass.Location = new System.Drawing.Point(36, 344);
            this.txt_debug_pass.Name = "txt_debug_pass";
            this.txt_debug_pass.PasswordChar = '¡´';
            this.txt_debug_pass.Size = new System.Drawing.Size(105, 20);
            this.txt_debug_pass.TabIndex = 4;
            this.txt_debug_pass.Visible = false;
            this.txt_debug_pass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_debug_pass_KeyDown);
            // 
            // real_time_monitor
            // 
            this.real_time_monitor.AllowUserToAddRows = false;
            this.real_time_monitor.AllowUserToDeleteRows = false;
            this.real_time_monitor.AllowUserToResizeColumns = false;
            this.real_time_monitor.AllowUserToResizeRows = false;
            this.real_time_monitor.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.real_time_monitor.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.real_time_monitor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.real_time_monitor.Location = new System.Drawing.Point(6, 44);
            this.real_time_monitor.MultiSelect = false;
            this.real_time_monitor.Name = "real_time_monitor";
            this.real_time_monitor.ReadOnly = true;
            this.real_time_monitor.RowHeadersVisible = false;
            this.real_time_monitor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.real_time_monitor.Size = new System.Drawing.Size(679, 227);
            this.real_time_monitor.TabIndex = 1;
            // 
            // lbl_wo
            // 
            this.lbl_wo.AutoSize = true;
            this.lbl_wo.Location = new System.Drawing.Point(6, 5);
            this.lbl_wo.Name = "lbl_wo";
            this.lbl_wo.Size = new System.Drawing.Size(83, 13);
            this.lbl_wo.TabIndex = 2;
            this.lbl_wo.Text = "Working thread:";
            // 
            // lbl_workingth
            // 
            this.lbl_workingth.AutoSize = true;
            this.lbl_workingth.Location = new System.Drawing.Point(90, 5);
            this.lbl_workingth.Name = "lbl_workingth";
            this.lbl_workingth.Size = new System.Drawing.Size(13, 13);
            this.lbl_workingth.TabIndex = 3;
            this.lbl_workingth.Text = "0";
            // 
            // lbl_la
            // 
            this.lbl_la.AutoSize = true;
            this.lbl_la.Location = new System.Drawing.Point(6, 21);
            this.lbl_la.Name = "lbl_la";
            this.lbl_la.Size = new System.Drawing.Size(75, 13);
            this.lbl_la.TabIndex = 4;
            this.lbl_la.Text = "Latest update:";
            // 
            // lbl_latestupdate
            // 
            this.lbl_latestupdate.AutoSize = true;
            this.lbl_latestupdate.Location = new System.Drawing.Point(81, 21);
            this.lbl_latestupdate.Name = "lbl_latestupdate";
            this.lbl_latestupdate.Size = new System.Drawing.Size(0, 13);
            this.lbl_latestupdate.TabIndex = 5;
            // 
            // timer_next
            // 
            this.timer_next.Interval = 1000;
            this.timer_next.Tick += new System.EventHandler(this.timer_next_Tick);
            // 
            // lbl_closing
            // 
            this.lbl_closing.AutoSize = true;
            this.lbl_closing.Location = new System.Drawing.Point(374, 347);
            this.lbl_closing.Name = "lbl_closing";
            this.lbl_closing.Size = new System.Drawing.Size(141, 13);
            this.lbl_closing.TabIndex = 8;
            this.lbl_closing.Text = "Waiting for data to be saved";
            this.lbl_closing.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_ne,
            this.lbl_nextup});
            this.statusStrip1.Location = new System.Drawing.Point(0, 375);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(723, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbl_ne
            // 
            this.lbl_ne.Name = "lbl_ne";
            this.lbl_ne.Size = new System.Drawing.Size(51, 17);
            this.lbl_ne.Text = "Stopped";
            // 
            // lbl_nextup
            // 
            this.lbl_nextup.Name = "lbl_nextup";
            this.lbl_nextup.Size = new System.Drawing.Size(0, 17);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // rtx_log
            // 
            this.rtx_log.BackColor = System.Drawing.Color.White;
            this.rtx_log.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtx_log.ForeColor = System.Drawing.Color.Black;
            this.rtx_log.Location = new System.Drawing.Point(6, 6);
            this.rtx_log.Name = "rtx_log";
            this.rtx_log.ReadOnly = true;
            this.rtx_log.Size = new System.Drawing.Size(679, 265);
            this.rtx_log.TabIndex = 0;
            this.rtx_log.Text = "";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 397);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lbl_loading);
            this.Controls.Add(this.lbl_closing);
            this.Controls.Add(this.txt_debug_pass);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbc_maintab);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "InRix Data Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tbc_maintab.ResumeLayout(false);
            this.tb_realtime.ResumeLayout(false);
            this.tb_realtime.PerformLayout();
            this.tb_connlog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.real_time_monitor)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.TabControl tbc_maintab;
        private System.Windows.Forms.TabPage tb_realtime;
        private System.Windows.Forms.TabPage tb_connlog;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Label lbl_loading;
        private System.Windows.Forms.Timer timer_effect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_debug_pass;
        private System.Windows.Forms.DataGridView real_time_monitor;
        private System.Windows.Forms.Label lbl_workingth;
        private System.Windows.Forms.Label lbl_wo;
        private System.Windows.Forms.Label lbl_la;
        private System.Windows.Forms.Label lbl_latestupdate;
        private System.Windows.Forms.Timer timer_next;
        private System.Windows.Forms.Label lbl_closing;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbl_ne;
        private System.Windows.Forms.ToolStripStatusLabel lbl_nextup;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        public System.Windows.Forms.RichTextBox rtx_log;
    }
}

