using System.Linq;
using System.Threading.Tasks;
using Marathon.DataBase;
using Marathon.Models;
using Marathon.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Marathon.Services.Implements
{
	public class MessageHandler : IMessageHandler
	{
		private readonly MarathonContext _context;
		private readonly TelegramBotClient _client;

		public MessageHandler(
			MarathonContext context,
			TelegramBotClient client)
		{
			_context = context ?? throw new System.ArgumentNullException(nameof(context));
			_client = client ?? throw new System.ArgumentNullException(nameof(client));
		}

		public async Task ExecuteMessage(UpdateModel message)
		{
			var chatId = message.Message.Chat.Id;
			var messageId = message.Message.MessageId;

			if (message.Message.Text.Equals(Constants.ACCOUNTS)
				&& message.Message.From.LastName.StartsWith("Многолетов"))
			{
				await GetAccountsListAsync(chatId, messageId);
			}
		}

		private async Task GetAccountsListAsync(long chatId, int messageId)
		{
			var keyBoard = await GenerateAccountsList();

			await _client.SendTextMessageAsync(
				chatId,
				"Найдены следующие аккаунты",
				replyMarkup: keyBoard);
		}

		private async Task<InlineKeyboardMarkup> GenerateAccountsList()
		{
			var users = await _context.Users.AsNoTracking().ToListAsync();

			return new InlineKeyboardMarkup(users
				.Select(u => new InlineKeyboardButton[]
				{
					InlineKeyboardButton.WithCallbackData(
						u.Description,
						$"{Constants.ACCOUNTS}{Constants.SEPARATOR}{u.Description}")
				}));
		}
	}
}
