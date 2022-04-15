using HomeBookeper.Application.Interfaces;
using HomeBookeper.Application.Interfaces.Repositories;
using HomeBookeper.Infrastructure.Persistence.Contexts;
using HomeBookeper.Infrastructure.Persistence.Repositories;
using HomeBookeper.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HomeBookeper.Infrastructure.Persistence;

public static class ServiceRegistration
{
	public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		if (configuration.GetValue<bool>("UseInMemoryDatabase"))
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseInMemoryDatabase("ApplicationDb"));
		}
		else
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					configuration.GetConnectionString("DefaultConnection"),
					b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
		}

		#region Repositories
		services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
		services.AddTransient<IProductRepositoryAsync, ProductRepositoryAsync>();

		services.AddTransient<IBookRepositoryAsync, BookRepositoryAsync>();

		#endregion
	}
}
