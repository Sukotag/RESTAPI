using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using RESTAPI.Models;
using RESTAPI.Service;
using Autofac;
using Autofac.Builder;
using Autofac.Extensions.DependencyInjection;

namespace RESTAPI
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		//public IServiceProvider ConfigureServices(IServiceCollection services)
		public void ConfigureServices(IServiceCollection services)
		{

			//var builder = new ContainerBuilder();
			//builder.Populate(services);


			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.RequireHttpsMetadata = false;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						// укзывает, будет ли валидироваться издатель при валидации токена
						ValidateIssuer = true,
						// строка, представляющая издателя
						ValidIssuer = AuthOptions.ISSUER,

						// будет ли валидироваться потребитель токена
						ValidateAudience = true,
						// установка потребителя токена
						ValidAudience = AuthOptions.AUDIENCE,
						// будет ли валидироваться время существования
						ValidateLifetime = true,

						// установка ключа безопасности
						IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
						// валидация ключа безопасности
						ValidateIssuerSigningKey = true,
					};
				});

			string con = "Server=I524379;Database=Agency;Trusted_Connection=True;MultipleActiveResultSets=True";
			services.AddEntityFrameworkSqlServer()
				.AddDbContext<RealtorContext>(options => options.UseSqlServer(con))
				.AddDbContext<DivisionContext>(options => options.UseSqlServer(con));

			services.Configure<CookiePolicyOptions>(options =>
			{				
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});


			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			//var container = builder.Build();
			//return new AutofacServiceProvider(container);
		}

		
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			//Перехват ошибок
			app.UseMiddleware<ErrorHandlingMiddleware>();

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseAuthentication();			

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
