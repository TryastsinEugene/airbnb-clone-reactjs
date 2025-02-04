using RealEstate.BLL.DTO;

namespace RealEstate.BLL.Interfaces
{
	public interface IBookingService
	{
		Task<IEnumerable<BookingModelDTO>> GetBookingsAsync();
		Task<BookingModelDTO> GetBookingAsync(int id);
		Task<IEnumerable<BookingModelDTO>> GetBookingsDTOsByClientId(int clientId);
		Task<BookingModelDTO> CreateBookingAsync(BookingModelDTO dto);
		Task<bool> UpdateBooking(BookingModelDTO dto);
		Task<bool> DeleteBooking(int id);
		Task<bool> IsBookingExists(int id);
	}
}
