namespace TourPlanner.API.DAL
{
    public interface IMapRepository
    {
        /// <summary>
        /// Adds a new picture into the file system
        /// </summary>
        /// <param name="mapId">the id of the map</param>
        /// <param name="image">the image of the map</param>
        /// <returns></returns>
        public Task SavePicture(Guid mapId, byte[] image);

        /// <summary>
        /// Adds a new picture into the file system
        /// </summary>
        /// <param name="mapId">the id of the map</param>
        /// <param name="image">the image of the map</param>
        /// <returns></returns>
        public Task<byte[]> GetPicture(Guid mapId);

        /// <summary>
        /// Deletes a map
        /// </summary>
        /// <param name="mapId">the id of the map</param>
        /// <returns></returns>
        public Task DeletePicture(Guid mapId);
    }
}
