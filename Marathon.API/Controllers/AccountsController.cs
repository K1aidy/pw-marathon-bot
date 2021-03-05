using System.Threading.Tasks;
using Marathon.Models.Dto;
using Marathon.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Marathon.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly IAccountRepository _accountRepository;

		public AccountsController(IAccountRepository accountRepository)
		{
			_accountRepository = accountRepository ?? throw new System.ArgumentNullException(nameof(accountRepository));
		}

		[HttpPost("add")]
		public Task<AddAccountResult> AddAccountAsync(
			[FromBody]AccountInfo accountInfo) =>
				_accountRepository.AddAccountAsync(accountInfo);
	}
}
