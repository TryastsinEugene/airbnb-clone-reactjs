
using RealEstate.BLL.Infrastructure;
using RealEstate.BLL.Interfaces;
using RealEstate.BLL.Services;
using RealEstateAPI.Helpers;
using RealEstateAPI.Models;

namespace RealEstateAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddCors();

			string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

			
			builder.Services.AddRealEstateContext(connection);
			builder.Services.AddUnitOfWorkService();
			builder.Services.AddScoped<IApartmentService, ApartmentServcie>();
			builder.Services.AddScoped<IClientService, ClientService>();
			builder.Services.AddScoped<IBookingService, BookingService>();
			builder.Services.AddScoped<JwtService>();
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			
			var app = builder.Build();
			app.UseCors(builder => builder.WithOrigins("http://localhost:3000")
											.AllowAnyHeader()
											.AllowAnyMethod()
											.AllowCredentials()
											);
			
			
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseStaticFiles();
			app.UseAuthorization();	
			app.UseAuthentication();
			app.UseHttpsRedirection();
			app.MapControllers();
			app.Run();
		}
	}
}
