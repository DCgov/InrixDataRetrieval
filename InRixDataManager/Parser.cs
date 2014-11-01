using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Collections;
using System.IO;

namespace InRixDataManager
{
    /// <summary>
    /// Parse the XML input to memory.
    /// </summary>
    class Parser
    {
        /// <summary>
        /// Parse the security query response (XML) to acquire the security key.
        /// </summary>
        /// <param name="inXML"></param>
        /// <returns></returns>
        public static String ParseKeyToken(String inXML, ref DateTime AuthExpiry)
        {
            /*
             * <Inrix docType="GetSecurityToken" copyright="Copyright INRIX Inc." 
             * versionNumber="3.5.0" createdDate="2011-02-14T17:07:24Z" statusId="0" 
             * statusText="" responseId="09b65f8f-5614-478b-87f9-52a0422aa2a9">
             * <AuthResponse>
             * <AuthToken expiry="2011-02-14T18:06:00Z">jaGi5446O0Wu8N1Voq8aXqjfTZOnU7E8HGpP6yoYyww|</AuthToken>
             * <ServerPath>devzone.inrix.com/traffic/inrix.ashx</ServerPath>
             * <ServerPaths><ServerPath type="API" region="NA">http://na.api.inrix.com/V3/Traffic/Inrix.ashx</ServerPath>
             * <ServerPath type="TTS" region="NA">http://na.tts.inrix.com/V3/Tiles/Tile.ashx</ServerPath></ServerPaths>
             * </AuthResponse>
             * </Inrix>
             * 
             */
            try{

                LogManager.writeLog(4010);//start

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(inXML);

                XmlNode xmlNode = xmlDoc.SelectSingleNode("Inrix");
                int statusID = Convert.ToInt32(xmlNode.Attributes["statusId"].Value);

                if (statusID != 0) //error message
                {
                    String tempstr = xmlNode.Attributes["statusText"].Value;
                    return "error - " + tempstr;
                }
                else
                {
                    xmlNode = xmlNode.SelectSingleNode("AuthResponse");
                    AuthExpiry = Convert.ToDateTime(xmlNode.SelectSingleNode("AuthToken").Attributes[0].Value);
                    String strAuthToken = xmlNode.SelectSingleNode("AuthToken").InnerText;

                    LogManager.writeLog(4011);//done
                    return strAuthToken;

                }
            }

            catch(Exception e){
                Console.WriteLine(e.Message);
                LogManager.writeLog(4110);//error
                return "error - " + e.ToString();
            }

        }

        /// <summary>
        /// Parse the query response (XML) to a DataIncome type.
        /// </summary>
        /// <param name="inXML"></param>
        /// <returns></returns>
        public static DataIncome ParseData(String inXML)
        {
            /*
             * <Inrix docType="GetRoadSpeedInSet" copyright="Copyright INRIX Inc." versionNumber="3.5.0" createdDate="2011-02-22T19:38:14Z" statusId="0" statusText="" responseId="a2447d37-db14-4f30-b467-1086f1bed1a2">
                    <RoadSpeedResultSet coverage="255">
                        <RoadSpeedResults timestamp="2011-02-22T19:38:14Z">
                            <TMC code="110-04338" average="61" speed="61" reference="61" score="30" travelTimeMinutes="1.003"/>
                            <TMC code="110P04266" average="63" speed="60" reference="63" score="30" travelTimeMinutes="0.165"/>
                            <TMC code="110-06912" average="58" speed="58" reference="63" score="20" travelTimeMinutes="1.365"/>
             *          </RoadSpeedResults>
                    </RoadSpeedResultSet>
                </Inrix>
             */

            try
            {
                LogManager.writeLog(4040);//start

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(inXML);

                XmlNode xmlNode = xmlDoc.SelectSingleNode("Inrix");
                int statusID = Convert.ToInt32(xmlNode.Attributes["statusId"].Value);

                if (statusID != 0)  //error message
                {
                    String tempstr = xmlNode.Attributes["statusText"].Value;
                    LogManager.writeLog(3120,tempstr);//error
                    return new DataIncome(DateTime.Now, statusID, tempstr);
                }
                else
                {
                    xmlNode = xmlNode.SelectSingleNode("RoadSpeedResultSet");
                    XmlNode nRoot = xmlNode.SelectSingleNode("RoadSpeedResults");
                    DataIncome inData = new DataIncome(Convert.ToDateTime(nRoot.Attributes["timestamp"].Value), 0, "");
                    
                    foreach (XmlNode nNode in nRoot)
                    {
                        InrixData tempInrix = new InrixData();
                        foreach (XmlAttribute nAttribute in nNode.Attributes)
                        {
                            switch(nAttribute.Name){
                                case "code":
                                    tempInrix.TMCCode = nAttribute.Value;
                                    break;
                                case "average":
                                    tempInrix.Average = Convert.ToInt32(nAttribute.Value); 
                                    break;
                                case "speed":
                                    tempInrix.Speed = Convert.ToInt32(nAttribute.Value);
                                    break;
                                case "reference":
                                    tempInrix.Reference = Convert.ToInt32(nAttribute.Value); 
                                    break;
                                case "score":
                                    tempInrix.Score = Convert.ToInt32(nAttribute.Value); 
                                    break;
                                case "travelTimeMinutes":
                                    tempInrix.TravelTimeMinutes = Convert.ToDouble(nAttribute.Value); 
                                    break;
                            }
                        }
                        inData.List.Add(tempInrix);
                    }
                    LogManager.writeLog(4041);//done
                    return inData;
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                LogManager.writeLog(4140);//error
                return new DataIncome(DateTime.Now, 999, "runtime error");
            }

        }
    }
}
