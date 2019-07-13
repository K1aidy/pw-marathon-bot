using Marathon.Extensions;
using Marathon.Models;
using Marathon.Services.Interfaces;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Marathon.Implements.Services
{
	public class BotService : IBotService
	{
		private readonly TelegramBotClient _client;

		public BotService()
		{
			var token = EnvironmentExtensions.GetTelegramKey();
			var hookUrl = EnvironmentExtensions.GetWebHookUrl();

			_client = new TelegramBotClient(token);

			_client.SetWebhookAsync(hookUrl, maxConnections: 1).ConfigureAwait(false);
		}

		public async Task ExecuteAsync(UpdateModel message)
		{
			var chatId = message.Message.Chat.Id;
			var messageId = message.Message.MessageId;

			await _client.SendTextMessageAsync(chatId, message.Message.Text, replyToMessageId: messageId);
		}

		public async Task SendMessageAsync(long chatId, string message)
		{
			await _client.SendTextMessageAsync(chatId, message);
		}
	}
}
