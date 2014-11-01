using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;
using System.Reflection;

namespace InRixDataManager
{
    /// <summary>
    /// Manage settings and parameters of the program.
    /// </summary>
    class LocalEnv
    {
        //parameters for inrix host
        public static String ix_Addr;
        public static String ix_Port;
        public static String ix_CredentialsAPI;
        public static String ix_GetDataAPI;
        public static String ix_VendorID;
        public static String ix_CustomerID;
        public static String ix_TMCSetID;
        public static int ix_Units;
        public static bool ix_FullTMC;
        public static int ix_retryAfterSec; //in seconds
        public static int ix_timeOut; //in seconds



        //parameters for ddot database
        public static String db_Addr;
        public static String db_Catagory;
        public static String db_Username;
        public static String db_Password;
        public static int db_Timeout; //in seconds
        public static int db_Retryafter; //how many seconds

        public static String db_inrix_hist_spd_scr_ttm;
        public static String db_inrix_hist_avg;
        public static String db_inrix_hist_ref;

        public static String db_inrix_latest_spd_scr_ttm;
        public static String db_inrix_latest_avg;
        public static String db_inrix_latest_ref;

        //query period for each attributes. in minutes.
        public static int pr_Average;
        public static int pr_Reference;
        public static int pr_Records;
        public static int pr_Buffertime; //time check buffer in second
        public static int pr_shortest;

        //log setting
        public static String log_fileloc;
        public static int log_type; //1:sys, 2:error and sys only, 3: log all
        public static bool log_enable;
        public static String log_event;

        //setting
        public static String settingfile = "settings.config";

        /// <summary>
        /// Load config from file.
        /// </summary>

        public static void Initialization()
        {
            LoadConfig();
            log_event = "LogEvents.xml";
            log_fileloc = "log_start_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Year.ToString()  + ".rtf";
            pr_shortest = pr_Records <= pr_Average ? (pr_Average <= pr_Reference ? 1 : (pr_Records <= pr_Reference ? 1 : 3)) : (pr_Reference > pr_Records ? 2 : 3);
            
        }

        public static void LoadConfig()
        {
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(settingfile);
                XmlNode xn0 = xd.SelectSingleNode("settings");

                XmlNode xn = xn0.SelectSingleNode("inrix");

                ix_Addr = xn.Attributes["ix_Addr"].Value;
                ix_Port = xn.Attributes["ix_Port"].Value;
                ix_CredentialsAPI = xn.Attributes["ix_CredentialsAPI"].Value;
                ix_GetDataAPI = xn.Attributes["ix_GetDataAPI"].Value;
                ix_VendorID = xn.Attributes["ix_VendorID"].Value;
                ix_CustomerID = xn.Attributes["ix_CustomerID"].Value;
                ix_TMCSetID = xn.Attributes["ix_TMCSetID"].Value;
                ix_Units = Convert.ToInt32(xn.Attributes["ix_Units"].Value);
                ix_FullTMC = Convert.ToBoolean(xn.Attributes["ix_FullTMC"].Value);
                ix_retryAfterSec = Convert.ToInt32(xn.Attributes["ix_retryAfterSec"].Value);
                ix_timeOut = Convert.ToInt32(xn.Attributes["ix_timeOut"].Value);
                pr_Average = Convert.ToInt32(xn.Attributes["pr_Average"].Value);
                pr_Reference = Convert.ToInt32(xn.Attributes["pr_Reference"].Value);
                pr_Records = Convert.ToInt32(xn.Attributes["pr_Records"].Value);


                xn = xn0.SelectSingleNode("database");

                db_Addr = xn.Attributes["db_Addr"].Value;
                db_Catagory = xn.Attributes["db_Catagory"].Value;
                db_Username = xn.Attributes["db_Username"].Value;
                db_Password = xn.Attributes["db_Password"].Value;
                db_Timeout = Convert.ToInt32(xn.Attributes["db_Timeout"].Value);
                db_Retryafter = Convert.ToInt32(xn.Attributes["db_Retryafter"].Value);
                db_inrix_hist_spd_scr_ttm = xn.Attributes["db_inrix_hist_spd_scr_ttm"].Value;
                db_inrix_hist_avg = xn.Attributes["db_inrix_hist_avg"].Value;
                db_inrix_hist_ref = xn.Attributes["db_inrix_hist_ref"].Value;
                db_inrix_latest_spd_scr_ttm = xn.Attributes["db_inrix_latest_spd_scr_ttm"].Value;
                db_inrix_latest_avg = xn.Attributes["db_inrix_latest_avg"].Value;
                db_inrix_latest_ref = xn.Attributes["db_inrix_latest_ref"].Value;

                xn = xn0.SelectSingleNode("other");

                log_type = Convert.ToInt32(xn.Attributes["log_type"].Value);
                log_enable = Convert.ToBoolean(xn.Attributes["log_enable"].Value);
                log_fileloc = xn.Attributes["log_fileloc"].Value;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
