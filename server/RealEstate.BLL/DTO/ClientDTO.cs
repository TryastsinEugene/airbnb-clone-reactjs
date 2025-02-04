using System.Text.Json.Serialization;

namespace RealEstate.BLL.DTO
{
	public class ClientDTO
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }
		//[JsonIgnore]
		public string? Password { get; set; }
		//[JsonIgnore]
		public string? Salt { get; set; }
		//public string? Photo { get; set; }
        public bool IsBanned { get; set; } = false;
		public IEnumerable<int>? AppartmentIds { get; set; }
		public IEnumerable<int>? BookingsIds { get; set; }
	}
}
