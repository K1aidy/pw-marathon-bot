using Marathon.DataBase;
using Marathon.Implements.Services;
using Marathon.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Marathon.Extensions
{
	public static class StartUpExtensions
	{
		public static IServiceCollection AddDbContext(this IServiceCollection services)
		{
			var builder = new PostgreSqlConnectionStringBuilder(EnvironmentExtensions.GetDataBaseUrl())
			{
				Pooling = true,
				TrustServerCertificate = true,
				SslMode = SslMode.Require
			};

			services.AddEntityFrameworkNpgsql()
				.AddDbContext<MarathonContext>(options => options.UseNpgsql(builder.ConnectionString));

			return services;
		}

		public static IServiceCollection AddTelegramBot(this IServiceCollection services)
		{
			return services.AddSingleton<IBotService, BotService>();
		}
	}
}
