using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace InRixDataManager
{
    public static class DBOperator
    {
        /// <summary>
        /// Insert data into database. 
        /// This function is especially for historical data which are 
        /// tables of inrix_historical_records and inrix_historical_avg_ref.
        /// </summary>
        /// <returns>Return 0 when no error occurs. Otherwise return error codes.</returns>
        public static int InsertData(DataIncome inData)
        {
            try
            {
                LogManager.writeLog(2020); //start inserting

                DBConn dbconn = new DBConn();
                dbconn.Connect();
                dbconn.Command = dbconn.conn.CreateCommand();

                String strInsert ="";
                String strInsertHead = "";
                int task = 0;

                if(inData.List[0].Speed>0){
                    strInsertHead = "INSERT into "
                        + LocalEnv.db_inrix_hist_spd_scr_ttm
                        + "(Timestamp, TMCCode, Speed, Score, TravelTimeMinutes) VALUES "
                        + "('" + inData.TimeStamp.ToString() + "','";
                    task = 1;
                }
                else if(inData.List[0].Average>0){
                    strInsertHead = "INSERT into "
                        + LocalEnv.db_inrix_hist_avg
                        + "(Timestamp, TMCCode, Average) VALUES "
                        + "('" + inData.TimeStamp.ToString() + "','";
                    task = 2;
                }
                else if(inData.List[0].Reference>0){
                    strInsertHead = "INSERT into "
                        + LocalEnv.db_inrix_hist_ref
                        + "(Timestamp, TMCCode, Reference) VALUES "
                        + "('" + inData.TimeStamp.ToString() + "','";
                    task = 3;
                }
                else{
                    LogManager.writeLog(4130);
                    return 4130; //error code for invalid inData
                }

                foreach (InrixData iDa in inData.List)
                {
                    switch(task){
                        case 1:
                            strInsert = strInsertHead
                                + (iDa.TMCCode+ "'," 
                                + iDa.Speed.ToString() + "," 
                                + iDa.Score.ToString() + "," 
                                + iDa.TravelTimeMinutes.ToString() + ")");
                            break;
                        case 2:
                            strInsert = strInsertHead
                                + (iDa.TMCCode+ "'," 
                                + iDa.Average.ToString() + ")");
                            break;
                        case 3:
                            strInsert = strInsertHead
                                + (iDa.TMCCode + "'," 
                                + iDa.Reference.ToString() + ")");
                            break;
                    
                    }
                    dbconn.Command.CommandText = strInsert;
                    dbconn.Command.ExecuteNonQuery();
                }

                dbconn.Disconnect();
                LogManager.writeLog(2021); //inserted
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                LogManager.writeLog(2120,e);         
                return 2120; //invalid code of database error
            }
        }

        /// <summary>
        /// Update data to database. 
        /// This function is especially for realtime(latest) data which are 
        /// tables of inrix_latest_records and inrix_latest_avg_ref.
        /// Note that this function will truncate table and re-insert the newest data.
        /// </summary>
        /// <returns>Return 0 when no error occurs. Otherwise return error codes.</returns>
        public static int UpdateData(DataIncome inData)
        {
            try
            {
                LogManager.writeLog(2030); //start updating

                DBConn dbconn = new DBConn();
                dbconn.Connect();
                dbconn.Command = dbconn.conn.CreateCommand();

                String strInsert = "";
                String strInsertHead = "";
                String strTruncate = "TRUNCATE TABLE ";
                int task = 0;

                if (inData.List[0].Speed > 0)
                {
                    strInsertHead = "INSERT into "
                        + LocalEnv.db_inrix_latest_spd_scr_ttm
                        + "(Timestamp, TMCCode, Speed, Score, TravelTimeMinutes) VALUES "
                        + "('" + inData.TimeStamp.ToString() + "','";
                    task = 1;
                    strTruncate += LocalEnv.db_inrix_latest_spd_scr_ttm;
                }
                else if (inData.List[0].Average > 0)
                {
                    strInsertHead = "INSERT into "
                        + LocalEnv.db_inrix_latest_avg
                        + "(Timestamp, TMCCode, Average) VALUES "
                        + "('" + inData.TimeStamp.ToString() + "','";
                    task = 2;
                    strTruncate += LocalEnv.db_inrix_latest_avg;
                }
                else if (inData.List[0].Reference > 0)
                {
                    strInsertHead = "INSERT into "
                        + LocalEnv.db_inrix_latest_ref
                        + "(Timestamp, TMCCode, Reference) VALUES "
                        + "('" + inData.TimeStamp.ToString() + "','";
                    task = 3;
                    strTruncate += LocalEnv.db_inrix_latest_ref;
                }
                else
                {
                    LogManager.writeLog(4130);
                    return 4130; //error code for invalid inData
                }

                dbconn.Command.CommandText = strTruncate;
                dbconn.Command.ExecuteNonQuery();

                foreach (InrixData iDa in inData.List)
                {
                    switch (task)
                    {
                        case 1:
                            strInsert = strInsertHead
                                + (iDa.TMCCode + "',"
                                + iDa.Speed.ToString() + ","
                                + iDa.Score.ToString() + ","
                                + iDa.TravelTimeMinutes.ToString() + ")");
                            break;
                        case 2:
                            strInsert = strInsertHead
                                + (iDa.TMCCode + "',"
                                + iDa.Average.ToString() + ")");
                            break;
                        case 3:
                            strInsert = strInsertHead
                                + (iDa.TMCCode + "',"
                                + iDa.Reference.ToString() + ")");
                            break;

                    }
                    dbconn.Command.CommandText = strInsert;
                    dbconn.Command.ExecuteNonQuery();
                }

                dbconn.Disconnect();
                LogManager.writeLog(2031);//done

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                LogManager.writeLog(2130,e);
                return 2130; //invalid code of database error
            }
        }

        /// <summary>
        /// select the DC road list from inrix_metadata
        /// input an empty string list.
        /// </summary>
        /// <returns></returns>
        public static int getRoadList(ref List<String> roadlist)
        {
            try
            {
                LogManager.writeLog(2040); //start getting metadata

                DBConn dbconn = new DBConn();
                if (!dbconn.Connect())
                {
                    throw new TimeoutException("Connection timeout. Retry after " + LocalEnv.db_Retryafter.ToString() + " seconds.");
                }
                dbconn.Command = dbconn.conn.CreateCommand();

                String strQuery = "SELECT TMCCode FROM inrix_metadata ORDER BY TMCCode ASC";

                dbconn.Command.CommandText = strQuery;
                SqlDataReader sqlReader = dbconn.Command.ExecuteReader();

                roadlist.Clear();

                while (sqlReader.Read())
                {
                    roadlist.Add(sqlReader.GetString(0));
                }

                dbconn.Disconnect();

                LogManager.writeLog(2041);//done
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                LogManager.writeLog(2140,e);
                return 2140; //metadata get error
            }
        }

        private static DBConn adapter_conn = new DBConn();

        public static SqlDataAdapter getRealTimeDataAdapter()
        {
            try
            {
                if (!adapter_conn.Connect())
                {
                    throw new TimeoutException("Connection timeout. Retry after "+ LocalEnv.db_Retryafter.ToString() + " seconds.");
                }
                adapter_conn.Command = adapter_conn.conn.CreateCommand();

                adapter_conn.Command.CommandText = "SELECT A.*, B.Average, C.Reference FROM inrix_latest_records as A inner join inrix_latest_avg as B on A.TMCCode = B.TMCCode inner join inrix_latest_ref as C on A.TMCCode = C.TMCCode";

                return new SqlDataAdapter(adapter_conn.Command);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);  //realtime data retrieval error
                LogManager.writeLog(2160, e);
                return null;
            }
        }
        public static void disposeAdapter()
        {
            adapter_conn.Disconnect();
        }
    }
}