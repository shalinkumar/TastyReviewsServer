namespace TastyReviewsServer.Model
{
    public class BookingsModel
    {
        public int Id { get; set; }
        public required string BookingName { get; set; }
        public string BookingEmail { get; set; } = string.Empty;
        public required string BookingNumber { get; set; } = string.Empty;
        public required string RestaurantName { get; set; } 
        public required string Persons { get; set; }
        public string Status { get; set; }
        public required DateTime BookingDate { get; set; }
        public required DateTime CreationDate { get; set; }        
    }
}
