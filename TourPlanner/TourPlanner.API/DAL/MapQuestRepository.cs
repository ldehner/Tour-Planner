using System;
using System.Drawing;
using System.Drawing.Imaging;
namespace TourPlanner.API.DAL

{
    public class MapQuestRepository : IMapQuestRepository
    {
        /// <summary>
        /// Adds a new picture into the file system
        /// </summary>
        /// <param name="mapId">the id of the map</param>
        /// <param name="image">the image of the map</param>
        /// <returns></returns>
        public async Task SavePicture(Guid mapId, byte[] image) {
            ImageConverter _imageConverter = new ImageConverter();
            MemoryStream ms = new MemoryStream(image);
            Image i = Image.FromStream(ms);
            i.Save(System.IO.Directory.GetCurrentDirectory()+"/maps/" + mapId.ToString() + ".jpg");
        }


        /// <summary>
        /// Deletes a map
        /// </summary>
        /// <param name="mapId">the id of the map</param>
        /// <returns></returns>
        public async Task DeletePicture(Guid mapId) {
            if (File.Exists(System.IO.Directory.GetCurrentDirectory() + "/maps/" + mapId.ToString() + ".jpg"))
            {
                File.Delete(System.IO.Directory.GetCurrentDirectory() + "/maps/" + mapId.ToString() + ".jpg");
            }
        }

        public async Task<byte[]> GetPicture(Guid mapId)
        {
            return await File.ReadAllBytesAsync(System.IO.Directory.GetCurrentDirectory() + "/maps/" + mapId.ToString() + ".jpg");
        }
    }
}
