using EmailService.DataAccess;
using EmailService.DataAccess.Attributes;
using EmailService.DataAccess.LiteDb;
using EmailService.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmailService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private IWebHostEnvironment CurrentEnvironment { get; set; }
        public static IConfiguration StaticConfig { get; private set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment currentEnvironment)
        {
            Configuration = configuration;
            CurrentEnvironment = currentEnvironment;
            StaticConfig = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            JWT.EnvName = CurrentEnvironment.EnvironmentName;

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "EmailService API",
                    Version = "v1",
                    Description = "Environment: " + CurrentEnvironment.EnvironmentName + "<br>" +
                    "Date: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                    Contact = new OpenApiContact
                    {
                        Name = "Meoww"
                    }
                });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.Last());
                OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter into field the word 'Bearer' following by space and JWT",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                };
                c.AddSecurityDefinition("Bearer", securityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      Array.Empty<string>()
                    }
                  });
            });

            // db
            services.AddScoped<ActionFilter>();
            services.Configure<LiteDbOptions>(Configuration.GetSection("LiteDbOptions"));
            services.AddSingleton<ILiteDbContext, LiteDbContext>();


            // service 
            services.AddTransient<IEmailLogicService, EmailLogicService>();
            //services.AddTransient<ILiteDbEmailService, LiteDbEmailService>();
            services.AddTransient<IRepoLiteDB, RepoLiteDB>();
            

            services.AddControllers();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Email service API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
