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
    [RoutePrefix("api/reservation")]
    public class ReservationController : ApiController
    {
        //SQL Connection
        SqlConnection sqlConnection = ConnectionString.GetSqlConnection();
        // ===================================================================================================
        [HttpGet, Route("{id:int}")]
        public IHttpActionResult GetId(int id)
        {
            Reservation reservation = new Reservation();
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT  [Res_ID]
                                                                      ,[Res_StartDate]
                                                                      ,[Res_ReservationDate]
                                                                      ,[Res_EndDate]
                                                                      ,[Res_Status]
                                                                      ,[Res_Quantity]
                                                                      ,[Res_ResolutionDate]
                                                                      ,[Res_PaymentID]
                                                                      ,[Cus_ID]
                                                                      ,[Roo_ID]
                                                                  FROM [dbo].[Reservation]
                                                                  WHERE Res_ID = @Res_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("Res_ID", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        reservation.Res_ID = sqlDataReader.GetInt32(0);
                        reservation.Res_StartDate = sqlDataReader.GetDateTime(1);
                        reservation.Res_ReservationDate = sqlDataReader.GetDateTime(2);
                        reservation.Res_EndDate = sqlDataReader.GetDateTime(3);
                        reservation.Res_Status = sqlDataReader.GetString(4);
                        reservation.Res_Quantity = sqlDataReader.GetDecimal(5);
                        reservation.Res_ResolutionDate = sqlDataReader.GetString(6);
                        reservation.Res_PaymentID = sqlDataReader.GetInt32(7);
                        reservation.Cus_ID = sqlDataReader.GetInt32(8);
                        reservation.Roo_ID = sqlDataReader.GetInt32(9);
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(reservation);
        }
        // ===================================================================================================
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Reservation> reservations = new List<Reservation>();
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT  [Res_ID]
                                                                      ,[Res_StartDate]
                                                                      ,[Res_ReservationDate]
                                                                      ,[Res_EndDate]
                                                                      ,[Res_Status]
                                                                      ,[Res_Quantity]
                                                                      ,[Res_ResolutionDate]
                                                                      ,[Res_PaymentID]
                                                                      ,[Cus_ID]
                                                                      ,[Roo_ID]
                                                                  FROM [dbo].[Reservation]", sqlConnection);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Reservation reservation = new Reservation();
                        reservation.Res_ID = sqlDataReader.GetInt32(0);
                        reservation.Res_StartDate = sqlDataReader.GetDateTime(1);
                        reservation.Res_ReservationDate = sqlDataReader.GetDateTime(2);
                        reservation.Res_EndDate = sqlDataReader.GetDateTime(3);
                        reservation.Res_Status = sqlDataReader.GetString(4);
                        reservation.Res_Quantity = sqlDataReader.GetDecimal(5);
                        reservation.Res_ResolutionDate = sqlDataReader.GetString(6);
                        reservation.Res_PaymentID = sqlDataReader.GetInt32(7);
                        reservation.Cus_ID = sqlDataReader.GetInt32(8);
                        reservation.Roo_ID = sqlDataReader.GetInt32(9);
                        reservations.Add(reservation);
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(reservations);
        }
        // ===================================================================================================
        [HttpPost]
        public IHttpActionResult Enter(Reservation reservation)
        {
            if (reservation == null)
            {
                return BadRequest("Please supply all the required fields for creating an accomodation.");
            }

            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO [dbo].[Reservation](
                                                                       Res_StartDate
                                                                      ,Res_ReservationDate
                                                                      ,Res_EndDate
                                                                      ,Res_Status
                                                                      ,Res_Quantity
                                                                      ,Res_ResolutionDate
                                                                      ,Res_PaymentID
                                                                      ,Cus_ID
                                                                      ,Roo_ID)
                                                              VALUES(@Res_StartDate, @Res_ReservationDate, @Res_EndDate, 
                                                                     @Res_Status, @Res_Quantity, @Res_ResolutionDate, 
                                                                     @Res_PaymentID, @Cus_ID, @Roo_ID)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Res_StartDate", reservation.Res_StartDate);
                    sqlCommand.Parameters.AddWithValue("@Res_ReservationDate", reservation.Res_ReservationDate);
                    sqlCommand.Parameters.AddWithValue("@Res_EndDate", reservation.Res_EndDate);
                    sqlCommand.Parameters.AddWithValue("@Res_Status", reservation.Res_Status);
                    sqlCommand.Parameters.AddWithValue("@Res_Quantity", reservation.Res_Quantity);
                    sqlCommand.Parameters.AddWithValue("@Res_ResolutionDate", reservation.Res_ResolutionDate);
                    sqlCommand.Parameters.AddWithValue("@Res_PaymentID", reservation.Res_PaymentID);
                    sqlCommand.Parameters.AddWithValue("@Cus_ID", reservation.Cus_ID);
                    sqlCommand.Parameters.AddWithValue("@Roo_ID", reservation.Roo_ID);

                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(reservation);
        }
        // ===================================================================================================
        [HttpPut]
        public IHttpActionResult Update(Reservation reservation)
        {
            if (reservation == null)
            {
                return BadRequest("Service not found in database.");
            }

            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE [dbo].[Reservation]
                                                              SET Res_StartDate = @Res_StartDate
                                                                , Res_ReservationDate = @Res_ReservationDate
                                                            	, Res_EndDate = @Res_EndDate
                                                                , Res_Status = @Res_Status
                                                                , Res_Quantity = @Res_Quantity
                                                                , Res_ResolutionDate = @Res_ResolutionDate
                                                                , Res_PaymentID = @Res_PaymentID
                                                                , Cus_ID = @Cus_ID
                                                                , Roo_ID = @Roo_ID
                                                            WHERE Res_ID = @Res_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Res_ID", reservation.Res_ID);
                    sqlCommand.Parameters.AddWithValue("@Res_StartDate", reservation.Res_StartDate);
                    sqlCommand.Parameters.AddWithValue("@Res_ReservationDate", reservation.Res_ReservationDate);
                    sqlCommand.Parameters.AddWithValue("@Res_EndDate", reservation.Res_EndDate);
                    sqlCommand.Parameters.AddWithValue("@Res_Status", reservation.Res_Status);
                    sqlCommand.Parameters.AddWithValue("@Res_Quantity", reservation.Res_Quantity);
                    sqlCommand.Parameters.AddWithValue("@Res_ResolutionDate", reservation.Res_ResolutionDate);
                    sqlCommand.Parameters.AddWithValue("@Res_PaymentID", reservation.Res_PaymentID);
                    sqlCommand.Parameters.AddWithValue("@Cus_ID", reservation.Cus_ID);
                    sqlCommand.Parameters.AddWithValue("@Roo_ID", reservation.Roo_ID);

                    sqlConnection.Open();
                    int rowsAffected = sqlCommand.ExecuteNonQuery();
                    if (rowsAffected <= 0)
                    {
                        return BadRequest($"Reservation with ID {reservation.Res_ID} doesn't exist in the database.");
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
        [HttpDelete, Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"Reservation with ID:{id} not found in the database.");
            }

            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE FROM [dbo].[Reservation]
                                                            WHERE Res_ID = @Res_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Res_ID", id);
                    sqlConnection.Open();

                    sqlCommand.ExecuteNonQuery();
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

