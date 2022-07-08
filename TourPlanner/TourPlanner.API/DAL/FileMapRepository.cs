using System;
using System.Drawing;
using System.Drawing.Imaging;
using TourPlanner.API.Exceptions;

namespace TourPlanner.API.DAL

{
    public class FileMapRepository : IMapRepository
    {
        private readonly string _path;
        public FileMapRepository(string path)
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
            if (File.Exists(_path + mapId.ToString() + ".jpg")) return await File.ReadAllBytesAsync(_path + mapId.ToString() + ".jpg");
            throw new TourNotFoundException();

        }
    }
}
