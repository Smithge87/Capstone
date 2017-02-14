using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VolunteerWebApp.Models;

namespace VolunteerWebApp.Controllers
{
    public class OpportunityController : Controller
    {
        public ApplicationDbContext _context;
        public OpportunityController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Opportunity
        public ActionResult Create()
        {
            var statesList = _context.State.ToList();
            var dayList = _context.DayNumber.ToList();
            var monthList = _context.MonthNumber.ToList();
            var yearList = _context.YearNumber.ToList();
            var categoryList = _context.Categories.ToList();

            var viewModel = new OpportunityViewModel()
            {
                StateList = statesList,
                DayList = dayList,
                MonthList = monthList,
                YearList = yearList,
                CategoryList = categoryList
            };

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(OpportunityViewModel model)
        {



            var currentUserName = User.Identity.Name;
            var currentUser = _context.Users.FirstOrDefault(m => m.UserName == currentUserName);
            var tempcat = Int32.Parse(model.Category);
            var category =  _context.Categories.SingleOrDefault(m => m.ID == tempcat);
            var justCat = category.Category;
            var tempDay = Int32.Parse(model.StartDay);
            var day = _context.DayNumber.SingleOrDefault(m => m.ID == tempDay);
            var justDay = day.Day;
            var tempMonth = Int32.Parse(model.StartMonth);
            var month = _context.MonthNumber.SingleOrDefault(m => m.ID == tempMonth);
            var justMonth = month.Month;
            var tempYear = Int32.Parse(model.StartYear);
            var year = _context.YearNumber.SingleOrDefault(m => m.ID == tempYear);
            var justYear = year.Year;
            var tempState = Int32.Parse(model.State);
            var state = _context.State.SingleOrDefault(m => m.ID == tempState);
            var justState = state.States;


            var newOpportunity = new Opportunity()
            {
                Category = justCat,
                OrganizationHostId = currentUser.Email,
                OrganizationHostName = currentUser.OrganizationName,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                StartDay = justDay,
                StartMonth = justMonth,
                StartYear = justYear,
                Duration = model.Duration,
                Title = model.Title,
                Venue = model.Venue,
                StreetAddress = model.StreetAddress,
                City = model.City,
                State = justState,
                Zipcode = model.Zipcode,
                AboutOpportunity = model.AboutOpportunity
            };
            _context.Opportunity.Add(newOpportunity);
            _context.SaveChanges();
            return RedirectToAction("SkillNeeds", "Organization");
        }
        public ActionResult SkillNeeds()
        {
            var currentUserName = User.Identity.Name;
            var currentUser = _context.Users.FirstOrDefault(m => m.UserName == currentUserName);
            var opportunity = _context.Opportunity.Max(item => item.ID);


            return View();
        }
    }
}