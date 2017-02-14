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
            context.DayNumber.AddOrUpdate(m => m.ID,
        new Models.DayNumber { ID = 1, Day = "1" },
        new Models.DayNumber { ID = 2, Day = "2" },
        new Models.DayNumber { ID = 3, Day = "3" },
        new Models.DayNumber { ID = 4, Day = "4" },
        new Models.DayNumber { ID = 5, Day = "5" },
        new Models.DayNumber { ID = 6, Day = "6" },
        new Models.DayNumber { ID = 7, Day = "7" },
        new Models.DayNumber { ID = 8, Day = "8" },
        new Models.DayNumber { ID = 9, Day = "9" },
        new Models.DayNumber { ID = 10, Day = "10" },
        new Models.DayNumber { ID = 11, Day = "11" },
        new Models.DayNumber { ID = 12, Day = "12" },
        new Models.DayNumber { ID = 13, Day = "13" },
        new Models.DayNumber { ID = 14, Day = "14" },
        new Models.DayNumber { ID = 15, Day = "15" },
        new Models.DayNumber { ID = 16, Day = "16" },
        new Models.DayNumber { ID = 17, Day = "17" },
        new Models.DayNumber { ID = 18, Day = "18" },
        new Models.DayNumber { ID = 19, Day = "19" },
        new Models.DayNumber { ID = 20, Day = "20" },
        new Models.DayNumber { ID = 21, Day = "21" },
        new Models.DayNumber { ID = 22, Day = "22" },
        new Models.DayNumber { ID = 23, Day = "23" },
        new Models.DayNumber { ID = 24, Day = "24" },
        new Models.DayNumber { ID = 25, Day = "25" },
        new Models.DayNumber { ID = 26, Day = "26" },
        new Models.DayNumber { ID = 27, Day = "27" },
        new Models.DayNumber { ID = 28, Day = "28" },
        new Models.DayNumber { ID = 29, Day = "29" },
        new Models.DayNumber { ID = 30, Day = "30" },
        new Models.DayNumber { ID = 31, Day = "31" }
        );
            context.MonthNumber.AddOrUpdate(m => m.ID,
new Models.MonthNumber { ID = 1, Month = "1" },
new Models.MonthNumber { ID = 2, Month = "2" },
new Models.MonthNumber { ID = 3, Month = "3" },
new Models.MonthNumber { ID = 4, Month = "4" },
new Models.MonthNumber { ID = 5, Month = "5" },
new Models.MonthNumber { ID = 6, Month = "6" },
new Models.MonthNumber { ID = 7, Month = "7" },
new Models.MonthNumber { ID = 8, Month = "8" },
new Models.MonthNumber { ID = 9, Month = "9" },
new Models.MonthNumber { ID = 10, Month = "10" },
new Models.MonthNumber { ID = 11, Month = "11" },
new Models.MonthNumber { ID = 12, Month = "12" }
);
            context.YearNumber.AddOrUpdate(m => m.ID,
           new Models.YearNumber { ID = 1, Year = "2017" },
           new Models.YearNumber { ID = 2, Year = "2018" },
           new Models.YearNumber { ID = 3, Year = "2019" },
           new Models.YearNumber { ID = 4, Year = "2020" }
           );
            context.Categories.AddOrUpdate(m => m.ID,
            new Models.Categories { ID = 1, Category = "Animal Services" },
            new Models.Categories { ID = 2, Category = "Disaster Preperation" },
            new Models.Categories { ID = 3, Category = "Education" },
            new Models.Categories { ID = 4, Category = "Enviornmental" },
            new Models.Categories { ID = 5, Category = "Health" },
            new Models.Categories { ID = 6, Category = "Human Services" }
            );


        }
    }
}
