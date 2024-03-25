namespace TastyReviewsServer.Model
{
    public class OwnerPostingsModel
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string RestaurantName { get; set; } = string.Empty;
        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;       
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; } 
        public string LastUpdatedBy { get; set;} = string.Empty;
        public List<RestaurantImagesModel> Images { get; set; }
    }
}
