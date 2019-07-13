using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Marathon.DataBase;
using Marathon.DataBase.Entities;
using Marathon.Extensions;
using Marathon.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Marathon.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly MarathonContext _context;
		private readonly IBotService _botService;

		public UsersController(
			MarathonContext context,
			IBotService botService)
		{
			_context = context ?? throw new System.ArgumentNullException(nameof(context));
			_botService = botService ?? throw new System.ArgumentNullException(nameof(botService));
		}

		// GET api/values
		[HttpGet()]
		public async Task<IEnumerable<User>> Get() =>
			await _context.Users.AsNoTracking().ToListAsync();

		[HttpGet("test/{text}")]
		public async Task<ActionResult<string>> Test([FromRoute]string text)
		{
			await _botService.SendMessageAsync(-328511448, text);

			return await Task.FromResult(EnvironmentExtensions.GetWebHookUrl());
		}

		[HttpGet("test2")]
		public async Task<ActionResult<string>> Test2()
		{
			using (var client = new HttpClient())
			{
				var response = await client.GetAsync($"https://api.telegram.org/bot{EnvironmentExtensions.GetTelegramKey()}/getWebhookInfo");
				response.EnsureSuccessStatusCode();
				return await response.Content.ReadAsStringAsync();
			}
		}
	}
}
