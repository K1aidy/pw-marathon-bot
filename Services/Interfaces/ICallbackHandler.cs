using Marathon.Models;
using System.Threading.Tasks;

namespace Marathon.Services.Interfaces
{
	public interface ICallbackHandler
	{
		Task ExecuteCallback(UpdateModel message);
	}
}
