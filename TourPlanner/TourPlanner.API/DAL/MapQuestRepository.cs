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
            i.Save("../maps/" + mapId.ToString() + ".jpg");
        }


        /// <summary>
        /// Deletes a map
        /// </summary>
        /// <param name="mapId">the id of the map</param>
        /// <returns></returns>
        public async Task DeletePicture(Guid mapId) {
            if (File.Exists("../maps/" + mapId.ToString() + ".jpg"))
            {
                File.Delete("../maps/" + mapId.ToString() + ".jpg");
            }
        }

        public async Task<byte[]> GetPicture(Guid mapId)
        {
            return await File.ReadAllBytesAsync("../maps/" + mapId.ToString() + ".map");
        }
    }
}
