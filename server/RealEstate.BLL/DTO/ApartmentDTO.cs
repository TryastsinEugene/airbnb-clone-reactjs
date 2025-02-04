namespace RealEstate.BLL.DTO
{
	public class ApartmentDTO
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Address { get; set; }
		public string? Description { get; set; }
		public double? Price { get; set; }
		public string? ExtraInfo { get; set; }
		public int? CheckIn { get; set; }
		public int? CheckOut { get; set; }
		public int? MaxGuests { get; set; }
		public List<string>? Perks { get; set; } = new List<string>();
		public List<string>? Photos { get; set; } = new List<string>();
		public int ClientId { get; set; }
        public IEnumerable<int>? BookingIds { get; set; }
    }
}
