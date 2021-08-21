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
    public class RoomManager
    {

        string Url = "http://localhost:49220/api/room/";

        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;

        }

        //Get all rooms
        public async Task<IEnumerable<Room>> GetRooms(string token)
        {
            HttpClient httpClient = GetClient(token);

            string result = await httpClient.GetStringAsync(Url);

            return JsonConvert.DeserializeObject<IEnumerable<Room>>(result); ;
        }

        //Get a single room by id
        public async Task<Room> GetRoom(string token, string id)
        {
            HttpClient httpClient = GetClient(token);

            string result = await httpClient.GetStringAsync(string.Concat(Url, id));

            return JsonConvert.DeserializeObject<Room>(result);
        }

        //Enter an room
        public async Task<Room> EnterRoom(Room room, string token)
        {
            HttpClient httpClient = GetClient(token);

            StringContent SerializedRoom = new StringContent(JsonConvert.SerializeObject(room), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(Url, SerializedRoom);

            return JsonConvert.DeserializeObject<Room>(await response.Content.ReadAsStringAsync());
        }

        //Update an room
        public async Task<Room> UpdateRoom(Room room, string token)
        {
            HttpClient httpClient = GetClient(token);

            StringContent SerializedRoom = new StringContent(JsonConvert.SerializeObject(room), Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(Url, SerializedRoom);

            return JsonConvert.DeserializeObject<Room>(await response.Content.ReadAsStringAsync());
        }

        //Delete an room
        public async Task<string> DeleteRoom(string id, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.DeleteAsync(string.Concat(Url, id));

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());

        }

    }
}