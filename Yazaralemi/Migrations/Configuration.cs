namespace Yazaralemi.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Yazaralemi.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Yazaralemi.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Yazaralemi.Models.ApplicationDbContext context)
        {
            string email = "yunusdemirpolatt@gmail.com";
            string password = "123456";

            #region admin rolünü ve kullanicisi olustur
            if (!context.Roles.Any(r => r.Name == "admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "admin" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == email))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = email, Email = email };

                manager.Create(user, password);
                manager.AddToRole(user.Id, "admin");

                //Olusturulan bu kullaniciya ait yazilar ekler
                #region Kategoriler ve Yazýlar
                if (!context.Categories.Any())
                {
                    Category cat1 = new Category
                    {
                        CategoryName = "Gezi Yazilari"
                    };

                    cat1.Posts = new List<Post>();

                    cat1.Posts.Add(new Post
                    {
                        Title = "Gezi Yazisi 1",
                        AuthorId = user.Id,
                        Content = @"<p>Maecenas finibus vel justo non mattis. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Praesent placerat est ac risus faucibus, vel eleifend felis luctus. Aliquam luctus sollicitudin quam, quis auctor est facilisis ac. Proin et molestie ipsum. Donec eu rutrum libero, sed efficitur nisl. Vestibulum feugiat, lectus ac volutpat sollicitudin, turpis sapien maximus ipsum, sit amet ultrices erat lacus non neque. Aliquam a ipsum at leo mattis volutpat.</p><p>        Maecenas et erat nisi. Nam pellentesque leo quam, eu malesuada nulla commodo ac. Aliquam aliquam, enim tincidunt varius laoreet, tellus orci egestas tortor, id vehicula lorem turpis ac turpis. Sed non odio mauris. Sed neque erat, fermentum aliquet semper sed, elementum quis eros. Cras sed felis neque. Cras dignissim lacus purus, eget hendrerit lacus auctor ut.</p>",
                        CreatedAt = DateTime.Now
                    });

                    cat1.Posts.Add(new Post
                    {
                        Title = "Gezi Yazisi 2",
                        AuthorId = user.Id,
                        Content = @"<p>Maecenas finibus vel justo non mattis. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Praesent placerat est ac risus faucibus, vel eleifend felis luctus. Aliquam luctus sollicitudin quam, quis auctor est facilisis ac. Proin et molestie ipsum. Donec eu rutrum libero, sed efficitur nisl. Vestibulum feugiat, lectus ac volutpat sollicitudin, turpis sapien maximus ipsum, sit amet ultrices erat lacus non neque. Aliquam a ipsum at leo mattis volutpat.</p><p>        Maecenas et erat nisi. Nam pellentesque leo quam, eu malesuada nulla commodo ac. Aliquam aliquam, enim tincidunt varius laoreet, tellus orci egestas tortor, id vehicula lorem turpis ac turpis. Sed non odio mauris. Sed neque erat, fermentum aliquet semper sed, elementum quis eros. Cras sed felis neque. Cras dignissim lacus purus, eget hendrerit lacus auctor ut.</p>",
                        CreatedAt = DateTime.Now
                    });


                    Category cat2 = new Category
                    {
                        CategoryName = "Is Yazilari"
                    };

                    cat2.Posts = new List<Post>();

                    cat2.Posts.Add(new Post
                    {
                        Title = "Is Yazisi 1",
                        AuthorId = user.Id,
                        Content = @"<p>Maecenas finibus vel justo non mattis. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Praesent placerat est ac risus faucibus, vel eleifend felis luctus. Aliquam luctus sollicitudin quam, quis auctor est facilisis ac. Proin et molestie ipsum. Donec eu rutrum libero, sed efficitur nisl. Vestibulum feugiat, lectus ac volutpat sollicitudin, turpis sapien maximus ipsum, sit amet ultrices erat lacus non neque. Aliquam a ipsum at leo mattis volutpat.</p><p>        Maecenas et erat nisi. Nam pellentesque leo quam, eu malesuada nulla commodo ac. Aliquam aliquam, enim tincidunt varius laoreet, tellus orci egestas tortor, id vehicula lorem turpis ac turpis. Sed non odio mauris. Sed neque erat, fermentum aliquet semper sed, elementum quis eros. Cras sed felis neque. Cras dignissim lacus purus, eget hendrerit lacus auctor ut.</p>",
                        CreatedAt = DateTime.Now
                    });


                    cat2.Posts.Add(new Post
                    {
                        Title = "Is Yazisi 2",
                        AuthorId = user.Id,
                        Content = @"<p>Maecenas finibus vel justo non mattis. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Praesent placerat est ac risus faucibus, vel eleifend felis luctus. Aliquam luctus sollicitudin quam, quis auctor est facilisis ac. Proin et molestie ipsum. Donec eu rutrum libero, sed efficitur nisl. Vestibulum feugiat, lectus ac volutpat sollicitudin, turpis sapien maximus ipsum, sit amet ultrices erat lacus non neque. Aliquam a ipsum at leo mattis volutpat.</p><p>        Maecenas et erat nisi. Nam pellentesque leo quam, eu malesuada nulla commodo ac. Aliquam aliquam, enim tincidunt varius laoreet, tellus orci egestas tortor, id vehicula lorem turpis ac turpis. Sed non odio mauris. Sed neque erat, fermentum aliquet semper sed, elementum quis eros. Cras sed felis neque. Cras dignissim lacus purus, eget hendrerit lacus auctor ut.</p>",
                        CreatedAt = DateTime.Now
                    });

                    context.Categories.Add(cat1);
                    context.Categories.Add(cat2);
                }
                #endregion
            }
            #endregion

            #region Admin kullanicisina 77 yeni yazi ekle
            string catName = "Diger";
            if (!context.Categories.Any(x => x.CategoryName == catName))
            {
                ApplicationUser admin = context.Users.Where(x => x.UserName == email).FirstOrDefault();
                if (admin != null)
                {
                    if (!context.Categories.Any(x => x.CategoryName == catName)){
                        Category diger = new Category
                        {
                            CategoryName = catName,
                            Posts = new HashSet<Post>()
                        };

                        for (int i = 1; i <= 77; i++)
                        {
                            diger.Posts.Add(new Post
                            {
                                Title = "Gezi Yazisi " + i,
                                AuthorId = admin.Id,
                                Content = @"<p>Maecenas finibus vel justo non mattis. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Praesent placerat est ac risus faucibus, vel eleifend felis luctus. Aliquam luctus sollicitudin quam, quis auctor est facilisis ac. Proin et molestie ipsum. Donec eu rutrum libero, sed efficitur nisl. Vestibulum feugiat, lectus ac volutpat sollicitudin, turpis sapien maximus ipsum, sit amet ultrices erat lacus non neque. Aliquam a ipsum at leo mattis volutpat.</p><p>        Maecenas et erat nisi. Nam pellentesque leo quam, eu malesuada nulla commodo ac. Aliquam aliquam, enim tincidunt varius laoreet, tellus orci egestas tortor, id vehicula lorem turpis ac turpis. Sed non odio mauris. Sed neque erat, fermentum aliquet semper sed, elementum quis eros. Cras sed felis neque. Cras dignissim lacus purus, eget hendrerit lacus auctor ut.</p>",
                                CreatedAt = DateTime.Now.AddMinutes(i)
                            });
                        }
                        context.Categories.Add(diger);
                    }
                }
            }
            #endregion
        }
    }
}