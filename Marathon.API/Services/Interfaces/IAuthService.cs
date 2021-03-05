using Marathon.Models.Mail;
using System.Threading.Tasks;

namespace Marathon.Services.Interfaces
{
	/// <summary>
	/// Сервис аутентификации.
	/// </summary>
	public interface IAuthService
	{
		/// <summary>
		/// Аутентификация.
		/// </summary>
		/// <param name="email">Логин.</param>
		/// <param name="password">Пароль.</param>
		/// <returns><see cref="AuthModel"/></returns>
		Task<AuthModel> AuthAsync(string email, string password);
	}
}
