using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Services;
namespace WebApplication1
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets("aspnet-WebApplication1-bfd5aab4-41c9-4287-ab8f-9e3dcb1da809");
            }
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddUserStore<ApplicationUserStore>()
                .AddUserManager<ApplicationUserManager>()
                .AddRoleStore<ApplicationRoleStore>()
                .AddRoleManager<ApplicationRoleManager>()
                .AddSignInManager<ApplicationSignInManager>()
              // You **cannot** use .AddEntityFrameworkStores() when you customize everything
              //.AddEntityFrameworkStores<ApplicationDbContext, int>()
              .AddDefaultTokenProviders();
            //./*AddEntityFrameworkStores<AppDbContext, int>()*/
            //.AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            //services.AddScoped<UserStore<ApplicationUser, ApplicationRole, AppDbContext, int, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationUserToken, ApplicationRoleClaim>, ApplicationUserStore>();
            services.AddScoped<UserManager<ApplicationUser>, ApplicationUserManager>();
            services.AddScoped<RoleManager<ApplicationRole>, ApplicationRoleManager>();
            services.AddScoped<SignInManager<ApplicationUser>, ApplicationSignInManager>();
            services.AddScoped<RoleStore<ApplicationRole, AppDbContext, int, ApplicationUserRole, ApplicationRoleClaim>, ApplicationRoleStore>();
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}