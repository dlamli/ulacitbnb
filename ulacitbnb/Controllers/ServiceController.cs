using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ulacit_bnb.db;
using ulacit_bnb.Models;

namespace ulacit_bnb.Controllers
{
    [Authorize]
    [RoutePrefix("api/service")]
    public class ServiceController : ApiController
    {
        //SQL Connection
        SqlConnection sqlConnection = ConnectionString.GetSqlConnection();
        // ===================================================================================================
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Service service = new Service();
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
                    sqlCommand.Parameters.AddWithValue("Ser_ID", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        service.Ser_ID = sqlDataReader.GetInt32(0);
                        service.Ser_Name = sqlDataReader.GetString(1);
                        service.Ser_Description = sqlDataReader.GetString(2);
                        service.Ser_Type = sqlDataReader.GetString(3);
                        service.Ser_Status = sqlDataReader.GetString(4);
                    }
                    sqlConnection.Close();
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
                        Service service = new Service();
                        service.Ser_ID = sqlDataReader.GetInt32(0);
                        service.Ser_Name = sqlDataReader.GetString(1);
                        service.Ser_Description = sqlDataReader.GetString(2);
                        service.Ser_Type = sqlDataReader.GetString(3);
                        service.Ser_Status = sqlDataReader.GetString(4);
                        services.Add(service);
                    }
                    sqlConnection.Close();
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

                    sqlCommand.Parameters.AddWithValue("Ser_Name", service.Ser_Name);
                    sqlCommand.Parameters.AddWithValue("Ser_Description", service.Ser_Description);
                    sqlCommand.Parameters.AddWithValue("Ser_Type", service.Ser_Type);
                    sqlCommand.Parameters.AddWithValue("Ser_Status", service.Ser_Status);


                    sqlConnection.Open();
                    int rowsAffected = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
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

                    sqlCommand.Parameters.AddWithValue("Ser_ID", service.Ser_ID);
                    sqlCommand.Parameters.AddWithValue("Ser_Name", service.Ser_Name);
                    sqlCommand.Parameters.AddWithValue("Ser_Description", service.Ser_Description);
                    sqlCommand.Parameters.AddWithValue("Ser_Type", service.Ser_Type);
                    sqlCommand.Parameters.AddWithValue("Ser_Status", service.Ser_Status);


                    sqlConnection.Open();
                    int rowsAffected = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    if(rowsAffected <= 0)
                    {
                        return BadRequest($"Service with ID {service.Ser_ID} doesn't exist in database.");
                    }
                    else
                    {
                    return Ok(rowsAffected);
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        // ===================================================================================================
        [HttpDelete]
        public IHttpActionResult DeleteService(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"Service with ID:{id} not found in database.");
            }
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE FROM [dbo].[Service]
                                                            WHERE Ser_ID = @Ser_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Ser_ID", id);
                    sqlConnection.Open();

                    int rowsAfected = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();

                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(id);
        }
    }
}
