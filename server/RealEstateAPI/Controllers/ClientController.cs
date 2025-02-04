using Microsoft.AspNetCore.Mvc;
using RealEstate.BLL.DTO;
using RealEstate.BLL.Interfaces;
using RealEstate.BLL.Services;
using RealEstateAPI.Helpers;
using RealEstateAPI.Models;

namespace RealEstateAPI.Controllers
{
	[ApiController]
	[Route("api/Clients")]
	public class ClientController : ControllerBase
	{
		private readonly IClientService _clientService;
		private readonly JwtService _jwtService;
		

		public ClientController(IClientService service, JwtService jwtService)
        {
            _clientService = service;
			_jwtService = jwtService;
		}
		[HttpGet]
		public async Task<ActionResult> GetClients()
		{
			var clients = await _clientService.GetClientsAsync();

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(clients);
		}
		[HttpGet("{id}")]
		public async Task<ActionResult> GetClient(int id)
		{
			if (!await _clientService.IsClientExist(id))
				return NotFound();

			var client = await _clientService.GetClientAsync(id);

			if(!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(client);
		}

		[HttpPut("{clientId}")]
		public async Task<ActionResult> UpdateClient(int clientId, [FromBody] ClientDTO client)
		{
			if(client == null) 
				return BadRequest(ModelState);

			if(clientId != client.Id) 
				return BadRequest(ModelState);

			if (! await _clientService.IsClientExist(clientId)) 
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if(!await _clientService.UpdateClientAsync(client))
			{
				ModelState.AddModelError("", "Something went wrong updating user");
				return StatusCode(500, ModelState);
			}
			return Ok(client);
		}
		[HttpPost]
		[Route("register")]
		public async Task<ActionResult<ClientDTO>> PostClient([FromBody] ClientDTO client)
		{
            if (client == null)
				return BadRequest(ModelState);

			var clients = await _clientService.GetClientsAsync();
			var tempClient = clients.Where(c => c.Email.Trim().ToUpper() == client.Email.TrimEnd().ToUpper()).FirstOrDefault();
				
			if (tempClient != null)
			{
				//ModelState.AddModelError("", "Email already exist");
				return StatusCode(409);
			}

			if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!await _clientService.CreateClientAsync(client))
			{
				ModelState.AddModelError("", "Something went wrong while savin");
				return StatusCode(500, ModelState);
			}

			return Ok(client);
		}
		[HttpDelete("{clientId}")]
		public async Task<ActionResult<ClientDTO>> DeleteClient(int clientId)
		{
			if (!await _clientService.IsClientExist(clientId))
				return NotFound();

			if(!ModelState.IsValid) 
				return BadRequest(ModelState);

			if(! await _clientService.DeleteClientAsync(clientId))
			{
				ModelState.AddModelError("", "Something went wrong deleting client");
				return BadRequest(ModelState);	
			}
			return NoContent();
		}
		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login(Login login)
		{
			if(await _clientService.IsComparePassword(login.Email, login.Password))
			{
				var client = await _clientService.GetClientAsync(login.Email);

				var jwt = _jwtService.Generate(client.Id);
				Response.Cookies.Append("jwt", jwt, new CookieOptions
				{
					HttpOnly = true,
					IsEssential = true
				});

				return Ok(client);
					
			}
			return BadRequest(login);
		}
		[HttpGet]
		[Route("profile")]
		public async Task<IActionResult> Profile()
		{
			try
			{
				var jwt = Request.Cookies["jwt"];
				var token = _jwtService.Verify(jwt);
				int userId = int.Parse(token.Issuer);
				var client = await _clientService.GetClientAsync(userId);
				return Ok(client);
			}
			catch (Exception ex)
			{
				return Ok(null);
			}
		}
		[HttpPost]
		[Route("logout")]
		public IActionResult Logout()
		{
			Response.Cookies.Delete("jwt");

			return Ok(new
			{
				message = "success"
			});
		}
	}
}
