using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ulacitbnb.Controllers;
using ulacitbnb.db;
using ulacitbnb.Models;

namespace ulacitbnb.Controllers
{
    [AllowAnonymous, RoutePrefix("api/customer")]
    public class CustomerController : ApiController
    {
        //SQL Connection
        SqlConnection sqlConnection = ConnectionString.GetSqlConnection();
        // ===================================================================================================
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Customer customer = new Customer();
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT [Cus_ID]
                                                                 ,[Cus_Name]
                                                                 ,[Cus_LastName]
                                                                 ,[Cus_Identification]
                                                                 ,[Cus_Password]
                                                                 ,[Cus_Email]
                                                                 ,[Cus_Status]
                                                                 ,[Cus_BirthDate]
                                                                 ,[Cus_Phone]
                                                             FROM [dbo].[Customer]
                                                            WHERE
                                                            	 Cus_ID = @Cus_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("Cus_ID", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        customer.Cus_ID = sqlDataReader.GetInt32(0);
                        customer.Cus_Name = sqlDataReader.GetString(1);
                        customer.Cus_LastName = sqlDataReader.GetString(2);
                        customer.Cus_Identification = sqlDataReader.GetString(3);
                        customer.Cus_Password = sqlDataReader.GetString(4);
                        customer.Cus_Email = sqlDataReader.GetString(5);
                        customer.Cus_Status = sqlDataReader.GetString(6);
                        customer.Cus_BirthDate = sqlDataReader.GetDateTime(7);
                        customer.Cus_Phone = sqlDataReader.GetString(8);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
            return Ok(customer);
        }
        // ===================================================================================================
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT [Cus_ID]
                                                              ,[Cus_Name]
                                                              ,[Cus_LastName]
                                                              ,[Cus_Identification]
                                                              ,[Cus_Password]
                                                              ,[Cus_Email]
                                                              ,[Cus_Status]
                                                              ,[Cus_BirthDate]
                                                              ,[Cus_Phone]
                                                          FROM [dbo].[Customer]", sqlConnection);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Customer customer = new Customer();
                        customer.Cus_ID = sqlDataReader.GetInt32(0);
                        customer.Cus_Name = sqlDataReader.GetString(1);
                        customer.Cus_LastName = sqlDataReader.GetString(2);
                        customer.Cus_Identification = sqlDataReader.GetString(3);
                        customer.Cus_Password = sqlDataReader.GetString(4);
                        customer.Cus_Email = sqlDataReader.GetString(5);
                        customer.Cus_Status = sqlDataReader.GetString(6);
                        customer.Cus_BirthDate = sqlDataReader.GetDateTime(7);
                        customer.Cus_Phone = sqlDataReader.GetString(8);
                        customers.Add(customer);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(customers);
        }
        // ===================================================================================================
        [HttpPost, Route("auth")]
        public IHttpActionResult Authenticate(LoginRequest loginRequest)
        {
            if (loginRequest == null) return BadRequest("Complete the fields to login an customer.");
            Customer customer = new Customer();
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Cus_ID
                                                                 ,Cus_Name
                                                                 ,Cus_LastName
                                                                 ,Cus_Identification
                                                                 ,Cus_Password
                                                                 ,Cus_Email
                                                                 ,Cus_Status
                                                                 ,Cus_BirthDate
                                                                 ,Cus_Phone
                                                             FROM [Customer]
                                                             WHERE 
                                                                Cus_Name = @Cus_Name 
                                                                AND Cus_Password = @Cus_Password", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Cus_Name", loginRequest.Username);
                    sqlCommand.Parameters.AddWithValue("@Cus_Password", loginRequest.Password);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.Read())
                    {
                        customer.Cus_ID = sqlDataReader.GetInt32(0);
                        customer.Cus_Name = sqlDataReader.GetString(1);
                        customer.Cus_LastName = sqlDataReader.GetString(2);
                        customer.Cus_Identification = sqlDataReader.GetString(3);
                        customer.Cus_Password = sqlDataReader.GetString(4);
                        customer.Cus_Email = sqlDataReader.GetString(5);
                        customer.Cus_Status = sqlDataReader.GetString(6);
                        customer.Cus_BirthDate = sqlDataReader.GetDateTime(7);
                        customer.Cus_Phone = sqlDataReader.GetString(8);

                        var token = TokenGenerator.GenerateTokenJwt(loginRequest.Username);
                        customer.Token = token;
                    }
                    if (!string.IsNullOrEmpty(customer.Token))
                        return Ok(customer);
                    else return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        // ===================================================================================================
        [HttpPost]
        public IHttpActionResult EnterCustomer(Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Please enter required fields to create an customer.");
            }
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO [dbo].[Customer](
                                                                  [Cus_Name]
                                                                 ,[Cus_LastName]
                                                                 ,[Cus_Identification]
                                                                 ,[Cus_Password]
                                                                 ,[Cus_Email]
                                                                 ,[Cus_Status]
                                                                 ,[Cus_BirthDate]
                                                                 ,[Cus_Phone])
                                                           VALUES(@Cus_Name
                                                                , @Cus_LastName
                                                            	, @Cus_Identification
                                                            	, @Cus_Password
                                                            	, @Cus_Email
                                                            	, @Cus_Status
                                                            	, @Cus_BirthDate
                                                                , @Cus_Phone)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("Cus_Name", customer.Cus_Name);
                    sqlCommand.Parameters.AddWithValue("Cus_LastName", customer.Cus_LastName);
                    sqlCommand.Parameters.AddWithValue("Cus_Identification", customer.Cus_Identification);
                    sqlCommand.Parameters.AddWithValue("Cus_Password", customer.Cus_Password);
                    sqlCommand.Parameters.AddWithValue("Cus_Email", customer.Cus_Email);
                    sqlCommand.Parameters.AddWithValue("Cus_Status", customer.Cus_Status);
                    sqlCommand.Parameters.AddWithValue("Cus_BirthDate", customer.Cus_BirthDate);
                    sqlCommand.Parameters.AddWithValue("Cus_Phone", customer.Cus_Phone);


                    sqlConnection.Open();
                    int filasAfectadas = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(customer);
        }
        // ===================================================================================================
        [HttpPut]
        public IHttpActionResult UpdateCustomer(Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer not found in database.");
            }
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE [dbo].[Customer]
                                                            SET Cus_Name = @Cus_Name
                                                                , Cus_LastName = @Cus_LastName
                                                            	, Cus_Identification = @Cus_Identification
                                                            	, Cus_Password = @Cus_Password
                                                            	, Cus_Email = @Cus_Email
                                                            	, Cus_Status = @Cus_Status
                                                            	, Cus_BirthDate = @Cus_BirthDate
                                                                , Cus_Phone = @Cus_Phone
                                                            WHERE Cus_ID = @Cus_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("Cus_ID", customer.Cus_ID);
                    sqlCommand.Parameters.AddWithValue("Cus_Name", customer.Cus_Name);
                    sqlCommand.Parameters.AddWithValue("Cus_LastName", customer.Cus_LastName);
                    sqlCommand.Parameters.AddWithValue("Cus_Identification", customer.Cus_Identification);
                    sqlCommand.Parameters.AddWithValue("Cus_Password", customer.Cus_Password);
                    sqlCommand.Parameters.AddWithValue("Cus_Email", customer.Cus_Email);
                    sqlCommand.Parameters.AddWithValue("Cus_Status", customer.Cus_Status);
                    sqlCommand.Parameters.AddWithValue("Cus_BirthDate", customer.Cus_BirthDate);
                    sqlCommand.Parameters.AddWithValue("Cus_Phone", customer.Cus_Phone);


                    sqlConnection.Open();
                    int rowsAffected = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (rowsAffected <= 0)
                    {
                        return BadRequest($"Customer with ID {customer.Cus_ID} doesn't exist in database.");
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
        public IHttpActionResult DeleteCustomer(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"Customer with ID:{id} not found in database.");
            }
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE FROM [dbo].[Customer]
                                                            WHERE Cus_ID = @Cus_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Cus_ID", id);
                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();
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
