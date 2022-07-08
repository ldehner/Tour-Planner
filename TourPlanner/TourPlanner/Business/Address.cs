using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour_planner.Business
{
    public class Address
    {
        public string Street { get; set; } = String.Empty;
        public string HouseNumber { get; set; } = String.Empty;
        public string City { get; set; } = String.Empty;
        public string PostalCode { get; set; } = String.Empty;
        public string Country { get; set; } = String.Empty;
        
        public string FullAddress { get { return $"{Country} {Street} {HouseNumber} {PostalCode} {City}"; } }
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
