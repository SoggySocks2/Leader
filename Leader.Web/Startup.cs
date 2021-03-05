using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Smeat.Leader.Infrastructure.Identity;
using Smeat.Leader.Web.Configuration;
using Smeat.Leader.Web.Resources;
using Smeat.Leader.Web.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Smeat.Leader.Web
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
            /* Add LeaderUser identity and AuthDbContext */
            services.AddIdentity<LeaderUser, LeaderRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<AuthDbContext>()                       
            .AddDefaultTokenProviders(); /* 2 factor authentication */

            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AuthData"));
            });


            /* Add ApplicationDbContext */
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Leader"));
            });

            /* Add specific configuration classes */
            services.AddCoreServices();

            services.AddDatabaseDeveloperPageExceptionFilter();
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();


            /* Globalization */
            services.AddLocalization(options => { options.ResourcesPath = "Resources"; });

            services.AddSingleton<CommonLocalizationService>();

            services.AddRazorPages(options =>
                    {
                        /* Prevent anonymous access to all (non area) pages except Privacy and Error */
                        options.Conventions.AuthorizeFolder("/").AllowAnonymousToPage("/Privacy").AllowAnonymousToPage("/Error");
                        /* Prevent anonymous access to the Identity area, secured folder */
                        options.Conventions.AuthorizeAreaFolder("Identity", "/Secured");
                        /* Allow anonymous access to the Identity area, account folder */
                        options.Conventions.AllowAnonymousToAreaFolder("Identity", "/Account");
                    }
                )
                .AddRazorRuntimeCompilation() /* Enable edit and continue */
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                //.AddDataAnnotationsLocalization(); /* Globalization */
                .AddDataAnnotationsLocalization(options => /* Globalization */
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var assemblyName = new AssemblyName(typeof(CommonResources).GetTypeInfo().Assembly.FullName);
                        return factory.Create(nameof(CommonResources), assemblyName.Name);
                    };
                });


            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en"),
                    new CultureInfo("es"),
                    new CultureInfo("fr")
                };
                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
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
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            /* Add Globalization to the pipleline */
            var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value;
            app.UseRequestLocalization(localizationOptions);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            //Ensure database migrations are automatically applied
            //InitializeDatabases(app);
        }

        // Automatically apply any outstanding database migrations
        private void InitializeDatabases(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                //scope.ServiceProvider.GetRequiredService<AuthDbContext>().Database.Migrate();
                scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
            }
        }
    }
}
