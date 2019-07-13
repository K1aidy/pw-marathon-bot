using Newtonsoft.Json;

namespace Marathon.Models
{
	public class Message
	{
		[JsonProperty("chat")]
		public Chat Chat { get; set; }

		[JsonProperty("from")]
		public From From { get; set; }

		[JsonProperty("message_id")]
		public int MessageId { get; set; }

		[JsonProperty("date")]
		public long Date { get; set; }

		[JsonProperty("text")]
		public string Text { get; set; }
	}
}
