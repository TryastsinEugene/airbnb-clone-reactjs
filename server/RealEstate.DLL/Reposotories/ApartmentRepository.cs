using Microsoft.EntityFrameworkCore;
using RealEstate.DLL.EF;
using RealEstate.DLL.Entites;
using RealEstate.DLL.Interfaces;

namespace RealEstate.DLL.Reposotories
{
	public class ApartmentRepository : IRepository<Apartment>
	{
		private RealEstateContext _context;
        public ApartmentRepository(RealEstateContext context)
        {
            _context = context;
        }
		public async Task<IEnumerable<Apartment>> GetAllAsync() => await _context.Apartments.Include(a => a.Bookings).ToListAsync();
		public async Task<Apartment> GetAsync(int id)
		{
			Apartment? apartment = await _context.Apartments.Include(a => a.Bookings).Where(a => a.Id == id).FirstOrDefaultAsync();
			return apartment;
		}
		public async Task CreateAsync(Apartment item)
		{
			 var client = await _context.Clients.Where(c => c.Id == item.ClientId).FirstOrDefaultAsync();
			 client.Apartments.Add(item);
		}
		public void Update(Apartment item)
		{
			Apartment apartment = _context.Apartments.Where(a => a.Id == item.Id).FirstOrDefault();
			apartment.Title= item.Title;	
			apartment.Description= item.Description;
			apartment.Price= item.Price;
			apartment.Address= item.Address;
			apartment.ExtraInfo= item.ExtraInfo;
			apartment.CheckIn = item.CheckIn;
			apartment.CheckOut = item.CheckOut;
			apartment.MaxGuests= item.MaxGuests;
			apartment.Perks	= item.Perks;
			apartment.Photos = item.Photos;
		}
		public async Task DeleteAsync(int id)
		{
			Apartment? apartment = await GetAsync(id);
			if (apartment != null)
				_context.Apartments.Remove(apartment);
		}
		public Task<Apartment> GetAsync(string name)
		{
			throw new NotImplementedException();
		}
	}
}
