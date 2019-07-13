using Marathon.DataBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Marathon
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			var connectionString =
#if DEBUG
				"Server=ec2-54-246-84-100.eu-west-1.compute.amazonaws.com;" +
				"Port=5432;" +
				"Database=d947dk4gm675m;" +
				"Userid=dlfwtdzdmcjozx;" +
				"Password=pwd;" +
				"Trust Server Certificate=true;" +
				"SslMode=Require;";
#else
			Environment.GetEnvironmentVariable("DATABASE_URL");
#endif

			services.AddDbContext<MarathonContext>(options =>
				options.UseNpgsql(connectionString));
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
