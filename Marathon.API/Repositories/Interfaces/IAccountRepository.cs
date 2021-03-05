using Marathon.DataBase.Entities;
using Marathon.Models.Dto;
using System.Threading.Tasks;

namespace Marathon.Repositories.Interfaces
{
	/// <summary>
	/// Репозиторий управления учетками.
	/// </summary>
	public interface IAccountRepository
	{
		/// <summary>
		/// Получить аккаунт по id.
		/// </summary>
		/// <param name="name">Имя аккаунта.</param>
		/// <returns><see cref="Account"/></returns>
		Task<Account> GetAccountAsync(string name);

		/// <summary>
		/// Добавить новый аккаунт.
		/// </summary>
		/// <param name="accountInfo"><see cref="AccountInfo"/></param>
		/// <returns><see cref="AddAccountResult"/></returns>
		Task<AddAccountResult> AddAccountAsync(AccountInfo accountInfo);
	}
}
