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
    [Authorize]
    [RoutePrefix("api/accomodation")]
    public class AccomodationController : ApiController
    {

        //SQL Connection
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ULACITBnB"].ConnectionString);

        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Accomodation accomodation = new Accomodation();
            try
            {
                using (sqlConnection) 
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT  [Acc_ID]
                                                                      ,[Acc_Name]
                                                                      ,[Acc_Country]
                                                                      ,[Acc_Zipcode]
                                                                      ,[Acc_State]
                                                                      ,[Acc_Address]
                                                                      ,[Acc_Description]
                                                                      ,[Acc_Evaluation]
                                                                      ,[Hos_ID]
                                                                  FROM [dbo].[Accomodation]
                                                                  WHERE Acc_ID = @Acc_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("Acc_ID", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        accomodation.Acc_ID = sqlDataReader.GetInt32(0);
                        accomodation.Acc_Name = sqlDataReader.GetString(1);
                        accomodation.Acc_Country = sqlDataReader.GetString(2);
                        accomodation.Acc_Zipcode = sqlDataReader.GetString(3);
                        accomodation.Acc_State = sqlDataReader.GetString(4);
                        accomodation.Acc_Address = sqlDataReader.GetString(5);
                        accomodation.Acc_Description = sqlDataReader.GetString(6);
                        accomodation.Acc_Evaluation = sqlDataReader.GetString(7);
                        accomodation.Hos_ID = sqlDataReader.GetInt32(8);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(accomodation);
        }

        [HttpGet]
        public IHttpActionResult GetAll() 
        {
            List<Accomodation> accomodations = new List<Accomodation>();
            try
            {
                using (sqlConnection) 
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT  [Acc_ID]
                                                                      ,[Acc_Name]
                                                                      ,[Acc_Country]
                                                                      ,[Acc_Zipcode]
                                                                      ,[Acc_State]
                                                                      ,[Acc_Address]
                                                                      ,[Acc_Description]
                                                                      ,[Acc_Evaluation]
                                                                      ,[Hos_ID]
                                                                  FROM [dbo].[Accomodation]", sqlConnection);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read()) 
                    {
                        Accomodation accomodation = new Accomodation();
                        accomodation.Acc_ID = sqlDataReader.GetInt32(0);
                        accomodation.Acc_Name = sqlDataReader.GetString(1);
                        accomodation.Acc_Country = sqlDataReader.GetString(2);
                        accomodation.Acc_Zipcode = sqlDataReader.GetString(3);
                        accomodation.Acc_State = sqlDataReader.GetString(4);
                        accomodation.Acc_Address = sqlDataReader.GetString(5);
                        accomodation.Acc_Description = sqlDataReader.GetString(6);
                        accomodation.Acc_Evaluation = sqlDataReader.GetString(7);
                        accomodation.Hos_ID = sqlDataReader.GetInt32(8);
                        accomodations.Add(accomodation);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(accomodations);
        }

        [HttpPost]
        public IHttpActionResult Enter(Accomodation accomodation) 
        {
            if (accomodation == null)
            {
                return BadRequest("Please supply all the required fields for creating an accomodation.");
            }

            try
            {
                using (sqlConnection) 
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO [dbo].[Accomodation](
                                                                       Acc_Name
                                                                      ,Acc_Country
                                                                      ,Acc_Zipcode
                                                                      ,Acc_State
                                                                      ,Acc_Address
                                                                      ,Acc_Description
                                                                      ,Acc_Evaluation
                                                                      ,Hos_ID)
                                                              VALUES(@Acc_Name, @Acc_Country, @Acc_Zipcode, 
                                                                     @Acc_State, @Acc_Address, @Acc_Description, 
                                                                     @Acc_Evaluation, @Hos_ID)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Acc_Name", accomodation.Acc_Name);
                    sqlCommand.Parameters.AddWithValue("@Acc_Country", accomodation.Acc_Country);
                    sqlCommand.Parameters.AddWithValue("@Acc_Zipcode", accomodation.Acc_Zipcode);
                    sqlCommand.Parameters.AddWithValue("@Acc_State", accomodation.Acc_State);
                    sqlCommand.Parameters.AddWithValue("@Acc_Address", accomodation.Acc_Address);
                    sqlCommand.Parameters.AddWithValue("@Acc_Description", accomodation.Acc_Description);
                    sqlCommand.Parameters.AddWithValue("@Acc_Evaluation", accomodation.Acc_Evaluation);
                    sqlCommand.Parameters.AddWithValue("@Hos_ID", accomodation.Hos_ID);

                    sqlConnection.Open();
                    int filasAfectadas = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(accomodation);
        }

        [HttpPut]
        public IHttpActionResult Update(Accomodation accomodation) 
        {
            if (accomodation == null)
            {
                return BadRequest("Service not found in database.");
            }

            try
            {
                using (sqlConnection) 
                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE [dbo].[Accomodation]
                                                              SET Acc_Name = @Acc_Name
                                                                , Acc_Country = @Acc_Country
                                                            	, Acc_Zipcode = @Acc_Zipcode
                                                                , Acc_State = @Acc_State
                                                                , Acc_Address = @Acc_Address
                                                                , Acc_Description = @Acc_Description
                                                                , Acc_Evaluation = @Acc_Evaluation
                                                                , Hos_ID = @Hos_ID
                                                            WHERE Acc_ID = @Acc_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Acc_ID", accomodation.Acc_ID);
                    sqlCommand.Parameters.AddWithValue("@Acc_Name", accomodation.Acc_Name);
                    sqlCommand.Parameters.AddWithValue("@Acc_Country", accomodation.Acc_Country);
                    sqlCommand.Parameters.AddWithValue("@Acc_Zipcode", accomodation.Acc_Zipcode);
                    sqlCommand.Parameters.AddWithValue("@Acc_State", accomodation.Acc_State);
                    sqlCommand.Parameters.AddWithValue("@Acc_Address", accomodation.Acc_Address);
                    sqlCommand.Parameters.AddWithValue("@Acc_Description", accomodation.Acc_Description);
                    sqlCommand.Parameters.AddWithValue("@Acc_Evaluation", accomodation.Acc_Evaluation);
                    sqlCommand.Parameters.AddWithValue("@Hos_ID", accomodation.Hos_ID);

                    sqlConnection.Open();
                    int rowsAfected = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (rowsAfected <= 0)
                    {
                        return BadRequest($"Accomodation with ID {accomodation.Acc_ID} doesn't exist in the database.");
                    }
                    else 
                    {
                        return Ok(rowsAfected);
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete, Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"Accomodation with ID:{id} not found in the database.");
            }

            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE FROM [dbo].[Accomodation]
                                                            WHERE Acc_ID = @Acc_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Acc_ID", id);
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
