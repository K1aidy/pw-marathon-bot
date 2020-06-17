using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Marathon.Services.Interfaces
{
	public interface IBotService
	{
		Task ExecuteAsync(Update message);
	}
}
