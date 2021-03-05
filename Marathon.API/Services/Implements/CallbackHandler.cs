using Marathon.DataBase;
using Marathon.Models;
using Marathon.Repositories.Interfaces;
using Marathon.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Marathon.Services.Implements
{
	public class CallbackHandler : ICallbackHandler
	{
		private readonly IParserService _parserService;
		private readonly TelegramBotClient _client;
		private readonly IAccountRepository _accountRepository;
		private readonly MarathonContext _context;

		public CallbackHandler(
			IParserService parserService,
			TelegramBotClient client,
			IAccountRepository accountRepository,
			MarathonContext context)
		{
			_parserService = parserService ?? throw new System.ArgumentNullException(nameof(parserService));
			_client = client ?? throw new System.ArgumentNullException(nameof(client));
			_accountRepository = accountRepository ?? throw new System.ArgumentNullException(nameof(accountRepository));
			_context = context ?? throw new System.ArgumentNullException(nameof(context));
		}

		public async Task ExecuteCallback(Update message)
		{
			var from = message.CallbackQuery.From.LastName;

			var chatFrom = message.CallbackQuery.Message.Chat.Id;

			if (message.CallbackQuery.Data.StartsWith(Constants.ACCOUNTS))
			{
				var name = message.CallbackQuery.Data.Split(Constants.SEPARATOR).Last();

				var account = await _accountRepository.GetAccountAsync(name);

				var answer = await _parserService.GetMarathonInfo(account);

				var keyBoard = await GenerateAccountsList();

				await _client.EditMessageTextAsync(
					new ChatId(chatFrom),
					message.CallbackQuery.Message.MessageId,
					answer,
					replyMarkup: keyBoard);
			}
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
