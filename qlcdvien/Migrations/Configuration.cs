namespace qlcdvien.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using qlcdvien.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<qlcdvien.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(qlcdvien.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);


            foreach (string line in File.ReadLines("C:\\userlist.txt"))
            {
                var user = new ApplicationUser
                {
                    UserName = line.Split('\t')[1] + line.Split('\t')[2]+ line.Split('\t')[5].Split('/')[0],
                    name = line.Split('\t')[0],
                    chucvuDoanthe = line.Split('\t')[3],
                    sex = true,
                    DOB = DateTime.Parse(line.Split('\t')[5]),
                    capcongdoan_id = int.Parse(line.Split('\t')[6]),
                };
                if (line.Split('\t')[4] != "Nam")
                    user.sex = false;

                    userManager.Create(user, "Abc@1234");
                userManager.AddToRole(user.Id, "user");
            }

        }
    }
}
