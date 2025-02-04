using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.DLL.EF;

namespace RealEstate.BLL.Infrastructure
{
	public static class RealEstateContextExtensions
	{
		public static void AddRealEstateContext(this IServiceCollection services, string connection)
		{
			services.AddDbContext<RealEstateContext>(options => options.UseSqlServer(connection));
		}
	}
}
