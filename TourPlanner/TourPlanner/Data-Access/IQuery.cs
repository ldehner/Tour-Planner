using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Business;

namespace TourPlanner.Data_Access
{
    public interface IQuery
    {
        public Task<Tourlist> GetTours();
        public Task<Tour> GetTourById(string id);
        public Task PostTour(Tour tour);
        public void DeleteTour(string Id);
        public Task UpdateTour(Tour newtour, string oldId);
        public Task<Tourlist> GetToursBySearchTerm(string searchTerm);
        public Task PostLog(TourLog tourLog, string TourId);
        public void DeleteLog(string TourId,string LogId);
        public Task UpdateLog(TourLog log, string Logid);
        public Task<string> GetReport();
        public Task<string> GetReportById(string Id);
        public Task<string> GetImageBytes(string tourId);
        public Task<string> GetWeatherFromLocation(string start, string destination);
    }
}
