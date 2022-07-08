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

            string content = $"{{ \"name\" : \"{tour.Name}\" , \"description\" : \"{tour.Description}\" , \"type\" : \"{tour.Type}\" , \"start\" : {{ \"street\" : \"{tour.Start.Street}\", \"houseNumber\" : \"{tour.Start.HouseNumber}\", \"plz\" : \"{tour.Start.PostalCode}\" , \"city\" : \"{tour.Start.City}\", \"country\" : \"{tour.Start.Country}\" }}, \"destination\" : {{ \"street\" : \"{tour.Destination.Street}\", \"houseNumber\" : \"{tour.Destination.HouseNumber}\", \"plz\" : \"{tour.Destination.PostalCode}\" , \"city\" : \"{tour.Destination.City}\", \"country\" : \"{tour.Destination.Country}\" }} }}";
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

            string content = $"{{ \"name\" : \"{newtour.Name}\" , \"description\" : \"{newtour.Description}\" , \"type\" : \"{newtour.Type}\" , \"start\" : {{ \"street\" : \"{newtour.Start.Street}\", \"houseNumber\" : \"{newtour.Start.HouseNumber}\", \"plz\" : \"{newtour.Start.PostalCode}\" , \"city\" : \"{newtour.Start.City}\", \"country\" : \"{newtour.Start.Country}\" }}, \"destination\" : {{ \"street\" : \"{newtour.Destination.Street}\", \"houseNumber\" : \"{newtour.Destination.HouseNumber}\", \"plz\" : \"{newtour.Destination.PostalCode}\" , \"city\" : \"{newtour.Destination.City}\", \"country\" : \"{newtour.Destination.Country}\" }} }}";
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
                Address start = new Address((string)obj["start"]["street"], (string)obj["start"]["houseNumber"], (string)obj["start"]["city"], (string)obj["start"]["plz"], (string)obj["start"]["country"]);
                Address destination = new Address((string)obj["destination"]["street"], (string)obj["destination"]["houseNumber"], (string)obj["destination"]["city"], (string)obj["destination"]["plz"], (string)obj["destination"]["country"]);
                Tour temp = new Tour((string)obj["tourId"], (string)obj["name"], (string)obj["description"], (string)obj["duration"], (double)obj["distance"], (string)obj["type"], start, destination);
                result.AddTourToList(temp);
            }

            return result;
        }
    }
}
