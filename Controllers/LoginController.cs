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
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT UserID
                                                                 ,Name
                                                                 ,LastName
                                                                 ,Identification
                                                                 ,Password
                                                                 ,Email
                                                                 ,Status
                                                                 ,BirthDate
                                                                 ,Phone
                                                             FROM [User]
                                                             WHERE 
                                                                Identification = @Identification 
                                                                AND Password = @Password", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Identification", loginRequest.Username);
                    sqlCommand.Parameters.AddWithValue("@Password", loginRequest.Password);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.Read())
                    {
                        user.UserID = sqlDataReader.GetInt32(0);
                        user.Name = sqlDataReader.GetString(1);
                        user.LastName = sqlDataReader.GetString(2);
                        user.Identification = sqlDataReader.GetString(3);
                        user.Password = sqlDataReader.GetString(4);
                        user.Email = sqlDataReader.GetString(5);
                        user.Status = sqlDataReader.GetString(6);
                        user.BirthDate = sqlDataReader.GetDateTime(7);
                        user.Phone = sqlDataReader.GetString(8);

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
                                                                  Name
                                                                 ,LastName
                                                                 ,Identification
                                                                 ,Password
                                                                 ,Email
                                                                 ,Status
                                                                 ,BirthDate
                                                                 ,Phone)
                                                            VALUES(
                                                                  @Name
                                                                 ,@LastName
                                                                 ,@Identification
                                                                 ,@Password
                                                                 ,@Email
                                                                 ,@Status
                                                                 ,@BirthDate
                                                                 ,@Phone)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("Name", user.Name);
                    sqlCommand.Parameters.AddWithValue("LastName", user.LastName);
                    sqlCommand.Parameters.AddWithValue("Identification", user.Identification);
                    sqlCommand.Parameters.AddWithValue("Password", user.Password);
                    sqlCommand.Parameters.AddWithValue("Email", user.Email);
                    sqlCommand.Parameters.AddWithValue("Status", user.Status);
                    sqlCommand.Parameters.AddWithValue("BirthDate", user.BirthDate);
                    sqlCommand.Parameters.AddWithValue("Phone", user.Phone);


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
