﻿using Newtonsoft.Json;

namespace Marathon.Models
{
	public class From
	{
		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("is_bot")]
		public bool IsBot { get; set; }

		[JsonProperty("first_name")]
		public string FirstName { get; set; }

		[JsonProperty("last_name")]
		public string LastName { get; set; }

		[JsonProperty("language_code")]
		public string LanguageCode { get; set; }
	}
}