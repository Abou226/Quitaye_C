using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quitaye.Controllers.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Quitaye.Web
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
            var GoogleClient_Id = Configuration.GetSection("ConfigSettings")["GoogleClient_Id"].ToString();
            var GoogleClient_Secret = Configuration.GetSection("ConfigSettings")["GoogleClient_Secret"].ToString();
            var FacebookClient_Id = Configuration.GetSection("ConfigSettings")["FacebookClient_Id"].ToString();
            var FacebookClient_Secret = Configuration.GetSection("ConfigSettings")["FacebookClient_Secret"].ToString();
            services.AddAutoMapper(typeof(Startup));
            services.ConfigureSqlContext(Configuration);
            services.ConfigureRepositoryWrapper(Configuration);
            services.AddAuthentication();
            //.AddCookie("Cook")
            //.AddGoogle(config =>
            //{
            //    config.SignInScheme = "Cook";
            //    config.ClientId = GoogleClient_Id;
            //    config.ClientSecret = GoogleClient_Secret;
            //    config.SaveTokens = true;

            //    config.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "UserId");
            //    config.ClaimActions.MapJsonKey(ClaimTypes.Email, "EmailAddress", ClaimValueTypes.Email);
            //    config.ClaimActions.MapJsonKey(ClaimTypes.Surname, "Surname");
            //    config.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "GivenName");
            //    config.Scope.Add("profile");
            //    config.Events.OnCreatingTicket = (context) =>
            //    {
            //        var picture = context.User.GetProperty("picture").GetString();
            //        context.Identity.AddClaim(new Claim("picture", picture));

            //        return Task.CompletedTask;
            //    };
            //})
            //.AddFacebook(config =>
            //{
            //    config.SignInScheme = "Cook";
            //    config.ClientId = FacebookClient_Id;
            //    config.ClientSecret = FacebookClient_Secret;
            //    config.SaveTokens = true;
            //    config.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "UserId");
            //    config.ClaimActions.MapJsonKey(ClaimTypes.Email, "EmailAddress", ClaimValueTypes.Email);
            //    config.ClaimActions.MapJsonKey(ClaimTypes.Surname, "Surname");
            //    config.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "GivenName");
            //});
            services.AddControllers();
            services.AddControllers().AddNewtonsoftJson();
            services.AddRazorPages();
            services.AddRazorPages()
            .AddNewtonsoftJson();
            services.AddRazorPages();
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
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
