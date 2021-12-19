using EfData.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NotificationsApp.Domain.ServicesContract;
using NotificationsApp.Infrastructure.Services;
using NotificationsApp.Infrastructure.Token;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace NotificationsApp.API
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            #region add services

            services.AddScoped<IAcceptNotification, AcceptNotificationService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IDictionaryService, DictionaryService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IMailSenderService, MailSenderService>();

            #endregion

            #region add JWT authentication

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.ISSUER,
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.AUDIENCE,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                    options.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = c => { return Task.CompletedTask; },
                        OnTokenValidated = c => { return Task.CompletedTask; }
                    };
                });

            #endregion

            #region add metric context

            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseMySql(_configuration.GetConnectionString("ApplicationConnectionString"),
                    new MySqlServerVersion(new Version(8, 0, 23)));
                // options.UseLoggerFactory(EfNlogFactory);
            });

            #endregion

            #region add helth check

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationContext>("DB check");

            #endregion

            services.AddControllers();

            #region add cors

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader()

                        );
            });

            #endregion

            #region add swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Notifications App",
                    Version = "v1",
                    Description = "Web API for Notifications App",
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            #endregion

            #region add spa

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "clientWebApp";
            });

            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseExceptionHandler("/error");
            // app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/error");

            #region use spa

            app.UseSpaStaticFiles();

            #endregion

            #region use swagger

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MilkShakeApp.API v1");
                c.RoutePrefix = "swagger";
            });

            #endregion

            #region use cors

            app.UseCors("CorsPolicy");

            #endregion

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../client-app";

                //if (env.EnvironmentName == "Development" &&
                //Environment.GetEnvironmentVariable("TYPE_WEB") == "FullApp")
                //{
                //    spa.UseAngularCliServer(npmScript: "start");
                //}

            });
        }
    }
}
