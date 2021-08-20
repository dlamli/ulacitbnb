using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ulacitbnb.Models;

namespace AppUlacitBnB.Controllers
{
    public class PaymentManager
    {
        string Url = "http://localhost:49220/api/payment/";

        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        public async Task<IEnumerable<Payment>> GetPaymentList(string token)
        {
            HttpClient httpClient = GetClient(token);

            string result = await httpClient.GetStringAsync(Url);

            return JsonConvert.DeserializeObject<IEnumerable<Payment>>(result);
        }

        public async Task<Payment> GetPayment(string token, string code)
        {
            HttpClient httpClient = GetClient(token);

            string result = await httpClient.GetStringAsync(string.Concat(Url, code));

            return JsonConvert.DeserializeObject<Payment>(result);
        }

        public async Task<Payment> EnterPayment(Payment payment, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(Url, new StringContent(JsonConvert.SerializeObject(payment),Encoding.UTF8,"application/json"));

            return JsonConvert.DeserializeObject<Payment>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Payment> UpdatePayment(Payment payment, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PutAsync(Url, new StringContent(JsonConvert.SerializeObject(payment), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Payment>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> DeletePayment(string code, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.DeleteAsync(string.Concat(Url, code));

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}