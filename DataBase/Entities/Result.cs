using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marathon.DataBase.Entities
{
	[Table("results", Schema = "marathon")]
	public class Result
	{
		[Key]
		[Column("id")]
		public int Id { get; set; }

		[Column("result")]
		public string Text { get; set; }

		[Column("dt")]
		public DateTime Dt { get; set; }

		[Column("accountid")]
		public int AccountId { get; set; }

		[ForeignKey(nameof(AccountId))]
		public virtual Account Account { get; set; }
	}
}
