using Marathon.DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marathon.DataBase
{
	public class MarathonContext : DbContext
	{
		public MarathonContext(DbContextOptions<MarathonContext> options)
			: base(options)
		{ }

		public DbSet<User> Users { get; set; }
	}
}
