using Marathon.Models.Mail;
using Marathon.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Marathon.Services.Implements
{
	/// <inheritdoc />
	public class AuthService : IAuthService
	{
		private readonly string _userAgent =
			"Mozilla/5.0 (Windows NT 10.0; WOW64) " +
			"AppleWebKit/537.36 (KHTML, like Gecko) " +
			"Chrome/70.0.3538.77 Downloader/15100 " +
			"MailRuGameCenter/1510 Safari/537.36";

		/// <inheritdoc />
		public async Task<AuthModel> AuthAsync(string email, string password)
		{
			using (var client = new HttpClient())
			{
				var dict = new Dictionary<string, string>();
				dict.Add("client_id", "gamecenter.mail.ru");
				dict.Add("grant_type", "password");
				dict.Add("username", email);
				dict.Add("password", password);

				var content = new FormUrlEncodedContent(dict);
				content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
				client.DefaultRequestHeaders.Add("User-Agent", _userAgent);

				var response = client.PostAsync(
					"https://o2.mail.ru/token",
					content).Result;
				response.EnsureSuccessStatusCode();
				return await response.Content.ReadAsAsync<AuthModel>();
			}
		}
	}
}
