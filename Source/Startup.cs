using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DrakeLambert.Peerra.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DrakeLambert.Peerra.Entities;
using DrakeLambert.Peerra.Services;

namespace DrakeLambert.Peerra
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        private readonly IHostingEnvironment _environment;

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<InitialDataOptions>(_configuration);

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                // TODO: Figure out real DB situation
                // if (_environment.IsDevelopment())
                // {
                options.UseInMemoryDatabase(nameof(ApplicationDbContext));
                // }
                // else
                // {
                //     options.Use[SOME OTHER DB PROVIDER]([CONNECTION STRING]);
                // }
            });
            services.AddTransient<DbInitializer>();

            services.AddDefaultIdentity<ApplicationUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication()
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = _configuration["Authentication:Google:ClientId"];
                    googleOptions.ClientSecret = _configuration["Authentication:Google:ClientSecret"];
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddTransient<IHelpRequestService, HelpRequestService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (_environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
