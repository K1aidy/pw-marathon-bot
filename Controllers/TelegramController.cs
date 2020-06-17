using Marathon.Services.Interfaces;
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
		public async Task<OkObjectResult> Update([FromBody]Update update)
		{
			await _botService.ExecuteAsync(update);
			return new OkObjectResult(update);
		}

		[HttpGet("healthcheck")]
		public OkResult Startup() => Ok();
	}
}
