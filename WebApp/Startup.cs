using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Data;
using WebApp.Models;
using WebApp.Services;
using WebApp.Demo;

namespace WebApp
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
            // Database connection string.
            // Make sure to update the Password value below from "Your_password123" to your actual password.
            var connectionStr = "Server=db;Database=Test;User=sa;Password=DemoPassword.1;";

            // This line uses 'UseSqlServer' in the 'options' parameter
            // with the connection string defined above.
            // services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
            // services.AddDbContext<BloggingContext>(options => 
            //             //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")) 
            //             options.UseSqlServer(connectionStr) 
            //                 .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            //                 );
           
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionStr));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            // try
            // {
            //     using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
            //     .CreateScope())
            //     {
            //        serviceScope.ServiceProvider.GetService<BloggingContext>().Database.EnsureCreated();
            //         serviceScope.ServiceProvider.GetService<BloggingContext>().Database.Migrate();
            //     }
            // }
            // catch (Exception ex)
            // {
            //     throw ex;
            //     //Log.Error(ex, "Failed to migrate or seed database");
            // }
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

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
