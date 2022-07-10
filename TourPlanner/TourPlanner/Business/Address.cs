using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Business
{
    public class Address
    {
        public string Street { get; set; } = String.Empty;
        public string HouseNumber { get; set; } = String.Empty;
        public string City { get; set; } = String.Empty;
        public string PostalCode { get; set; } = String.Empty;
        public string Country { get; set; } = String.Empty;
        public string Weather { get; set; } = String.Empty;
        public string FullAddress { get { return $"{Street} {HouseNumber} {PostalCode} {City} {Country} "; } }
        public Address()
        {

        }


        public Address(string street, string houseNumber, string city, string postalCode, string country)
        {
            Street = street;
            HouseNumber = houseNumber;
            City = city;
            PostalCode = postalCode;
            Country = country;
        }
    }
}
