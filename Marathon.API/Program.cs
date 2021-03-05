using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace Marathon
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			Console.WriteLine(Environment.GetEnvironmentVariable("PORT"));
			await CreateWebHostBuilder(args).Build().RunAsync();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.UseUrls($"http://*:{Environment.GetEnvironmentVariable("PORT")}");
	}
}
