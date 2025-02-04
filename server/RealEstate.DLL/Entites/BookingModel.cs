using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.DLL.Entites
{
	public class BookingModel
	{
        public int Id { get; set; }
        public string? CheckIn { get; set; }
        public string? CheckOut { get; set; }
        public int NumberOfGuests { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public float Price { get; set; }
        public string? Photo {  get; set; }
        public int? AppartmentId { get; set; }
        [ForeignKey("AppartmentId")]
        public Apartment? Appartment { get; set; }
        public int? ClientId { get; set; }
		[ForeignKey("ClientId")]
		public Client? Client { get; set; }
    }
}
