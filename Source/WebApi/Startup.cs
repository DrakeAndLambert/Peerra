using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using DrakeLambert.Peerra.WebApi.Core.Repositories;
using DrakeLambert.Peerra.WebApi.Infrastructure;
using DrakeLambert.Peerra.WebApi.Infrastructure.Data;
using DrakeLambert.Peerra.WebApi.WebCore;
using DrakeLambert.Peerra.WebApi.WebCore.Authentication;
using DrakeLambert.Peerra.WebApi.WebCore.Authentication.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace DrakeLambert.Peerra.WebApi
{
    public class Startup
    {
        /// <summary>
        /// Creates a new instance with the given configuration.
        /// </summary>
        /// <param name="configuration">The configuration for the web host.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// The configuration for the web host.
        /// </summary>
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("testname");
            });

            services.AddIdentityCore<WebUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = nameof(Peerra.WebApi), Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    In = "header",
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = "apiKey"
                });

                options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });
            });

            var authOptions = Configuration.GetSection(nameof(AuthOptions));

            services.Configure<AuthOptions>(authOptions);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateActor = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = authOptions.Get<AuthOptions>().SecurityKey
                };
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
            });

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWebUserRepository, WebUserRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EFRepository<>));
            services.AddTransient(typeof(Core.ExternalServices.ILogger<>), typeof(Logger<>));
            services.AddScoped<IWebUserTokenService, WebUserTokenService>();
            services.AddScoped<ITokenFactory, TokenFactory>();
            services.AddScoped<ITokenValidator, TokenValidator>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHsts();
            app.UseHttpsRedirection();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", nameof(Peerra.WebApi) + " v1");
            });

            app.UseSwagger();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
