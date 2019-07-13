using Newtonsoft.Json;

namespace Marathon.Models
{
	public class UpdateModel
	{
		[JsonProperty("update_id")]
		public long UpdateId { get; set; }

		[JsonProperty("message")]
		public Message Message { get; set; }
	}
}
