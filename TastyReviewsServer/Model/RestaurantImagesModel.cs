namespace TastyReviewsServer.Model
{
    public class RestaurantImagesModel
    {
        public Guid Guid { get; set; }
        public bool IsInterior { get; set; }       
        public Byte[]? Image { get; set; }
    }
}
