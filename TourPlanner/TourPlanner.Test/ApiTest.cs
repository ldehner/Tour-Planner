using System.Text;
using TourPlanner.API.Data;
using TourPlanner.API.Mapping;
using TourPlanner.Data;

namespace TourPlanner.Test
{
    [TestFixture]
    class ApiTest
    {
        [TestCase("10236bb6-4302-4b88-936c-1f0b0104be2a", "Test1", "Vienna", "Austria", "Berlin", "Germany", "dfgdfgdfg", "fastest", 34.3, 0, 4, 4)]
        [TestCase("20236bb6-4302-4b88-936c-1f0b0104be2a", "Test2", "Berlin", "Germany", "Vienna", "Austria", "hgfjzhj", "Bycecle", 100.5, 0, 8, 12)]
        [TestCase("30236bb6-4302-4b88-936c-1f0b0104be2a", "Test3", "Wolkersdorf", "Austria", "Korneuburg", "Austria", "ewtrret", "fastest", 33.1, 0, 2, 54)]
        [TestCase("40236bb6-4302-4b88-936c-1f0b0104be2a", "Test4", "Korneuburg", "Austria", "Wolkersdorf", "Austria", "hjztzu", "fastest", 534.5, 1, 4, 41)]
        [TestCase("50236bb6-4302-4b88-936c-1f0b0104be2a", "Test5", "Paris", "France", "Barcelona", "Spain", "gfdgfgdfg", "Bycecle", 2341.1, 3, 21, 43)]
        public async Task CheckIfToursIsConvertedSuccessfullyToPresentationTour(string guid, string name, string fromcity, string fromcountry, string tocity, string tocountry, string description, string type, double distance, int days, int hours, int minutes)
        {
            // arrange
            var from = new Adresses { City = fromcity, Country = fromcountry };
            var to = new Adresses { City = tocity, Country = tocountry };
            var tour = new Tours
            {
                TourId = Guid.Parse(guid),
                Name = name,
                Start = from,
                Destination = to,
                Description = description,
                Distance = distance,
                Duration = new TimeSpan(days, hours, minutes, 0),
                Type = type,
                Logs = new List<Logs>(),
            };

            // act
            var pTour = await TourConverter.ToursToPresentationTour(tour);
            

            // assert
            Assert.AreEqual(tour.TourId, pTour.TourId);
            Assert.AreEqual(tour.Name, pTour.Name);
            Assert.AreEqual(tour.Description, pTour.Description);
            Assert.AreEqual(tour.Distance, pTour.Distance);
            Assert.AreEqual(tour.Duration, pTour.Duration);
            Assert.AreEqual(tour.Type, pTour.Type);
        }

        [TestCase("10236bb6-4302-4b88-936c-1f0b0104be2a", "Test1", "Vienna", "Austria", "Berlin", "Germany", "dfgdfgdfg", "fastest", 34.3, 0, 4, 4)]
        [TestCase("20236bb6-4302-4b88-936c-1f0b0104be2a", "Test2", "Berlin", "Germany", "Vienna", "Austria", "hgfjzhj", "Bycecle", 100.5, 0, 8, 12)]
        [TestCase("30236bb6-4302-4b88-936c-1f0b0104be2a", "Test3", "Wolkersdorf", "Austria", "Korneuburg", "Austria", "ewtrret", "fastest", 33.1, 0, 2, 54)]
        [TestCase("40236bb6-4302-4b88-936c-1f0b0104be2a", "Test4", "Korneuburg", "Austria", "Wolkersdorf", "Austria", "hjztzu", "fastest", 534.5, 1, 4, 41)]
        [TestCase("50236bb6-4302-4b88-936c-1f0b0104be2a", "Test5", "Paris", "France", "Barcelona", "Spain", "gfdgfgdfg", "Bycecle", 2341.1, 3, 21, 43)]
        public async Task CheckIfPresentationTourIsConvertedSuccessfullyToTours(string guid, string name, string fromcity, string fromcountry, string tocity, string tocountry, string description, string type, double distance, int days, int hours, int minutes)
        {
            // arrange
            var from = new Adress(fromcity, fromcountry);
            var to = new Adress(tocity, tocountry);
            var pTour = new PresentationTour
            {
                TourId = Guid.Parse(guid),
                Name = name,
                Start = from,
                Destination = to,
                Description = description,
                Distance = distance,
                Duration = new TimeSpan(days, hours, minutes, 0),
                Type = type,
                Logs = new List<PresentationLog>(),
            };

            // act
            var tour = await TourConverter.PresentationTourToTours(pTour);


            // assert
            Assert.AreEqual(tour.TourId, pTour.TourId);
            Assert.AreEqual(tour.Name, pTour.Name);
            Assert.AreEqual(tour.Description, pTour.Description);
            Assert.AreEqual(tour.Distance, pTour.Distance);
            Assert.AreEqual(tour.Duration, pTour.Duration);
            Assert.AreEqual(tour.Type, pTour.Type);
        }

