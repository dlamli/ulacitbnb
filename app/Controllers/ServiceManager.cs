using AppUlacitBnB.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AppUlacitBnB.Controllers
{
    public class ServiceManager
    {
        string Url = "http://localhost:49220/api/service/";

        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "aplication/json");

            return client;
        }

        public async Task<IEnumerable<Service>> getServiceList(string token)
        {
            HttpClient httpClient = GetClient(token);

            string result = await httpClient.GetStringAsync(Url);

            return JsonConvert.DeserializeObject<IEnumerable<Service>>(result);
        }

        public async Task<Service> getService(string token, string serviceID)
        {
            HttpClient httpClient = GetClient(token);

            string result = await httpClient.GetStringAsync(string.Concat(Url, serviceID));

            return JsonConvert.DeserializeObject<Service>(result);
        }

        public async Task<Service> EnterService(Service service, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(Url,
                new StringContent(JsonConvert.SerializeObject(service), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Service>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Service> UpdateService(Service service, string token)
        {
            try
            {
                HttpClient httpClient = GetClient(token);

                var response = await httpClient.PutAsync(Url,
                    new StringContent(JsonConvert.SerializeObject(service), Encoding.UTF8, "application/json"));

                return JsonConvert.DeserializeObject<Service>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<string> DeleteService(string serviceID, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.DeleteAsync(string.Concat(Url, serviceID));

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}