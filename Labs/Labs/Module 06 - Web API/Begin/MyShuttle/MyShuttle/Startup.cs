using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyShuttle.Data;
using MyShuttle.Model;
using MyShuttle.Web.AppBuilderExtensions;

namespace MyShuttle.Web
{
	public class Startup
	{
		public IConfiguration Configuration { get; private set; }

		public Startup(IWebHostEnvironment env)
		{
			var config = new ConfigurationBuilder()
				.AddJsonFile("config.json", optional: true)
				.SetBasePath(env.ContentRootPath)
				.Build();

			Configuration = config;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.ConfigureDataContext(Configuration);

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<MyShuttleContext>()
				.AddDefaultTokenProviders();

			services.ConfigureDependencies();
			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app)
		{
            app.UseRouting();
			app.ConfigureRoutes();
			app.UseStaticFiles();
		}
	}
}