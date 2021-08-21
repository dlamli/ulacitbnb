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
    [RoutePrefix("api/room")]
    public class RoomController : ApiController
    {
        //SQL Connection
        SqlConnection sqlConnection = ConnectionString.GetSqlConnection();

        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Room room = new Room();
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT [Roo_ID]
                                                                  ,[Roo_Price]
                                                                  ,[Roo_Quantity]
                                                                  ,[Roo_Type]
                                                                  ,[Roo_Evaluation]
                                                                  ,[Roo_BedQuantity]
                                                                  ,[Ser_ID]
                                                                  ,[Acc_ID]
                                                              FROM [dbo].[Room]
                                                              WHERE Roo_ID = @Roo_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Roo_ID", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        room.Roo_ID = sqlDataReader.GetInt32(0);
                        room.Roo_Price = sqlDataReader.GetDecimal(1);
                        room.Roo_Quantity = sqlDataReader.GetDecimal(2);
                        room.Roo_Type = sqlDataReader.GetString(3);
                        room.Roo_Evaluation = sqlDataReader.GetString(4);
                        room.Roo_BedQuantity = sqlDataReader.GetDecimal(5);
                        room.Ser_ID = sqlDataReader.GetInt32(6);
                        room.Acc_ID = sqlDataReader.GetInt32(7);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(room);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Room> rooms = new List<Room>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(@"SELECT [Roo_ID]
                                                                  ,[Roo_Price]
                                                                  ,[Roo_Quantity]
                                                                  ,[Roo_Type]
                                                                  ,[Roo_Evaluation]
                                                                  ,[Roo_BedQuantity]
                                                                  ,[Ser_ID]
                                                                  ,[Acc_ID]
                                                              FROM [dbo].[Room]", sqlConnection);

                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Room room = new Room();

                    room.Roo_ID = sqlDataReader.GetInt32(0);
                    room.Roo_Price = sqlDataReader.GetDecimal(1);
                    room.Roo_Quantity = sqlDataReader.GetDecimal(2);
                    room.Roo_Type = sqlDataReader.GetString(3);
                    room.Roo_Evaluation = sqlDataReader.GetString(4);
                    room.Roo_BedQuantity = sqlDataReader.GetDecimal(5);
                    room.Ser_ID = sqlDataReader.GetInt32(6);
                    room.Acc_ID = sqlDataReader.GetInt32(7);
                    rooms.Add(room);
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(rooms);
        }

        [HttpPost]
        public IHttpActionResult Enter(Room room)
        {
            if (room == null)
            {
                return BadRequest("Please enter all the required fields for creating a room.");
            }

            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO [dbo].[Room](
                                                                  Roo_Price
                                                                  ,Roo_Quantity
                                                                  ,Roo_Type
                                                                  ,Roo_Evaluation
                                                                  ,Roo_BedQuantity
                                                                  ,Ser_ID
                                                                  ,Acc_ID)
                                                             VALUES(@Roo_Price
                                                                ,@Roo_Quantity
                                                            	,@Roo_Type
                                                            	,@Roo_Evaluation
                                                                ,@Roo_BedQuantity
                                                                ,@Ser_ID
                                                                ,@Acc_ID)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Roo_Price", room.Roo_Price);
                    sqlCommand.Parameters.AddWithValue("@Roo_Quantity", room.Roo_Quantity);
                    sqlCommand.Parameters.AddWithValue("@Roo_Type", room.Roo_Type);
                    sqlCommand.Parameters.AddWithValue("@Roo_Evaluation", room.Roo_Evaluation);
                    sqlCommand.Parameters.AddWithValue("@Roo_BedQuantity", room.Roo_BedQuantity);
                    sqlCommand.Parameters.AddWithValue("@Ser_ID", room.Ser_ID);
                    sqlCommand.Parameters.AddWithValue("@Acc_ID", room.Acc_ID);

                    sqlConnection.Open();
                    int rowsAffected = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(room);
        }

        [HttpPut]
        public IHttpActionResult Update(Room room)
        {
            if (room == null)
            {
                return BadRequest("Room not found in the database.");
            }

            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE [dbo].[Room]
                                                            SET Roo_Price = @Roo_Price
                                                                  ,Roo_Quantity = @Roo_Quantity
                                                                  ,Roo_Type = @Roo_Type
                                                                  ,Roo_Evaluation = @Roo_Evaluation
                                                                  ,Roo_BedQuantity = @Roo_BedQuantity
                                                                  ,Ser_ID = @Ser_ID
                                                                  ,Acc_ID = @Acc_ID
                                                            WHERE Roo_ID = @Roo_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Roo_ID", room.Roo_ID);
                    sqlCommand.Parameters.AddWithValue("@Roo_Price", room.Roo_Price);
                    sqlCommand.Parameters.AddWithValue("@Roo_Quantity", room.Roo_Quantity);
                    sqlCommand.Parameters.AddWithValue("@Roo_Type", room.Roo_Type);
                    sqlCommand.Parameters.AddWithValue("@Roo_Evaluation", room.Roo_Evaluation);
                    sqlCommand.Parameters.AddWithValue("@Roo_BedQuantity", room.Roo_BedQuantity);
                    sqlCommand.Parameters.AddWithValue("@Ser_ID", room.Ser_ID);
                    sqlCommand.Parameters.AddWithValue("@Acc_ID", room.Acc_ID);

                    sqlConnection.Open();
                    int rowsAffected = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (rowsAffected <= 0)
                    {
                        return BadRequest($"The room with ID {room.Roo_ID} doesn't exist in the database.");
                    }
                    else
                    {
                        return Ok(room);
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"Room with ID:{id} not found in the database.");
            }

            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE FROM [dbo].[Room]
                                                            WHERE Roo_ID = @Roo_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Roo_ID", id);
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
