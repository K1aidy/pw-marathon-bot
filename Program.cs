using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace pw_marathon_bot
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var config = new ConfigurationBuilder().AddCommandLine(args).Build();
			var host = new WebHostBuilder()
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseConfiguration(config)
				.UseIISIntegration()
				.UseStartup<Startup>()
				.Build();

			host.Run();
		}
	}
}
