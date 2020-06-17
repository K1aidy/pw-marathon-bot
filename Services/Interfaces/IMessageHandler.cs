using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Marathon.Services.Interfaces
{
	public interface IMessageHandler
	{
		Task ExecuteMessage(Update message);
	}
}
