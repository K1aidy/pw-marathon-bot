using System.Threading.Tasks;

namespace Marathon.Services.Interfaces
{
	public interface IParserService
	{
		Task<string> GetMarathonInfo(string login, string password);
	}
}
