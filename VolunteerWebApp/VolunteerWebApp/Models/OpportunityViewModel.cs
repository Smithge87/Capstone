using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VolunteerWebApp.Models
{
    public class OpportunityViewModel
    {
        private ApplicationDbContext _context;

        public OpportunityViewModel()
        {
            _context = new ApplicationDbContext();
        }
        public IEnumerable<DayNumber> DayList { get; set; }
        public IEnumerable<MonthNumber> MonthList { get; set; }
        public IEnumerable<YearNumber> YearList { get; set; }
        public IEnumerable<Categories> CategoryList { get; set; }
        public IEnumerable<State> StateList { get; set; }

        [Required]
        [Display(Name = "Duration")]
        public string Duration { get; set; }

        [Required]
        [Display(Name = "Day")]
        public string StartDay { get; set; }

        [Required]
        [Display(Name = "Month")]
        public string StartMonth { get; set; }

        [Required]
        [Display(Name = "Year")]
        public string StartYear { get; set; }

        [Required]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required]
        [Display(Name = "Zipcode")]
        public string Zipcode { get; set; }

        [Required]
        [Display(Name = "Venue")]
        public string Venue { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string AboutOpportunity { get; set; }
    }
}