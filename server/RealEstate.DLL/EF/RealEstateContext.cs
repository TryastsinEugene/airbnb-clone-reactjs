using Microsoft.EntityFrameworkCore;
using RealEstate.DLL.Entites;

namespace RealEstate.DLL.EF
{
	public class RealEstateContext : DbContext
	{
        public RealEstateContext(DbContextOptions<RealEstateContext> options) : base(options)
        {
        }

        //public DbSet<User> Users{ get; set; }
        public DbSet<Client> Clients{ get; set; }
        public DbSet<Apartment> Apartments { get; set; }
		public DbSet<BookingModel> Bookings { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Apartment>().HasOne(a => a.Client).WithMany(c => c.Apartments).OnDelete(DeleteBehavior.Cascade);
		}
	}
}
