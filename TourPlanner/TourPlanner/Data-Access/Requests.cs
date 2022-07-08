using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tour_planner.Business;

namespace Tour_planner.Data_Access
{
    public class Requests : IQuery
    {

        private const string Url = "https://localhost:7180/api/TourPlanner";
        public async Task<Tourlist> GetTours()
        {
            using var client = new HttpClient();

            var response = await client.GetStringAsync(Url+"/GetTours");
            JArray data = JArray.Parse(response);
            Tourlist? tourlist = fillTourlist(data);
            return tourlist;
        }

        public async Task<Tour> GetTourById(string id)
        {
            using var client = new HttpClient();

            var response = await client.GetStringAsync(Url + "/GetTours/" + id);
            Tour? tourlist = JsonSerializer.Deserialize<Tour>(response.ToString());
            return tourlist;
        }

        public async Task PostTour(Tour tour)
        {
            using var client = new HttpClient();

            string content = $"{{ \"name\" : \"{tour.Name}\" , \"description\" : \"{tour.Description}\" , \"type\" : \"{tour.Type}\" , \"start\" : \"{tour.Start}\" , \"destination\" : \"{tour.Destination}\" }}";
            var data = new StringContent(content, Encoding.UTF8, "application/json");

            
            string test = await data.ReadAsStringAsync();

            var response = await client.PostAsync(Url + "/AddTour", data);

            var result = response.Content.ReadAsStreamAsync();

            Console.WriteLine(result);
        }

        public void DeleteTour(string Id)
        {
            using var client = new HttpClient();

            var response = client.DeleteAsync(Url + "/DeleteTour/" + Id);

            var result = response.Result;
            

        }

        public async Task UpdateTour(Tour newtour, string oldId)
        {
            using var client = new HttpClient();

            string content = $"{{ \"name\" : \"{newtour.Name}\" , \"description\" : \"{newtour.Description}\" , \"type\" : \"{newtour.Type}\" , \"start\" : \"{newtour.Start}\" , \"destination\" : \"{newtour.Destination}\" }}";
            var data = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(Url, data);

            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);

        }

        public Task<Tourlist> GetToursBySearchTerm(string searchTerm)
        {
            throw new NotImplementedException();
        }



        private Tourlist fillTourlist(JArray data)
        {
            var result = new Tourlist();

            foreach(JObject obj in data)
            {
                Tour temp = new Tour((string)obj["tourId"], (string)obj["name"], (string)obj["description"], (string)obj["duration"], (double)obj["distance"], (string)obj["type"], (string)obj["start"], (string)obj["destination"]);
                result.AddTourToList(temp);
            }

            return result;
        }
    }
}
