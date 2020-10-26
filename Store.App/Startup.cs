using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Antiforgery;
using FluentValidation.AspNetCore;

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
            services.AddMvc().AddFluentValidation(f => f.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.Configure<AdoOptions>(Configuration.GetSection(nameof(AdoOptions)));
            
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<CartIdHandler>();

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

            services.AddDefaultIdentity<StoreUser>().AddEntityFrameworkStores<StoreContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.Configure<AntiforgeryOptions>(opts =>
            {
                opts.FormFieldName = "StoreSecretInput";
                opts.HeaderName = "X-CSRF-TOKEN";
                opts.SuppressXFrameOptionsHeader = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
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

            app.UseMiddleware<CartIdHandler>();

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