using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DrakeLambert.Peerra
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Adding MVC
            services.AddMvc()
                // This opts into behavior changes implemented in 2.1+
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
