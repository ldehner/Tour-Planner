using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Tour_planner.Business;

namespace Tour_planner.UI.Models
{
    public class TourListModel
    {
        private ObservableCollection<TourModel> Tlist;
        private Dictionary<string, BitmapImage> Ilist;

        public TourListModel()
        {
            Tlist = new ObservableCollection<TourModel>();
            Ilist = new Dictionary<string, BitmapImage>();
        }

        

        public void AddTourToList(Tour t, BitmapImage image)
        {
            TourModel tour = new TourModel(t);

            tour.Image = image;

            Tlist.Add(tour);
        }

        public void AddImageToList(string TourID, BitmapImage image)
        {
            Ilist.Add(TourID,image);
        }

        public ObservableCollection<TourModel> Tourlist
        {
            get { return Tlist; }
            set { Tlist = value; }
        }

        public Dictionary<string, BitmapImage> ImageList
        {
            get { return Ilist; }
            set { Ilist = value; }
        }

    }
}
