using DigiShop.Models.DataModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigiShop.Contexts;
using System.Security.Claims;
using DigiShop.Constants;

namespace DigiShop.Contexts
{
    public static class DataSeeder
    {
        public static void SeedData(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<DataContext>())
                {
                    if (!context.categories.Any())
                    {

                        var items = new List<Category>()
                        {
                            new Category()
                            {
                                CategoryId = Guid.NewGuid(),
                                CategoryName = "موبایل",
                                Products = new List<Product>()
                                {
                                    new Product()
                                    {
                                        ProductName = "سامسونگ گلکسی A10",
                                        CountOfAvailable = 10,
                                        ProductDescription = "گوشی موبایل سامسونگ مدل Galaxy A10 SM-A105F/DS دو سیم کارت ظرفیت 32 گیگابایت",
                                        ProductId = Guid.NewGuid(),
                                        ProductImage = "galaxyA10.jpg",
                                        ProductPrice = 7_600_000,
                                        ProductType = Models.ProductType.Mobile,
                                        Features = new List<Feature>()
                                        {
                                            new Feature()
                                            {
                                                FeatureId = Guid.NewGuid(),
                                                FeatureName = "پردازنده",
                                                FeatureValue = "Exynos 9820",
                                            },
                                            new Feature()
                                            {
                                                FeatureId = Guid.NewGuid(),
                                                FeatureName = "حافظه داخلی",
                                                FeatureValue = "32 گیگابایت"
                                            },
                                            new Feature()
                                            {
                                                FeatureId = Guid.NewGuid(),
                                                FeatureName = "شبکه ارتباطی",
                                                FeatureValue = "4G"
                                            },
                                            new Feature()
                                            {
                                                FeatureId = Guid.NewGuid(),
                                                FeatureName = "حسگر",
                                                FeatureValue = "مجاورت (Proximity)"
                                            },
                                        }

                                    }
                                }
                            }
                        };

                        context.categories.AddRange(items);
                        context.SaveChanges();
                    }
                }
            }
        }


        public static void SeedRoles(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var manager = scope.ServiceProvider.GetRequiredService <RoleManager<IdentityRole>>())
                {
                    var roles = new List<string>()
                    {
                        RoleConstants.Admin, RoleConstants.User
                    };
                    foreach (var item in roles)
                    {
                        if (!manager.RoleExistsAsync(item).Result)
                        {
                            manager.CreateAsync(new IdentityRole()
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name = item,
                            }).Wait();
                        }
                    }
                }
            }
        }


        public static void SeedUsers(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var manager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>())
                {
                    if (manager.Users.Any())
                        return;

                    var user = new AppUser()
                    {

                        Id = Guid.NewGuid().ToString(),
                        UserName = "Admin",
                        Email = "Admin@CityKala.ir",
                    };

                    manager.CreateAsync(user , "123").Wait();
                    manager.AddClaimAsync(user, new Claim(ClaimConstants.FullName, "مریم عماری")).Wait();
                    manager.AddToRoleAsync(user,RoleConstants.Admin).Wait();

                }
            }
        }

    }
}
