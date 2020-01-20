using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportStore.Core;
using SportStore.Core.Entities;
using SportStore.Infrastructure;
using SportStore.Infrastructure.Data;
using SportStore.Web.Models;

namespace SportStore.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext();
            services.AddControllersWithViews();
            // Transient: new for every service call
            services.AddTransient<IRepository<Product>, EfRepository<Product>>();
            services.AddTransient<IRepository<Order>, OrderEfRepository>();
            // Scoped: new for every request
            services.AddScoped(SessionCart.GetCart);
            // Singleton: shared between requests
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAutoMapper(typeof(Startup));
            services.AddMemoryCache();
            services.AddSession();
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    null,
                    "{category}/Page{productPage:int}",
                    new {controller = "Product", action = "List"}
                );

                endpoints.MapControllerRoute(
                    null,
                    "Page{productPage:int}",
                    new
                    {
                        controller = "Product",
                        action = "List",
                        productPage = 1
                    }
                );
                endpoints.MapControllerRoute(
                    null,
                    "{category}",
                    new
                    {
                        controller = "Product",
                        action = "List",
                        productPage = 1
                    }
                );
                endpoints.MapControllerRoute(
                    null,
                    "",
                    new
                    {
                        controller = "Product",
                        action = "List",
                        productPage = 1
                    });

                endpoints.MapControllerRoute(null, "{controller}/{action}/{id?}");
            });
        }
    }
}