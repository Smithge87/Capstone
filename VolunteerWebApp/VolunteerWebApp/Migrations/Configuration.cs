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
            context.Numbers.AddOrUpdate(m => m.ID,
                new Models.Numbers { ID = 1, Number = "1" },
                new Models.Numbers { ID = 2, Number = "2" },
                new Models.Numbers { ID = 3, Number = "3" },
                new Models.Numbers { ID = 4, Number = "4" },
                new Models.Numbers { ID = 5, Number = "5" },
                new Models.Numbers { ID = 6, Number = "6" },
                new Models.Numbers { ID = 7, Number = "7" },
                new Models.Numbers { ID = 8, Number = "8" },
                new Models.Numbers { ID = 9, Number = "9" },
                new Models.Numbers { ID = 10, Number = "10" },
                new Models.Numbers { ID = 11, Number = "11" },
                new Models.Numbers { ID = 12, Number = "12" },
                new Models.Numbers { ID = 13, Number = "13" },
                new Models.Numbers { ID = 14, Number = "14" },
                new Models.Numbers { ID = 15, Number = "15" },
                new Models.Numbers { ID = 16, Number = "16" },
                new Models.Numbers { ID = 17, Number = "17" },
                new Models.Numbers { ID = 18, Number = "18" },
                new Models.Numbers { ID = 19, Number = "19" },
                new Models.Numbers { ID = 20, Number = "20" },
                new Models.Numbers { ID = 21, Number = "21" },
                new Models.Numbers { ID = 22, Number = "22" },
                new Models.Numbers { ID = 23, Number = "23" },
                new Models.Numbers { ID = 24, Number = "24" },
                new Models.Numbers { ID = 25, Number = "25" },
                new Models.Numbers { ID = 26, Number = "26" },
                new Models.Numbers { ID = 27, Number = "27" },
                new Models.Numbers { ID = 28, Number = "28" },
                new Models.Numbers { ID = 29, Number = "29" },
                new Models.Numbers { ID = 30, Number = "30" },
                new Models.Numbers { ID = 31, Number = "31" },
                new Models.Numbers { ID = 32, Number = "32" },
                new Models.Numbers { ID = 33, Number = "33" },
                new Models.Numbers { ID = 34, Number = "34" },
                new Models.Numbers { ID = 35, Number = "35" },
                new Models.Numbers { ID = 36, Number = "36" },
                new Models.Numbers { ID = 37, Number = "37" },
                new Models.Numbers { ID = 38, Number = "38" },
                new Models.Numbers { ID = 39, Number = "39" },
                new Models.Numbers { ID = 40, Number = "40" },
                new Models.Numbers { ID = 41, Number = "41" },
                new Models.Numbers { ID = 42, Number = "42" },
                new Models.Numbers { ID = 43, Number = "43" },
                new Models.Numbers { ID = 44, Number = "44" },
                new Models.Numbers { ID = 45, Number = "45" },
                new Models.Numbers { ID = 46, Number = "46" },
                new Models.Numbers { ID = 47, Number = "47" },
                new Models.Numbers { ID = 48, Number = "48" },
                new Models.Numbers { ID = 49, Number = "49" },
                new Models.Numbers { ID = 50, Number = "50" },
                new Models.Numbers { ID = 51, Number = "51" },
                new Models.Numbers { ID = 52, Number = "52" },
                new Models.Numbers { ID = 53, Number = "53" },
                new Models.Numbers { ID = 54, Number = "54" },
                new Models.Numbers { ID = 55, Number = "55" },
                new Models.Numbers { ID = 56, Number = "56" },
                new Models.Numbers { ID = 57, Number = "57" },
                new Models.Numbers { ID = 58, Number = "58" },
                new Models.Numbers { ID = 59, Number = "59" },
                new Models.Numbers { ID = 60, Number = "60" },
                new Models.Numbers { ID = 61, Number = "61" },
                new Models.Numbers { ID = 62, Number = "62" },
                new Models.Numbers { ID = 63, Number = "63" },
                new Models.Numbers { ID = 64, Number = "64" },
                new Models.Numbers { ID = 65, Number = "65" },
                new Models.Numbers { ID = 66, Number = "66" },
                new Models.Numbers { ID = 67, Number = "67" },
                new Models.Numbers { ID = 68, Number = "68" },
                new Models.Numbers { ID = 69, Number = "69" },
                new Models.Numbers { ID = 70, Number = "70" },
                new Models.Numbers { ID = 71, Number = "71" },
                new Models.Numbers { ID = 72, Number = "72" },
                new Models.Numbers { ID = 73, Number = "73" },
                new Models.Numbers { ID = 74, Number = "74" },
                new Models.Numbers { ID = 75, Number = "75" },
                new Models.Numbers { ID = 76, Number = "76" },
                new Models.Numbers { ID = 77, Number = "77" },
                new Models.Numbers { ID = 78, Number = "78" },
                new Models.Numbers { ID = 79, Number = "79" },
                new Models.Numbers { ID = 80, Number = "80" },
                new Models.Numbers { ID = 81, Number = "81" },
                new Models.Numbers { ID = 82, Number = "82" },
                new Models.Numbers { ID = 83, Number = "83" },
                new Models.Numbers { ID = 84, Number = "84" },
                new Models.Numbers { ID = 85, Number = "85" },
                new Models.Numbers { ID = 86, Number = "86" },
                new Models.Numbers { ID = 87, Number = "87" },
                new Models.Numbers { ID = 88, Number = "88" },
                new Models.Numbers { ID = 89, Number = "89" },
                new Models.Numbers { ID = 90, Number = "90" },
                new Models.Numbers { ID = 91, Number = "91" },
                new Models.Numbers { ID = 92, Number = "92" },
                new Models.Numbers { ID = 93, Number = "93" },
                new Models.Numbers { ID = 94, Number = "94" },
                new Models.Numbers { ID = 95, Number = "95" },
                new Models.Numbers { ID = 96, Number = "96" },
                new Models.Numbers { ID = 97, Number = "97" },
                new Models.Numbers { ID = 98, Number = "98" },
                new Models.Numbers { ID = 99, Number = "99" },
                new Models.Numbers { ID = 100, Number = "100" },
                new Models.Numbers { ID = 101, Number = "100+" }
                );

        }
    }
}
