using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using ulacit_bnb.Models;

namespace ulacit_bnb.Controllers
{
    [Authorize]
    [RoutePrefix("api/host")]
    public class HostController : ApiController
    {
        readonly string DB_CONNECTION_STRING = ConfigurationManager.ConnectionStrings["UlacitbnbAzureDB"].ConnectionString;

        // ===================================================================================================
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Host host = new Host();
            
            try
            {
                SqlConnection sqlConnection = new SqlConnection(DB_CONNECTION_STRING);
                using (sqlConnection)
                {
                    SqlCommand selectHostById = new SqlCommand(@"SELECT * FROM Host
                                                                    WHERE Hos_ID = @Hos_ID", sqlConnection);
                    selectHostById.Parameters.AddWithValue("Hos_ID", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = selectHostById.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        host.Hos_ID = sqlDataReader.GetInt32(0);
                        host.Hos_Name = sqlDataReader.GetString(1);
                        host.Hos_LastName = sqlDataReader.GetString(2);
                        host.Hos_Password = sqlDataReader.GetString(3);
                        host.Hos_Description = sqlDataReader.GetString(4);
                        host.Hos_Status = sqlDataReader.GetString(5);
                    }
                }

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(host);
        }

        // ===================================================================================================
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Host> hosts = new List<Host>();

            try
            {
                SqlConnection sqlConnection = new SqlConnection(DB_CONNECTION_STRING);
                using (sqlConnection)
                {
                    SqlCommand selectAllHosts = new SqlCommand(@"SELECT * FROM [Host]", sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = selectAllHosts.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Host host = new Host
                        {
                            Hos_ID = sqlDataReader.GetInt32(0),
                            Hos_Name = sqlDataReader.GetString(1),
                            Hos_LastName = sqlDataReader.GetString(2),
                            Hos_Password = sqlDataReader.GetString(3),
                            Hos_Description = sqlDataReader.GetString(4),
                            Hos_Status = sqlDataReader.GetString(5)
                        };
                        hosts.Add(host);
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(hosts);
        }

    }
}
