namespace Sahm.Migrations
{
    using Sahm.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Sahm.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Sahm.Models.ApplicationDbContext context)
        {
            context.Users.AddOrUpdate(
                u => u.UserId, 
                new User
                {
                    UserId = 1,
                    Name = "Mohammad"
                },
                new User
                {
                    UserId = 2,
                    Name = "Loay"
                }
            );

            context.SaveChanges();
        }
    }
}
