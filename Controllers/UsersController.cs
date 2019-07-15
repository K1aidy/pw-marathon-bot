using System;
using System.Collections.Generic;
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
		private readonly IParserService _parserService;

		public UsersController(
			MarathonContext context,
			IParserService parserService)
		{
			_context = context ?? throw new System.ArgumentNullException(nameof(context));
			_parserService = parserService ?? throw new System.ArgumentNullException(nameof(parserService));
		}

		[HttpGet()]
		public async Task<IEnumerable<User>> Get() =>
			await _context.Users.AsNoTracking().ToListAsync();

		[HttpGet("add")]
		public async Task<bool> AddUser(
			[FromQuery]string login,
			[FromQuery]string password,
			[FromQuery]string description)
		{
			var newUser = new User
			{
				Email = login,
				HashPwd = password.Encrypt(EnvironmentExtensions.GetSecret()),
				Description = description
			};

			await _context.Users.AddAsync(newUser);

			var isAdded = await _context.SaveChangesAsync();

			return isAdded > 0;
		}

		[HttpGet("info/{name}")]
		public async Task<ActionResult<string>> Test([FromRoute]string name)
		{
			var user = await _context.Users.FirstAsync(u => u.Description == name);

			return await _parserService.GetMarathonInfo(user.Email, user.HashPwd);
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
