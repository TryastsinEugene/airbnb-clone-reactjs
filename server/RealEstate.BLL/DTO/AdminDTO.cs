namespace RealEstate.BLL.DTO
{
	public class AdminDTO
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }
		public string? Phone { get; set; }
		public string? Date { get; set; } = DateTime.Now.ToString("MM/dd/yy");
		public string? Login { get; set; }
		public string? Password { get; set; }
		public string? Salt { get; set; }
		public string? Photo { get; set; }
	}
}
