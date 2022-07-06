using HandlebarsDotNet;
using iText.Html2pdf;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using Newtonsoft.Json;
using TourPlanner.API.DAL;
using TourPlanner.API.Data;
using TourPlanner.API.Exceptions;
using TourPlanner.API.Mapping;

namespace TourPlanner.API.BL
{
    public class TourManager : ITourManager
    {
        private readonly ITourRepository _tourRepository;
        public TourManager(ITourRepository tourRepository)
        {
            _tourRepository = tourRepository;
        }

        public async Task<PresentationTour> AddTourAsync(SimpleTour tour, double distance, TimeSpan duration)
        {
            return await _tourRepository.AddTourAsync(tour, distance, duration);
        }

        public async Task<List<PresentationTour>> DeleteTourAsync(Guid tourId)
        {
            return await _tourRepository.DeleteTourAsync(tourId);

        }

        public async Task<string> ExportTourAsync(Guid tourId)
        {
            var tour = await GetTourAsync(tourId);
            return JsonConvert.SerializeObject(tour);
        }

        public async Task<byte[]> GenerateTourReportAsync(Guid tourId)
        {
            string templateHtml = File.ReadAllText(System.IO.Directory.GetCurrentDirectory()+"/TourReport.html");
            var template = Handlebars.Compile(templateHtml);
            var tour = await GetTourAsync(tourId);
            var tourLogs = new List<PdfLog>();
            tour.Logs.ForEach(log => tourLogs.Add(new PdfLog
            {
                Date = log.Date.ToShortDateString(),
                Duration = log.Duration.ToString(@"hh\:mm\:ss"),
                Comment = log.Comment,
                Rating = log.Rating,
                Difficulty = log.Difficulty
            }));
            string url = System.IO.Directory.GetCurrentDirectory();
            var data = new
            {
                baseUrl = url,
                name = tour.Name,
                tourId = tour.TourId,
                transport = tour.Type,
                from = tour.Start,
                distance = Math.Round(tour.Distance, 1),
                duration = tour.Duration.ToString(@"hh\:mm\:ss"),
                to = tour.Destination,
                description = tour.Description,
                logs = tourLogs
            };

            string tourHtml = template(data);

            ConverterProperties converterProperties = new ConverterProperties();

            //string file = System.IO.Directory.GetCurrentDirectory()+"/" + tour.TourId.ToString() + ".pdf";
            var stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            pdf.SetDefaultPageSize(PageSize.A4);
            var document = HtmlConverter.ConvertToDocument(tourHtml, pdf, converterProperties);
            document.Close();
            return stream.ToArray();
        }

        public async Task<byte[]> GenerateTourOverviewAsync()
        {
           
            string templateHtml = File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "/AllToursReport.html");
            var template = Handlebars.Compile(templateHtml);
            var tours = await GetToursAsync();
            var pdfTours = new List<PdfTourOverview>();
            tours.ForEach(tour => {
                var times = new List<TimeSpan>();
                var rating = 0.0;
                var difficulty = 0.0;
                tour.Logs.ForEach(log =>
                {
                    times.Add(log.Duration);
                    rating += log.Rating;
                    difficulty += log.Difficulty;
                });
                if(tour.Logs.Count > 0)
                {
                    var avgtime = new TimeSpan(Convert.ToInt64(times.Average(t => t.Ticks)));
                    rating = Math.Round(rating / tour.Logs.Count(), 1);
                    difficulty = Math.Round(difficulty / tour.Logs.Count(), 1);
                    pdfTours.Add(new PdfTourOverview
                    {
                        name = tour.Name,
                        tourId = tour.TourId.ToString(),
                        entrys = tour.Logs.Count().ToString(),
                        distance = Math.Round(tour.Distance, 1).ToString(),
                        duration = tour.Duration.ToString(@"hh\:mm\:ss"),
                        transport = tour.Type,
                        description = tour.Description,
                        from = tour.Start,
                        to = tour.Destination,
                        difficulty = difficulty.ToString(),
                        rating = rating.ToString(),
                        avgduration = avgtime.ToString(@"hh\:mm\:ss"),
                        baseUrl = System.IO.Directory.GetCurrentDirectory()
                    });
                }
                else
                {
                    pdfTours.Add(new PdfTourOverview
                    {
                        name = tour.Name,
                        entrys = tour.Logs.Count().ToString(),
                        tourId = tour.TourId.ToString(),
                        distance = Math.Round(tour.Distance, 1).ToString(),
                        duration = tour.Duration.ToString(@"hh\:mm\:ss"),
                        transport = tour.Type,
                        description = tour.Description,
                        from = tour.Start,
                        to = tour.Destination,
                        difficulty = "-",
                        rating = "-",
                        avgduration = "-",
                        baseUrl = System.IO.Directory.GetCurrentDirectory()
                    }); 
                }
            });
            var data = new
            {
                tours = pdfTours
            };

            string tourHtml = template(data);

            ConverterProperties converterProperties = new ConverterProperties();

            //string file = System.IO.Directory.GetCurrentDirectory() + "/AllTours.pdf";
            var stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            pdf.SetDefaultPageSize(PageSize.A4);
            var document = HtmlConverter.ConvertToDocument(tourHtml, pdf, converterProperties);
            document.Close();
            return stream.ToArray();
        }

        public async Task<PresentationTour> GetTourAsync(Guid tourId)
        {
           return await _tourRepository.GetTourAsync(tourId);
        }

        public async Task<List<PresentationTour>> GetToursAsync()
        {
            return await _tourRepository.GetToursAsync();
        }

        public async Task<PresentationTour> ImportTourAsync(PresentationTour tour)
        {
            return await _tourRepository.ImportTourAsync(tour);
        }

        public async Task<List<PresentationTour>> SearchAsync(string searchTerm)
        {
            return await _tourRepository.SearchAsync(searchTerm);
        }

        public async Task<PresentationTour> UpdateTourAsync(Guid tourId, SimpleTour requestTour, double distance, TimeSpan duration)
        {
            return await _tourRepository.UpdateTourAsync(tourId, requestTour, distance, duration); ;
        }
    }
}
