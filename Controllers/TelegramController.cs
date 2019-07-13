﻿using Marathon.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Marathon.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TelegramController : ControllerBase
	{
		private readonly IBotService _botService;

		public TelegramController(IBotService botService)
		{
			_botService = botService ?? throw new System.ArgumentNullException(nameof(botService));
		}

		[HttpPost("update")]
		public async Task<OkObjectResult> Update([FromBody]object input)
		{
			if (input is Update update)
			{
				await _botService.ExecuteAsync(update.Message);
			}

			return new OkObjectResult(input);
		}
	}
}