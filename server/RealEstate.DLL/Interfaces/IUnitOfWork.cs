using RealEstate.DLL.Entites;

namespace RealEstate.DLL.Interfaces
{
	public interface IUnitOfWork
	{
		IRepository<Client> Clients { get; }
		IRepository<Apartment> Apartments { get; }
		IRepository<BookingModel> Bookings { get; }
		Task<bool> Save();
	}
}
