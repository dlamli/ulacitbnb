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
    public class ReservationManager
    {
        string Url = "http://localhost:49220/api/reservation/";

        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        public async Task<IEnumerable<Reservation>> GetReservationList(string token)
        {
            HttpClient httpClient = GetClient(token);

            string result = await httpClient.GetStringAsync(Url);

            return JsonConvert.DeserializeObject<IEnumerable<Reservation>>(result);
        }

        public async Task<Reservation> GetReservation(string token, string code)
        {
            HttpClient httpClient = GetClient(token);

            string result = await httpClient.GetStringAsync(string.Concat(Url, code));

            return JsonConvert.DeserializeObject<Reservation>(result);
        }

        public async Task<Reservation> EnterReservation(Reservation reservation, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(Url, new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Reservation>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Reservation> UpdateReservation(Reservation reservation, string token)
        {
            try
            {
                HttpClient httpClient = GetClient(token);

                var response = await httpClient.PutAsync(Url, new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8, "application/json"));

                return JsonConvert.DeserializeObject<Reservation>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public async Task<string> DeleteReservation(string code, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.DeleteAsync(string.Concat(Url, code));

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}
