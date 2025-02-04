namespace RealEstate.DLL.Entites
{
	public class Apartment
	{
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
		public double? Price { get; set; }
        public string? ExtraInfo { get; set; }
		public int? CheckIn{ get; set; }
        public int? CheckOut { get; set; }
		public int? MaxGuests { get; set; }
        public List<string>? Perks { get; set; }
		public List<string>? Photos { get; set; }
        public int? ClientId { get; set; }
        public Client? Client { get; set; }
        public ICollection<BookingModel>? Bookings{ get; set; } = new List<BookingModel>();
    }
}
