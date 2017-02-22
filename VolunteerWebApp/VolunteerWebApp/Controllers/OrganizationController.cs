using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using VolunteerWebApp.Models;

namespace VolunteerWebApp.Controllers
{
    public class OrganizationController : Controller
    {
        public ApplicationDbContext _context;
        public OrganizationController()
        {
            _context = new ApplicationDbContext();
        }



        // GET: Organization
        public ActionResult Index()
        {
            var opportunityList = new List<Opportunity>();
            var currentUserName = User.Identity.Name;
            var currentUser = _context.Users.FirstOrDefault(m => m.UserName == currentUserName);
            foreach (var opportunity in _context.Opportunity)
            {
                if (opportunity.OrganizationHostId == currentUser.Email)
                {
                    opportunityList.Add(opportunity);
                }
            }

            var currentInfo = _context.Address.FirstOrDefault(m => m.UserId == currentUser.Email);
            var viewModel = new OrganizationProfileViewModel()
            {
                ApplicationUser = currentUser,
                OpportunityList = opportunityList
            };
            if (currentInfo != null)
            {
                viewModel.Information = currentInfo;
            }
            return View(viewModel);
        }
        public ActionResult Info()
        {
            var currentUserName = User.Identity.Name;
            var states = _context.State.ToList();
            var currentUser = _context.Users.FirstOrDefault(m => m.UserName == currentUserName);
            var currentInfo = _context.Address.FirstOrDefault(m => m.UserId == currentUserName);
            if (currentInfo != null)
            {
                var viewModel = new OrganizationInfoViewModel()
                {
                    StateList = states,
                    FirstName = currentUser.FirstName,
                    LastName = currentUser.LastName,
                    PhoneNumber = currentUser.PhoneNumber,
                    OrganizationName = currentUser.OrganizationName,
                    AboutInfo = currentInfo.AboutInfo,
                    StreetAddress = currentInfo.StreetAddress,
                    City = currentInfo.City,
                    Zipcode = currentInfo.Zipcode,
                    StateId = currentInfo.StateId
                };
                return View(viewModel);
            }
            else
            {
                var viewModel = new OrganizationInfoViewModel()
                {
                    StateList = states,
                    FirstName = currentUser.FirstName,
                    LastName = currentUser.LastName,
                    PhoneNumber = currentUser.PhoneNumber,
                    OrganizationName = currentUser.OrganizationName
                };

                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Info(OrganizationInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUserName = User.Identity.Name;
                var currentUser = _context.Users.FirstOrDefault(m => m.UserName == currentUserName);
                var userInDatabase = _context.Users.SingleOrDefault(m => m.UserName == currentUserName);
                var userAddress = _context.Address.SingleOrDefault(m => m.UserId == currentUser.Email);

                userInDatabase.FirstName = model.FirstName;
                userInDatabase.LastName = model.LastName;
                userInDatabase.PhoneNumber = model.PhoneNumber;
                userInDatabase.OrganizationName = model.OrganizationName;

                if (userAddress != null)
                {
                    userAddress.StreetAddress = model.StreetAddress;
                    userAddress.City = model.City;
                    userAddress.StateId = model.Address.StateId;
                    userAddress.Zipcode = model.Zipcode;
                    userAddress.UserId = currentUser.Email;
                    userAddress.AboutInfo = model.AboutInfo;
                }
                else
                {
                    var newAddress = new Information()
                    {
                        StreetAddress = model.StreetAddress,
                        City = model.City,
                        StateId = model.Address.StateId,
                        Zipcode = model.Zipcode,
                        UserId = currentUser.Email,
                        AboutInfo = model.AboutInfo
                    };
                    _context.Address.Add(newAddress);
                }
                _context.SaveChanges();
                return RedirectToAction("Index", "Organization");

            }
            return View(model);
        }
        public ActionResult AddPhoto()
        {
            var currentUserName = User.Identity.Name;
            var currentUser = _context.Users.FirstOrDefault(m => m.UserName == currentUserName);
            var viewModel = new PhotoViewModel()
            {
                ApplicationUser = currentUser
            };
            return View(viewModel);
        }
        [HttpPost]

        public ActionResult AddPhoto(PhotoViewModel model, FormCollection form)
        {
            var currentUserName = User.Identity.Name;
            var currentUser = _context.Users.FirstOrDefault(m => m.UserName == currentUserName);
            WebImage photo = null;
            var newFileName = "";
            var imagePath = "";
            photo = WebImage.GetImageFromRequest();
            if (photo != null)
            {
                newFileName = Guid.NewGuid().ToString() + "_" +
                Path.GetFileName(photo.FileName);
                imagePath = @"images\" + newFileName;

                photo.Save(@"~\" + imagePath);
                currentUser.ProfilePhoto = ("../" + imagePath);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Organization");
        }
        public ActionResult FullView(int id)
        {
            var skillList = new List<SkillsNeeded>();
            foreach (var skill in _context.SkillsNeeded)
            {
                if (skill.OpportunityId == id)
                {
                    skill.SkillImgSrc = imagePull(skill.SkillLevel);
                    skillList.Add(skill);
                }
            }
            var currentOpportunity = _context.Opportunity.FirstOrDefault(m => m.ID == id);
            var interestedUsers = new List<Interest>();
            foreach (var interests in _context.Interest)
            {
                if (interests.OpportunityId == currentOpportunity.ID && interests.CanShow == true )
                {
                    interestedUsers.Add(interests);
                }
            }
            var viewModel = new OpportunityIndexViewModel()
            {
                Opportunity = currentOpportunity,
                SkillsNeededList = skillList,
                InterestedUsers = interestedUsers,
                oppId = currentOpportunity.ID
            };
            return View(viewModel);
        }
        public ActionResult Delete(int id)
        {
            var removeOpp = _context.Opportunity.FirstOrDefault(m => m.ID == id);
            _context.Opportunity.Remove(removeOpp);

            List<SkillsNeeded> deleteSkills = new List<SkillsNeeded>();
            foreach(var item in _context.SkillsNeeded)
            {
                if (item.OpportunityId == id)
                {
                    deleteSkills.Add(item);
                }
            }
            foreach (var item in deleteSkills)
            {
                var deleteSkill = _context.SkillsNeeded.FirstOrDefault(m => m.ID == item.ID);
                _context.SkillsNeeded.Remove(deleteSkill);
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Organization");
        }
        public ActionResult EditOpp(int id)
        {
            var currentOpp = _context.Opportunity.FirstOrDefault(m => m.ID == id);
            var statesList = _context.State.ToList();
            var dayList = _context.DayNumber.ToList();
            var monthList = _context.MonthNumber.ToList();
            var yearList = _context.YearNumber.ToList();
            var categoryList = _context.Categories.ToList();

            var viewModel = new OpportunityEditViewModel()
            {
                DayList = dayList,
                MonthList = monthList,
                YearList = yearList,
                CategoryList = categoryList,
                StateList = statesList,
                ShortDescription = currentOpp.AboutShort,
                StartTime = currentOpp.StartTime,
                EndTime = currentOpp.EndTime,
                Duration = currentOpp.Duration,
                StartDay = currentOpp.StartDay,
                StartMonth = currentOpp.StartMonth,
                StartYear = currentOpp.StartYear,
                StreetAddress = currentOpp.StreetAddress,
                City = currentOpp.City,
                State = currentOpp.State,
                Zipcode = currentOpp.Zipcode,
                Venue = currentOpp.Venue,
                Title = currentOpp.Title,
                AboutOpportunity = currentOpp.AboutOpportunity,
                Category = currentOpp.Category,
                currentEdit = currentOpp
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult EditOpp(OpportunityEditViewModel model, int id)
        {
            var tempcat = Int32.Parse(model.Category);
            var category = _context.Categories.SingleOrDefault(m => m.ID == tempcat);
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

            var currentOpp = _context.Opportunity.FirstOrDefault(m => m.ID == id);

            currentOpp.Category = justCat;
            currentOpp.StartTime = model.StartTime;
            currentOpp.EndTime = model.EndTime;
            currentOpp.StartDay = justDay;
            currentOpp.StartMonth = justMonth;
            currentOpp.StartYear = justYear;
            currentOpp.Duration = model.Duration;
            currentOpp.Title = model.Title;
            currentOpp.Venue = model.Venue;
            currentOpp.StreetAddress = model.StreetAddress;
            currentOpp.City = model.City;
            currentOpp.State = justState;
            currentOpp.Zipcode = model.Zipcode;
            currentOpp.AboutOpportunity = model.AboutOpportunity;
            currentOpp.AboutShort = model.ShortDescription;

            _context.SaveChanges();

            return RedirectToAction("Index", "Organization");
        }
        public ActionResult PartialProfile(int id)
        {
            var refOpportunity = _context.Opportunity.FirstOrDefault(m => m.ID == id);
            var wantedUser = _context.Users.FirstOrDefault(m => m.Email == refOpportunity.OrganizationHostId);
            var wantedInfo = _context.Address.FirstOrDefault(m => m.UserId == wantedUser.Email);
            List<Opportunity> orgOpps = new List<Opportunity>();
            foreach(var opp in _context.Opportunity)
            {
                if(opp.OrganizationHostId == wantedUser.Email)
                {
                    orgOpps.Add(opp);
                }
            }
            var viewModel = new PartialProfileOrganizationViewModel()
            {
                Organization = wantedUser,
                OrganizationInfo = wantedInfo,
                Opportunities = orgOpps
            };
            return View(viewModel);
        }
        public string imagePull(string value)
        {
            if (value == "1")
            {
                return ("../images/oneStar.png");
            }
            else if (value == "2")
            {
                return ("../images/twoStar.png");
            }
            else if (value == "3")
            {
                return ("../images/threeStar.png");
            }
            else if (value == "4")
            {
                return ("../images/fourStar.png");
            }
            else
            {
                return ("../images/fiveStar.png");
            }
        }
    }
}