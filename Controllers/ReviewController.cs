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
    [RoutePrefix("api/review")]
    public class ReviewController : ApiController
    {
        readonly string DB_CONNECTION_STRING = ConfigurationManager.ConnectionStrings["UlacitbnbAzureDB"].ConnectionString;

        // ===================================================================================================
        [HttpGet]
        public IHttpActionResult GetReview(int id)
        {
            Review review = null;

            try
            {
                SqlConnection sqlConnection = new SqlConnection(DB_CONNECTION_STRING);
                using (sqlConnection)
                {
                    SqlCommand selectReviewById = new SqlCommand(@"SELECT * FROM Review
                                                                    WHERE Rev_ID = @Rev_ID", sqlConnection);
                    selectReviewById.Parameters.AddWithValue("Rev_ID", id);
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
                            UserID = sqlDataReader.GetInt32(7),
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
                SqlConnection sqlConnection = new SqlConnection(DB_CONNECTION_STRING);
                using (sqlConnection)
                {
                    SqlCommand selectAllReviews = new SqlCommand("SELECT * FROM Review", sqlConnection);
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
                            UserID = sqlDataReader.GetInt32(7),
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
        [HttpPost]
        public HttpResponseMessage CreateNewReview(Review review)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(DB_CONNECTION_STRING);
                using (sqlConnection)
                {
                    SqlCommand insertNewReview = new SqlCommand(@"INSERT INTO Review 
                                                                (Rev_Date, Rev_Rate, Rev_Recommendation, Rev_Comment, Rev_Usefull, Rev_Title, Use_ID, Acc_ID)
                                                                VALUES (@Rev_Date, @Rev_Rate, @Rev_Recommendation, @Rev_Comment, @Rev_Usefull, @Rev_Title, @Use_ID, @Acc_ID)", sqlConnection);
                    insertNewReview.Parameters.AddWithValue("Rev_Date", review.Date);
                    insertNewReview.Parameters.AddWithValue("Rev_Rate", review.Rate);
                    insertNewReview.Parameters.AddWithValue("Rev_Recommendation", review.Recommendation);
                    insertNewReview.Parameters.AddWithValue("Rev_Comment", review.Comment);
                    insertNewReview.Parameters.AddWithValue("Rev_Usefull", review.Usefull);
                    insertNewReview.Parameters.AddWithValue("Rev_Title", review.Title);
                    insertNewReview.Parameters.AddWithValue("Use_ID", review.UserID);
                    insertNewReview.Parameters.AddWithValue("Acc_ID", review.AccomodationID);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = insertNewReview.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(ex.ToString());
            }
            return Request.CreateResponse(HttpStatusCode.OK, $"NEW REVIEW CREATED SUCCESFULLY: {review.Title}!");
        }


        // ===================================================================================================
        [HttpPut]
        public HttpResponseMessage UpdateReview(Review review)
        {
            if (review == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "REVIEW NOT FOUND!");
            }

            try
            {
                SqlConnection sqlConnection = new SqlConnection(DB_CONNECTION_STRING);
                using (sqlConnection)
                {
                    SqlCommand insertNewReview = new SqlCommand(@"UPDATE Review 
                                                                SET Rev_Date = @Rev_Date,
                                                                Rev_Rate = @Rev_Rate,
                                                                Rev_Recommendation = @Rev_Recommendation,
                                                                Rev_Comment = @Rev_Comment,
                                                                Rev_Usefull = @Rev_Usefull,
                                                                Rev_Title = @Rev_Title,
                                                                Use_ID = @Use_ID,
                                                                Acc_ID = @Acc_ID
                                                                WHERE Rev_ID = @Rev_ID", sqlConnection);
                    insertNewReview.Parameters.AddWithValue("Rev_ID", review.ID);
                    insertNewReview.Parameters.AddWithValue("Rev_Date", review.Date);
                    insertNewReview.Parameters.AddWithValue("Rev_Rate", review.Rate);
                    insertNewReview.Parameters.AddWithValue("Rev_Recommendation", review.Recommendation);
                    insertNewReview.Parameters.AddWithValue("Rev_Comment", review.Comment);
                    insertNewReview.Parameters.AddWithValue("Rev_Usefull", review.Usefull);
                    insertNewReview.Parameters.AddWithValue("Rev_Title", review.Title);
                    insertNewReview.Parameters.AddWithValue("Use_ID", review.UserID);
                    insertNewReview.Parameters.AddWithValue("Acc_ID", review.AccomodationID);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = insertNewReview.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(ex.ToString());
            }
            return Request.CreateResponse(HttpStatusCode.OK, $"REVIEW {review.Title} UPDATED SUCCESFULLY!");
        }

        // ===================================================================================================
        [HttpDelete]
        public HttpResponseMessage RemoveReview(int id)
        {
          if (id < 1)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "INVALID REVIEW ID");
            }

            try
            {
                SqlConnection sqlConnection = new SqlConnection(DB_CONNECTION_STRING);
                using (sqlConnection)
                {
                    SqlCommand insertNewReview = new SqlCommand(@"DELETE Review
                                                                    WEHRE Rev_ID = @Rev_ID", sqlConnection);
                    insertNewReview.Parameters.AddWithValue("Rev_ID", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = insertNewReview.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(ex.ToString());
            }
            return Request.CreateResponse(HttpStatusCode.OK, $"REVIEW DELETED WITH ANY PROBLEM");
        }

    }
}
