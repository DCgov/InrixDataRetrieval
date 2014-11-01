using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Reflection;

namespace InRixDataManager
{
    public partial class frmSetting : Form
    {
        public frmSetting()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static IEnumerable<Control> GetControls(Control form)
        {
            foreach (Control childControl in form.Controls)
            {   // Recurse child controls.
                foreach (Control grandChild in GetControls(childControl))
                {
                    yield return grandChild;
                }
                yield return childControl;
            }
        }

        private void frmSetting_Load(object sender, EventArgs e)
        {
            fill_setting_from_env();

            IEnumerable<Control> tmpiec = GetControls(this);
            IEnumerator<Control> iec = tmpiec.GetEnumerator();

            while(iec.MoveNext())
            {
                Control f = iec.Current;
                if (f.GetType().Equals(typeof(TextBox)))
                {
                    TextBox tmptext = f as TextBox;
                    tmptext.TextChanged += new EventHandler(event_modified);
                }
                else if (f.GetType().Equals(typeof(CheckBox)))
                {
                    CheckBox tmpcheck = f as CheckBox;
                    tmpcheck.CheckedChanged += new EventHandler(event_modified);
                }
                else if (f.GetType().Equals(typeof(ComboBox)))
                {
                    ComboBox tmpcombo = f as ComboBox;
                    tmpcombo.SelectedIndexChanged += new EventHandler(event_modified);
                }
                    
             }
        }

        
        private void event_modified(object sender, EventArgs e)
        {
            btn_apply.Enabled = true;
            
        }

        private void save_setting(String filename)
        {
            IEnumerable<Control> tmpiec = GetControls(this);
            IEnumerator<Control> iec = tmpiec.GetEnumerator();

            while (iec.MoveNext())
            {
                Control f = iec.Current;
                if (f.GetType().Equals(typeof(TextBox)))
                {
                    TextBox tmptext = f as TextBox;
                    if (f.Text == "")
                    {
                        MessageBox.Show("All blanks have to be filled.");
                        return;
                    }
                }
            }

            XmlTextWriter xw = new XmlTextWriter(filename, null);
            xw.WriteStartDocument();
            xw.WriteStartElement("settings");
            xw.WriteStartElement("inrix");
            xw.WriteAttributeString("ix_Addr", txt_ix_addr.Text);
            xw.WriteAttributeString("ix_Port", txt_ix_port.Text);
            xw.WriteAttributeString("ix_CredentialsAPI", txt_ix_credapi.Text);
            xw.WriteAttributeString("ix_GetDataAPI", txt_ix_dataapi.Text);
            xw.WriteAttributeString("ix_VendorID", txt_ix_vender.Text);
            xw.WriteAttributeString("ix_CustomerID", txt_ix_customer.Text);
            xw.WriteAttributeString("ix_TMCSetID", txt_ix_tmcsetid.Text);
            xw.WriteAttributeString("ix_Units", txt_ix_unit.Text);
            xw.WriteAttributeString("ix_FullTMC", chk_ix_fulltmc.Checked.ToString());
            xw.WriteAttributeString("ix_retryAfterSec", txt_ix_retry.Text);
            xw.WriteAttributeString("ix_timeOut", txt_ix_timeout.Text);
            xw.WriteAttributeString("pr_Average", txt_ix_avg.Text);
            xw.WriteAttributeString("pr_Reference", txt_ix_ref.Text);
            xw.WriteAttributeString("pr_Records", txt_ix_record.Text);
            xw.WriteEndElement();
           
            xw.WriteStartElement("database");
            xw.WriteAttributeString("db_Addr", txt_db_addr.Text);
            xw.WriteAttributeString("db_Catagory", txt_db_category.Text);
            xw.WriteAttributeString("db_Username", txt_db_user.Text);
            xw.WriteAttributeString("db_Password", txt_db_pass.Text);
            xw.WriteAttributeString("db_Timeout", txt_db_timeout.Text);
            xw.WriteAttributeString("db_Retryafter", txt_db_retry.Text);
            xw.WriteAttributeString("db_inrix_hist_spd_scr_ttm", txt_db_recordh.Text);
            xw.WriteAttributeString("db_inrix_hist_avg", txt_db_avgh.Text);
            xw.WriteAttributeString("db_inrix_hist_ref", txt_db_refh.Text);
            xw.WriteAttributeString("db_inrix_latest_spd_scr_ttm", txt_db_record.Text);
            xw.WriteAttributeString("db_inrix_latest_avg", txt_db_avg.Text);
            xw.WriteAttributeString("db_inrix_latest_ref", txt_db_ref.Text);
            xw.WriteEndElement();
            xw.WriteStartElement("other");
            xw.WriteAttributeString("log_type", ((int)(cmb_log_type.SelectedIndex +1)).ToString());
            xw.WriteAttributeString("log_enable", chk_log_save.Checked.ToString() );
            xw.WriteAttributeString("log_fileloc", txt_log_loc.Text);
            //xw.WriteAttributeString("", );

            xw.WriteEndElement();
            xw.WriteEndElement();
            xw.WriteEndDocument();
            xw.Close();
            btn_apply.Enabled = false;
            saveToolStripMenuItem.Enabled = true;
            
        }

