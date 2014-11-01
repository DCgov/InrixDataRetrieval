using System;
using System.Collections.Generic;
using System.Text;

namespace InRixDataManager
{
    /// <summary>
    /// Records the timestamp, total row count, connection status and all of the INRIX data in each query.
    /// </summary>
    public sealed class DataIncome
    {
        private List<InrixData> list;
        private DateTime timestamp;
        private int status;
        private String errormessage;

        public DataIncome()
        {
            list = new List<InrixData>();
            timestamp = DateTime.Now;
        }

        public DataIncome(DateTime TimeStamp, int Status)
        {
            list = new List<InrixData>();
            timestamp = TimeStamp;
            status = Status;
        }

        public DataIncome(DateTime TimeStamp, int Status, String Error)
        {
            list = new List<InrixData>();
            timestamp = TimeStamp;
            status = Status;
            errormessage = Error;
        }

        public void Add(InrixData inrixdata)
        {
            list.Add(inrixdata);
        }

        public DateTime TimeStamp
        {
            get { return timestamp; }
            //set { timestamp = value; }
        }

        public int Count
        {
            get { return list.Count; }
        }

        public List<InrixData> List{
            get { return list; }
            set { list = value; }
        }

        public int ConnStatus
        {
            get { return status; }
            set { status = value; }
        }

        public String ErrorMessage
        {
            get { return errormessage; }
            set { errormessage = value; }
        }
    }
}
