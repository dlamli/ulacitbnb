using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ulacitbnb.db
{
    public class ConnectionString
    {
        static readonly string db_conn_string = ConfigurationManager.ConnectionStrings["UlacitbnbAzureDB"].ConnectionString;

        public static SqlConnection GetSqlConnection()
        {
            SqlConnection sqlConn = new SqlConnection(db_conn_string);
            return sqlConn;
        }
    }
}