        private void fill_setting_from_env()
        {

            //inrix host
            txt_ix_addr.Text = LocalEnv.ix_Addr;
            txt_ix_port.Text = LocalEnv.ix_Port;
            txt_ix_credapi.Text  = LocalEnv.ix_CredentialsAPI;
            txt_ix_dataapi.Text  = LocalEnv.ix_GetDataAPI;
            txt_ix_vender.Text = LocalEnv.ix_VendorID;
            txt_ix_customer.Text = LocalEnv.ix_CustomerID;
            txt_ix_tmcsetid.Text  = LocalEnv.ix_TMCSetID;
            txt_ix_unit.Text = LocalEnv.ix_Units.ToString();
            chk_ix_fulltmc.Checked = LocalEnv.ix_FullTMC;
            txt_ix_timeout.Text = LocalEnv.ix_timeOut.ToString();
            txt_ix_retry.Text = LocalEnv.ix_retryAfterSec.ToString();
            txt_ix_avg.Text = LocalEnv.pr_Average.ToString();
            txt_ix_record.Text = LocalEnv.pr_Records.ToString();
            txt_ix_ref.Text = LocalEnv.pr_Reference.ToString();
            
            //database
            txt_db_addr.Text = LocalEnv.db_Addr;
            txt_db_user.Text = LocalEnv.db_Username;
            txt_db_pass.Text = LocalEnv.db_Password;
            txt_db_timeout.Text = LocalEnv.db_Timeout.ToString();
            txt_db_retry.Text = LocalEnv.db_Retryafter.ToString();
            txt_db_category.Text = LocalEnv.db_Catagory;
            txt_db_record.Text = LocalEnv.db_inrix_latest_spd_scr_ttm;
            txt_db_recordh.Text = LocalEnv.db_inrix_hist_spd_scr_ttm;
            txt_db_ref.Text = LocalEnv.db_inrix_latest_ref;
            txt_db_refh.Text = LocalEnv.db_inrix_hist_ref;
            txt_db_avg.Text = LocalEnv.db_inrix_latest_avg;
            txt_db_avgh.Text = LocalEnv.db_inrix_hist_avg;

            //other

            chk_log_save.Checked = LocalEnv.log_enable;
            txt_log_loc.Text = LocalEnv.log_fileloc;
            cmb_log_type.SelectedIndex = LocalEnv.log_type - 1;

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btn_apply_Click(object sender, EventArgs e)
        {
            this.save_setting(LocalEnv.settingfile);
            LocalEnv.LoadConfig();

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save_setting(LocalEnv.settingfile);
            LocalEnv.LoadConfig();
        }

        private void importSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String filestr;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filestr = openFileDialog.FileName;
                }
                else
                {
                    return;
                }
                
