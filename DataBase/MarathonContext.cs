using Marathon.DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marathon.DataBase
{
	public class MarathonContext : DbContext
	{
		public MarathonContext(DbContextOptions<MarathonContext> options)
			: base(options)
		{ }

		public virtual DbSet<User> Users { get; set; }

		public virtual DbSet<Result> Results { get; set; }

		public virtual DbSet<Account> Accounts { get; set; }

		public virtual DbSet<Request> Requests { get; set; }
	}
}
