using Marathon.DataBase;
using Marathon.DataBase.Entities;
using Marathon.Models.Dto;
using Marathon.Repositories.Interfaces;
using Marathon.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Marathon.Repositories.Implements
{
	/// <inheritdoc />
	public class AccountRepository : IAccountRepository
	{
		private readonly MarathonContext _contex;
		private readonly IAuthService _authService;

		public AccountRepository(
			MarathonContext contex,
			IAuthService authService)
		{
			_contex = contex ?? throw new ArgumentNullException(nameof(contex));
			_authService = authService ?? throw new ArgumentNullException(nameof(authService));
		}

		/// <inheritdoc />
		public async Task<AddAccountResult> AddAccountAsync(AccountInfo accountInfo)
		{
			var auth = await _authService.AuthAsync(accountInfo.Email, accountInfo.Password);

			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public Task<Account> GetAccountAsync(string name) =>
			_contex.Accounts.AsNoTracking()
				.FirstOrDefaultAsync(a => a.Name == name) ??
					throw new ApplicationException($"Не найден аккаунт с Name = {name}");
	}
}