                XmlDocument xd = new XmlDocument();
                xd.Load(filestr);
                XmlNode xn0 = xd.SelectSingleNode("settings");

                XmlNode xn = xn0.SelectSingleNode("inrix");

                txt_ix_addr.Text = xn.Attributes["ix_Addr"].Value;
                txt_ix_port.Text = xn.Attributes["ix_Port"].Value;
                txt_ix_credapi.Text = xn.Attributes["ix_CredentialsAPI"].Value;
                txt_ix_dataapi.Text = xn.Attributes["ix_GetDataAPI"].Value;
                txt_ix_vender.Text = xn.Attributes["ix_VendorID"].Value;
                txt_ix_customer.Text = xn.Attributes["ix_CustomerID"].Value;
                txt_ix_tmcsetid.Text = xn.Attributes["ix_TMCSetID"].Value;
                txt_ix_unit.Text = xn.Attributes["ix_Units"].Value;
                chk_ix_fulltmc.Checked = Convert.ToBoolean(xn.Attributes["ix_FullTMC"].Value);
                txt_ix_timeout.Text = xn.Attributes["ix_retryAfterSec"].Value;
                txt_ix_retry.Text = xn.Attributes["ix_timeOut"].Value;
                txt_ix_avg.Text = xn.Attributes["pr_Average"].Value;
                txt_ix_ref.Text = xn.Attributes["pr_Reference"].Value;
                txt_ix_record.Text = xn.Attributes["pr_Records"].Value;


                xn = xn0.SelectSingleNode("database");

                txt_db_addr.Text = xn.Attributes["db_Addr"].Value;
                txt_db_category.Text = xn.Attributes["db_Catagory"].Value;
                txt_db_user.Text = xn.Attributes["db_Username"].Value;
                txt_db_pass.Text = xn.Attributes["db_Password"].Value;
                txt_db_timeout.Text = xn.Attributes["db_Timeout"].Value;
                txt_db_retry.Text = xn.Attributes["db_Retryafter"].Value;
                txt_db_recordh.Text = xn.Attributes["db_inrix_hist_spd_scr_ttm"].Value;
                txt_db_avgh.Text = xn.Attributes["db_inrix_hist_avg"].Value;
                txt_db_refh.Text = xn.Attributes["db_inrix_hist_ref"].Value;
                txt_db_record.Text = xn.Attributes["db_inrix_latest_spd_scr_ttm"].Value;
                txt_db_avg.Text = xn.Attributes["db_inrix_latest_avg"].Value;
                txt_db_ref.Text = xn.Attributes["db_inrix_latest_ref"].Value;

                xn = xn0.SelectSingleNode("other");

                cmb_log_type.SelectedIndex = Convert.ToInt32(xn.Attributes["log_type"].Value) - 1;
                chk_log_save.Checked = Convert.ToBoolean(xn.Attributes["log_enable"].Value);
                txt_log_loc.Text = xn.Attributes["log_fileloc"].Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Not a legal setting file.");
                Console.WriteLine(ex.Message);
            }
        }

        private void exportSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            { 
                String filestr;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filestr = saveFileDialog.FileName;
                    bool tmpbool = btn_apply.Enabled;
                    save_setting(filestr);
                    btn_apply.Enabled = tmpbool;
                    saveToolStripMenuItem.Enabled = tmpbool;

                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Export failed.");
                Console.WriteLine(ex.Message);
            }
        }

        private void btn_log_openloc_Click(object sender, EventArgs e)
        {
            OpenFileDialog tmpopd = new OpenFileDialog();
            if (tmpopd.ShowDialog() == DialogResult.OK)
            {
                txt_log_loc.Text = tmpopd.FileName;
            }
            else
            {
                return;
            }
        }

        private void btn_default_Click(object sender, EventArgs e)
        {
            LocalEnv.defaultsetting();
            this.fill_setting_from_env();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            save_setting(LocalEnv.settingfile);
            LocalEnv.LoadConfig();
            this.Dispose();
        }
    }
}