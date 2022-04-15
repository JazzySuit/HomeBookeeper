using HomeBookeper.Application;
using HomeBookeper.Application.Interfaces;
using HomeBookeper.Infrastructure.Identity;
using HomeBookeper.Infrastructure.Persistence;
using HomeBookeper.Infrastructure.Shared;
using HomeBookeper.WebApi.Extensions;
using HomeBookeper.WebApi.Services;

namespace HomeBookeper.WebApi;

public class Startup
{
	private IConfiguration _config { get; }
	public Startup(IConfiguration configuration)
	{
		_config = configuration;
	}
	public void ConfigureServices(IServiceCollection services)
	{
		services.AddApplicationLayer();
		services.AddIdentityInfrastructure(_config);
		services.AddPersistenceInfrastructure(_config);
		services.AddSharedInfrastructure(_config);
		services.AddSwaggerExtension();
		services.AddControllers();
		services.AddApiVersioningExtension();
		services.AddHealthChecks();
		services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}
		else
		{
			app.UseExceptionHandler("/Error");
			app.UseHsts();
		}
		app.UseHttpsRedirection();
		app.UseRouting();
		app.UseAuthentication();
		app.UseAuthorization();
		app.UseSwaggerExtension();
		app.UseErrorHandlingMiddleware();
		app.UseHealthChecks("/health");

		app.UseEndpoints(endpoints =>
		 {
			 endpoints.MapControllers();
		 });
	}
}
