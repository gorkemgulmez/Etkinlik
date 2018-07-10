using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Etkinlik
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer("Server=.;Database=StajEtkinlik;Trusted_Connection=True;MultipleActiveResultSets=true"));
                //options.UseSqlServer("Server=.;Database=etkinlik;Trusted_Connection=True;MultipleActiveResultSets=true"));
                //options.UseSqlServer("Server=.\\SQLEXPRESS;Database=etkinlik;Trusted_Connection=True;MultipleActiveResultSets=true"));

            //Cookie based user auth
            services.AddIdentity<UserModel, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                //generate unique keys and links for: forgot password, verification etc.
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options => {
                //Password
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

                //Other
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/login";
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            IoCContainer.Provider = (ServiceProvider)serviceProvider;

            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
