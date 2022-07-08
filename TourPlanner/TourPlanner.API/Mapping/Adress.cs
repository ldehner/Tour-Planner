using System.Text;

namespace TourPlanner.API.Mapping
{
    public class Adress
    {
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public string? Plz { get; set; }
        public string City { get; set; } 
        public string Country { get; set; }

        public Adress(string city, string country)
        {
            Country = country;
            City = city;   
        }

        public string GetAdressString()
        {
            var builder = new StringBuilder();
            if (Street is not null)
            {
                builder.Append(Street);
                if(HouseNumber is not null) builder.Append(" "+HouseNumber);
                builder.Append(", ");
            }
            if (Plz is not null) builder.Append(Plz);
            builder.Append(City);
            builder.Append(", ");
            builder.Append(Country);
            return builder.ToString();
        }
    }
}
