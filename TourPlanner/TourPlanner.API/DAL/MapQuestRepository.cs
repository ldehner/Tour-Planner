using System;
using System.Drawing;
using System.Drawing.Imaging;
namespace TourPlanner.API.DAL

{
    public class MapQuestRepository : IMapQuestRepository
    {
        private readonly string _path;
        public MapQuestRepository(string path)
        {
            _path = path;
        }

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
            i.Save(_path + mapId.ToString() + ".jpg");
        }


        /// <summary>
        /// Deletes a map
        /// </summary>
        /// <param name="mapId">the id of the map</param>
        /// <returns></returns>
        public async Task DeletePicture(Guid mapId) {
            if (File.Exists(_path + mapId.ToString() + ".jpg"))
            {
                File.Delete(_path + mapId.ToString() + ".jpg");
            }
        }

        public async Task<byte[]> GetPicture(Guid mapId)
        {
            return await File.ReadAllBytesAsync(_path + mapId.ToString() + ".jpg");
        }
    }
}
