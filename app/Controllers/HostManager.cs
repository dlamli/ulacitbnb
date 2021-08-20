using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using ulacitbnb.Models;

namespace AppUlacitBnB.Controllers
{
    public class Class1
    {
        string controllerUrl = "http://localhost:49220/api/host";

        HttpClient GetHttpClient(string token)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            return httpClient;
        }

        public async Task<IEnumerable<Host>>

    }
}