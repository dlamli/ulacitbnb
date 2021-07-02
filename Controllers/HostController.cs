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
        public IHttpActionResult GetHost(int id)
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
                            ID = sqlDataReader.GetInt32(0),
                            Name = sqlDataReader.GetString(1),
                            LastName = sqlDataReader.GetString(2),
                            Password = sqlDataReader.GetString(3),
                            Description = sqlDataReader.GetString(4),
                            Status = sqlDataReader.GetString(5)
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
        public IHttpActionResult GetAllHosts()
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
                            ID = sqlDataReader.GetInt32(0),
                            Name = sqlDataReader.GetString(1),
                            LastName = sqlDataReader.GetString(2),
                            Password = sqlDataReader.GetString(3),
                            Description = sqlDataReader.GetString(4),
                            Status = sqlDataReader.GetString(5)
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
                    insertNewHost.Parameters.AddWithValue("Hos_Name", host.Name);
                    insertNewHost.Parameters.AddWithValue("Hos_LastName", host.LastName);
                    insertNewHost.Parameters.AddWithValue("Hos_Password", host.Password);
                    insertNewHost.Parameters.AddWithValue("Hos_Description", host.Description);
                    insertNewHost.Parameters.AddWithValue("Hos_Status", host.Status);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = insertNewHost.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(ex.ToString());
            }
            return Request.CreateResponse(HttpStatusCode.OK, $"NEW HOST CREATED: {host.Name}");
        }

        // ===================================================================================================
        [HttpPut]
        public HttpResponseMessage UpdateHost(Host host)
        {
            if (host == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Host not found");
            }
            try
            {
                SqlConnection sqlConnection = new SqlConnection(DB_CONNECTION_STRING);
                using (sqlConnection)
                {
                    SqlCommand insertNewHost = new SqlCommand(@"UPDATE Host 
                                                                SET Hos_Name = @Hos_Name,
                                                                Hos_LastName = @Hos_LastName,
                                                                Hos_Password = Hos_Password,
                                                                Hos_Description = @Hos_Description,
                                                                Hos_Status = @Hos_Status
                                                                WHERE Hos_ID = @Hos_ID", sqlConnection);
                    insertNewHost.Parameters.AddWithValue("Hos_ID", host.ID);
                    insertNewHost.Parameters.AddWithValue("Hos_Name", host.Name);
                    insertNewHost.Parameters.AddWithValue("Hos_LastName", host.LastName);
                    insertNewHost.Parameters.AddWithValue("Hos_Password", host.Password);
                    insertNewHost.Parameters.AddWithValue("Hos_Description", host.Description);
                    insertNewHost.Parameters.AddWithValue("Hos_Status", host.Status);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = insertNewHost.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
            return Request.CreateResponse(HttpStatusCode.OK, $"HOST {host.Name} UPDATED");
        }

        // ===================================================================================================
        [HttpDelete]
        public HttpResponseMessage RemoveHost(int id)
        {
            if (id < 1)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Invalid Host ID");
            }
            try
            {
                SqlConnection sqlConnection = new SqlConnection(DB_CONNECTION_STRING);
                using (sqlConnection)
                {
                    SqlCommand insertNewHost = new SqlCommand(@"DELETE FROM Host
                                                                WHERE Hos_ID = @Hos_ID", sqlConnection);
                    insertNewHost.Parameters.AddWithValue("Hos_ID", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = insertNewHost.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
            return Request.CreateResponse(HttpStatusCode.OK, $"HOST DELETED");
        }

    }
}
