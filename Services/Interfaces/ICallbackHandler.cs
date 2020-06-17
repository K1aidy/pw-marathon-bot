using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Marathon.Services.Interfaces
{
	public interface ICallbackHandler
	{
		Task ExecuteCallback(Update message);
	}
}
