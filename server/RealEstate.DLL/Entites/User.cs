﻿
namespace RealEstate.DLL.Entites
{
	public abstract class User
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }
     //   public string? Photo {  get; set; }
    }
}
