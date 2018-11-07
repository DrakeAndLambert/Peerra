using System;
using System.IO;
using System.Reflection;
using DrakeLambert.Peerra.WebApi.Core.Data;
using DrakeLambert.Peerra.WebApi.Core.Entities;
using DrakeLambert.Peerra.WebApi.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace DrakeLambert.Peerra.WebApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase(nameof(ApplicationDbContext));
            });

            services.AddIdentityCore<ApplicationUser>(options =>
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
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase(nameof(ApplicationDbContext));
            });

            services.AddTransient(typeof(IAsyncRepository<,>), typeof(EFRepository<,>));

            services.AddAuthentication().AddJwtBearer(options =>
            {
                
            });
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

            app.UseMvc();
        }
    }
}
