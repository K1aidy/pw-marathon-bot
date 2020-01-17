using Marathon.Models;
using Marathon.Services.Interfaces;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;

namespace Marathon.Implements.Services
{
	public class BotService : IBotService
	{
		private readonly IMessageHandler _messageHandler;
		private readonly ICallbackHandler _callbackHandler;

		public BotService(
			IMessageHandler messageHandler,
			ICallbackHandler callbackHandler)
		{
			_messageHandler = messageHandler ?? throw new System.ArgumentNullException(nameof(messageHandler));
			_callbackHandler = callbackHandler ?? throw new System.ArgumentNullException(nameof(callbackHandler));
		}

		public async Task ExecuteAsync(UpdateModel message)
		{
			if (message.Type == UpdateType.Message)
			{
				await _messageHandler.ExecuteMessage(message);
			}
			if (message.Type == UpdateType.CallbackQuery)
			{
				await _callbackHandler.ExecuteCallback(message);
			}
		}
	}
}
