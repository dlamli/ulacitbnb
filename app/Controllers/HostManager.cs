using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using ulacitbnb.Models;

namespace AppUlacitBnB.Controllers
{
    public class HostManager
    {
        string controllerUrl = "http://localhost:49220/api/host";

        HttpClient GetHttpClient(string token)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            return httpClient;
        }

        public async Task<IEnumerable<Host>> GetHostsList(string token)
        {
            HttpClient httpClient = GetHttpClient(token);
            string result = await httpClient.GetStringAsync(controllerUrl);
            return JsonConvert.DeserializeObject<IEnumerable<Host>>(result);
        }

        public async Task<Host> GetHost(string token, string id)
        {
            HttpClient httpClient = GetHttpClient(token);
            string result = await httpClient.GetStringAsync($"{controllerUrl}/{id}");
            return JsonConvert.DeserializeObject<Host>(result);
        }

    }
}