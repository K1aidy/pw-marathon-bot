using System.Linq;
using Marathon.DataBase;
using Marathon.DataBase.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Marathon.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly MarathonContext _context;

		public UsersController(MarathonContext context)
		{
			_context = context ?? throw new System.ArgumentNullException(nameof(context));
		}

		// GET api/values
		[HttpGet("users")]
		public IQueryable<User> Get() =>
			_context.Users.AsNoTracking().Take(10);
	}
}
