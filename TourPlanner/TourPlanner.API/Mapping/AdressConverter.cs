namespace TourPlanner.API.Mapping
{
    public static class AdressConverter
    {
        public static async Task<TourPlanner.Data.Adresses> AdressToEfAsync(Adress adress, Guid TourId)
        {
            var tmp = new TourPlanner.Data.Adresses
            {
                TourIdStart = TourId,
                City = adress.City,
                Country = adress.Country
            };
            if(adress.Street is not null) tmp.Street = adress.Street;
            if(adress.HouseNumber is not null) tmp.HouseNumber = adress.HouseNumber;
            if(adress.Plz is not null) tmp.Plz = adress.Plz;
            return tmp;
        }

        public static async Task<Adress> EfToAdressAsync(TourPlanner.Data.Adresses adress)
        {
            var tmp = new Adress
            {
                City = adress.City,
                Country = adress.Country
            };
            if (adress.Street is not null) tmp.Street = adress.Street;
            if (adress.HouseNumber is not null) tmp.HouseNumber = adress.HouseNumber;
            if (adress.Plz is not null) tmp.Plz = adress.Plz;
            return tmp;
        }
    }
}
