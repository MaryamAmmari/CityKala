using System;
using System.Collections.Generic;
using System.Text;
using DigiShop.Contexts;
using DigiShop.Extensions;
using DigiShop.Helpers;
using DigiShop.Models.DataModels;
using DigiShop.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace DigiShop
{
    public class Startup
    {

        private IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();
            services.AddSingleton(appSettings);
            services.AddDbContext<DataContext>(option =>
            {
                option.UseSqlServer(appSettings.AppConnectionString);
            });
            services.AddDbContext<AuthDbContext>(option =>
            {
                option.UseSqlServer(appSettings.AppConnectionString);
            });

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.AllowedForNewUsers = false;

                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddMvc().AddRazorRuntimeCompilation();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "Account/AccessDenied";
                options.SlidingExpiration = true;
                options.ReturnUrlParameter = "ReturnUrl";
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = "Http://CityKala.ir",
                    ValidAudience = "Http://CityKala.ir",
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    RoleClaimType = "Roles",
                    NameClaimType = "UserName",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.Unicode.GetBytes("My Password")),
                };
            });

            services.AddAuthorization();

            services.AddTransient<PagingHelper>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.MigrateDatabase<DataContext>();
            app.MigrateDatabase<AuthDbContext>();

            DataSeeder.SeedRoles(app);
            DataSeeder.SeedUsers(app);
            DataSeeder.SeedData(app);

            app.UseRouting();
            app.UseStaticFiles();
            app.UseStatusCodePages();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }


    }
}
