using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ulacit_bnb.db
{
    public class ConnectionString
    {
        static readonly string db_conn_string = GetConnectionStringByName("UlacitbnbAzureDB");

        public static SqlConnection GetSqlConnection() {
            SqlConnection sqlConn = new SqlConnection(db_conn_string);
            return sqlConn;
        }

        public static  string GetConnectionStringByName(string name)
        {
            string conn = null;
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];

            if (settings != null)
            {
                    conn = settings.ConnectionString;
                    Console.WriteLine(conn);
            }

            return conn;
        }
    }
}