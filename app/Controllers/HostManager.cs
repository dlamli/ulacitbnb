using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ulacitbnb.Models;

namespace AppUlacitBnB.Controllers
{
    public class HostManager
    {
        string controllerUrl = "http://localhost:49220/api/host";


        public async Task<Host> Validate(LoginRequest loginRequest)
        {
            HttpClient httpClient = new HttpClient();
            StringContent requestBody = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{controllerUrl}/auth", requestBody);
            return JsonConvert.DeserializeObject<Host>(await response.Content.ReadAsStringAsync());
        }

        public async Task<IEnumerable<Host>> GetHostsList(string token)
        {
            HttpClient httpClient = new HttpClient();
            string result = await httpClient.GetStringAsync(controllerUrl);
            return JsonConvert.DeserializeObject<IEnumerable<Host>>(result);
        }


        public async Task<Host> GetHost(string token, string id)
        {
            HttpClient httpClient = new HttpClient();
            string result = await httpClient.GetStringAsync($"{controllerUrl}/{id}");
            return JsonConvert.DeserializeObject<Host>(result);
        }

    }
}