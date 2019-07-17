using Newtonsoft.Json;

namespace Marathon.Models
{
	public class CallBack
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("from")]
		public From From { get; set; }

		[JsonProperty("message")]
		public Message Message { get; set; }

		[JsonProperty("chat_instance")]
		public string ChatInstance { get; set; }

		[JsonProperty("data")]
		public string Data { get; set; }
	}
}
