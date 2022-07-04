namespace TourPlanner.API.DAL
{
    public interface IMapQuestRepository
    {
        /// <summary>
        /// Adds a new picture into the file system
        /// </summary>
        /// <param name="mapId">the id of the map</param>
        /// <param name="image">the image of the map</param>
        /// <returns></returns>
        public Task<Boolean> SavePicture(Guid mapId, byte[] image);

        /// <summary>
        /// Adds a new picture into the file system
        /// </summary>
        /// <param name="mapId">the id of the map</param>
        /// <param name="image">the image of the map</param>
        /// <returns></returns>
        public Task<byte[]> GetPicture(Guid mapId);

        /// <summary>
        /// Updates an existing map
        /// </summary>
        /// <param name="mapId">the id of the map</param>
        /// <param name="image">the image of the map</param>
        /// <returns></returns>
        public Task<Boolean> UpdatePicture(Guid mapId, byte[] image);

        /// <summary>
        /// Deletes a map
        /// </summary>
        /// <param name="mapId">the id of the map</param>
        /// <returns></returns>
        public Task<Boolean> DeletePicture(Guid mapId);
    }
}
