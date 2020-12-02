using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Store.App.Core;
using Store.App.Filters;
using Store.App.Identity;
using Store.App.Mapper;
using Store.Core;
using Store.Core.Queries;
using Store.DAL;
using Store.DAL.Models;
using WebApiContrib.Core.Formatter.Csv;

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
            services.AddMvc()
                .AddFluentValidation(f => f.RegisterValidatorsFromAssemblyContaining<Startup>())
                .AddXmlDataContractSerializerFormatters()
                .AddCsvSerializerFormatters()
                .AddMvcOptions(opts =>
                {
                    // opts.Filters.Add(typeof(CacheFilterAttribute));
                    opts.FormatterMappings.SetMediaTypeMappingForFormat("xml",
                        new MediaTypeHeaderValue("application/xml"));
                })
                .AddJsonOptions(opts => opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
                .AddRazorRuntimeCompilation();

            services.AddScoped<CacheFilterAttribute>();

            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<ICartService, CartService>();

            services.AddDbContext<StoreContext>(o =>
            {
                o.UseSqlServer(Configuration.GetConnectionString("StoreConnection"))
                    .EnableSensitiveDataLogging();
            });

            services.AddDefaultIdentity<StoreUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<StoreContext>()
                .AddClaimsPrincipalFactory<StoreClaimsPrincipalFactory>();

            services.AddIdentityServer()
                .AddApiAuthorization<StoreUser, StoreContext>(opt =>
                {
                    opt.Clients.First().RedirectUris.Add("/swagger/oauth2-redirect.html");
                })
                .AddProfileService<StoreProfileService>();

            services.AddAuthentication()
                .AddIdentityServerJwt()
                .AddPolicyScheme("ApplicationDefinedAuthentication", null, options =>
                {
                    options.ForwardDefaultSelector = context => context.Request.Path.StartsWithSegments(
                        new PathString("/api"),
                        StringComparison.OrdinalIgnoreCase)
                        ? IdentityServerJwtConstants.IdentityServerJwtBearerScheme
                        : IdentityConstants.ApplicationScheme;
                });

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.Configure<AuthenticationOptions>(options =>
                options.DefaultScheme = "ApplicationDefinedAuthentication");

            services.AddAuthorization(opts =>
            {
                opts.AddPolicy(nameof(CartOwnerOrAdmin), policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(Constants.CartClaimName);
                    policy.AddRequirements(new CartOwnerOrAdmin());
                });
            });

            services.AddSingleton<IAuthorizationHandler, CartOwnerOrAdminHandler>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
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

            services.AddSwaggerGen(opts =>
            {
                var file = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var path = Path.Combine(AppContext.BaseDirectory, file);
                opts.IncludeXmlComments(path);

                opts.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("https://localhost:5001/connect/authorize"),
                            TokenUrl = new Uri("https://localhost:5001/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                {"Store.AppAPI", "Store API"}
                            }
                        }
                    }
                });

                opts.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            services.AddMemoryCache();

            services.AddAutoMapper(typeof(MappingProfile));

            services.Scan(scan => scan
                .FromAssemblyOf<BaseQuery>()
                .AddClasses(c => c.AssignableTo(typeof(ISortingProvider<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddSpaStaticFiles(config => { config.RootPath = "/ClientApp/build"; });
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

            app.UseSwagger();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseSwaggerUI(opts =>
            {
                opts.SwaggerEndpoint("/swagger/v1/swagger.json", "DemoStore API v1");
                opts.OAuthClientId("Store.App");
                opts.OAuthAppName("Store API - Swagger");
                opts.OAuthUsePkce();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });

            const string spaPath = "/ClientApp";
            if (env.IsDevelopment())
                app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments(spaPath)
                                   || ctx.Request.Path.StartsWithSegments("/sockjs-node"),
                    client =>
                    {
                        client.UseSpa(spa =>
                        {
                            spa.Options.SourcePath = "ClientApp";
                            spa.UseReactDevelopmentServer("start");
                        });
                    });
            else
                app.Map(new PathString(spaPath), client =>
                {
                    client.UseSpaStaticFiles(new StaticFileOptions
                    {
                        OnPrepareResponse = ctx =>
                        {
                            if (ctx.Context.Request.Path.StartsWithSegments($"{spaPath}/static"))
                            {
                                var headers = ctx.Context.Response.GetTypedHeaders();
                                headers.CacheControl = new CacheControlHeaderValue
                                {
                                    Public = true,
                                    MaxAge = TimeSpan.FromDays(365)
                                };
                            }
                            else
                            {
                                var headers = ctx.Context.Response.GetTypedHeaders();
                                headers.CacheControl = new CacheControlHeaderValue
                                {
                                    Public = true,
                                    MaxAge = TimeSpan.FromDays(0)
                                };
                            }
                        }
                    });

                    client.UseSpa(spa =>
                    {
                        spa.Options.SourcePath = "ClientApp";
                        spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
                        {
                            OnPrepareResponse = ctx =>
                            {
                                var headers = ctx.Context.Response.GetTypedHeaders();
                                headers.CacheControl = new CacheControlHeaderValue
                                {
                                    Public = true,
                                    MaxAge = TimeSpan.FromDays(0)
                                };
                            }
                        };
                    });
                });
        }
    }
}
