﻿using Marathon.Models;
using Marathon.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

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
		public async Task<OkObjectResult> Update([FromBody]UpdateModel update)
		{
			await _botService.ExecuteAsync(update);
			return new OkObjectResult(update);
		}
	}
}
