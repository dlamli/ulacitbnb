using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

    }
}