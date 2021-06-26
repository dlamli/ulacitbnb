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
    //Public Access
    [AllowAnonymous]

    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        /**
         * Authentication
         * ====================
         */
        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(LoginRequest loginRequest)
        {
            //LoginRequest null
            if (loginRequest == null) return BadRequest("Complete the fields to login.");

            //New instance of Usuario
            User user = new User();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ULACITBnB"].ConnectionString))
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
                                                                Use_Identification = @Use_Identification 
                                                                AND Use_Password = @Use_Password", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Use_Identification", loginRequest.Username);
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
                    sqlConnection.Close();

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

        /**
         * Registration
         * ====================
         */
        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register(User user)
        {
            if (user == null) return BadRequest("Complete the fields to register user.");

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ULACITBnB"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO [dbo].[User](               
                                                                  Use_Name
                                                                 ,Use_LastName
                                                                 ,Use_Identification
                                                                 ,Use_Password
                                                                 ,Use_Email
                                                                 ,Use_Status
                                                                 ,Use_BirthDate
                                                                 ,Use_Phone)
                                                            VALUES(
                                                                  @Use_Name
                                                                 ,@Use_LastName
                                                                 ,@Use_Identification
                                                                 ,@Use_Password
                                                                 ,@Use_Email
                                                                 ,@Use_Status
                                                                 ,@Use_BirthDate
                                                                 ,@Use_Phone)", sqlConnection);

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


                    if (filasAfectadas > 0) return Ok(user);


                }


            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }

            return Ok();

        }
      
    }
}
