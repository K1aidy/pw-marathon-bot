using Marathon.DataBase;
using Marathon.Implements.Services;
using Marathon.Repositories.Implements;
using Marathon.Repositories.Interfaces;
using Marathon.Services.Implements;
using Marathon.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

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
			var updateTypes = new UpdateType[]
			{
				UpdateType.CallbackQuery,
				UpdateType.ChannelPost,
				UpdateType.ChosenInlineResult,
				UpdateType.EditedChannelPost,
				UpdateType.EditedMessage,
				UpdateType.InlineQuery,
				UpdateType.Message,
				UpdateType.Poll,
				UpdateType.PreCheckoutQuery,
				UpdateType.ShippingQuery,
				UpdateType.Unknown
			};

			var token = EnvironmentExtensions.GetTelegramKey();
			var hookUrl = EnvironmentExtensions.GetWebHookUrl();

			var client = new TelegramBotClient(token);

			client.SetWebhookAsync(
					hookUrl,
					maxConnections: 3,
					allowedUpdates: updateTypes)
				.ConfigureAwait(false);

			return services
				.AddSingleton(client);
		}
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			return services
				.AddTransient<IBotService, BotService>()
				.AddTransient<IAuthService, AuthService>()
				.AddTransient<IParserService, ParserService>()
				.AddTransient<IMessageHandler, MessageHandler>()
				.AddTransient<ICallbackHandler, CallbackHandler>()
				.AddTransient<IMarathonRepository, MarathonRepository>()
				.AddTransient<IAccountRepository, AccountRepository>();
		}

	}
}
