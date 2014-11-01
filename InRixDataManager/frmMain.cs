using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;

namespace InRixDataManager
{
    /// <summary>
    /// Main GUI frame of the program.
    /// </summary>
    public partial class frmMain : Form
    {
        private DateTime last_record_update;
        private DateTime last_avg_update;
        private DateTime last_ref_update;
        private DateTime next_record_update;
        private DateTime next_avg_update;
        private DateTime next_ref_update;
        private DateTime next_update_att;
        private int task = 0; //for controlling timer_effect, 0:none, 1:loading, 2:threadchecking, 3:closing, 4:stoping, 5:wait for initialize

        private SqlDataAdapter realtime_adapter;
        private DataSet realtime_dataset;
        private bool initialized = false;

        public frmMain()
        {
            InitializeComponent();
            LocalEnv.Initialization();
            realtime_dataset = new DataSet();
            if (LogManager.initialize(this.rtx_log)) rtx_log.Text = "Log manager initializaed.\n";
            else { rtx_log.Text = "Log manager initialization failed.\n"; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmDebugMode dbg = new frmDebugMode();
            dbg.Show();
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            if (txt_debug_pass.Visible == false)
            {
                txt_debug_pass.Visible = true;
                txt_debug_pass.Focus();
            }
            else
            {
                txt_debug_pass.Visible = false;
                button1.Visible = false;
            }
        }

        private void txt_debug_pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txt_debug_pass.Text.Equals("keepsafe"))
                {
                    button1.Visible = true;
                }
            }
        }

        private void getAdapterThread()
        {
            OperationManager.CurrentThreadCount++;
            while(realtime_adapter==null||realtime_adapter.SelectCommand==null){
                realtime_adapter = DBOperator.getRealTimeDataAdapter();
                if (realtime_adapter == null)
                {
                    Thread.Sleep(LocalEnv.db_Retryafter*1000);
                }
            }
            initialized = true;
            OperationManager.CurrentThreadCount--;
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            LogManager.writeLog(1010);
            if (realtime_adapter == null || realtime_adapter.SelectCommand == null)
            {
                new Thread(new ThreadStart(getAdapterThread)).Start();
            }
            task = 5;
            timer_effect.Enabled = true;
        }

        delegate void showRealtimeDatadelegate();
        private void showRealtimeData()
        {
            if (real_time_monitor.InvokeRequired)
            {
                real_time_monitor.Invoke(new showRealtimeDatadelegate(showRealtimeData));
            }
            else
            {
                try
                {
                    realtime_dataset.Clear();
                    realtime_adapter.Fill(realtime_dataset);
                    real_time_monitor.DataSource = realtime_dataset.Tables[0];
                }
                catch (Exception e)
                {
                    LogManager.writeLog(2160, e);
                }
            }
        }

        private void _update(int updateTask)
        {
            switch (updateTask)
            {
                case 1:
                    OperationManager.Execute("speed,score,ttm");
                    task = 2; 
                    last_record_update = DateTime.Now;
                    next_record_update = DateTime.Now.AddMinutes(LocalEnv.pr_Records);
                    timer_effect.Enabled = true; break;
                case 2:    
                    OperationManager.Execute("average");
                    task = 2;
                    last_avg_update = DateTime.Now;
                    next_avg_update = DateTime.Now.AddMinutes(LocalEnv.pr_Average);
                    timer_effect.Enabled = true; break;
                case 3:
                    OperationManager.Execute("reference");
                    task = 2;
                    last_ref_update = DateTime.Now;
                    next_ref_update = DateTime.Now.AddMinutes(LocalEnv.pr_Reference);
                    timer_effect.Enabled = true; break;
            }

            switch (LocalEnv.pr_shortest)
            {
                case 1: next_update_att = last_record_update.AddMinutes(LocalEnv.pr_Records); break;
                case 2: next_update_att = last_avg_update.AddMinutes(LocalEnv.pr_Average); break;
                case 3: next_update_att = last_ref_update.AddMinutes(LocalEnv.pr_Reference); break;
            }

            new Thread(new ThreadStart(showRealtimeData)).Start();

            timer_next.Enabled = true;
            timer_next.Start();

            lbl_latestupdate.Text = "Speed, Score, TTM: " + last_record_update.ToString() + ", Average: " + last_avg_update.ToString() + ", Reference: " + last_ref_update.ToString();

        }

        int global_count;

        private void timer_effect_Tick(object sender, EventArgs e)
        {
            if (task == 2)
            {
                lbl_workingth.Text = OperationManager.CurrentThreadCount.ToString();
                lbl_ne.Text = "Updating...";
                lbl_nextup.Text = "";
                if (OperationManager.CurrentThreadCount <= 0)
                {
                    timer_effect.Enabled = false;
                    lbl_workingth.Text = "0";
                    task = 0;
                }
            }
            if (task == 3)
            {
                lbl_closing.Visible = true;
                lbl_closing.Text = lbl_closing.Text + ".";
                global_count++;
                if (lbl_closing.Text.Equals("Waiting for data to be saved...."))
                {
                    lbl_closing.Text = "Waiting for data to be saved";
                }
                if (OperationManager.CurrentThreadCount <= 0 || global_count > 10)
                {
                    LogManager.writeLog(1030);
                    LogManager.writeLog(1040);
                    timer_effect.Enabled = false;
                    Environment.Exit(0);
                }
            }
            if (task == 4)
            {
                lbl_closing.Visible = true;
                lbl_closing.Text = lbl_closing.Text + ".";
                if (lbl_closing.Text.Equals("Waiting for data to be saved...."))
                {
                    lbl_closing.Text = "Waiting for data to be saved";
                }
                if (OperationManager.CurrentThreadCount <= 0)
                {
                    LogManager.writeLog(1030);
                    timer_effect.Enabled = false;
                    lbl_closing.Visible = false;
                }
            }
            if (task == 5)
            {
                lbl_workingth.Text = OperationManager.CurrentThreadCount.ToString();
                lbl_ne.Text = "Initializing...";
                if (initialized == true)
                {
                    
                    timer_next_Tick(sender, e);
                    task = 0;
                    timer_next.Enabled = true;
                    timer_next.Start();
                    timer_effect.Enabled = false;
                }
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DBOperator.disposeAdapter();
            if (OperationManager.CurrentThreadCount > 0)
            {
                task = 3;
                global_count = 0;
                timer_effect.Enabled = true;
                e.Cancel = true;
            }
            else
            {
                LogManager.writeLog(1030);
                LogManager.writeLog(1040);
            }
        }

        private void timer_next_Tick(object sender, EventArgs e)
        {
            DateTime tmpdt = DateTime.Now;
            TimeSpan tmpts = next_update_att.Subtract(tmpdt);
            if (task == 0)
            {
                lbl_ne.Text = "Next update coming in:";
                lbl_nextup.Text = tmpts.Minutes.ToString() + ":" + String.Format("{0:00}", tmpts.Seconds);
            }
            int a = next_record_update.CompareTo(tmpdt.AddSeconds(LocalEnv.pr_Buffertime));
            int b = next_avg_update.CompareTo(tmpdt.AddSeconds(LocalEnv.pr_Buffertime));
            int c = next_ref_update.CompareTo(tmpdt.AddSeconds(LocalEnv.pr_Buffertime));

            if ((last_record_update == DateTime.MinValue) || a <= 0)
                _update(1);
            if ((last_avg_update == DateTime.MinValue) || b <= 0)
                _update(2);
            if ((last_ref_update == DateTime.MinValue) || c <= 0)
                _update(3);
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            LogManager.writeLog(1020);
            task = 4;
            timer_effect.Enabled = true;

            timer_next.Stop();
            timer_next.Enabled = false;
            realtime_adapter.Dispose();
            DBOperator.disposeAdapter();
            lbl_ne.Text = "Stopped";
            lbl_nextup.Text = "";
            initialized = false;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSetting frmsetting = new frmSetting();
            frmsetting.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}