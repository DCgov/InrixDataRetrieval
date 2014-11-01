using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace InRixDataManager
{
    class LogManager
    {
        private static List<Event> elist;
        private static RichTextBox LogArea;
      
        public static bool initialize(RichTextBox logarea)
        {
            try
            {
                elist = new List<Event>(); 
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(LocalEnv.log_event);
                XmlNode xmlNode = xmlDoc.SelectSingleNode("Event");
                foreach (XmlNode xn in xmlNode)
                {
                    if (xn.NodeType != XmlNodeType.Comment)
                    {
                        String a = xn.Attributes["ID"].Value;
                        String b = xn.Attributes["Text"].Value;
                        String c = xn.Attributes["Type"].Value;
                        int tmp=0;
                        switch (c)
                        {
                            case "Sys": tmp = 1; break;
                            case "Error": tmp = 2; break;
                            case "Notification": tmp = 3; break;
                        }
                        Event tmpev = new Event(Convert.ToInt32(a), b, tmp);
                        elist.Add(tmpev);
                    }
                }
                LogArea = logarea;
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static void writeLog(int eventID, Exception e)
        {
            Event evt = elist.Find(delegate(Event ev) { return ev.eid == eventID; });
            if (LocalEnv.log_type >= evt.type)
            {
                String str = "[" + DateTime.Now.ToString() + "] " + evt.eid.ToString() + ": " + evt.text + "; " + e.Message + "\n";
                writeUI(str, evt.type);
            }
        }

        public static void writeLog(int eventID)
        {
            Event evt = elist.Find(delegate(Event ev) { return ev.eid == eventID; });
            if (evt != null && LocalEnv.log_type >= evt.type)
            {
                String str = "[" + DateTime.Now.ToString() + "] " + evt.eid.ToString() + ": " + evt.text + "\n";
                writeUI(str, evt.type); 
                if (LocalEnv.log_enable)
                {
                    writeFile();
                }
            }
        }

        public static void writeLog(int eventID, String errorMsg)
        {
            Event evt = elist.Find(delegate(Event ev) { return ev.eid == eventID; });
            if (evt != null && LocalEnv.log_type >= evt.type)
            {
                String str = "[" + DateTime.Now.ToString() + "] " + evt.eid.ToString() + ": " + evt.text + "; " + errorMsg + "\n";
                writeUI(str ,evt.type);
            }
        }

        private delegate void writeFileDelegate();
        private static void writeFile()
        {
            try
            {
                if (LogArea.InvokeRequired)
                {
                    LogArea.Invoke(new writeFileDelegate(writeFile));
                }
                else
                {
                    if (LocalEnv.log_fileloc != "")
                    {
                        LogArea.SaveFile(LocalEnv.log_fileloc);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void writeUI(String input, int eventType)
        {
            AppendToLogArea(input, eventType);
        }

        private delegate void AppendToLogAreaDelegate(String value, int eventType);
        private static void AppendToLogArea(String value, int eventType)
        {
            try
            {
                if (LogArea.InvokeRequired)
                {
                    LogArea.Invoke(new AppendToLogAreaDelegate(AppendToLogArea), new object[] { value, eventType });
                }
                else
                {
                    switch (eventType)
                    {
                        default: LogArea.SelectionColor = Color.Black; break;
                        case 1: LogArea.SelectionColor = Color.Blue; break;
                        case 2: LogArea.SelectionColor = Color.Red; break;
                    }

                    LogArea.AppendText(value);
                    LogArea.Update();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public class Event
        {
            public int eid;
            public String text;
            public int type; // 1: sys, 2: error, 3:notification

            public Event()
            {
                eid = 0;
                text = "";
                type = 0;
            }

            public Event(int Eid, String Text, int Type)
            {
                eid = Eid;
                text = Text;
                type = Type;
            }
        }
    }
}
