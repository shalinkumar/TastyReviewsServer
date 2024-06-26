﻿namespace TastyReviewsServer.Model
{
    public class RestaurantPostingsModel
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string RestaurantName { get; set; } = string.Empty;
        public string PlaceId { get; set; } = string.Empty;
        public string Rating { get; set; } = string.Empty;
        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;       
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ReservationNumber { get; set; } = string.Empty;
        public string ReservationEmail { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; } 
        public string LastUpdatedBy { get; set;} = string.Empty;
        public IList<string> InteriorImage { get; set; }
        public IList<string> ExteriorImage { get; set; }
        public List<RestaurantImagesModel>? Images { get; set; } = new List<RestaurantImagesModel>();
    }
}
