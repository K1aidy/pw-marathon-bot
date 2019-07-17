using Marathon.DataBase;
using Marathon.Models;
using Marathon.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Marathon.Implements.Services
{
	public class BotService : IBotService
	{
		private readonly IParserService _parserService;
		private readonly TelegramBotClient _client;
		private readonly MarathonContext _context;

		public BotService(
			IParserService parserService,
			TelegramBotClient client,
			MarathonContext context)
		{
			_parserService = parserService ?? throw new System.ArgumentNullException(nameof(parserService));
			_client = client ?? throw new System.ArgumentNullException(nameof(client));
			_context = context ?? throw new System.ArgumentNullException(nameof(context));
		}

		public async Task ExecuteAsync(UpdateModel message)
		{
			if (message.Type == UpdateType.Message)
			{
				await ExecuteMessage(message);
			}
			if (message.Type == UpdateType.CallbackQuery)
			{
				await ExecuteCallback(message);
			}
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
				replyMarkup: keyBoard);
		}

		private async Task ExecuteMessage(UpdateModel message)
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

		private async Task ExecuteCallback(UpdateModel message)
		{
			throw new System.Exception(JsonConvert.SerializeObject(message));
			var from = message.CallBack.From.LastName;

			var chatFrom = message.CallBack.Message.Chat.Id;

			if (message.CallBack.Message.ReplyToMessage.Text.Equals("/accounts"))
			{
				var name = message.CallBack.Data;
				var user = await _context.Users.FirstAsync(u => u.Description == name);
				var answer = await _parserService.GetMarathonInfo(user.Email, user.HashPwd);

				await _client.SendTextMessageAsync(chatFrom, answer);
			}
		}
	}
}
