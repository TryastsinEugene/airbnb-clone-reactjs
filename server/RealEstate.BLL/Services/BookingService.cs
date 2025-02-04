using AutoMapper;
using RealEstate.BLL.DTO;
using RealEstate.BLL.Interfaces;
using RealEstate.DLL.Entites;
using RealEstate.DLL.Interfaces;

namespace RealEstate.BLL.Services
{
	public class BookingService : IBookingService
	{
		IUnitOfWork Database { get; set; }
		private readonly IMapper _mapper;
        public BookingService(IUnitOfWork database, IMapper mapper)
        {
			Database = database;
			_mapper = mapper;
		}
        public async Task<IEnumerable<BookingModelDTO>> GetBookingsAsync()
		{
			return _mapper.Map<IEnumerable<BookingModel>, IEnumerable<BookingModelDTO>>(await Database.Bookings.GetAllAsync());
		}
		public async Task<BookingModelDTO> GetBookingAsync(int id)
		{
			return _mapper.Map<BookingModel, BookingModelDTO>(await Database.Bookings.GetAsync(id));
		}
		public async Task<IEnumerable<BookingModelDTO>> GetBookingsDTOsByClientId(int clientId)
		{
			//var client = await Database.Clients.GetAsync(clientId);
			//var bookings = client.Bookings;
			var bookings = await Database.Bookings.GetAllAsync();
			bookings = bookings.Where(x => x.ClientId == clientId);
			return _mapper.Map<IEnumerable<BookingModel>, IEnumerable<BookingModelDTO>>(bookings);
		}
		public async Task<BookingModelDTO> CreateBookingAsync(BookingModelDTO bookingDto)
		{
			var booking = _mapper.Map<BookingModel>(bookingDto);

			var clients = await Database.Clients.GetAllAsync();
			var appartments = await Database.Apartments.GetAllAsync();
			var appartment = appartments.Where(a => a.Id == booking.AppartmentId).FirstOrDefault();
			var client = clients.Where(c => c.Id == booking.ClientId).FirstOrDefault();

			if (client == null || appartment == null)
				return null;

			await Database.Bookings.CreateAsync(booking);
			await Database.Save();

			var bookings = await Database.Bookings.GetAllAsync();
			var tempBooking = bookings.Where(b => b.AppartmentId == appartment.Id && b.ClientId == client.Id).LastOrDefault();
			bookingDto = _mapper.Map<BookingModelDTO>(tempBooking);

			return bookingDto;
		}
		public async Task<bool> UpdateBooking(BookingModelDTO bookingDto)
		{
			var booking = new BookingModel
			{
				Id = bookingDto.Id,
				ClientId = bookingDto.ClientId,
				AppartmentId = bookingDto.AppartmentId,
				Price = bookingDto.Price,
				CheckIn = bookingDto.CheckIn,
				CheckOut = bookingDto.CheckOut,
				Name = bookingDto.Name,
				NumberOfGuests = bookingDto.NumberOfGuests,
				Photo = bookingDto.Photo,
				Phone = bookingDto.Phone
			};
			Database.Bookings.Update(booking);

			if(!await Database.Save()) 
				return false;

			return true;
		}
		public async Task<bool> DeleteBooking(int id)
		{
			await Database.Bookings.DeleteAsync(id);
			if (!await Database.Save())
				return false;

			return true;
		}
		public async Task<bool> IsBookingExists(int id)
		{
			var bookings = await Database.Bookings.GetAllAsync();

			return bookings.Any(b => b.Id == id);
		}
		public async Task<IEnumerable<BookingModelDTO>> GetApartmentDTOsByClientId(int clientId)
		{
			var client = await Database.Clients.GetAsync(clientId);
			var bookings = client.Bookings;

			return _mapper.Map<IEnumerable<BookingModel>, IEnumerable<BookingModelDTO>>(bookings);
		}
	}
}