        [TestCase("10236bb6-4302-4b88-936c-1f0b0104be2a", "Test1", "Vienna", "Austria", "Berlin", "Germany", "dfgdfgdfg", "fastest", 34.3, 0, 4, 4)]
        [TestCase("20236bb6-4302-4b88-936c-1f0b0104be2a", "Test2", "Berlin", "Germany", "Vienna", "Austria", "hgfjzhj", "Bycecle", 100.5, 0, 8, 12)]
        [TestCase("30236bb6-4302-4b88-936c-1f0b0104be2a", "Test3", "Wolkersdorf", "Austria", "Korneuburg", "Austria", "ewtrret", "fastest", 33.1, 0, 2, 54)]
        [TestCase("40236bb6-4302-4b88-936c-1f0b0104be2a", "Test4", "Korneuburg", "Austria", "Wolkersdorf", "Austria", "hjztzu", "fastest", 534.5, 1, 4, 41)]
        [TestCase("50236bb6-4302-4b88-936c-1f0b0104be2a", "Test5", "Paris", "France", "Barcelona", "Spain", "gfdgfgdfg", "Bycecle", 2341.1, 3, 21, 43)]
        public async Task CheckIfToursIsConvertedSuccessfullyToSimpleTour(string guid, string name, string fromcity, string fromcountry, string tocity, string tocountry, string description, string type, double distance, int days, int hours, int minutes)
        {
            // arrange
            var from = new Adresses { City = fromcity, Country = fromcountry };
            var to = new Adresses { City = tocity, Country = tocountry };
            var tour = new Tours
            {
                TourId = Guid.Parse(guid),
                Name = name,
                Start = from,
                Destination = to,
                Description = description,
                Distance = distance,
                Duration = new TimeSpan(days, hours, minutes, 0),
                Type = type,
                Logs = new List<Logs>(),
            };

            // act
            var sTour = await TourConverter.ToursToSimpleTour(tour);


            // assert
            Assert.AreEqual(tour.Name, sTour.Name);
            Assert.AreEqual(tour.Description, sTour.Description);
            Assert.AreEqual(tour.Type, sTour.Type);
        }

        [TestCase("10236bb6-4302-4b88-936c-1f0b0104be2a", "Test1", "Vienna", "Austria", "Berlin", "Germany", "dfgdfgdfg", "fastest", 34.3, 0, 4, 4)]
        [TestCase("20236bb6-4302-4b88-936c-1f0b0104be2a", "Test2", "Berlin", "Germany", "Vienna", "Austria", "hgfjzhj", "Bycecle", 100.5, 0, 8, 12)]
        [TestCase("30236bb6-4302-4b88-936c-1f0b0104be2a", "Test3", "Wolkersdorf", "Austria", "Korneuburg", "Austria", "ewtrret", "fastest", 33.1, 0, 2, 54)]
        [TestCase("40236bb6-4302-4b88-936c-1f0b0104be2a", "Test4", "Korneuburg", "Austria", "Wolkersdorf", "Austria", "hjztzu", "fastest", 534.5, 1, 4, 41)]
        [TestCase("50236bb6-4302-4b88-936c-1f0b0104be2a", "Test5", "Paris", "France", "Barcelona", "Spain", "gfdgfgdfg", "Bycecle", 2341.1, 3, 21, 43)]
        public async Task CheckIfPresentationTourIsConvertedSuccessfullyToSimpleTour(string guid, string name, string fromcity, string fromcountry, string tocity, string tocountry, string description, string type, double distance, int days, int hours, int minutes)
        {
            // arrange
            var from = new Adress(fromcity, fromcountry);
            var to = new Adress(tocity, tocountry);
            var pTour = new PresentationTour
            {
                TourId = Guid.Parse(guid),
                Name = name,
                Start = from,
                Destination = to,
                Description = description,
                Distance = distance,
                Duration = new TimeSpan(days, hours, minutes, 0),
                Type = type,
                Logs = new List<PresentationLog>(),
            };

            // act
            var sTour = await TourConverter.PresentationTourToSimpleTour(pTour);


            // assert
            Assert.AreEqual(pTour.Name, sTour.Name);
            Assert.AreEqual(pTour.Description, sTour.Description);
            Assert.AreEqual(pTour.Type, sTour.Type);
        }


