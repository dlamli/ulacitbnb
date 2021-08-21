using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ulacit_bnb.Models;
using ulacitbnb.db;

namespace ulacitbnb.Controllers
{
    [Authorize]
    [RoutePrefix("api/review")]
    public class ReviewController : ApiController
    {
        SqlConnection sqlConnection = ConnectionString.GetSqlConnection();

        // ===================================================================================================
        [HttpGet, Route("{reviewId:int}")]
        public IHttpActionResult GetReview(int reviewId)
        {
            if (reviewId < 1)
            {
                return BadRequest("Invalid review ID");
            }
            Review review = null;

            try
            {
                using (sqlConnection)
                {
                    SqlCommand selectReviewById = new SqlCommand(@"SELECT
                                                                        Rev_ID,
                                                                        Rev_Date,
                                                                        Rev_Rate,
                                                                        Rev_Recommendation,
                                                                        Rev_Comment,
                                                                        Rev_Usefull,
                                                                        Rev_Title,
                                                                        Cus_ID,
                                                                        Acc_ID 
                                                                    FROM Review
                                                                    WHERE Rev_ID = @Rev_ID", sqlConnection);
                    selectReviewById.Parameters.AddWithValue("Rev_ID", reviewId);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = selectReviewById.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        review = new Review
                        {
                            ID = sqlDataReader.GetInt32(0),
                            Date = sqlDataReader.GetDateTime(1),
                            Rate = sqlDataReader.GetInt32(2),
                            Recommendation = sqlDataReader.GetBoolean(3),
                            Comment = sqlDataReader.GetString(4),
                            Usefull = sqlDataReader.GetInt32(5),
                            Title = sqlDataReader.GetString(6),
                            CustomerID = sqlDataReader.GetInt32(7),
                            AccomodationID = sqlDataReader.GetInt32(8)
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(review);
        }
        // ===================================================================================================
        [HttpGet]
        public IHttpActionResult GetAllReviews()
        {
            List<Review> reviews = new List<Review>();

            try
            {
                using (sqlConnection)
                {
                    SqlCommand selectAllReviews = new SqlCommand(@"SELECT 
                                                                        Rev_ID,
                                                                        Rev_Date,
                                                                        Rev_Rate,
                                                                        Rev_Recommendation,
                                                                        Rev_Comment,
                                                                        Rev_Usefull,
                                                                        Rev_Title,
                                                                        Cus_ID,
                                                                        Acc_ID
                                                                  FROM Review", sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = selectAllReviews.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Review review = new Review
                        {
                            ID = sqlDataReader.GetInt32(0),
                            Date = sqlDataReader.GetDateTime(1),
                            Rate = sqlDataReader.GetInt32(2),
                            Recommendation = sqlDataReader.GetBoolean(3),
                            Comment = sqlDataReader.GetString(4),
                            Usefull = sqlDataReader.GetInt32(5),
                            Title = sqlDataReader.GetString(6),
                            CustomerID = sqlDataReader.GetInt32(7),
                            AccomodationID = sqlDataReader.GetInt32(8)
                        };
                        reviews.Add(review);
                    }
                }

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(reviews);
        }
        // ===================================================================================================
        [HttpGet, Route("~/api/user/{customerId:int}/reviews")]
        public IHttpActionResult GetUserReviews(int customerId)
        {
            Review review = null;

            try
            {
                using (sqlConnection)
                {
                    SqlCommand selectUserReviews = new SqlCommand(@"SELECT 
                                                                        Rev_ID,
                                                                        Rev_Date,
                                                                        Rev_Rate,
                                                                        Rev_Recommendation,
                                                                        Rev_Comment,
                                                                        Rev_Usefull,
                                                                        Rev_Title,
                                                                        Cus_ID,
                                                                        Acc_ID
                                                                    FROM Review
                                                                    WHERE Cus_ID = @Cus_ID", sqlConnection);
                    selectUserReviews.Parameters.AddWithValue("Cus_ID", customerId);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = selectUserReviews.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        review = new Review
                        {
                            ID = sqlDataReader.GetInt32(0),
                            Date = sqlDataReader.GetDateTime(1),
                            Rate = sqlDataReader.GetInt32(2),
                            Recommendation = sqlDataReader.GetBoolean(3),
                            Comment = sqlDataReader.GetString(4),
                            Usefull = sqlDataReader.GetInt32(5),
                            Title = sqlDataReader.GetString(6),
                            CustomerID = sqlDataReader.GetInt32(7),
                            AccomodationID = sqlDataReader.GetInt32(8)
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(review);
        }
        // ===================================================================================================
        [HttpGet, Route("~/api/accomodation/{accomodationId:int}/reviews")]
        public IHttpActionResult GetAccomodationReviews(int accomodationId)
        {
            Review review = null;

            try
            {
                using (sqlConnection)
                {
                    SqlCommand selectAccomodationReviews = new SqlCommand(@"SELECT
                                                                                Rev_ID,
                                                                                Rev_Date,
                                                                                Rev_Rate,
                                                                                Rev_Recommendation,
                                                                                Rev_Comment,
                                                                                Rev_Usefull,
                                                                                Rev_Title,
                                                                                Cus_ID,
                                                                                Acc_ID
                                                                            FROM Review
                                                                            WHERE Acc_ID = @Acc_ID", sqlConnection);
                    selectAccomodationReviews.Parameters.AddWithValue("Acc_ID", accomodationId);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = selectAccomodationReviews.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        review = new Review
                        {
                            ID = sqlDataReader.GetInt32(0),
                            Date = sqlDataReader.GetDateTime(1),
                            Rate = sqlDataReader.GetInt32(2),
                            Recommendation = sqlDataReader.GetBoolean(3),
                            Comment = sqlDataReader.GetString(4),
                            Usefull = sqlDataReader.GetInt32(5),
                            Title = sqlDataReader.GetString(6),
                            CustomerID = sqlDataReader.GetInt32(7),
                            AccomodationID = sqlDataReader.GetInt32(8)
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(review);
        }
        // ===================================================================================================
        [HttpPost]
        public HttpResponseMessage CreateNewReview(Review review)
        {
            if (review == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Required fields to create review are invalid");
            }

            try
            {
                using (sqlConnection)
                {
                    SqlCommand insertNewReview = new SqlCommand(@"INSERT INTO Review 
                                                                    (Rev_Date, Rev_Rate, Rev_Recommendation, Rev_Comment, Rev_Usefull, Rev_Title, Cus_ID, Acc_ID)
                                                                VALUES (@Rev_Date, @Rev_Rate, @Rev_Recommendation, @Rev_Comment, @Rev_Usefull, @Rev_Title, @Cus_ID, @Acc_ID)", sqlConnection);
                    insertNewReview.Parameters.AddWithValue("Rev_Date", review.Date);
                    insertNewReview.Parameters.AddWithValue("Rev_Rate", review.Rate);
                    insertNewReview.Parameters.AddWithValue("Rev_Recommendation", review.Recommendation);
                    insertNewReview.Parameters.AddWithValue("Rev_Comment", review.Comment);
                    insertNewReview.Parameters.AddWithValue("Rev_Usefull", review.Usefull);
                    insertNewReview.Parameters.AddWithValue("Rev_Title", review.Title);
                    insertNewReview.Parameters.AddWithValue("Cus_ID", review.CustomerID);
                    insertNewReview.Parameters.AddWithValue("Acc_ID", review.AccomodationID);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = insertNewReview.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(ex.ToString());
            }
            return Request.CreateResponse(HttpStatusCode.OK, review);
        }
        // ===================================================================================================
        [HttpPut]
        public HttpResponseMessage UpdateReview(Review review)
        {
            if (review == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Required fields to update review are invalid");
            }

            try
            {
                using (sqlConnection)
                {
                    SqlCommand updateReview = new SqlCommand(@"UPDATE Review 
                                                                SET Rev_Date = @Rev_Date,
                                                                    Rev_Rate = @Rev_Rate,
                                                                    Rev_Recommendation = @Rev_Recommendation,
                                                                    Rev_Comment = @Rev_Comment,
                                                                    Rev_Usefull = @Rev_Usefull,
                                                                    Rev_Title = @Rev_Title,
                                                                    Cus_ID = @Cus_ID,
                                                                    Acc_ID = @Acc_ID
                                                                WHERE Rev_ID = @Rev_ID", sqlConnection);
                    updateReview.Parameters.AddWithValue("Rev_ID", review.ID);
                    updateReview.Parameters.AddWithValue("Rev_Date", review.Date);
                    updateReview.Parameters.AddWithValue("Rev_Rate", review.Rate);
                    updateReview.Parameters.AddWithValue("Rev_Recommendation", review.Recommendation);
                    updateReview.Parameters.AddWithValue("Rev_Comment", review.Comment);
                    updateReview.Parameters.AddWithValue("Rev_Usefull", review.Usefull);
                    updateReview.Parameters.AddWithValue("Rev_Title", review.Title);
                    updateReview.Parameters.AddWithValue("Cus_ID", review.CustomerID);
                    updateReview.Parameters.AddWithValue("Acc_ID", review.AccomodationID);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = updateReview.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(ex.ToString());
            }
            return Request.CreateResponse(HttpStatusCode.OK, review);
        }
        // ===================================================================================================
        [HttpDelete, Route("{reviewId:int}")]
        public IHttpActionResult RemoveReview(int reviewId)
        {
            if (reviewId < 1)
            {
                return BadRequest("Invalid review ID");
            }

            try
            {
                using (sqlConnection)
                {
                    SqlCommand deleteReview = new SqlCommand(@"DELETE Review
                                                                  WHERE Rev_ID = @Rev_ID", sqlConnection);
                    deleteReview.Parameters.AddWithValue("Rev_ID", reviewId);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = deleteReview.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(reviewId);
        }

    }
}
