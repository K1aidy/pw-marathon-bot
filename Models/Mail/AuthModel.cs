using Newtonsoft.Json;

namespace Marathon.Models.Mail
{
	public class AuthModel
	{
		[JsonProperty("expires_in")]
		public int? ExpiresIn { get; set; }

		[JsonProperty("refresh_token")]
		public string RefreshToken { get; set; }

		[JsonProperty("access_token")]
		public string AccessToken { get; set; }

		[JsonProperty("token_type")]
		public string TokenType { get; set; }

		[JsonProperty("error")]
		public string Error { get; set; }

		[JsonProperty("error_description")]
		public string ErrorDescription { get; set; }

		[JsonProperty("tsa_token")]
		public string TsaToken { get; set; }

		[JsonProperty("error_code")]
		public int? ErrorCode { get; set; }

		[JsonProperty("length")]
		public int? Length { get; set; }

		[JsonProperty("timeout")]
		public int? Timeout { get; set; }
	}
}
