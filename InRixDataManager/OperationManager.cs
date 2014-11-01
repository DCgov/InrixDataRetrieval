using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace InRixDataManager
{
    class OperationManager
    {
        private static String Credential;
        private static DateTime CredentialExpiration = new DateTime();
        private static List<String> RoadList = new List<string>();
        private static int _threadworking;

        static public void Execute(String QueryAttribute)
        {
            _threadworking++;

            ThreadUnit threadu = new ThreadUnit(QueryAttribute);
            new Thread(new ThreadStart(threadu.processThread)).Start();
        }

        static public void resetCredential()
        {
            Credential = null;
        }

        static public int CurrentThreadCount
        {
            get { return _threadworking; }
            set { _threadworking = CurrentThreadCount; }
        }

        protected class ThreadUnit
        {
            private String QueryAttribute;

            public ThreadUnit(String queryAttribute)
            {
                QueryAttribute = queryAttribute;
            }

            public void processThread()
            {
                //get data
                String tempstr2 = null;
                while (tempstr2 == null)
                {
                    credentialOperation(); //check if the credential is valid first.
                    tempstr2 = DataRetriever.getData(Credential, QueryAttribute);
                }
                //parse data
                DataIncome tempdata = Parser.ParseData(tempstr2);

                //filter
                DataIncome processeddata = RouteFilter.DCFilter(tempdata, RoadList);

                //save into database
                DBOperator.InsertData(processeddata);
                DBOperator.UpdateData(processeddata);

                _threadworking--;
            }

            private void credentialOperation()
            {
                if (Credential == null || DateTime.Now.CompareTo(CredentialExpiration.AddMinutes(-5)) < 0)
                {
                    bool sucflag = false;
                    do
                    {
                        //get new credential
                        String tempstr = DataRetriever.getCredentialKey();

                        //parse credential
                        if (tempstr != null)
                        {
                            Credential = Parser.ParseKeyToken(tempstr, ref CredentialExpiration);

                            if (Credential.Contains("error"))
                            {
                                sucflag = false;
                                Thread.Sleep(10000);
                            }
                            else
                            {
                                sucflag = true;
                            }
                        }
                        else
                        {
                            sucflag = false;
                            Thread.Sleep(10000);
                        }
                    }
                    while (sucflag == false);

                    //update roadlist
                    while (DBOperator.getRoadList(ref RoadList) != 0)
                    {
                        Thread.Sleep(LocalEnv.db_Retryafter * 1000);
                    }
                }
            }

        }
    }
}
