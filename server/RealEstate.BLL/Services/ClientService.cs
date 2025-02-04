
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RealEstate.BLL.DTO;
using RealEstate.BLL.Infrastructure;
using RealEstate.BLL.Interfaces;
using RealEstate.DLL.Entites;
using RealEstate.DLL.Interfaces;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace RealEstate.BLL.Services
{
	public class ClientService : IClientService
	{
		private IUnitOfWork Database {  get; set; }
		private readonly IMapper _mapper;
		public ClientService(IUnitOfWork database, IMapper mapper) 
		{
			Database = database;	
			_mapper = mapper;
		}
		public async Task<bool> CreateClientAsync(ClientDTO clientDTO)
		{
			var client = _mapper.Map<Client>(clientDTO);

			byte[] saltbuf = new byte[16];

			RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
			randomNumberGenerator.GetBytes(saltbuf);

			StringBuilder sb = new StringBuilder(16);
			for (int i = 0; i < 16; i++)
				sb.Append(string.Format("{0:X2}", saltbuf[i]));
			string salt = sb.ToString();

			byte[] password = Encoding.Unicode.GetBytes(salt + clientDTO.Password);

			var md5 = MD5.Create();

			byte[] byteHash = md5.ComputeHash(password);

			StringBuilder hash = new StringBuilder(byteHash.Length);
			for (int i = 0; i < byteHash.Length; i++)
				hash.Append(string.Format("{0:X2}", byteHash[i]));

			client.Password = hash.ToString();
			client.Salt = salt;

			await Database.Clients.CreateAsync(client);
			if(! await Database.Save())
				return false;

			return true;
		}
		public async Task<bool> UpdateClientAsync(ClientDTO clientDTO)
		{
			var client = new Client
			{
				Id = clientDTO.Id,
				Name = clientDTO.Name,
				Email = clientDTO.Email,
				Password = clientDTO.Password,
				Salt = clientDTO.Salt,
			};
			client.Apartments = await GetAppartmentsByIds(clientDTO.AppartmentIds);
			Database.Clients.Update(client);

			if (!await Database.Save())
				return false;

			return true;
		}
		public async Task<ICollection<Apartment>> GetAppartmentsByIds(IEnumerable<int> ids)
		{
			var apps = await Database.Apartments.GetAllAsync();
			var appsByIds = apps.Where(a => ids.Contains(a.Id)).ToList();

			return appsByIds;
		}
		public async Task<bool> DeleteClientAsync(int id)
		{
			await Database.Clients.DeleteAsync(id);

			if(!await Database.Save())
				return false;

			return true;
		}
		public async Task<ClientDTO> GetClientAsync(int id)
		{
			return _mapper.Map<Client, ClientDTO>(await Database.Clients.GetAsync(id));
		}
		public async Task<ClientDTO> GetClientAsync(string email)
		{
			if (email == null) return null;
			var client = await Database.Clients.GetAsync(email);
			if (client == null)
				throw new ValidationException("Wrong user!", "");
			return new ClientDTO
			{
				Id = client.Id,
				Name = client.Name,
				Email = client.Email,
				//Login = client.Login,
				//Date = client.Date,
				//Photo = client.Photo,
				//Phone = client.Phone,
				Password = client.Password,
			};
		}
		public async Task<IEnumerable<ClientDTO>> GetClientsAsync()
		{
			//var config = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>()).CreateMapper();
			return _mapper.Map<IEnumerable<Client>, IEnumerable<ClientDTO>>(await Database.Clients.GetAllAsync());
		}
		public async Task<bool> IsComparePassword(string login, string pass)
		{
			//if (login == "admin" && pass == "admin") return true;

			var user = await Database.Clients.GetAsync(login);
			if (user == null)
				return false;

			string? salt = user.Salt;

			byte[] password = Encoding.Unicode.GetBytes(salt + pass);
			var md5 = MD5.Create();
			byte[] byteHash = md5.ComputeHash(password);
			StringBuilder hash = new StringBuilder(byteHash.Length);
			for (int i = 0; i < byteHash.Length; i++)
			{
				hash.Append(string.Format("{0:X2}", byteHash[i]));
			}
			if (user.Password != hash.ToString())
				return false;
			return true;
		}

		public async Task<bool> IsClientExist(int id)
		{
			var clients = await Database.Clients.GetAllAsync();

			return clients.Any(c => c.Id == id);
		}
		public async Task<bool> IsClientExist(string email)
		{
			var clients = await Database.Clients.GetAllAsync();

			return clients.Any(c => c.Email == email);
		}

		//public async Task<IEnumerable<Apartment>> GetApartmentsByClientId(int clientId)
		//{
		//	var apps = await Database.Apartments.GetAllAsync();
		//	var app = apps.Where(a => a.ClientId == clientId);

		//	return app;
		//}
		//public async Task<IEnumerable<BookingModel>> GetBookingsByClientId(int clientId)
		//{
		//	var bookings = await Database.Bookings.GetAllAsync();
		//	var booking = bookings.Where(b => b.ClientId == clientId);

		//	return bookings;
		//}
	}
}
