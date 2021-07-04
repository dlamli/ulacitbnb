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
    [AllowAnonymous, RoutePrefix("api/host")]
    public class HostController : ApiController
    {
        readonly string DB_CONNECTION_STRING = ConfigurationManager.ConnectionStrings["UlacitbnbAzureDB"].ConnectionString;

        // ===================================================================================================
        [HttpGet, Route("{hostId:int}")]
        public IHttpActionResult GetHost(int hostId)
        {
            if (hostId < 1) 
            {
                return BadRequest("Invalid Host ID");
            }
            Host host = null;
            
            try
            {
                SqlConnection sqlConnection = new SqlConnection(DB_CONNECTION_STRING);
                using (sqlConnection)
                {
                    SqlCommand selectHostById = new SqlCommand(@"SELECT 
                                                                    Hos_ID,
                                                                    Hos_Name,
                                                                    Hos_LastName,
                                                                    Hos_Password,
                                                                    Hos_Description,
                                                                    Hos_Status 
                                                                FROM Host
                                                                WHERE Hos_ID = @Hos_ID", sqlConnection);
                    selectHostById.Parameters.AddWithValue("Hos_ID", hostId);
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
                    SqlCommand selectAllHosts = new SqlCommand(@"SELECT
                                                                    Hos_ID,
                                                                    Hos_Name,
                                                                    Hos_LastName,
                                                                    Hos_Password,
                                                                    Hos_Description,
                                                                    Hos_Status
                                                                FROM Host", sqlConnection);
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
            if (host == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Required fields to create host are invalid");
            }

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
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
            return Request.CreateResponse(HttpStatusCode.OK, $"NEW HOST CREATED SUCCESFULLY: {host.Name} {host.LastName}");
        }
        // ===================================================================================================
        [HttpPost, Route("auth")]
        public IHttpActionResult Authenticate(LoginRequest loginRequest)
        {
            if (loginRequest == null) return BadRequest("Complete the fields to login an host.");
            Host host = new Host();
            try
            {
                SqlConnection sqlConnection = new SqlConnection(DB_CONNECTION_STRING);
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT 
                                                                    Hos_ID,
                                                                    Hos_Name,
                                                                    Hos_LastName,
                                                                    Hos_Password,
                                                                    Hos_Description,
                                                                    Hos_Status 
                                                                FROM Host
                                                                WHERE Hos_Name = @Hos_Name 
                                                                AND Hos_Password = @Hos_Password", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Hos_Name", loginRequest.Username);
                    sqlCommand.Parameters.AddWithValue("@Hos_Password", loginRequest.Password);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.Read())
                    {
                        host.ID = sqlDataReader.GetInt32(0);
                        host.Name = sqlDataReader.GetString(1);
                        host.LastName = sqlDataReader.GetString(2);
                        host.Password = sqlDataReader.GetString(3);
                        host.Description = sqlDataReader.GetString(4);
                        host.Status = sqlDataReader.GetString(5);

                        var token = TokenGenerator.GenerateTokenJwt(loginRequest.Username);
                        host.Token = token;
                    }
                    if (!string.IsNullOrEmpty(host.Token))
                        return Ok(host);
                    else return Unauthorized();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
        // ===================================================================================================
        [HttpPut]
        public HttpResponseMessage UpdateHost(Host host)
        {
            if (host == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Required fields to update host are invalid");
            }
            try
            {
                SqlConnection sqlConnection = new SqlConnection(DB_CONNECTION_STRING);
                using (sqlConnection)
                {
                    SqlCommand updateHost = new SqlCommand(@"UPDATE Host 
                                                             SET Hos_Name = @Hos_Name,
                                                                 Hos_LastName = @Hos_LastName,
                                                                 Hos_Password = Hos_Password,
                                                                 Hos_Description = @Hos_Description,
                                                                 Hos_Status = @Hos_Status
                                                            WHERE Hos_ID = @Hos_ID", sqlConnection);
                    updateHost.Parameters.AddWithValue("Hos_ID", host.ID);
                    updateHost.Parameters.AddWithValue("Hos_Name", host.Name);
                    updateHost.Parameters.AddWithValue("Hos_LastName", host.LastName);
                    updateHost.Parameters.AddWithValue("Hos_Password", host.Password);
                    updateHost.Parameters.AddWithValue("Hos_Description", host.Description);
                    updateHost.Parameters.AddWithValue("Hos_Status", host.Status);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = updateHost.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
            return Request.CreateResponse(HttpStatusCode.OK, $"HOST {host.Name} UPDATED SUCCESFULLY");
        }
        // ===================================================================================================
        [HttpDelete, Route("{hostId:int}")]
        public HttpResponseMessage RemoveHost(int hostId)
        {
            if (hostId < 1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid host ID");
            }
            try
            {
                SqlConnection sqlConnection = new SqlConnection(DB_CONNECTION_STRING);
                using (sqlConnection)
                {
                    SqlCommand deleteHost = new SqlCommand(@"DELETE FROM Host
                                                                WHERE Hos_ID = @Hos_ID", sqlConnection);
                    deleteHost.Parameters.AddWithValue("Hos_ID", hostId);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = deleteHost.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
            return Request.CreateResponse(HttpStatusCode.OK, $"HOST DELETED WITH ANY PROBLEM");
        }

    }
}
