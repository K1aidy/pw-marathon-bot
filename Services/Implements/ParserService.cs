using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
			/*var accessToken = await GetAccessToken(login, password);
			var location = await GetLocation(accessToken);
			var mpop = await GetMpop(location);
			return await GetMaraphonInfo(mpop);*/

			var temp = "1563202984:034e42705c7376411905000017031f051c054f6c5150445e05190401041d425043425f49504b445e5e41145a545858194b44:sisunpisunov@mail.ru:";
			return await GetMaraphonInfo(temp);
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
				var content = new StringContent($"<?xml version=\"1.0\" encoding=\"UTF-8\"?><MrPage2 SessionKey=\"{access}\" Page=\"https://games.mail.ru/gamecenter/sdc/\"/>");

				content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

				client.DefaultRequestHeaders.Add("User-Agent", _userAgent);

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
			using (var handler = new HttpClientHandler())
			using (var client = new HttpClient(handler))
			{
				var response = await client.GetAsync(location);
				client.DefaultRequestHeaders.Add("User-Agent", _userAgent);

				response.EnsureSuccessStatusCode();

				var cookies = handler.CookieContainer.GetCookies(new System.Uri(location));

				return cookies["Mpop"].Value;
			}
		}
		private async Task<string> GetMaraphonInfo(string mpop)
		{
			using (var handler = new HttpClientHandler())
			using (var client = new HttpClient(handler) { MaxResponseContentBufferSize  = 100000})
			{
				handler.CookieContainer.Add(
				new System.Uri("https://pw.mail.ru/supermarathon.php"),
				new Cookie("Mpop", mpop));

				client.DefaultRequestHeaders.Add("User-Agent", _userAgent);

				var response = await client.GetAsync("https://pw.mail.ru/supermarathon.php");

				response.EnsureSuccessStatusCode();

				var html = await response.Content.ReadAsStringAsync();
				var doc = new HtmlDocument();
				doc.LoadHtml(html);
				return doc.GetElementbyId("content_body").InnerHtml;
			}
		}
	}
}
