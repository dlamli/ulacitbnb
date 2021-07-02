using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
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
            Host host = null;
            
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
                        host = new Host
                        {
                            Hos_ID = sqlDataReader.GetInt32(0),
                            Hos_Name = sqlDataReader.GetString(1),
                            Hos_LastName = sqlDataReader.GetString(2),
                            Hos_Password = sqlDataReader.GetString(3),
                            Hos_Description = sqlDataReader.GetString(4),
                            Hos_Status = sqlDataReader.GetString(5)
                        };
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
                    SqlCommand selectAllHosts = new SqlCommand("SELECT * FROM Host", sqlConnection);
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

        // ===================================================================================================
        [HttpPost]
        public HttpResponseMessage CreateNewHost(Host host)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(DB_CONNECTION_STRING);
                using (sqlConnection)
                {
                    SqlCommand insertNewHost = new SqlCommand(@"INSERT INTO Host 
                                                                (Hos_Name, Hos_LastName, Hos_Password, Hos_Description, Hos_Status)
                                                                VALUES (@Hos_Name, @Hos_LastName, @Hos_Password, @Hos_Description, @Hos_Status)", sqlConnection);
                    insertNewHost.Parameters.AddWithValue("Hos_Name", host.Hos_Name);
                    insertNewHost.Parameters.AddWithValue("Hos_LastName", host.Hos_LastName);
                    insertNewHost.Parameters.AddWithValue("Hos_Password", host.Hos_Password);
                    insertNewHost.Parameters.AddWithValue("Hos_Description", host.Hos_Description);
                    insertNewHost.Parameters.AddWithValue("Hos_Status", host.Hos_Status);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = insertNewHost.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(ex.ToString());
            }
            return Request.CreateResponse(HttpStatusCode.OK, $"NEW HOST CREATED: {host.Hos_Name}");
        }

    }
}
