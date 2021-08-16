using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ulacitbnb.db;
using ulacitbnb.Models;

namespace ulacitbnb.Controllers
{
    [Authorize]
    [RoutePrefix("api/service")]
    public class ServiceController : ApiController
    {
        //SQL Connection
        SqlConnection sqlConnection = ConnectionString.GetSqlConnection();
        // ===================================================================================================
        [HttpGet, Route("{serviceId:int}")]
        public IHttpActionResult GetId(int serviceId)
        {
            Service service = null;
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT [Ser_ID]
                                                                    ,[Ser_Name]
                                                                    ,[Ser_Description]
                                                                    ,[Ser_Type]
                                                                    ,[Ser_Status]
                                                             FROM [dbo].[Service]
                                                            WHERE
                                                            	 Ser_ID = @Ser_ID", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("Ser_ID", serviceId);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        service = new Service()
                        {
                            ID = sqlDataReader.GetInt32(0),
                            Name = sqlDataReader.GetString(1),
                            Description = sqlDataReader.GetString(2),
                            Type = sqlDataReader.GetString(3),
                            Status = sqlDataReader.GetString(4)
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(service);
        }
        // ===================================================================================================
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Service> services = new List<Service>();
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT [Ser_ID]
                                                                    ,[Ser_Name]
                                                                    ,[Ser_Description]
                                                                    ,[Ser_Type]
                                                                    ,[Ser_Status]
                                                             FROM [dbo].[Service]", sqlConnection);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Service service = new Service()
                        {
                            ID = sqlDataReader.GetInt32(0),
                            Name = sqlDataReader.GetString(1),
                            Description = sqlDataReader.GetString(2),
                            Type = sqlDataReader.GetString(3),
                            Status = sqlDataReader.GetString(4)
                        };
                        services.Add(service);
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(services);
        }
        // ===================================================================================================
        [HttpPost]
        public IHttpActionResult EnterService(Service service)
        {
            if (service == null)
            {
                return BadRequest("Please enter required fields to create an service.");
            }
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO [dbo].[Service](
                                                                  Ser_Name
                                                                  ,Ser_Description
                                                                  ,Ser_Type
                                                                  ,Ser_Status)
                                                            VALUES(@Ser_Name
                                                                , @Ser_Description
                                                            	, @Ser_Type
                                                            	, @Ser_Status)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("Ser_Name", service.Name);
                    sqlCommand.Parameters.AddWithValue("Ser_Description", service.Description);
                    sqlCommand.Parameters.AddWithValue("Ser_Type", service.Type);
                    sqlCommand.Parameters.AddWithValue("Ser_Status", service.Status);


                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(service);
        }
        // ===================================================================================================
        [HttpPut]
        public IHttpActionResult UpdateService(Service service)
        {
            if (service == null)
            {
                return BadRequest("Service not found in database.");
            }
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE [dbo].[Service]
                                                            SET Ser_Name = @Ser_Name
                                                                , Ser_Description = @Ser_Description
                                                            	, Ser_Type = @Ser_Type
                                                            	, Ser_Status = @Ser_Status
                                                            WHERE Ser_ID = @Ser_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("Ser_ID", service.ID);
                    sqlCommand.Parameters.AddWithValue("Ser_Name", service.Name);
                    sqlCommand.Parameters.AddWithValue("Ser_Description", service.Description);
                    sqlCommand.Parameters.AddWithValue("Ser_Type", service.Type);
                    sqlCommand.Parameters.AddWithValue("Ser_Status", service.Status);


                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(service);
        }
        // ===================================================================================================
        [HttpDelete, Route("{serviceId:int}")]
        public IHttpActionResult DeleteService(int serviceId)
        {
            if (serviceId <= 0)
            {
                return BadRequest($"Service with ID:{serviceId} not found in database.");
            }
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE FROM [dbo].[Service]
                                                            WHERE Ser_ID = @Ser_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Ser_ID", serviceId);
                    sqlConnection.Open();

                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(serviceId);
        }
    }
}
