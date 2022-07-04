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
        public async Task<Boolean> SavePicture(Guid mapId, byte[] image) {
            await File.WriteAllBytesAsync("../maps/"+mapId.ToString()+".map", image);
            return true;
        }

        /// <summary>
        /// Updates an existing map
        /// </summary>
        /// <param name="mapId">the id of the map</param>
        /// <param name="image">the image of the map</param>
        /// <returns></returns>
        public async Task<Boolean> UpdatePicture(Guid mapId, byte[] image) { throw new NotImplementedException(); }

        /// <summary>
        /// Deletes a map
        /// </summary>
        /// <param name="mapId">the id of the map</param>
        /// <returns></returns>
        public async Task<Boolean> DeletePicture(Guid mapId) {
            if (File.Exists("../maps/" + mapId.ToString() + ".map"))
            {
                File.Delete("../maps/" + mapId.ToString() + ".map");
            }
            return true;
        }

        public async Task<byte[]> GetPicture(Guid mapId)
        {
            return await File.ReadAllBytesAsync("../maps/" + mapId.ToString() + ".map");
        }
    }
}