        [TestCase("50236bb6-4302-4b88-936c-1f0b0104be2a", "10236bb6-4302-4b88-936c-1f0b0104be2a", "Test1", 4, 1, 0, 4, 4, 2022, 12, 8)]
        [TestCase("30236bb6-4302-4b88-936c-1f0b0104be2a", "20236bb6-4302-4b88-936c-1f0b0104be2a", "Test2", 1, 2, 0, 8, 12, 2021, 11, 6)]
        [TestCase("10236bb6-4302-4b88-936c-1f0b0104be2a", "30236bb6-4302-4b88-936c-1f0b0104be2a", "Test3", 3, 3, 0, 2, 54, 2020, 3, 12)]
        [TestCase("20236bb6-4302-4b88-936c-1f0b0104be2a", "40236bb6-4302-4b88-936c-1f0b0104be2a", "Test4", 5, 4, 1, 4, 41, 2019, 5, 3)]
        [TestCase("40236bb6-4302-4b88-936c-1f0b0104be2a", "50236bb6-4302-4b88-936c-1f0b0104be2a", "Test5", 0, 5, 3, 21, 43, 2002, 8, 4)]
        public async Task CheckIfLogsIsConvertedSuccessfullyToPresentationLog(string logId, string tourId, string comment, Int16 difficulty, Int16 rating, int days, int hours, int minutes, int year, int month, int day)
        {
            // arrange
            var logs = new Logs
            {
                LogId = Guid.Parse(logId),
                TourId = Guid.Parse(tourId),
                Comment = comment,
                Difficulty = difficulty,
                Rating = rating,
                Duration = new TimeSpan(days, hours, minutes, 0),
                Date = new DateTime(year, month, day)
        };

            // act
            var pLog = await LogConverter.LogsToPresentationLog(logs);


            // assert
            Assert.That(pLog.LogId, Is.EqualTo(logs.LogId));
            Assert.That(pLog.TourId, Is.EqualTo(logs.TourId));
            Assert.That(pLog.Comment, Is.EqualTo(logs.Comment));
            Assert.That(pLog.Difficulty, Is.EqualTo(logs.Difficulty));
            Assert.That(pLog.Rating, Is.EqualTo(logs.Rating));
            Assert.That(pLog.Duration, Is.EqualTo(logs.Duration));
            Assert.That(pLog.Date, Is.EqualTo(logs.Date));
        }

        [TestCase("50236bb6-4302-4b88-936c-1f0b0104be2a", "10236bb6-4302-4b88-936c-1f0b0104be2a", "Test1", 4, 1, 0, 4, 4, 2022, 12, 8)]
        [TestCase("30236bb6-4302-4b88-936c-1f0b0104be2a", "20236bb6-4302-4b88-936c-1f0b0104be2a", "Test2", 1, 2, 0, 8, 12, 2021, 11, 6)]
        [TestCase("10236bb6-4302-4b88-936c-1f0b0104be2a", "30236bb6-4302-4b88-936c-1f0b0104be2a", "Test3", 3, 3, 0, 2, 54, 2020, 3, 12)]
        [TestCase("20236bb6-4302-4b88-936c-1f0b0104be2a", "40236bb6-4302-4b88-936c-1f0b0104be2a", "Test4", 5, 4, 1, 4, 41, 2019, 5, 3)]
        [TestCase("40236bb6-4302-4b88-936c-1f0b0104be2a", "50236bb6-4302-4b88-936c-1f0b0104be2a", "Test5", 0, 5, 3, 21, 43, 2002, 8, 4)]
        public async Task CheckIfPresentationLogIsConvertedSuccessfullyToLogs(string logId, string tourId, string comment, Int16 difficulty, Int16 rating, int days, int hours, int minutes, int year, int month, int day)
        {
            // arrange
            var pLog = new PresentationLog
            {
                LogId = Guid.Parse(logId),
                TourId = Guid.Parse(tourId),
                Comment = comment,
                Difficulty = difficulty,
                Rating = rating,
                Duration = new TimeSpan(days, hours, minutes, 0),
                Date = new DateTime(year, month, day)
            };

            // act
            var logs = await LogConverter.PresentationLogToLogs(pLog);


            // assert
            Assert.That(pLog.LogId, Is.EqualTo(logs.LogId));
            Assert.That(pLog.TourId, Is.EqualTo(logs.TourId));
            Assert.That(pLog.Comment, Is.EqualTo(logs.Comment));
            Assert.That(pLog.Difficulty, Is.EqualTo(logs.Difficulty));
            Assert.That(pLog.Rating, Is.EqualTo(logs.Rating));
            Assert.That(pLog.Duration, Is.EqualTo(logs.Duration));
            Assert.That(pLog.Date, Is.EqualTo(logs.Date));
        }

