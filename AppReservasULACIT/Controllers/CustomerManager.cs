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
    public class CustomerManager
    {
        string Url = "http://localhost:49220/api/customer/";

        //INICIALIZAR EL HTTPCLIENT (REQUEST)
        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "aplication/json");

            return client;
        }

        public async Task<IEnumerable<Customer>> getCustomerList(string token)
        {
            HttpClient httpClient = GetClient(token);

            string resultado = await httpClient.GetStringAsync(Url);

            return JsonConvert.DeserializeObject<IEnumerable<Customer>>(resultado);
        }

        public async Task<Customer> getCustomer(string token, string codigo)
        {
            HttpClient httpClient = GetClient(token);

            string resultado = await httpClient.GetStringAsync(string.Concat(Url, codigo));

            return JsonConvert.DeserializeObject<Customer>(resultado);
        }

        public async Task<Customer> EnterCustomer(Customer customer, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(Url,
                new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Customer>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Customer> UpdateCustomer(Customer customer, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PutAsync(Url,
                new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Customer>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> DeleteCustomer(string codigo, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.DeleteAsync(string.Concat(Url, codigo));

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}