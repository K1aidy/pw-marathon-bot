using Marathon.DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marathon.DataBase
{
	public class MarathonContext : DbContext
	{
		public DbSet<User> Users { get; set; }
	}
}
