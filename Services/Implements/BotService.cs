using Marathon.Extensions;
using Marathon.Services.Interfaces;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

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

			_client.SetWebhookAsync(hookUrl).ConfigureAwait(false);
		}

		public async Task ExecuteAsync(Message message)
		{
			var chatId = message.Chat.Id;
			var messageId = message.MessageId;

			await _client.SendTextMessageAsync(chatId, message.Text, replyToMessageId: messageId);
		}

		public async Task SendMessageAsync(long chatId, string message)
		{
			await _client.SendTextMessageAsync(chatId, message);
		}
	}
}
