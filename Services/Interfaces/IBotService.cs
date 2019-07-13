using Marathon.Models;
using System.Threading.Tasks;

namespace Marathon.Services.Interfaces
{
	public interface IBotService
	{
		Task ExecuteAsync(UpdateModel message);

		Task SendMessageAsync(long chatId, string message);
	}
}
