using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
		[HttpGet()]
		public async Task<IEnumerable<User>> Get() =>
			await _context.Users.AsNoTracking().ToListAsync();
	}
}
