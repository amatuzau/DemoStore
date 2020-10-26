using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Store.App.Core;
using Store.Core;
using Store.DAL;
using Store.DAL.Ado;
using Store.DAL.Models;

namespace Store.App
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
            services.AddControllersWithViews();

            services.Configure<AdoOptions>(Configuration.GetSection(nameof(AdoOptions)));
            
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<ICartService, CartService>();

            var dataSource = Configuration.GetValue<string>("DataSource");
            
            switch (dataSource)
            {
                case "File":
                    services.AddScoped(s => new UnitOfWork(@"d:/data"));
                    break;
                case "Ado":
                    //services.AddScoped<IRepository<Category>, CategoriesAdoRepository>();
                    //services.AddScoped<IRepository<Product>, ProductsAdoRepository>();
                    //services.AddScoped<IRepository<Cart>, CartAdoRepository>();
                    break;
                case "EF":
                    services.AddDbContext<StoreContext>(o =>
                    {
                        o.UseSqlServer(Configuration.GetConnectionString("StoreConnection"))
                            .EnableSensitiveDataLogging();
                    });
                    break;
            }
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}