        [TestCase("50236bb6-4302-4b88-936c-1f0b0104be2a", "10236bb6-4302-4b88-936c-1f0b0104be2a", "Test1", 4, 1, 0, 4, 4, 2022, 12, 8)]
        [TestCase("30236bb6-4302-4b88-936c-1f0b0104be2a", "20236bb6-4302-4b88-936c-1f0b0104be2a", "Test2", 1, 2, 0, 8, 12, 2021, 11, 6)]
        [TestCase("10236bb6-4302-4b88-936c-1f0b0104be2a", "30236bb6-4302-4b88-936c-1f0b0104be2a", "Test3", 3, 3, 0, 2, 54, 2020, 3, 12)]
        [TestCase("20236bb6-4302-4b88-936c-1f0b0104be2a", "40236bb6-4302-4b88-936c-1f0b0104be2a", "Test4", 5, 4, 1, 4, 41, 2019, 5, 3)]
        [TestCase("40236bb6-4302-4b88-936c-1f0b0104be2a", "50236bb6-4302-4b88-936c-1f0b0104be2a", "Test5", 0, 5, 3, 21, 43, 2002, 8, 4)]
        public async Task CheckIfSimpleLogIsConvertedSuccessfullyToLogs(string logId, string tourId, string comment, Int16 difficulty, Int16 rating, int days, int hours, int minutes, int year, int month, int day)
        {
            // arrange
            var sLog = new SimpleLog(year.ToString() + "-" + month.ToString() + "-" + day.ToString(), days.ToString() + ":" + hours.ToString() + ":" + minutes.ToString(), comment, difficulty, rating);

            // act
            var logs = await LogConverter.SimpleLogToLogs(sLog, Guid.Parse(tourId));


            // assert
            Assert.That(Guid.Parse(tourId), Is.EqualTo(logs.TourId));
            Assert.That(sLog.Comment, Is.EqualTo(logs.Comment));
            Assert.That(sLog.Difficulty, Is.EqualTo(logs.Difficulty));
            Assert.That(sLog.Rating, Is.EqualTo(logs.Rating));
            Assert.That(sLog.Duration, Is.EqualTo(logs.Duration.Days.ToString() + ":" + logs.Duration.Hours.ToString() + ":" + logs.Duration.Minutes.ToString()));
            Assert.That(sLog.Date, Is.EqualTo(logs.Date.Year.ToString() + "-" + logs.Date.Month.ToString() + "-" + logs.Date.Day.ToString()));
        }

