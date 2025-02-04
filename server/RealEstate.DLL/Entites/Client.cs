namespace RealEstate.DLL.Entites
{
	public class Client : User
	{
        public ICollection<Apartment>? Apartments { get; set; } = new List<Apartment>();
		public ICollection<BookingModel>? Bookings{ get; set; } = new List<BookingModel>();
    }
}
