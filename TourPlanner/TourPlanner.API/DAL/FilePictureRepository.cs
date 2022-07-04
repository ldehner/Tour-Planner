namespace TourPlanner.API.DAL
{
    public class FilePictureRepository : IPictureRepository
    {
        /// <summary>
        /// Adds a new picture into the file system
        /// </summary>
        /// <param name="mapId">the id of the map</param>
        /// <param name="image">the image of the map</param>
        /// <returns></returns>
        public Boolean SavePicture(Guid mapId, string image) { throw new NotImplementedException(); }

        /// <summary>
        /// Updates an existing map
        /// </summary>
        /// <param name="mapId">the id of the map</param>
        /// <param name="image">the image of the map</param>
        /// <returns></returns>
        public Boolean UpdatePicture(Guid mapId, string image) { throw new NotImplementedException(); }

        /// <summary>
        /// Deletes a map
        /// </summary>
        /// <param name="mapId">the id of the map</param>
        /// <returns></returns>
        public Boolean DeletePicture(Guid mapId) { throw new NotImplementedException(); }
    }
}
