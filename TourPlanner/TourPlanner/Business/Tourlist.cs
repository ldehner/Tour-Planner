using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour_planner.Business
{
    public class Tourlist
    {
        public Dictionary<string, Tour> TourList { get; set; }


        public Tourlist()
        {
            TourList = new Dictionary<string, Tour>();
        }

        public bool AddTourToList(Tour tour)
        {
            try
            {
                TourList.Add(tour.Name, tour);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool RemoveTourFromList(string Name)
        {
            if (TourList.Remove(Name))
                return true;
            else
                return false;
        }

        public Tour GetTourFromList(string name)
        {

            return TourList[name];
        }


        public bool UpdateTourInList(string name, Tour tour)
        {
            try
            {

                if (RemoveTourFromList(name))
                {
                    if (AddTourToList(tour))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
