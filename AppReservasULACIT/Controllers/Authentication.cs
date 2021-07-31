using AppReservasULACIT.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AppReservasULACIT.Controllers
{
    public class Authentication
    {
        string UrlAuthenticate = "http://localhost:49220/api/customer/auth";
        string UrlRegister = "http://localhost:49220/api/customer/";

        public async Task<Customer> Validar(LoginRequest loginRequest)
        {
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync(UrlAuthenticate,
                new StringContent(JsonConvert.SerializeObject(loginRequest),
                Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Customer>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Customer> Registrar(Customer usuario)
        {
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync(UrlRegister,
                new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8,
                "application/json"));

            return JsonConvert.DeserializeObject<Customer>(await response.Content.ReadAsStringAsync());
        }

    }
}