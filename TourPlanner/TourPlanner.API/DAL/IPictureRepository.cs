namespace TourPlanner.API.DAL
{
    /// <summary>
    /// Interface to handle map images
    /// </summary>
    public interface IPictureRepository
    {
        /// <summary>
        /// Adds a new picture into the file system
        /// </summary>
        /// <param name="mapId">the id of the map</param>
        /// <param name="image">the image of the map</param>
        /// <returns></returns>
        public Boolean AddPicture(Guid mapId, string image);
        /// <summary>
        /// Updates an existing map
        /// </summary>
        /// <param name="mapId">the id of the map</param>
        /// <param name="image">the image of the map</param>
        /// <returns></returns>
        public Boolean UpdatePicture(Guid mapId, string image);
        /// <summary>
        /// Deletes a map
        /// </summary>
        /// <param name="mapId">the id of the map</param>
        /// <returns></returns>
        public Boolean DeletePicture(Guid mapId);
    }
}
