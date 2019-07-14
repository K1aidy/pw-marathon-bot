using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HtmlAgilityPack;
using Marathon.Models.Mail;
using Marathon.Services.Interfaces;

namespace Marathon.Services.Implements
{
	public class ParserService : IParserService
	{
		private readonly string _userAgent = 
			"Mozilla/5.0 (Windows NT 10.0; WOW64) " +
			"AppleWebKit/537.36 (KHTML, like Gecko) " +
			"Chrome/70.0.3538.77 Downloader/15100 " +
			"MailRuGameCenter/1510 Safari/537.36";

		public async Task<string> GetMarathonInfo(string login, string password)
		{
			var accessToken = await GetAccessToken(login, password);
			var location = await GetLocation(accessToken);
			var mpop = await GetMpop(location);
			return await GetMaraphonInfo(mpop);
		}

		private async Task<string> GetAccessToken(string login, string password)
		{
			using (var client = new HttpClient())
			{
				var dict = new Dictionary<string, string>();
				dict.Add("client_id", "gamecenter.mail.ru");
				dict.Add("grant_type", "password");
				dict.Add("username", login);
				dict.Add("password", password);

				var content = new FormUrlEncodedContent(dict);
				content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
				client.DefaultRequestHeaders.Add("User-Agent", _userAgent);

				var response = client.PostAsync(
					"https://o2.mail.ru/token",
					content).Result;
				response.EnsureSuccessStatusCode();
				var token = await response.Content.ReadAsAsync<AuthModel>();

				return token.AccessToken;
			}
		}

		private async Task<string> GetLocation(string access)
		{
			using (var client = new HttpClient())
			{
				var userAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.77 Downloader/15100 MailRuGameCenter/1510 Safari/537.36";

				var content = new StringContent($"<?xml version=\"1.0\" encoding=\"UTF-8\"?><MrPage2 SessionKey=\"{access}\" Page=\"https://games.mail.ru/gamecenter/sdc/\"/>");

				content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

				client.DefaultRequestHeaders.Add("User-Agent", userAgent);

				var response = await client.PostAsync(
					"https://authdl.mail.ru/ec.php?hint=MrPage2",
					content);

				response.EnsureSuccessStatusCode();

				var responseString = await response.Content.ReadAsStringAsync();

				var doc = new XmlDocument();
				doc.LoadXml(responseString);

				return doc.DocumentElement.Attributes["Location"].Value;
			}
		}

		private async Task<string> GetMpop(string location)
		{
			var userAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.77 Downloader/15100 MailRuGameCenter/1510 Safari/537.36";

			using (var handler = new HttpClientHandler())
			using (var client = new HttpClient(handler))
			{
				var response = await client.GetAsync(location);
				client.DefaultRequestHeaders.Add("User-Agent", userAgent);

				response.EnsureSuccessStatusCode();

				var cookies = handler.CookieContainer.GetCookies(new System.Uri(location));

				return cookies["Mpop"].Value;
			}
		}
		private async Task<string> GetMaraphonInfo(string mpop)
		{
			var userAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.77 Downloader/15100 MailRuGameCenter/1510 Safari/537.36";

			using (var handler = new HttpClientHandler())
			using (var client = new HttpClient(handler))
			{
				handler.CookieContainer.Add(
					new System.Uri("https://pw.mail.ru:443/supermarathon.php"),
					new Cookie("Mpop", mpop));

				client.DefaultRequestHeaders.Add("User-Agent", userAgent);

				var response = await client.GetByteArrayAsync("https://pw.mail.ru:443/supermarathon.php");

				var text = Encoding.UTF8.GetString(response);

				var doc = new HtmlDocument();
				doc.LoadHtml(text);
				return doc.GetElementbyId("content_body").InnerHtml;
			}
		}
	}
}
