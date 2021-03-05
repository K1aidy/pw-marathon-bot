using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marathon.DataBase.Entities
{
	[Table("requests", Schema = "marathon")]
	public class Request
	{
		[Key]
		[Column("id")]
		public int Id { get; set; }

		[Column("tsa")]
		public string Tsa { get; set; }

		[Column("name")]
		public string Name { get; set; }
	}
}
