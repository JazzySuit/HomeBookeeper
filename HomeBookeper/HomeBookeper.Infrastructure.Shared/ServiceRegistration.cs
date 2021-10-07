using HomeBookeper.Application.Interfaces;
using HomeBookeper.Domain.Settings;
using HomeBookeper.Infrastructure.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HomeBookeper.Infrastructure.Shared
{
	public static class ServiceRegistration
	{
		public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
		{
			services.Configure<MailSettings>(_config.GetSection("MailSettings"));
			services.AddTransient<IDateTimeService, DateTimeService>();
			services.AddTransient<IEmailService, EmailService>();
		}
	}
}
