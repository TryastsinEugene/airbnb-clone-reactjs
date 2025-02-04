
using RealEstate.DLL.EF;
using RealEstate.DLL.Entites;
using RealEstate.DLL.Interfaces;

namespace RealEstate.DLL.Reposotories
{
	public class UnitOfWork : IUnitOfWork
	{
		private RealEstateContext _context;
		private ClientRepository _clientRepository;
		private ApartmentRepository _apartmentRepository;
		private BookingRepository _bookingRepository;
        public UnitOfWork(RealEstateContext context)
        {
            _context = context;
        }
        public IRepository<Client> Clients
		{
			get
			{
				if( _clientRepository == null ) 
					_clientRepository = new ClientRepository( _context );
				return _clientRepository;
			}
		}
		public IRepository<Apartment> Apartments
		{
			get
			{
				if( _apartmentRepository == null )
					_apartmentRepository = new ApartmentRepository( _context );
				return _apartmentRepository;
			}
		}
		public IRepository<BookingModel> Bookings
		{
			get
			{
				if (_bookingRepository == null)
					_bookingRepository = new BookingRepository(_context);
				return _bookingRepository;
			}
		}
		public async Task<bool> Save()
		{
			var saved = await _context.SaveChangesAsync();
			return saved > 0 ? true : false;
		}
		
	}
}
