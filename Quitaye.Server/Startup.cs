using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Quitaye.Controllers.Extensions;
using Repository;

namespace Quitaye.Server
{
    public class Startup
    {
        public IWebHostEnvironment WebHostEnvironment { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            WebHostEnvironment = environment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //var GoogleClient_Id = Configuration.GetSection("ConfigSettings")["GoogleClient_Id"].ToString();
            //var GoogleClient_Secret = Configuration.GetSection("ConfigSettings")["GoogleClient_Secret"].ToString();
            //var FacebookClient_Id = Configuration.GetSection("ConfigSettings")["FacebookClient_Id"].ToString();
            //var FacebookClient_Secret = Configuration.GetSection("ConfigSettings")["FacebookClient_Secret"].ToString();
            //var googleCredential = _environment.ContentRootPath;
            //var filePath = Configuration.GetSection("GoogleFirebase")["fileName"];
            //googleCredential = Path.Combine(googleCredential, filePath);
            //var credential = GoogleCredential.FromFile(googleCredential);
            //FirebaseApp.Create(new AppOptions()
            //{

            //    Credential = credential
            //});
            services.AddAutoMapper(typeof(Startup));
            services.ConfigureSqlContext(Configuration);
            services.ConfigureRepositoryWrapper(Configuration, WebHostEnvironment);
            services.AddAuthentication();
            //services.AddNewtonsoftJson();
            services.AddControllers();
            services.AddControllers().AddNewtonsoftJson();
            services.AddSignalR();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Quitaye.Server", Version = "v1" });
            });

            var jwtsection = Configuration.GetSection("MySettings");
            services.Configure<ConfigSettings>(jwtsection);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quitaye.Server v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
