using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodSugarLog.DL;
using BloodSugarLog.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BloodSugarLog
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BloodSugarDbContext>(apt => apt.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<ApplicationUser>().AddEntityFrameworkStores<BloodSugarDbContext>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            //   app.UseAuthorization();

            app.UseMvc(routes =>
            {     
                routes.MapRoute(
               "Default",                                              // Route name
               "{controller}/{action}/{id?}",                      // URL with parameters
               new { controller = "Account", action = "Register", id = ""});
              
            });
        }
    }
}
