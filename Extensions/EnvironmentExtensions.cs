using System;

namespace Marathon.Extensions
{
	public static class EnvironmentExtensions
	{
		public static string GetDataBaseUrl() =>
			Environment.GetEnvironmentVariable("DATABASE_URL")
				?? throw new ApplicationException("Не найдена переменная среды DATABASE_URL");

		public static string GetWebHookUrl() =>
			Environment.GetEnvironmentVariable("WebHookUrl")
				?? throw new ApplicationException("Не найдена переменная среды WebHookUrl");

		public static string GetTelegramKey() =>
			Environment.GetEnvironmentVariable("TelegramKey")
				?? throw new ApplicationException("Не найдена переменная среды TelegramKey");

		public static string GetProxy() =>
			Environment.GetEnvironmentVariable("Proxy")
				?? throw new ApplicationException("Не найдена переменная среды Proxy");

		public static string GetSecret() =>
			Environment.GetEnvironmentVariable("Secret")
				?? throw new ApplicationException("Не найдена переменная среды Secret");

		public static string GetSocks5Host() => Environment.GetEnvironmentVariable("Socks5Host");

		public static int GetSocks5Port()
		{
			var portStr = Environment.GetEnvironmentVariable("Socks5Port");

			if (!int.TryParse(portStr, out var port))
			{
				return default;
			}

			return port;
		}
	}
}
