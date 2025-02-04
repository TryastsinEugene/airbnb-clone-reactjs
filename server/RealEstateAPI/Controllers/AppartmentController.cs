using Microsoft.AspNetCore.Mvc;
using RealEstate.BLL.DTO;
using RealEstate.BLL.Interfaces;
using RealEstate.DLL.Entites;
using RealEstateAPI.Helpers;
using RealEstateAPI.Models;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace RealEstateAPI.Controllers
{
	[ApiController]
	[Route("api/appartments")]
	public class AppartmentController : ControllerBase
	{
		private readonly IClientService _clientService;
		private readonly IApartmentService _apartmentService;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly JwtService _jwtService;

		public AppartmentController(IClientService clientService, IApartmentService apartmentService, JwtService jwtService, IWebHostEnvironment hostingEnvironment)
		{
			_clientService = clientService;
			_apartmentService = apartmentService;
			_jwtService = jwtService;
			_hostingEnvironment = hostingEnvironment;
		}
		[HttpGet]
		[Route("getappartments")]
		public async Task<IActionResult> GetAppartments()
		{
			var apps = await _apartmentService.GetApartmentsAsync();

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(apps);
		}
		[HttpGet]
		[Route("places")]
		public async Task<IActionResult> GetAppartmetnsByClientId()
		{
			var jwt = Request.Cookies["jwt"];
			var token = _jwtService.Verify(jwt);
			int userId = int.Parse(token.Issuer);

			var appartments = await _apartmentService.GetApartmentDTOsByClientId(userId);

			return Ok(appartments);
		}
		[HttpGet]
		[Route("places/{id}")]
		public async Task<IActionResult> GetAppartment(int id)
		{
			if (!await _apartmentService.IsAppartmentExist(id))
				return NotFound();

			var app = await _apartmentService.GetApartmentAsync(id);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(app);
		}
		[HttpPost]
		[Route("addnewplace")]
		public async Task<IActionResult> CreateAppartment([FromBody] ApartmentDTO apartment)
		{

			var jwt = Request.Cookies["jwt"];
			var token = _jwtService.Verify(jwt);
			int userId = int.Parse(token.Issuer);

			if (!await _clientService.IsClientExist(userId))
			{
				ModelState.AddModelError("", "Client was not found");
				return BadRequest(ModelState);
			}

			if (apartment == null)
				return BadRequest(ModelState);


			apartment.ClientId = userId;

			if (!await _apartmentService.CreateApartmentAsync(apartment))
			{
				return BadRequest(ModelState);
			}

			return Ok(apartment);
		}
		[HttpPut]
		[Route("updateplace")]
		public async Task<IActionResult> UpdateAppartment([FromBody] ApartmentDTO appartment)
		{
			if (appartment == null)
				return BadRequest(ModelState);

			if (!await _apartmentService.IsAppartmentExist(appartment.Id))
				return NotFound(appartment);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!await _apartmentService.UpdateApartmentAsync(appartment))
			{
				ModelState.AddModelError("", "Something went wrong updating user");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}
		[HttpDelete("{appartmentId}")]
		public async Task<ActionResult<ClientDTO>> DeleteAppartment(int appartmentId)
		{
			if (!await _apartmentService.IsAppartmentExist(appartmentId))
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!await _apartmentService.DeleteApartmentAsync(appartmentId))
			{
				ModelState.AddModelError("", "Something went wrong deleting client");
				return BadRequest(ModelState);
			}
			return NoContent();

		}
		
	}
}
