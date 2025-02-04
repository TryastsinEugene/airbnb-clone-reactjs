using Microsoft.AspNetCore.Mvc;
using RealEstate.BLL.DTO;
using RealEstate.BLL.Interfaces;
using RealEstate.BLL.Services;
using RealEstateAPI.Helpers;

namespace RealEstateAPI.Controllers
{
	[ApiController]
	[Route("api/booking")]
	public class BookingController : ControllerBase
	{
		private readonly IBookingService _bookingService;
		private readonly IClientService _clientService;
		private readonly JwtService _jwtService;
		public BookingController(IBookingService bookingService, IClientService clientService, JwtService jwtService)
		{
			_bookingService = bookingService;
			_jwtService = jwtService;
			_clientService = clientService;
		}
		[HttpGet]
		[Route("bookings")]
		public async Task<ActionResult> GetBookings()
		{
			var bookings = await _bookingService.GetBookingsAsync();

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(bookings);
		}
		[HttpGet]
		[Route("bookingsbyid")]
		public async Task<IActionResult> GetBookingsByClientId()
		{
			var jwt = Request.Cookies["jwt"];
			var token = _jwtService.Verify(jwt);
			int userId = int.Parse(token.Issuer);

			var bookings = await _bookingService.GetBookingsDTOsByClientId(userId);

			return Ok(bookings);
		}
		[HttpGet]
		[Route("bookings/{id}")]
		public async Task<IActionResult> GetBooking(int id)
		{
			if (!await _bookingService.IsBookingExists(id))
				return NotFound();

			var booking = await _bookingService.GetBookingAsync(id);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(booking);
		}
		[HttpPost]
		[Route("addbooking")]
		public async Task<IActionResult> CreateBooking([FromBody] BookingModelDTO booking)
		{

			var jwt = Request.Cookies["jwt"];
			var token = _jwtService.Verify(jwt);
			int userId = int.Parse(token.Issuer);

			if (!await _clientService.IsClientExist(userId))
			{
				ModelState.AddModelError("", "Client was not found");
				return BadRequest(ModelState);
			}

			if (booking == null)
				return BadRequest(ModelState);


			booking.ClientId = userId;
			booking = await _bookingService.CreateBookingAsync(booking);
			if (booking == null)
			{
				return BadRequest(ModelState);
			}

			return Ok(booking);
		}
		[HttpPut]
		[Route("updatebooking")]
		public async Task<IActionResult> UpdateBooking([FromBody] BookingModelDTO booking)
		{
			if (booking == null)
				return BadRequest(ModelState);

			if (!await _bookingService.IsBookingExists(booking.Id))
				return NotFound(booking);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!await _bookingService.UpdateBooking(booking))
			{
				ModelState.AddModelError("", "Something went wrong updating user");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}
		[HttpDelete("{bookingId}")]
		public async Task<ActionResult<ClientDTO>> DeleteBooking(int bookingId)
		{
			if (!await _bookingService.IsBookingExists(bookingId))
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!await _bookingService.DeleteBooking(bookingId))
			{
				ModelState.AddModelError("", "Something went wrong deleting client");
				return BadRequest(ModelState);
			}
			return NoContent();

		}
	}
}
