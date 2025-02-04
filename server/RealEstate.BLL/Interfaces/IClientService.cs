using RealEstate.BLL.DTO;

namespace RealEstate.BLL.Interfaces
{
	public interface IClientService
	{
		Task<IEnumerable<ClientDTO>> GetClientsAsync();
		Task<ClientDTO> GetClientAsync(int id);
		Task<ClientDTO> GetClientAsync(string email);
		Task<bool> CreateClientAsync(ClientDTO clientDTO);
		Task<bool> UpdateClientAsync(ClientDTO clientDTO);
		Task<bool> DeleteClientAsync(int id);
		Task<bool> IsComparePassword(string login, string password);
		Task<bool> IsClientExist(int id);
		Task<bool> IsClientExist(string email);
	}
}
