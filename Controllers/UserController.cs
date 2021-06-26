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
    //Authentication Access
    [Authorize]
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        //GET
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            User user = new User();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ULACITBnB"].ConnectionString))
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

        //GET
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<User> users = new List<User>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ULACITBnB"].ConnectionString))
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

        ////POST
        //[HttpPost]
        //public IHttpActionResult Ingresar(User user)
        //{
        //    if (habitacion == null)
        //    {
        //        return BadRequest("Ingrese los datos necesarios para insertar una habitación.");
        //    }
        //    try
        //    {
        //        using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
        //        {
        //            SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO Habitacion(
        //                                                          HOT_CODIGO
        //                                                        , HAB_NUMERO
        //                                                    	, HAB_CAPACIDAD
        //                                                    	, HAB_TIPO
        //                                                    	, HAB_DESCRIPCION
        //                                                    	, HAB_ESTADO
        //                                                    	, HAB_PRECIO)
        //                                                    VALUES(@HOT_CODIGO
        //                                                        , @HAB_NUMERO
        //                                                    	, @HAB_CAPACIDAD
        //                                                    	, @HAB_TIPO
        //                                                    	, @HAB_DESCRIPCION
        //                                                    	, @HAB_ESTADO
        //                                                    	, @HAB_PRECIO)", sqlConnection);

        //            sqlCommand.Parameters.AddWithValue("HOT_CODIGO", habitacion.HOT_CODIGO);
        //            sqlCommand.Parameters.AddWithValue("HAB_NUMERO", habitacion.HAB_NUMERO);
        //            sqlCommand.Parameters.AddWithValue("HAB_CAPACIDAD", habitacion.HAB_CAPACIDAD);
        //            sqlCommand.Parameters.AddWithValue("HAB_TIPO", habitacion.HAB_TIPO);
        //            sqlCommand.Parameters.AddWithValue("HAB_DESCRIPCION", habitacion.HAB_DESCRIPCION);
        //            sqlCommand.Parameters.AddWithValue("HAB_ESTADO", habitacion.HAB_ESTADO);
        //            sqlCommand.Parameters.AddWithValue("HAB_PRECIO", habitacion.HAB_PRECIO);

        //            sqlConnection.Open();
        //            int filasAfectadas = sqlCommand.ExecuteNonQuery();
        //            sqlConnection.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //    return Ok(habitacion);
        //}

        ////PUT
        //[HttpPut]
        //public IHttpActionResult Actualizar(User user)
        //{
        //    if (habitacion == null)
        //    {
        //        return BadRequest("La habitación no existe en la base de datos.");
        //    }
        //    try
        //    {
        //        using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
        //        {
        //            SqlCommand sqlCommand = new SqlCommand(@"UPDATE Habitacion
        //                                                    SET HOT_CODIGO = @HOT_CODIGO
        //                                                        , HAB_NUMERO = @HAB_NUMERO
        //                                                    	, HAB_CAPACIDAD = @HAB_CAPACIDAD
        //                                                    	, HAB_TIPO = @HAB_TIPO
        //                                                    	, HAB_DESCRIPCION = @HAB_DESCRIPCION
        //                                                    	, HAB_ESTADO = @HAB_ESTADO
        //                                                    	, HAB_PRECIO = @HAB_PRECIO
        //                                                    WHERE HAB_CODIGO = @HAB_CODIGO", sqlConnection);

        //            sqlCommand.Parameters.AddWithValue("HAB_CODIGO", habitacion.HAB_CODIGO);
        //            sqlCommand.Parameters.AddWithValue("HOT_CODIGO", habitacion.HOT_CODIGO);
        //            sqlCommand.Parameters.AddWithValue("HAB_NUMERO", habitacion.HAB_NUMERO);
        //            sqlCommand.Parameters.AddWithValue("HAB_CAPACIDAD", habitacion.HAB_CAPACIDAD);
        //            sqlCommand.Parameters.AddWithValue("HAB_TIPO", habitacion.HAB_TIPO);
        //            sqlCommand.Parameters.AddWithValue("HAB_DESCRIPCION", habitacion.HAB_DESCRIPCION);
        //            sqlCommand.Parameters.AddWithValue("HAB_ESTADO", habitacion.HAB_DESCRIPCION);
        //            sqlCommand.Parameters.AddWithValue("HAB_PRECIO", habitacion.HAB_PRECIO);

        //            sqlConnection.Open();
        //            int filasAfectadas = sqlCommand.ExecuteNonQuery();
        //            sqlConnection.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //    return Ok(habitacion);
        //}

        ////DELETE
        //[HttpDelete]
        //public IHttpActionResult Eliminar(int id)
        //{

        //    if (id <= 0)
        //    {
        //        return BadRequest($"La habitación con el ID:{id} no existe en la base de datos.");
        //    }
        //    try
        //    {
        //        using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
        //        {
        //            SqlCommand sqlCommand = new SqlCommand(@"DELETE FROM Habitacion
        //                                                    WHERE HAB_CODIGO = @HAB_CODIGO", sqlConnection);

        //            sqlCommand.Parameters.AddWithValue("@HAB_CODIGO", id);
        //            sqlConnection.Open();

        //            int filasAfectadas = sqlCommand.ExecuteNonQuery();
        //            sqlConnection.Close();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //    return Ok(id);
        //}
    }
}