        [TestCase("4", "Praunstraße", "2103", "Langenzersdorf", "Austria", "50236bb6-4302-4b88-936c-1f0b0104be2a")]
        [TestCase("77", "Via Albona", "00177", "Rome", "Italy", "30236bb6-4302-4b88-936c-1f0b0104be2a")]
        [TestCase("30", "Seilerstätte", "1010", "Vienna", "Austria", "10236bb6-4302-4b88-936c-1f0b0104be2a")]
        [TestCase("35", "Dudenstraße ", "10965", "Berlin", "Germany", "20236bb6-4302-4b88-936c-1f0b0104be2a")]
        [TestCase("8", "Willemsparkweg", "1071", "Amsterdam", "Netherlands", "40236bb6-4302-4b88-936c-1f0b0104be2a")]
        public async Task CheckIfAdressIsConvertedSuccessfullyToAdresses(string number, string street, string plz, string city, string country, string tourId)
        {
            // arrange
            var adress = new Adress(city, country);
            adress.HouseNumber = number;
            adress.Street = street;
            adress.Plz = plz;

            // act
            var adresses = await AdressConverter.AdressToEfAsync(adress, Guid.Parse(tourId));


            // assert
            Assert.That(adress.HouseNumber, Is.EqualTo(adresses.HouseNumber));
            Assert.That(adress.Street, Is.EqualTo(adresses.Street));
            Assert.That(adress.Plz, Is.EqualTo(adresses.Plz));
            Assert.That(adress.City, Is.EqualTo(adresses.City));
            Assert.That(adress.Country, Is.EqualTo(adresses.Country));
            Assert.That(Guid.Parse(tourId), Is.EqualTo(adresses.TourIdStart));
        }

        [TestCase("4", "Praunstraße", "2103", "Langenzersdorf", "Austria", "50236bb6-4302-4b88-936c-1f0b0104be2a")]
        [TestCase("77", "Via Albona", "00177", "Rome", "Italy", "30236bb6-4302-4b88-936c-1f0b0104be2a")]
        [TestCase("30", "Seilerstätte", "1010", "Vienna", "Austria", "10236bb6-4302-4b88-936c-1f0b0104be2a")]
        [TestCase("35", "Dudenstraße ", "10965", "Berlin", "Germany", "20236bb6-4302-4b88-936c-1f0b0104be2a")]
        [TestCase("8", "Willemsparkweg", "1071", "Amsterdam", "Netherlands", "40236bb6-4302-4b88-936c-1f0b0104be2a")]
        public async Task CheckIfAdressesIsConvertedSuccessfullyToAdress(string number, string street, string plz, string city, string country, string tourId)
        {
            // arrange
            var adresses = new Adresses
            {
                TourIdStart = Guid.Parse(tourId),
                HouseNumber = number,
                Street = street,
                Plz = plz,
                City = city,
                Country = country
            };

            // act
            var adress = await AdressConverter.EfToAdressAsync(adresses);


            // assert
            Assert.That(adress.HouseNumber, Is.EqualTo(adresses.HouseNumber));
            Assert.That(adress.Street, Is.EqualTo(adresses.Street));
            Assert.That(adress.Plz, Is.EqualTo(adresses.Plz));
            Assert.That(adress.City, Is.EqualTo(adresses.City));
            Assert.That(adress.Country, Is.EqualTo(adresses.Country));
            Assert.That(Guid.Parse(tourId), Is.EqualTo(adresses.TourIdStart));
        }

        [TestCase("4", "Praunstraße", "2103", "Langenzersdorf", "Austria", "50236bb6-4302-4b88-936c-1f0b0104be2a")]
        [TestCase("77", "Via Albona", "00177", "Rome", "Italy", "30236bb6-4302-4b88-936c-1f0b0104be2a")]
        [TestCase("30", "Seilerstätte", "1010", "Vienna", "Austria", "10236bb6-4302-4b88-936c-1f0b0104be2a")]
        [TestCase("35", "Dudenstraße ", "10965", "Berlin", "Germany", "20236bb6-4302-4b88-936c-1f0b0104be2a")]
        [TestCase("8", "Willemsparkweg", "1071", "Amsterdam", "Netherlands", "40236bb6-4302-4b88-936c-1f0b0104be2a")]
        public async Task CheckIfAdressIsConvertedSuccessfullyToString(string number, string street, string plz, string city, string country, string tourId)
        {
            // arrange
            var adress = new Adress(city, country);
            adress.HouseNumber = number;
            adress.Street = street;
            adress.Plz = plz;
            var builder = new StringBuilder();

            // act
            if (street is not null)
            {
                builder.Append(street);
                if (number is not null) builder.Append(" " + number);
                builder.Append(", ");
            }
            if (plz is not null) builder.Append(plz);
            builder.Append(" ");
            builder.Append(city);
            builder.Append(", ");
            builder.Append(country);


            // assert
            Assert.That(adress.GetAdressString(), Is.EqualTo(builder.ToString()));
        }

    }
}
