using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HtmlAgilityPack;
using Marathon.Extensions;
using Marathon.Models.Mail;
using Marathon.Services.Interfaces;
using Newtonsoft.Json;

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
			var html = await GetMaraphonInfo(mpop);
			return ParseHtml(html);
		}

		private async Task<string> GetAccessToken(string login, string password)
		{
			using (var client = new HttpClient())
			{
				var dict = new Dictionary<string, string>();
				dict.Add("client_id", "gamecenter.mail.ru");
				dict.Add("grant_type", "password");
				dict.Add("username", login);
				dict.Add("password", password.Decrypt(EnvironmentExtensions.GetSecret()));

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
			using (var handler = new HttpClientHandler()
			{
				Proxy = new WebProxy(EnvironmentExtensions.GetProxy(), false),
				PreAuthenticate = true,
				UseDefaultCredentials = false,
			})
			using (var client = new HttpClient(handler))
			{
				handler.CookieContainer.Add(
					new Uri("https://pw.mail.ru/supermarathon.php"),
					new Cookie("Mpop", mpop));

				client.DefaultRequestHeaders.Add("User-Agent", _userAgent);

				var response = await client.GetAsync("https://pw.mail.ru/supermarathon.php");

				response.EnsureSuccessStatusCode();

				return await response.Content.ReadAsStringAsync();
			}
		}

		private string ParseHtml(string html)
		{
			var doc = new HtmlDocument();
			doc.LoadHtml(html);

			var titles = doc.DocumentNode
				.SelectNodes("//div[@class='info']")
				.Select(n => n.ChildNodes.First(cn => cn.OriginalName.Equals("b")).InnerText)
				.ToList();

			var progresses = doc.DocumentNode
				.SelectNodes("//div[@class='progress']")
				.Select(d => d.InnerText)
				.ToList();

			var result = titles.Zip(progresses, (title, progress) =>
				new MarathonResult
				{
					Title = title,
					Result = progress
				});

			var stringBuilder = new StringBuilder()
				.AppendLine("|Квест|Выполнено|")
				.AppendLine("|---|---|");

			foreach (var item in result)
			{
				stringBuilder.AppendLine($"|{item.Title}|{item.Result}|");
			}

			return stringBuilder.ToString();
		}
	}
}
