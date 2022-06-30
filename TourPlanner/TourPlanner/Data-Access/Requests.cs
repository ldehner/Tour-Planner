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

        private const string Url = "https://localhost:7180/api";
        public async Task<Tourlist> GetTours()
        {
            using var client = new HttpClient();

            var response = await client.GetStringAsync(Url);
            Tourlist? tourlist = JsonSerializer.Deserialize<Tourlist>(response.ToString());
            return tourlist;
        }
    }
}
