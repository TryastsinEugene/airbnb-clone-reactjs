using AutoMapper;
using RealEstate.BLL.DTO;
using RealEstate.BLL.Infrastructure;
using RealEstate.BLL.Interfaces;
using RealEstate.DLL.Entites;
using RealEstate.DLL.Interfaces;

namespace RealEstate.BLL.Services
{
	public class ApartmentServcie : IApartmentService
	{
		IUnitOfWork Database { get; set; }
		private readonly IMapper _mapper;

		public ApartmentServcie(IUnitOfWork database, IMapper mapper)
		{
			Database = database;
			_mapper = mapper;
		}
		public async Task<bool> CreateApartmentAsync(ApartmentDTO apartmentDTO)
		{
			var apartment = _mapper.Map<Apartment>(apartmentDTO);

			var clients = await Database.Clients.GetAllAsync();
			var client = clients.Where(c => c.Id == apartment.ClientId);
			if (client == null)
				return false;

			await Database.Apartments.CreateAsync(apartment);
			await Database.Save();
			return true;
		}
		public async Task<bool> UpdateApartmentAsync(ApartmentDTO apartmentDTO)
		{
			var apartment = new Apartment
			{
				Id = apartmentDTO.Id,
				Title = apartmentDTO.Title,
				Address = apartmentDTO.Address,
				Description = apartmentDTO.Description,
				Price = apartmentDTO.Price,
				ExtraInfo = apartmentDTO.ExtraInfo,
				CheckIn = apartmentDTO.CheckIn,
				CheckOut = apartmentDTO.CheckOut,
				MaxGuests = apartmentDTO.MaxGuests,
				Perks = apartmentDTO.Perks,
				Photos = apartmentDTO.Photos,
				ClientId = apartmentDTO.ClientId,
			};
			Database.Apartments.Update(apartment);

			if (!await Database.Save())
				return false;

			return true;
		}
		public async Task<bool> DeleteApartmentAsync(int id)
		{
			await Database.Apartments.DeleteAsync(id);
			if(!await Database.Save())
				return false;

			return true;
		}
		public async Task<ApartmentDTO> GetApartmentAsync(int id)
		{
			return _mapper.Map<Apartment, ApartmentDTO>(await Database.Apartments.GetAsync(id));
		}
		public async Task<IEnumerable<ApartmentDTO>> GetApartmentsAsync()
		{
			return _mapper.Map<IEnumerable<Apartment>, IEnumerable<ApartmentDTO>>(await Database.Apartments.GetAllAsync());
		}
		public async Task<bool> IsAppartmentExist(int id)
		{
			var apps = await Database.Apartments.GetAllAsync();

			return apps.Any(c => c.Id == id);
		}

		public async Task<IEnumerable<ApartmentDTO>> GetApartmentDTOsByClientId(int clientId)
		{
			var client = await Database.Clients.GetAsync(clientId);
			var appartments = client.Apartments;

			return _mapper.Map<IEnumerable<Apartment>, IEnumerable<ApartmentDTO>>(appartments);
		}
	}
}
