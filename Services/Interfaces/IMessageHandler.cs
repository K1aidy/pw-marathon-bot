using Marathon.Models;
using System.Threading.Tasks;

namespace Marathon.Services.Interfaces
{
	public interface IMessageHandler
	{
		Task ExecuteMessage(UpdateModel message);
	}
}
