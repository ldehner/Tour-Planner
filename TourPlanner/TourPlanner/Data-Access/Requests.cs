using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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

            var response = await client.PostAsync(Url + "/AddTour", data);

            var result = response.Content.ReadAsStreamAsync();
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
            var response = await client.PutAsync(Url +"/UpdateTour/"+oldId, data);

            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);

        }

        public async Task<Tourlist> GetToursBySearchTerm(string searchTerm)
        {
            using var client = new HttpClient();

            var response = await client.GetStringAsync(Url + "/search/"+searchTerm);
            JArray data = JArray.Parse(response);
            Tourlist? tourlist = fillTourlist(data);
            return tourlist;
        }




        public async Task PostLog(TourLog tourLog, string TourId)
        {
            using var client = new HttpClient();

            string content = $"{{ \"date\": \"{tourLog.Date}\",\"duration\": \"{tourLog.Time}\", \"comment\": \"{tourLog.Comment}\", \"difficulty\": {tourLog.Difficulty}, \"rating\": {tourLog.Rating} }}";
            var data = new StringContent(content, Encoding.UTF8, "application/json");


            string test = await data.ReadAsStringAsync();

            var response = await client.PostAsync(Url + "/AddLog/"+TourId, data);

            var result = response.Content.ReadAsStreamAsync();

            Console.WriteLine(result);

        }

        public void DeleteLog(string TourId, string LogId)
        {
            using var client = new HttpClient();

            var response = client.DeleteAsync(Url + "/DeleteLog/" + TourId + "/" + LogId);

            var result = response.Result;
        }

        public async Task UpdateLog(TourLog log, string TourId)
        {
            using var client = new HttpClient();

            string content = $"{{ \"date\" : \"{log.Date}\" , \"duration\" : \"{log.Time}\" , \"comment\" : \"{log.Comment}\" , \"difficulty\" : {log.Difficulty}, \"rating\" : {log.Rating} }}";
            var data = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(Url+"/UpdateLog/"+TourId+"/"+log.LogId, data);

            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }

        public async Task<string> GetReport()
        {
            using var client = new HttpClient();

            var response = await client.GetAsync(Url + "/TourOverviewReport");

            var result = response.Content.ReadAsStringAsync();


            return await result;
            
        }


        public async Task<string> GetImageBytes(string tourId)
        {
            using var client = new HttpClient();

            var response = await client.GetAsync(Url + "/GetMap/" + tourId);

            return await response.Content.ReadAsStringAsync();
        }

        private Tourlist fillTourlist(JArray data)
        {
            var result = new Tourlist();

            foreach(JObject obj in data)
            {
                Address start = new Address((string)obj["start"]["street"], (string)obj["start"]["houseNumber"], (string)obj["start"]["city"], (string)obj["start"]["plz"], (string)obj["start"]["country"]);
                Address destination = new Address((string)obj["destination"]["street"], (string)obj["destination"]["houseNumber"], (string)obj["destination"]["city"], (string)obj["destination"]["plz"], (string)obj["destination"]["country"]);
                Dictionary<string, TourLog> loglist = new();
                
                foreach (JObject log in obj["logs"])
                {
                    string time = $"{log["duration"]}";
                    TourLog templog = new TourLog((string)log["logId"],(string)log["tourid"], (string)log["date"], (string)log["comment"], (int)log["difficulty"], time, (int)log["rating"]);
                    loglist.Add((string)log["logId"], templog);
                }

                Tour temp = new Tour((string)obj["tourId"], (string)obj["name"], (string)obj["description"], (string)obj["duration"], (double)obj["distance"], (string)obj["type"], start, destination);
                temp.LogList = loglist; 
                result.AddTourToList(temp);
            }

            return result;
        }

        
    }
}
