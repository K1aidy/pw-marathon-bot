using Newtonsoft.Json;

namespace Marathon.Models
{
	public class Chat
	{
		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("first_name")]
		public string FirstName { get; set; }

		[JsonProperty("last_name")]
		public string LastName { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }
	}
}
