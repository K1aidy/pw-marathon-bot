using Marathon.DataBase;
using Marathon.Models;
using Marathon.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Marathon.Implements.Services
{
	public class BotService : IBotService
	{
		private readonly TelegramBotClient _client;
		private readonly MarathonContext _context;

		public BotService(
			TelegramBotClient client,
			MarathonContext context)
		{
			_client = client ?? throw new System.ArgumentNullException(nameof(client));
			_context = context ?? throw new System.ArgumentNullException(nameof(context));
		}

		public async Task ExecuteAsync(UpdateModel message)
		{
			var chatId = message.Message.Chat.Id;
			var messageId = message.Message.MessageId;

			if (message.Message.Text.Equals("/accounts")
				&& message.Message.From.LastName.StartsWith("Многолетов"))
			{
				await GetAccountsListAsync(chatId, messageId);
			}

			await _client.SendTextMessageAsync(
				chatId,
				message.Message.Text,
				replyToMessageId: messageId);
		}

		public async Task SendMessageAsync(long chatId, string message)
		{
			await _client.SendTextMessageAsync(chatId, message);
		}

		private async Task GetAccountsListAsync(long chatId, int messageId)
		{
			var users = await _context.Users.AsNoTracking().ToListAsync();

			var keyBoard = new InlineKeyboardMarkup(users
				.Select(u => new InlineKeyboardButton[]
				{
					InlineKeyboardButton.WithCallbackData(u.Description)
				}));

			await _client.SendTextMessageAsync(
				chatId,
				"Найдены следующие аккаунты",
				replyToMessageId: messageId,
				replyMarkup: keyBoard);
		}
	}
}
