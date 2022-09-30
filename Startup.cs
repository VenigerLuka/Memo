using AutoMapper;
using MemoProject.Contracts;
using MemoProject.Data;
using MemoProject.Helpers;
using MemoProject.Repository;
using MemoProject.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Globalization;

namespace MemoProject
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
            services.AddDbContext<MemoDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MemoDbConnection")));
            services.AddDbContext<IdentityContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("IdentityDbConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();


            services.AddAuthentication(IdentityConstants.ApplicationScheme)
                     .AddCookie(IdentityConstants.ApplicationScheme, (CookieAuthenticationOptions options) =>
                     {
                         options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
                         options.SlidingExpiration = true;
                         options.Cookie = new CookieBuilder
                         {
                             HttpOnly = true,
                             SecurePolicy = CookieSecurePolicy.Always,
                             IsEssential = true,
                             Name = IdentityConstants.ApplicationScheme

                         };
                         options.LoginPath = "/Identity/Account/Login";
                     });


            services.AddIdentityCore<IdentityUser>((IdentityOptions options) =>
            {
                //password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;

                //User settings
                options.User.RequireUniqueEmail = true;

            })
                .AddRoles<IdentityRole<string>>()
                .AddSignInManager<SignInManager<IdentityUser>>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            /*
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<MemoDbContext>();
            */

            services.AddLocalization(options => { options.ResourcesPath = "Resources"; });
            services.AddControllersWithViews();


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMemoService, MemoService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<IAuditService, AuditService>();



            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddLogging();
            services.AddRazorPages().AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();
            services.Configure<RequestLocalizationOptions>(
                opt =>
                {
                    var supportedCultures = new[]
                    {
                        new CultureInfo("en"),
                        new CultureInfo("sr")
                    };
                    opt.DefaultRequestCulture = new RequestCulture("en");
                    opt.SupportedCultures = supportedCultures;
                    opt.SupportedUICultures = supportedCultures;
                }

        );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<IdentityUser> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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

            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
            //var supportedCultures = new[] { "en", "fr", "sr" };
            //var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
            //    .AddSupportedCultures(supportedCultures)
            //    .AddSupportedUICultures(supportedCultures);
            //app.UseRequestLocalization(localizationOptions);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
