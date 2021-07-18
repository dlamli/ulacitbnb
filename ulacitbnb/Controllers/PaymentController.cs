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
    [RoutePrefix("api/payment")]
    public class PaymentController : ApiController
    {
        //SQL Connection
        SqlConnection sqlConnection = ConnectionString.GetSqlConnection();
        // ===================================================================================================
        [HttpGet, Route("{id:int}")]
        public IHttpActionResult GetId(int id)
        {
            Payment payment = new Payment();
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT  [Pay_ID]
                                                                      ,[Pay_Brand]
                                                                      ,[Pay_Type]
                                                                      ,[Pay_Modality]
                                                                      ,[Pay_Date]
                                                                      ,[Pay_Amount]
                                                                      ,[Pay_Taxes]
                                                                      ,[Pay_Total]
                                                                  FROM [dbo].[Payment]
                                                                  WHERE Pay_ID = @Pay_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("Pay_ID", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        payment.Pay_ID = sqlDataReader.GetInt32(0);
                        payment.Pay_Brand = sqlDataReader.GetString(1);
                        payment.Pay_Type = sqlDataReader.GetString(2);
                        payment.Pay_Modality = sqlDataReader.GetString(3);
                        payment.Pay_Date = sqlDataReader.GetDateTime(4);
                        payment.Pay_Amount = sqlDataReader.GetInt32(5);
                        payment.Pay_Taxes = sqlDataReader.GetDecimal(6);
                        payment.Pay_Total = sqlDataReader.GetDecimal(7);
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(payment);
        }
        // ===================================================================================================
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Payment> payments = new List<Payment>();
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT  [Pay_ID]
                                                                      ,[Pay_Brand]
                                                                      ,[Pay_Type]
                                                                      ,[Pay_Modality]
                                                                      ,[Pay_Date]
                                                                      ,[Pay_Amount]
                                                                      ,[Pay_Taxes]
                                                                      ,[Pay_Total]
                                                                  FROM [dbo].[Payment]", sqlConnection);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Payment payment = new Payment();
                        payment.Pay_ID = sqlDataReader.GetInt32(0);
                        payment.Pay_Brand = sqlDataReader.GetString(1);
                        payment.Pay_Type = sqlDataReader.GetString(2);
                        payment.Pay_Modality = sqlDataReader.GetString(3);
                        payment.Pay_Date = sqlDataReader.GetDateTime(4);
                        payment.Pay_Amount = sqlDataReader.GetInt32(5);
                        payment.Pay_Taxes = sqlDataReader.GetDecimal(6);
                        payment.Pay_Total = sqlDataReader.GetDecimal(7);
                        payments.Add(payment);
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(payments);
        }
        // ===================================================================================================
        [HttpPost]
        public IHttpActionResult Enter(Payment payment)
        {
            if (payment == null)
            {
                return BadRequest("Please supply all the required fields for creating a payment.");
            }

            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO [dbo].[Payment](
                                                                       Pay_Brand
                                                                      ,Pay_Type
                                                                      ,Pay_Modality
                                                                      ,Pay_Date
                                                                      ,Pay_Amount
                                                                      ,Pay_Taxes
                                                                      ,Pay_Total)
                                                              VALUES(@Pay_Brand, @Pay_Type, @Pay_Modality, 
                                                                     @Pay_Date, @Pay_Amount, @Pay_Taxes, 
                                                                     @Pay_Total)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Pay_Brand", payment.Pay_Brand);
                    sqlCommand.Parameters.AddWithValue("@Pay_Type", payment.Pay_Type);
                    sqlCommand.Parameters.AddWithValue("@Pay_Modality", payment.Pay_Modality);
                    sqlCommand.Parameters.AddWithValue("@Pay_Date", payment.Pay_Date);
                    sqlCommand.Parameters.AddWithValue("@Pay_Amount", payment.Pay_Amount);
                    sqlCommand.Parameters.AddWithValue("@Pay_Taxes", payment.Pay_Taxes);
                    sqlCommand.Parameters.AddWithValue("@Pay_Total", payment.Pay_Total);

                    sqlConnection.Open();
                    int rowsAfected = sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(payment);
        }
        // ===================================================================================================
        [HttpPut]
        public IHttpActionResult Update(Payment payment)
        {
            if (payment == null)
            {
                return BadRequest("Service not found in database.");
            }

            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE [dbo].[Payment]
                                                              SET Pay_Brand = @Pay_Brand
                                                                , Pay_Type = @Pay_Type
                                                            	, Pay_Modality = @Pay_Modality
                                                                , Pay_Date = @Pay_Date
                                                                , Pay_Amount = @Pay_Amount
                                                                , Pay_Taxes = @Pay_Taxes
                                                                , Pay_Total = @Pay_Total
                                                            WHERE Pay_ID = @Pay_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Pay_ID", payment.Pay_ID);
                    sqlCommand.Parameters.AddWithValue("@Pay_Brand", payment.Pay_Brand);
                    sqlCommand.Parameters.AddWithValue("@Pay_Type", payment.Pay_Type);
                    sqlCommand.Parameters.AddWithValue("@Pay_Modality", payment.Pay_Modality);
                    sqlCommand.Parameters.AddWithValue("@Pay_Date", payment.Pay_Date);
                    sqlCommand.Parameters.AddWithValue("@Pay_Amount", payment.Pay_Amount);
                    sqlCommand.Parameters.AddWithValue("@Pay_Taxes", payment.Pay_Taxes);
                    sqlCommand.Parameters.AddWithValue("@Pay_Total", payment.Pay_Total);

                    sqlConnection.Open();
                    int rowsAffected = sqlCommand.ExecuteNonQuery();
                    if (rowsAffected <= 0)
                    {
                        return BadRequest($"Payment with ID {payment.Pay_ID} doesn't exist in the database.");
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
                return BadRequest($"Payment with ID:{id} not found in the database.");
            }

            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE FROM [dbo].[Payment]
                                                            WHERE Pay_ID = @Pay_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Pay_ID", id);
                    sqlConnection.Open();

                    int rowsAfected = sqlCommand.ExecuteNonQuery();
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




