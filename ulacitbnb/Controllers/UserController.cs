using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ulacit_bnb.Models;

namespace ulacit_bnb.Controllers
{
    [AllowAnonymous, RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        //SQL Connection
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["UlacitbnbAzureDB"].ConnectionString);
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            User user = new User();
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT [Use_ID]
                                                                 ,[Use_Name]
                                                                 ,[Use_LastName]
                                                                 ,[Use_Identification]
                                                                 ,[Use_Password]
                                                                 ,[Use_Email]
                                                                 ,[Use_Status]
                                                                 ,[Use_BirthDate]
                                                                 ,[Use_Phone]
                                                             FROM [dbo].[User]
                                                            WHERE
                                                            	 Use_ID = @Use_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("Use_ID", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        user.Use_ID = sqlDataReader.GetInt32(0);
                        user.Use_Name = sqlDataReader.GetString(1);
                        user.Use_LastName = sqlDataReader.GetString(2);
                        user.Use_Identification = sqlDataReader.GetString(3);
                        user.Use_Password = sqlDataReader.GetString(4);
                        user.Use_Email = sqlDataReader.GetString(5);
                        user.Use_Status = sqlDataReader.GetString(6);
                        user.Use_BirthDate = sqlDataReader.GetDateTime(7);
                        user.Use_Phone = sqlDataReader.GetString(8);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
            return Ok(user);
        }
        //------------------------------------------------------------------------------
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<User> users = new List<User>();
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT [Use_ID]
                                                              ,[Use_Name]
                                                              ,[Use_LastName]
                                                              ,[Use_Identification]
                                                              ,[Use_Password]
                                                              ,[Use_Email]
                                                              ,[Use_Status]
                                                              ,[Use_BirthDate]
                                                              ,[Use_Phone]
                                                          FROM [dbo].[User]", sqlConnection);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        User user = new User();
                        user.Use_ID = sqlDataReader.GetInt32(0);
                        user.Use_Name = sqlDataReader.GetString(1);
                        user.Use_LastName = sqlDataReader.GetString(2);
                        user.Use_Identification = sqlDataReader.GetString(3);
                        user.Use_Password = sqlDataReader.GetString(4);
                        user.Use_Email = sqlDataReader.GetString(5);
                        user.Use_Status = sqlDataReader.GetString(6);
                        user.Use_BirthDate = sqlDataReader.GetDateTime(7);
                        user.Use_Phone = sqlDataReader.GetString(8);
                        users.Add(user);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(users);
        }
        //------------------------------------------------------------------------------
        [HttpPost, Route("auth")]
        public IHttpActionResult Authenticate(LoginRequest loginRequest)
        {
            if (loginRequest == null) return BadRequest("Complete the fields to login an user.");
            User user = new User();
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Use_ID
                                                                 ,Use_Name
                                                                 ,Use_LastName
                                                                 ,Use_Identification
                                                                 ,Use_Password
                                                                 ,Use_Email
                                                                 ,Use_Status
                                                                 ,Use_BirthDate
                                                                 ,Use_Phone
                                                             FROM [User]
                                                             WHERE 
                                                                Use_Name = @Use_Name 
                                                                AND Use_Password = @Use_Password", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Use_Name", loginRequest.Username);
                    sqlCommand.Parameters.AddWithValue("@Use_Password", loginRequest.Password);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.Read())
                    {
                        user.Use_ID = sqlDataReader.GetInt32(0);
                        user.Use_Name = sqlDataReader.GetString(1);
                        user.Use_LastName = sqlDataReader.GetString(2);
                        user.Use_Identification = sqlDataReader.GetString(3);
                        user.Use_Password = sqlDataReader.GetString(4);
                        user.Use_Email = sqlDataReader.GetString(5);
                        user.Use_Status = sqlDataReader.GetString(6);
                        user.Use_BirthDate = sqlDataReader.GetDateTime(7);
                        user.Use_Phone = sqlDataReader.GetString(8);

                        var token = TokenGenerator.GenerateTokenJwt(loginRequest.Username);
                        user.Token = token;
                    }
                    if (!string.IsNullOrEmpty(user.Token))
                        return Ok(user);
                    else return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        //------------------------------------------------------------------------------
        [HttpPost]
        public IHttpActionResult EnterUser(User user)
        {
            if (user == null)
            {
                return BadRequest("Please enter required fields to create an user.");
            }
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO [dbo].[User](
                                                                  [Use_Name]
                                                                 ,[Use_LastName]
                                                                 ,[Use_Identification]
                                                                 ,[Use_Password]
                                                                 ,[Use_Email]
                                                                 ,[Use_Status]
                                                                 ,[Use_BirthDate]
                                                                 ,[Use_Phone])
                                                            VALUES(@Use_Name
                                                                , @Use_LastName
                                                            	, @Use_Identification
                                                            	, @Use_Password
                                                            	, @Use_Email
                                                            	, @Use_Status
                                                            	, @Use_BirthDate
                                                                , @Use_Phone)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("Use_Name", user.Use_Name);
                    sqlCommand.Parameters.AddWithValue("Use_LastName", user.Use_LastName);
                    sqlCommand.Parameters.AddWithValue("Use_Identification", user.Use_Identification);
                    sqlCommand.Parameters.AddWithValue("Use_Password", user.Use_Password);
                    sqlCommand.Parameters.AddWithValue("Use_Email", user.Use_Email);
                    sqlCommand.Parameters.AddWithValue("Use_Status", user.Use_Status);
                    sqlCommand.Parameters.AddWithValue("Use_BirthDate", user.Use_BirthDate);
                    sqlCommand.Parameters.AddWithValue("Use_Phone", user.Use_Phone);


                    sqlConnection.Open();
                    int filasAfectadas = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(user);
        }
        //------------------------------------------------------------------------------
        [HttpPut]
        public IHttpActionResult UpdateUser(User user)
        {
            if (user == null)
            {
                return BadRequest("User not found in database.");
            }
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE [dbo].[User]
                                                            SET Use_Name = @Use_Name
                                                                , Use_LastName = @Use_LastName
                                                            	, Use_Identification = @Use_Identification
                                                            	, Use_Password = @Use_Password
                                                            	, Use_Email = @Use_Email
                                                            	, Use_Status = @Use_Status
                                                            	, Use_BirthDate = @Use_BirthDate
                                                                , Use_Phone = @Use_Phone
                                                            WHERE Use_ID = @Use_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("Use_ID", user.Use_ID);
                    sqlCommand.Parameters.AddWithValue("Use_Name", user.Use_Name);
                    sqlCommand.Parameters.AddWithValue("Use_LastName", user.Use_LastName);
                    sqlCommand.Parameters.AddWithValue("Use_Identification", user.Use_Identification);
                    sqlCommand.Parameters.AddWithValue("Use_Password", user.Use_Password);
                    sqlCommand.Parameters.AddWithValue("Use_Email", user.Use_Email);
                    sqlCommand.Parameters.AddWithValue("Use_Status", user.Use_Status);
                    sqlCommand.Parameters.AddWithValue("Use_BirthDate", user.Use_BirthDate);
                    sqlCommand.Parameters.AddWithValue("Use_Phone", user.Use_Phone);


                    sqlConnection.Open();
                    int rowsAffected = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (rowsAffected <= 0)
                    {
                        return BadRequest($"User with ID {user.Use_ID} doesn't exist in database.");
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
        //------------------------------------------------------------------------------
        [HttpDelete]
        public IHttpActionResult DeleteUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"User with ID:{id} not found in database.");
            }
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE FROM [dbo].[User]
                                                            WHERE Use_ID = @Use_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Use_ID", id);
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
        //------------------------------------------------------------------------------
    }
}
