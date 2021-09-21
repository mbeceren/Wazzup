using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;
using Wazzup.Identity;
using Wazzup.Messaging;

namespace Wazzup
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
			services.AddControllersWithViews().AddRazorRuntimeCompilation();
			services.AddSingleton<ISocketPool, SocketPool>(s => {
				var poolSize = Configuration.GetSection("SocketSettings:SocketPoolSize").Get<int>();
				return new SocketPool(poolSize);
			});
			services.AddScoped<ISocketPoolManager, SocketPoolManager>(s => {
				return new SocketPoolManager(s.GetRequiredService<ISocketPool>());
			});
			services.AddSingleton<IUserPool, UserPool>();
			
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}


			app.UseWebSockets(new WebSocketOptions
			{
				KeepAliveInterval = TimeSpan.FromSeconds(120)
			});

			app.Use(async (context, next) =>
			{
				if (context.Request.Path.StartsWithSegments("/messsage") && !context.WebSockets.IsWebSocketRequest)
				{
					context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
				}
				else
				{
					await next();
				}
			});

			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
