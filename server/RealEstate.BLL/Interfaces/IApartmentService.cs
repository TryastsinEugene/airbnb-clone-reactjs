using RealEstate.BLL.DTO;

namespace RealEstate.BLL.Interfaces
{
	public interface IApartmentService
	{
		Task<IEnumerable<ApartmentDTO>> GetApartmentsAsync();
		Task<IEnumerable<ApartmentDTO>> GetApartmentDTOsByClientId(int clientId);
		Task<ApartmentDTO> GetApartmentAsync(int id);
		Task<bool> CreateApartmentAsync(ApartmentDTO apartmentDTO);
		Task<bool> UpdateApartmentAsync(ApartmentDTO apartmentDTO);
		Task<bool> DeleteApartmentAsync(int id);
		Task<bool> IsAppartmentExist(int id);
	}
}
