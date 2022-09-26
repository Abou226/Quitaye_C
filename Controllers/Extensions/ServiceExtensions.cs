using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;

namespace Quitaye.Controllers.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(configuration.GetConnectionString("DbConnection")));
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services, IConfiguration config, IWebHostEnvironment WebHostEnvironment)
        {
            var key = config.GetSection("ConfigSettings")["Key"].ToString();
            var hostName = config.GetSection("ConfigSettings")["HostName"].ToString();
            var GatoniniGoogleClient_Id = config.GetSection("ConfigSettings")["QuitayeGoogleAppId"].ToString();
            var GatoniniGoogleClient_Secret = config.GetSection("ConfigSettings")["QuitayeGoogleAppSecret"].ToString();
            var GatoniniFacebookClient_Id = config.GetSection("ConfigSettings")["QuitayeFacebookAppId"].ToString();
            var GatoniniFacebookClient_Secret = config.GetSection("ConfigSettings")["QuitayeFacebookAppSecret"].ToString();
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedProto;
            }); 
            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddCookie()
                .AddFacebook(fb =>
                {
                    fb.AppId = GatoniniFacebookClient_Id;
                    fb.AppSecret = GatoniniFacebookClient_Secret;
                    fb.SaveTokens = true;
                })
                .AddGoogle(g =>
                {
                    g.ClientId = GatoniniGoogleClient_Id;
                    g.ClientSecret = GatoniniGoogleClient_Secret;
                    g.SaveTokens = true;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = hostName,
                        ValidAudience = hostName,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                });

            //services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.Configure<ConfigSettings>(config.GetSection("ConfigSettings"));
            services.AddScoped<IConfigSettings, ConfigSettings>();
            services.AddScoped<IFacebook, FacebookService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IFileManager, FileManager>();
            services.AddAWSService<IAmazonS3>();
            //services.AddNewtonsoftJson();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped(typeof(IGenericRepository<,,>), typeof(GenericRepository<,,>));
            services.AddScoped(typeof(IGenericRepository<,,,>), typeof(GenericRepository<,,,>));
            services.AddScoped(typeof(IGenericRepository<,,,,>), typeof(GenericRepository<,,,,>));
            services.AddScoped(typeof(IGenericRepository<,,,,,>), typeof(GenericRepository<,,,,,>));
            services.AddScoped(typeof(IGenericRepository<,,,,,,>), typeof(GenericRepository<,,,,,,>));
            services.AddScoped(typeof(IGenericRepository<,,,,,,,>), typeof(GenericRepository<,,,,,,,>));
            services.AddScoped(typeof(IGenericRepository<,,,,,,,,>), typeof(GenericRepository<,,,,,,,,>));
            services.AddScoped(typeof(IGenericRepository<,,,,,,,,,>), typeof(GenericRepository<,,,,,,,,,>));
            services.AddScoped(typeof(IGenericRepository<,,,,,,,,,,>), typeof(GenericRepository<,,,,,,,,,,>));
            services.AddScoped(typeof(IGenericRepository<,,,,,,,,,,,>), typeof(GenericRepository<,,,,,,,,,,,>));
            services.AddScoped(typeof(IGenericRepository<,,,,,,,,,,,,>), typeof(GenericRepository<,,,,,,,,,,,,>));
            services.AddScoped(typeof(IGenericRepository<,,,,,,,,,,,,,>), typeof(GenericRepository<,,,,,,,,,,,,,>));
            services.AddScoped(typeof(IGenericRepository<,,,,,,,,,,,,,,>), typeof(GenericRepository<,,,,,,,,,,,,,,>));
            services.AddScoped(typeof(IGenericRepository<,,,,,,,,,,,,,,,>), typeof(GenericRepository<,,,,,,,,,,,,,,,>));
            services.AddScoped(typeof(IGenericRepository<,,,,,,,,,,,,,,,,>), typeof(GenericRepository<,,,,,,,,,,,,,,,,>));
            services.AddScoped(typeof(IGenericRepository<,,,,,,,,,,,,,,,,,>), typeof(GenericRepository<,,,,,,,,,,,,,,,,,>));
            
            services.AddScoped(typeof(IGenericController<>), typeof(GenericController<>));
            services.AddScoped(typeof(IGenericController<,>), typeof(GenericController<,>));
            services.AddScoped(typeof(IGenericController<,,>), typeof(GenericController<,,>));
            services.AddScoped(typeof(IGenericController<,,,>), typeof(GenericController<,,,>));
            services.AddScoped(typeof(IGenericController<,,,,>), typeof(GenericController<,,,,>)); 
            services.AddScoped(typeof(IGenericController<,,,,,>), typeof(GenericController<,,,,,>));
            services.AddScoped(typeof(IGenericController<,,,,,,>), typeof(GenericController<,,,,,,>));
            services.AddScoped(typeof(IGenericController<,,,,,,,>), typeof(GenericController<,,,,,,,>));
            services.AddScoped(typeof(IGenericController<,,,,,,,,>), typeof(GenericController<,,,,,,,,>));
            services.AddScoped(typeof(IGenericController<,,,,,,,,,>), typeof(GenericController<,,,,,,,,,>));
            services.AddScoped(typeof(IGenericController<,,,,,,,,,,>), typeof(GenericController<,,,,,,,,,,>));
            services.AddScoped(typeof(IGenericController<,,,,,,,,,,,>), typeof(GenericController<,,,,,,,,,,,>));
            services.AddScoped(typeof(IGenericController<,,,,,,,,,,,,>), typeof(GenericController<,,,,,,,,,,,,>));
            services.AddScoped(typeof(IGenericController<,,,,,,,,,,,,,>), typeof(GenericController<,,,,,,,,,,,,,>));
            services.AddScoped(typeof(IGenericController<,,,,,,,,,,,,,,>), typeof(GenericController<,,,,,,,,,,,,,,>));
            services.AddScoped(typeof(IGenericController<,,,,,,,,,,,,,,,>), typeof(GenericController<,,,,,,,,,,,,,,,>));
            services.AddScoped(typeof(IGenericController<,,,,,,,,,,,,,,,,>), typeof(GenericController<,,,,,,,,,,,,,,,,>));
            services.AddScoped(typeof(IGenericController<,,,,,,,,,,,,,,,,,>), typeof(GenericController<,,,,,,,,,,,,,,,,,>));
            
            services.AddScoped(typeof(IGenericRepositoryWrapper<>), typeof(GenericRepositoryWrapper<>));
            services.AddScoped(typeof(IGenericRepositoryWrapper<,>), typeof(GenericRepositoryWrapper<,>));
            services.AddScoped(typeof(IGenericRepositoryWrapper<,,>), typeof(GenericRepositoryWrapper<,,>));
            services.AddScoped(typeof(IGenericRepositoryWrapper<,,,>), typeof(GenericRepositoryWrapper<,,,>));
            services.AddScoped(typeof(IGenericRepositoryWrapper<,,,,>), typeof(GenericRepositoryWrapper<,,,,>));
            services.AddScoped(typeof(IGenericRepositoryWrapper<,,,,,>), typeof(GenericRepositoryWrapper<,,,,,>));
            services.AddScoped(typeof(IGenericRepositoryWrapper<,,,,,,>), typeof(GenericRepositoryWrapper<,,,,,,>));
            services.AddScoped(typeof(IGenericRepositoryWrapper<,,,,,,,>), typeof(GenericRepositoryWrapper<,,,,,,,>));
            services.AddScoped(typeof(IGenericRepositoryWrapper<,,,,,,,,>), typeof(GenericRepositoryWrapper<,,,,,,,,>));
            services.AddScoped(typeof(IGenericRepositoryWrapper<,,,,,,,,,>), typeof(GenericRepositoryWrapper<,,,,,,,,,>));
            services.AddScoped(typeof(IGenericRepositoryWrapper<,,,,,,,,,,>), typeof(GenericRepositoryWrapper<,,,,,,,,,,>));
            services.AddScoped(typeof(IGenericRepositoryWrapper<,,,,,,,,,,,>), typeof(GenericRepositoryWrapper<,,,,,,,,,,,>));
            services.AddScoped(typeof(IGenericRepositoryWrapper<,,,,,,,,,,,,>), typeof(GenericRepositoryWrapper<,,,,,,,,,,,,>));
            services.AddScoped(typeof(IGenericRepositoryWrapper<,,,,,,,,,,,,,>), typeof(GenericRepositoryWrapper<,,,,,,,,,,,,,>));
            services.AddScoped(typeof(IGenericRepositoryWrapper<,,,,,,,,,,,,,,>), typeof(GenericRepositoryWrapper<,,,,,,,,,,,,,,>));
            services.AddScoped(typeof(IGenericRepositoryWrapper<,,,,,,,,,,,,,,,>), typeof(GenericRepositoryWrapper<,,,,,,,,,,,,,,,>));
            services.AddScoped(typeof(IGenericRepositoryWrapper<,,,,,,,,,,,,,,,,>), typeof(GenericRepositoryWrapper<,,,,,,,,,,,,,,,,>));
            services.AddScoped(typeof(IGenericRepositoryWrapper<,,,,,,,,,,,,,,,,,>), typeof(GenericRepositoryWrapper<,,,,,,,,,,,,,,,,,>));
            
        }

        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            
        }
    }
}
