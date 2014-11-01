using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Data;

namespace InRixDataManager
{
    /// <summary>
    /// Manage the Database connection.
    /// </summary>
    public sealed class DBConn
    {
        public SqlConnection conn;
        public SqlDataReader DataReader;
        public SqlCommand Command;
        private String connectionString;
        private String connectionStringFull;
        private int timeOut; 

        public DBConn()
        {
            this.connectionString = String.Format(
                "Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}",
                LocalEnv.db_Addr,
                LocalEnv.db_Catagory,
                LocalEnv.db_Username,
                LocalEnv.db_Password);

            this.timeOut = LocalEnv.db_Timeout;

            this.connectionStringFull = "Min Pool Size=5;Max Pool Size=20;Connect Timeout=" + timeOut + ";" + connectionString + ";";
        }

        public Boolean Connect()
        {
            try
            {
                LogManager.writeLog(2010); //connecting to database

                if (conn == null)
                {
                    conn = new SqlConnection();
                }

                if (String.IsNullOrEmpty(conn.ConnectionString) || conn.State != ConnectionState.Open)
                {
                    conn.ConnectionString = this.connectionStringFull;
                    conn.Open();
                    LogManager.writeLog(2011); //connected
                    return true;
                }
                return false;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                LogManager.writeLog(2110, e); //connect failed
                return false;
            }
        }

        public void Disconnect()
        {
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                conn.Dispose();
            }

            LogManager.writeLog(2050); //disconnected
        }
    }
}
