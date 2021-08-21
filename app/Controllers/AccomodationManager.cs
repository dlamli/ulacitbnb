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
    public class AccomodationManager
    {
        string Url = "http://localhost:49220/api/accomodation/";

        HttpClient GetClient(string token) 
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;

        }

        //Get all accomodations
        public async Task<IEnumerable<Accomodation>> GetAccomodations(string token) 
        {
            HttpClient httpClient = GetClient(token);

            string result = await httpClient.GetStringAsync(Url);
            
            return JsonConvert.DeserializeObject<IEnumerable<Accomodation>>(result); ;
        }

        //Get a single accomodation by id
        public async Task<Accomodation> GetAccomodation(string token, string id)
        {
            HttpClient httpClient = GetClient(token);

            string result = await httpClient.GetStringAsync(string.Concat(Url,id));

            return JsonConvert.DeserializeObject<Accomodation>(result);
        }
        //Enter an accomodation
        public async Task<Accomodation> EnterAccomodation(Accomodation accomodation, string token) 
        {
            HttpClient httpClient = GetClient(token);

            StringContent SerializedAccomodation = new StringContent(JsonConvert.SerializeObject(accomodation), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(Url, SerializedAccomodation);

            return JsonConvert.DeserializeObject<Accomodation>(await response.Content.ReadAsStringAsync());
        }

        //Update an accomodation
        public async Task<Accomodation> UpdateAccomodation(Accomodation accomodation, string token)
        {
            HttpClient httpClient = GetClient(token);

            StringContent SerializedAccomodation = new StringContent(JsonConvert.SerializeObject(accomodation), Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(Url, SerializedAccomodation);

            return JsonConvert.DeserializeObject<Accomodation>(await response.Content.ReadAsStringAsync());
        }

        //Delete an accomodation
        public async Task<string> DeleteAccomodation(string id, string token) 
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.DeleteAsync(string.Concat(Url, id));

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());

        }

    }
}