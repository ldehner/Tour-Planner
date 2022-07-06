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
            string templateHtml = File.ReadAllText(@"C:\Users\linus\Documents\FH\Tour-Planner\TourPlanner\TourPlanner.API\TourReport.html");
            var template = Handlebars.Compile(templateHtml);
            var tour = await GetTourAsync(tourId);
            var tourLogs = new List<PdfLog>();
            tour.Logs.ForEach(log => tourLogs.Add(new PdfLog
            {
                Date = log.Date.ToShortDateString(),
                Duration = log.Duration.Hours + ":" + log.Duration.Minutes,
                Comment = log.Comment,
                Rating = log.Rating,
                Difficulty = log.Difficulty
            }));

            var data = new
            {
                name = tour.Name,
                tourId = tour.TourId,
                transport = tour.Type,
                from = tour.Start,
                distance = Math.Round(tour.Distance, 1),
                duration = tour.Duration,
                to = tour.Destination,
                description = tour.Description,
                logs = tourLogs
            };

            string tourHtml = template(data);

            ConverterProperties converterProperties = new ConverterProperties();

            //string file = @"C:\Users\linus\Documents\FH\Tour-Planner\TourPlanner\" + tour.TourId.ToString() + ".pdf";
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
