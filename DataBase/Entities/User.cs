using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marathon.DataBase.Entities
{
	[Table("users", Schema = "marathon")]
	public class User
	{
		[Key]
		[Column("id")]
		public int Id { get; set; }

		[Column("email")]
		public string Email { get; set; }

		[Column("hashpwd")]
		public string HashPwd { get; set; }

		[Column("description")]
		public string Description { get; set; }
	}
}
