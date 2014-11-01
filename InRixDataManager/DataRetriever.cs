using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using System.IO;

namespace InRixDataManager
{
    /// <summary>
    /// This class give the functions to perform each retrieval from Inrix server.
    /// </summary>
    class DataRetriever
    {
        public static String getCredentialKey()
        {
            String fullurl = LocalEnv.ix_Addr
                + "?Action=" + LocalEnv.ix_CredentialsAPI
                + "&vendorId=" + LocalEnv.ix_VendorID
                + "&consumerId=" + LocalEnv.ix_CustomerID;

            try
            {
                LogManager.writeLog(3010); //start 
                HttpWebRequest httpreq = (HttpWebRequest)HttpWebRequest.Create(fullurl);
                httpreq.Timeout = LocalEnv.ix_timeOut * 1000;

                HttpWebResponse httpresponse = (HttpWebResponse)httpreq.GetResponse();
                if(httpresponse.StatusCode == HttpStatusCode.OK){
                    Stream streamResponse = httpresponse.GetResponseStream();
                    StreamReader sReader = new StreamReader(streamResponse);
                    LogManager.writeLog(3011); //done
                    return sReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                LogManager.writeLog(3110,e); //exception
                return null;
            }
            return null;
        }

        public static String getData(String KeyToken, String QueryAttribute)
        {
            String fullurl = LocalEnv.ix_Addr
                + "?Action=" + LocalEnv.ix_GetDataAPI
                + "&TmcSetId=" + LocalEnv.ix_TMCSetID
                + "&SpeedOutputFields=" + QueryAttribute
                + "&Units=" + LocalEnv.ix_Units.ToString()
                +"&FullTMC=" + LocalEnv.ix_FullTMC.ToString()
                + "&Token=" + KeyToken;

            try
            {
                LogManager.writeLog(3020); //start
                HttpWebRequest httpreq = (HttpWebRequest)HttpWebRequest.Create(fullurl);
                httpreq.Timeout = LocalEnv.ix_timeOut * 1000;

                HttpWebResponse httpresponse = (HttpWebResponse)httpreq.GetResponse();
                if (httpresponse.StatusCode == HttpStatusCode.OK)
                {
                    Stream streamResponse = httpresponse.GetResponseStream();
                    StreamReader sReader = new StreamReader(streamResponse);
                    LogManager.writeLog(3021); //done
                    return sReader.ReadToEnd();
                }
            }
            catch (TimeoutException e)
            {
                LogManager.writeLog(3121,e); //timeout
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                LogManager.writeLog(3120, e); //exception
                OperationManager.resetCredential(); //problem probably caused by invalid credential, reset it and get a new one.
                return null;
            }

            return null;
        } 
    }
}
