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
                AboutOpportunity = model.AboutOpportunity,
                AboutShort = model.ShortDescription
            };
            _context.Opportunity.Add(newOpportunity);
            _context.SaveChanges();
            return RedirectToAction("SkillNeeds", "Opportunity");
        }
        public ActionResult SkillNeeds()
        {
            var tempSkillList = _context.TempSkills.ToList();
            var categoryList = _context.Categories.ToList();
            var numberList = _context.Numbers.ToList();

            var viewModel = new SkillsNeededViewModel()
            {
                TempSkillsList = tempSkillList,
                CategoryList = categoryList,
                NumberList = numberList
            };

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult SkillNeeds(FormCollection collection)
        {
            List<int> Ids = new List<int>();
            foreach (var item in _context.TempSkills)
            {
                Ids.Add(item.ID);
            }
            for (int i = 0; i < 100; i++)
            {
                var tempBox = "checkBox" + i.ToString();
                if (!string.IsNullOrEmpty(collection[tempBox]))
                {
                    var tempRest = _context.TempSkills.Find(Ids[i]);
                    var temper = new SkillsNeeded()
                    {
                        OpportunityId = tempRest.OpportunityId,
                        OrganizationId = tempRest.OrganizationId,
                        Category = tempRest.Category,
                        SkillLevel = tempRest.SkillLevel,
                        Amount = tempRest.Amount
                    };
                    _context.SkillsNeeded.Add(temper);
                }
            }
            _context.TempSkills.RemoveRange(_context.TempSkills);
            _context.SaveChanges();
            return RedirectToAction("Index", "Organization");
        }
        public ActionResult AddTempSkill(SkillsNeededViewModel model)
        {
            var currentUserName = User.Identity.Name;
            var currentUser = _context.Users.FirstOrDefault(m => m.UserName == currentUserName);
            var opportunity = _context.Opportunity.Max(item => item.ID);

            var tempcat = Int32.Parse(model.Category);
            var category = _context.Categories.SingleOrDefault(m => m.ID == tempcat);
            var justCat = category.Category;
            var tempAmount = Int32.Parse(model.Amount);
            var amount = _context.Numbers.SingleOrDefault(m => m.ID == tempAmount);
            var justAmount = amount.Number;

            var newSkillNeed = new TempSkills()
            {
                OpportunityId = opportunity,
                OrganizationId = currentUser.Email,
                Category = justCat,
                SkillLevel = model.SkillSet,
                Amount = justAmount
            };
            _context.TempSkills.Add(newSkillNeed);
            _context.SaveChanges();
            return RedirectToAction("SkillNeeds", "Opportunity");
        }
        //public ActionResult Index(int id)
        //{
        //    var skillList = new List<SkillsNeeded>();
        //    foreach(var skill in _context.SkillsNeeded)
        //    {
        //        if (skill.OpportunityId == id)
        //        {
        //            skillList.Add(skill);
        //        }
        //    }
        //    var currentOpportunity = _context.Opportunity.FirstOrDefault(m => m.ID == id);
        //    var viewModel = new OpportunityIndexViewModel()
        //    {
        //        Opportunity = currentOpportunity,
        //        SkillsNeededList = skillList
        //    };
        //    return View(viewModel);
        //}
    }
}