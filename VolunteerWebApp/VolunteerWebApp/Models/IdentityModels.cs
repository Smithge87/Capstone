using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace VolunteerWebApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OrganizationName { get; set; }
        public string UserTitle { get; set; }
        public string ProfilePhoto { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<State> State { get; set; }
        public DbSet<Information> Address { get; set; }
        public DbSet<Skill> Skill { get; set; }
        public DbSet<VolunteerSettings> VolunteerSettings { get; set; }
        public DbSet<DayNumber> DayNumber { get; set; }
        public DbSet<MonthNumber> MonthNumber { get; set; }
        public DbSet<YearNumber> YearNumber { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Opportunity> Opportunity { get; set; }
        public DbSet<TempSkills> TempSkills { get; set; }
        public DbSet<SkillsNeeded> SkillsNeeded { get; set; }
        public DbSet<Numbers> Numbers { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}