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
        [HttpGet, Route("{customerId:int}")]
        public IHttpActionResult GetId(int customerId)
        {
            Customer customer = null;
            try
            {
                if (customerId < 1)
                {
                    return BadRequest("Invalid Customer ID");
                }
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

                    sqlCommand.Parameters.AddWithValue("Cus_ID", customerId);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        customer = new Customer
                        {
                            ID = sqlDataReader.GetInt32(0),
                            Name = sqlDataReader.GetString(1),
                            LastName = sqlDataReader.GetString(2),
                            Identification = sqlDataReader.GetString(3),
                            Password = sqlDataReader.GetString(4),
                            Email = sqlDataReader.GetString(5),
                            Status = sqlDataReader.GetString(6),
                            BirthDate = sqlDataReader.GetDateTime(7),
                            Phone = sqlDataReader.GetString(8)
                        };
                    }
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
                        Customer customer = new Customer
                        {
                            ID = sqlDataReader.GetInt32(0),
                            Name = sqlDataReader.GetString(1),
                            LastName = sqlDataReader.GetString(2),
                            Identification = sqlDataReader.GetString(3),
                            Password = sqlDataReader.GetString(4),
                            Email = sqlDataReader.GetString(5),
                            Status = sqlDataReader.GetString(6),
                            BirthDate = sqlDataReader.GetDateTime(7),
                            Phone = sqlDataReader.GetString(8)
                        };
                        customers.Add(customer);
                    }
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
            Customer customer = null;
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
                        customer = new Customer { 
                            ID = sqlDataReader.GetInt32(0),
                            Name = sqlDataReader.GetString(1),
                            LastName = sqlDataReader.GetString(2),
                            Identification = sqlDataReader.GetString(3),
                            Password = sqlDataReader.GetString(4),
                            Email = sqlDataReader.GetString(5),
                            Status = sqlDataReader.GetString(6),
                            BirthDate = sqlDataReader.GetDateTime(7),
                            Phone = sqlDataReader.GetString(8)
                        };

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
            try
            {
                if (customer == null) return BadRequest("Please enter required fields to create an customer.");
                
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

                    sqlCommand.Parameters.AddWithValue("Cus_Name", customer.Name);
                    sqlCommand.Parameters.AddWithValue("Cus_LastName", customer.LastName);
                    sqlCommand.Parameters.AddWithValue("Cus_Identification", customer.Identification);
                    sqlCommand.Parameters.AddWithValue("Cus_Password", customer.Password);
                    sqlCommand.Parameters.AddWithValue("Cus_Email", customer.Email);
                    sqlCommand.Parameters.AddWithValue("Cus_Status", customer.Status);
                    sqlCommand.Parameters.AddWithValue("Cus_BirthDate", customer.BirthDate);
                    sqlCommand.Parameters.AddWithValue("Cus_Phone", customer.Phone);

                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
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
                return BadRequest("Invalid Customer");
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

                    sqlCommand.Parameters.AddWithValue("Cus_ID", customer.ID);
                    sqlCommand.Parameters.AddWithValue("Cus_Name", customer.Name);
                    sqlCommand.Parameters.AddWithValue("Cus_LastName", customer.LastName);
                    sqlCommand.Parameters.AddWithValue("Cus_Identification", customer.Identification);
                    sqlCommand.Parameters.AddWithValue("Cus_Password", customer.Password);
                    sqlCommand.Parameters.AddWithValue("Cus_Email", customer.Email);
                    sqlCommand.Parameters.AddWithValue("Cus_Status", customer.Status);
                    sqlCommand.Parameters.AddWithValue("Cus_BirthDate", customer.BirthDate);
                    sqlCommand.Parameters.AddWithValue("Cus_Phone", customer.Phone);


                    sqlConnection.Open();
                    int rowsAffected = sqlCommand.ExecuteNonQuery();
                    if (rowsAffected <= 0)
                    {
                        return BadRequest($"Customer with ID {customer.ID} doesn't exist in database.");
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
        [HttpDelete,Route("{customerId:int}")]
        public IHttpActionResult DeleteCustomer(int customerId)
        {
            try
            {
                if (customerId <= 0)
                {
                    return BadRequest($"Customer with ID:{customerId} not found in database.");
                }
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE FROM [dbo].[Customer]
                                                            WHERE Cus_ID = @Cus_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Cus_ID", customerId);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(customerId);
        }
    }
}
