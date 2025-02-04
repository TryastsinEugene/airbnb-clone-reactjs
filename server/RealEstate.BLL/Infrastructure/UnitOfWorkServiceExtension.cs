
using Microsoft.Extensions.DependencyInjection;
using RealEstate.DLL.Interfaces;
using RealEstate.DLL.Reposotories;

namespace RealEstate.BLL.Infrastructure
{
	public static class UnitOfWorkServiceExtension
	{
		public static void AddUnitOfWorkService(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
		}
	}
}
