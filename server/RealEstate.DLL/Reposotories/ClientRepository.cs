using Microsoft.EntityFrameworkCore;
using RealEstate.DLL.EF;
using RealEstate.DLL.Entites;
using RealEstate.DLL.Interfaces;

namespace RealEstate.DLL.Reposotories
{
	public class ClientRepository : IRepository<Client>
	{
		private RealEstateContext _context;
        public ClientRepository(RealEstateContext context)
        {
				_context = context;
        }
		public async Task<IEnumerable<Client>> GetAllAsync() => await _context.Clients.Include(c => c.Apartments).Include(b => b.Bookings).ToListAsync();
		public async Task<Client> GetAsync(int id)
		{
			Client? client = await _context.Clients.Include(c => c.Apartments).Include(b => b.Bookings).Where(c => c.Id == id).FirstOrDefaultAsync();
			return client;
		}
		public async Task<Client> GetAsync(string email)
		{
			Client client = await _context.Clients.Where(c => c.Email == email).FirstOrDefaultAsync();
			return client;
		}
		public async Task CreateAsync(Client item) => await _context.Clients.AddAsync(item);
		public void Update(Client item)
		{
			Client client = _context.Clients.Where(c => c.Id == item.Id).FirstOrDefault();
			client.Name = item.Name;
			client.Email = item.Email;
			client.Password = item.Password;
			client.Salt = item.Salt;
			client.Apartments = item.Apartments;
		}
		public async Task DeleteAsync(int id)
		{
			Client? client = await GetAsync(id);
			if(client != null) 
				_context.Clients.Remove(client);
		}
	}
}
