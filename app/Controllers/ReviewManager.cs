using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ulacit_bnb.Models;

namespace AppUlacitBnB.Controllers
{
    public class ReviewManager
    {
        string controllerUrl = "http://localhost:49220/api/review";

        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<IEnumerable<Review>> GetReviewsList(string token)
        {
            HttpClient httpClient = GetClient(token);
            string result = await httpClient.GetStringAsync(controllerUrl);
            return JsonConvert.DeserializeObject<IEnumerable<Review>>(result);
        }

        public async Task<Review> EnterReview(Review review, string token)
        {
            HttpClient httpClient = GetClient(token);
            StringContent SerializedReview = new StringContent(JsonConvert.SerializeObject(review), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(controllerUrl, SerializedReview);
            return JsonConvert.DeserializeObject<Review>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Review> UpdateReview(Review review, string token)
        {
            HttpClient httpClient = GetClient(token);
            StringContent SerializedReview = new StringContent(JsonConvert.SerializeObject(review), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(controllerUrl, SerializedReview);
            return JsonConvert.DeserializeObject<Review>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> DeleteRoom(string id, string token)
        {
            HttpClient httpClient = GetClient(token);
            var response = await httpClient.DeleteAsync($"{controllerUrl}/{id}");
            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());

        }
    }
}