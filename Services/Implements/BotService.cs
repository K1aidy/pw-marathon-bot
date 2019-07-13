using Marathon.Extensions;
using Marathon.Models;
using Marathon.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Marathon.Implements.Services
{
	public class BotService : IBotService
	{
		private readonly TelegramBotClient _client;
		private readonly IReadOnlyList<UpdateType> updateTypes = new UpdateType[]
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

		public BotService()
		{
			var token = EnvironmentExtensions.GetTelegramKey();
			var hookUrl = EnvironmentExtensions.GetWebHookUrl();

			_client = new TelegramBotClient(token);

			_client
				.SetWebhookAsync(
					hookUrl,
					maxConnections: 3,
					allowedUpdates: updateTypes)
				.ConfigureAwait(false);
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
