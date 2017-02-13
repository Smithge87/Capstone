namespace VolunteerWebApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<VolunteerWebApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(VolunteerWebApp.Models.ApplicationDbContext context)
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
            context.State.AddOrUpdate(m => m.ID,
                    new Models.State { ID = 1, States = "Wisconsin" },
                    new Models.State { ID = 2, States = "Illinois" },
                    new Models.State { ID = 3, States = "Indiania" },
                    new Models.State { ID = 4, States = "Minnesota" },
                    new Models.State { ID = 5, States = "Iowa" },
                    new Models.State { ID = 6, States = "Ohio" },
                    new Models.State { ID = 7, States = "Kansas" },
                    new Models.State { ID = 8, States = "Missouri" },
                    new Models.State { ID = 9, States = "North Dakota" }
                    );
        }
    }
}
