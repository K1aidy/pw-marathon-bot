using Newtonsoft.Json;

namespace Marathon.Models
{
	public class MessageEntity
	{
		[JsonProperty("offset")]
		public int Offset { get; set; }

		[JsonProperty("length")]
		public int Length { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }
	}
}
