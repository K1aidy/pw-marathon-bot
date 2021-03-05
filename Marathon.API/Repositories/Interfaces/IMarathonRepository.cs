using Marathon.DataBase.Entities;
using System.Threading.Tasks;

namespace Marathon.Repositories.Interfaces
{
	/// <summary>
	/// Управление объектами модуля 'Марафон'.
	/// </summary>
	public interface IMarathonRepository
	{
		/// <summary>
		/// Добавить/изменить результат.
		/// </summary>
		/// <param name="accountId">Id аккаунта.</param>
		/// <param name="text">Результат.</param>
		/// <returns><see cref="Result"/></returns>
		Task<Result> AddResult(int accountId, string text);
	}
}
