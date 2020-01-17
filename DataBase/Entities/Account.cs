using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marathon.DataBase.Entities
{
	[Table("accounts", Schema = "marathon")]
	public class Account
	{
		[Key]
		[Column("id")]
		public int Id { get; set; }

		[Column("name")]
		public string Name { get; set; }

		[Column("access")]
		public string AccessToken { get; set; }

		[Column("refresh")]
		public string RefreshToken { get; set; }

		[Column("expiration")]
		public DateTime Expiration { get; set; }
	}
}
