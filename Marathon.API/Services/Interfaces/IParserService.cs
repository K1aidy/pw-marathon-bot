using Marathon.DataBase.Entities;
using System.Threading.Tasks;

namespace Marathon.Services.Interfaces
{
	public interface IParserService
	{
		Task<string> GetMarathonInfo(Account account);
	}
}
