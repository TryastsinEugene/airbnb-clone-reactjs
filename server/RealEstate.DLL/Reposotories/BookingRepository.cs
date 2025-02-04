
using Microsoft.EntityFrameworkCore;
using RealEstate.DLL.EF;
using RealEstate.DLL.Entites;
using RealEstate.DLL.Interfaces;

namespace RealEstate.DLL.Reposotories
{
	public class BookingRepository : IRepository<BookingModel>
	{
		private RealEstateContext _context;
        public BookingRepository(RealEstateContext context)
        {
            _context = context;
        }
		public async Task<IEnumerable<BookingModel>> GetAllAsync()
		{
			return await _context.Bookings.Include(b => b.Appartment).ToListAsync();
		}
		public async Task<BookingModel> GetAsync(int id)
		{
			BookingModel model = await _context.Bookings.Where(b => b.Id == id).Include(b => b.Appartment).FirstOrDefaultAsync();
			return model;
		}
		public async Task CreateAsync(BookingModel item)
		{
			Client? client = await _context.Clients.Where(c => c.Id == item.ClientId).FirstOrDefaultAsync();
			Apartment? apartment = await _context.Apartments.Where(c => c.Id == item.AppartmentId).FirstOrDefaultAsync();
			item.Photo = apartment.Photos[0];
			client.Bookings.Add(item);
			apartment.Bookings.Add(item);
		}
		public void Update(BookingModel item)
		{
			BookingModel booking = _context.Bookings.Where(b => b.Id ==  item.Id).FirstOrDefault(); 
			booking.Price = item.Price;
			booking.NumberOfGuests = item.NumberOfGuests;
			booking.CheckOut = item.CheckOut;
			booking.CheckIn = item.CheckIn;
			booking.Name = item.Name;
			booking.Phone = item.Phone;
		}
		public async Task DeleteAsync(int id)
		{
			BookingModel? booking = await GetAsync(id);
			if(booking != null) 
				_context.Bookings.Remove(booking);
		}
		public Task<BookingModel> GetAsync(string name)
		{
			throw new NotImplementedException();
		}
	}
}
