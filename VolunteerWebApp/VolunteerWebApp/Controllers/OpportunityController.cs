using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Twilio;
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

            var currentUserName = User.Identity.Name;
            var currentUser = _context.Users.FirstOrDefault(m => m.UserName == currentUserName);
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
            string exitLocation = currentUser.ProfilePhoto.Substring(10);
            Resize((@"C:\Users\Jack\Desktop\Projects\Capstone\VolunteerWebApp\VolunteerWebApp" + currentUser.ProfilePhoto), @"C:\Users\Jack\Desktop\Projects\Capstone\VolunteerWebApp\VolunteerWebApp/mapImages/" + exitLocation, .5);
            string pinImage = "../mapImages/" + exitLocation;

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
                AboutShort = model.ShortDescription,
                LogoSrc = pinImage
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
            foreach (var tempSkill in _context.TempSkills)
            {
                var temper = new SkillsNeeded()
                {
                    OpportunityId = tempSkill.OpportunityId,
                    OrganizationId = tempSkill.OrganizationId,
                    Category = tempSkill.Category,
                    SkillLevel = tempSkill.SkillLevel,
                    Amount = tempSkill.Amount,
                    SkillImgSrc = imagePull(tempSkill.SkillLevel)
                };
                _context.SkillsNeeded.Add(temper);
            }
            List<ApplicationUser> allUsers = new List<ApplicationUser>();
            foreach (var user in _context.Users)
            {
                allUsers.Add(user);
            }
            List<Skill> allSkills = new List<Skill>();
                foreach (Skill skills in _context.Skill)
                {
                    allSkills.Add(skills);
                }
            foreach (var skill in _context.TempSkills)
            {
                foreach (var skills in allSkills)
                {
                    if (skill.Category == "Animal Services" && skill.SkillLevel == skills.AnimalSkill)
                    {
                        var wantedUser = allUsers.FirstOrDefault(m => m.Email == skills.UserId);
                        sendText(skill, wantedUser);
                    }
                    if (skill.Category == "Disaster Preperation" && skill.SkillLevel == skills.DisasterSkill)
                    {
                        var wantedUser = allUsers.FirstOrDefault(m => m.Email == skills.UserId);

                        sendText(skill, wantedUser);
                    }
                    if (skill.Category == "Education" && skill.SkillLevel == skills.EducationSkill)
                    {
                        var wantedUser = allUsers.FirstOrDefault(m => m.Email == skills.UserId);

                        sendText(skill, wantedUser);
                    }
                    if (skill.Category == "Enviornmental" && skill.SkillLevel == skills.EnviornmentSkill)
                    {
                        var wantedUser = allUsers.FirstOrDefault(m => m.Email == skills.UserId);

                        sendText(skill, wantedUser);
                    }
                    if (skill.Category == "Health" && skill.SkillLevel == skills.HealthSkill)
                    {
                        var wantedUser = allUsers.FirstOrDefault(m => m.Email == skills.UserId);

                        sendText(skill, wantedUser);
                    }
                    if (skill.Category == "Human Services" && skill.SkillLevel == skills.HumanServicesSkill)
                    {
                        var wantedUser = allUsers.FirstOrDefault(m => m.Email == skills.UserId);

                        sendText(skill, wantedUser);
                    }
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
                Amount = justAmount,
                SkillImgSrc = imagePull(model.SkillSet)
            };
            _context.TempSkills.Add(newSkillNeed);
            _context.SaveChanges();
            return RedirectToAction("SkillNeeds", "Opportunity");
        }
        public ActionResult CreateInterest(OpportunityIndexViewModel model)
        {
            var currentOpp = _context.Opportunity.FirstOrDefault(m => m.ID == model.oppId);
            var currentUserName = User.Identity.Name;
            var currentUser = _context.Users.FirstOrDefault(m => m.UserName == currentUserName);
            var currentSettings = _context.VolunteerSettings.FirstOrDefault(m => m.UserId == currentUser.Email);
            List<Interest> conflicts = new List<Interest>();
            foreach (var interest in _context.Interest)
            {
                if (interest.VolunteerId == currentUser.Email)
                {
                    conflicts.Add(interest);
                }
            }
            var conflictOfInterest = conflicts.FirstOrDefault(m => m.OpportunityId == currentOpp.ID);
            if (conflictOfInterest != null)
            {
                conflictOfInterest.InterestLevel = model.InterestSet;
                conflictOfInterest.InterestLevelImgSrc = imagePull(model.InterestSet);
            }
            else
            {
                var newInterest = new Interest()
                {
                    OpportunityId = currentOpp.ID,
                    VolunteerUserName = currentUser.UserTitle,
                    VolunteerId = currentUser.Email,
                    InterestLevel = model.InterestSet,
                    CanContact = currentSettings.CanContact,
                    CanShow = currentSettings.CanSee,
                    InterestLevelImgSrc = imagePull(model.InterestSet)
                };
                _context.Interest.Add(newInterest);
            }
            _context.SaveChanges();
            return RedirectToAction("FullView", "Organization", new { id = currentOpp.ID});

        }
        public void Resize(string imageFile, string outputFile, double scaleFactor)
        {
            using (var srcImage = Image.FromFile(imageFile))
            {
                var newWidth = (int)(40);
                var newHeight = (int)(40);
                using (var newImage = new Bitmap(newWidth, newHeight))
                using (var graphics = Graphics.FromImage(newImage))
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.DrawImage(srcImage, new Rectangle(0, 0, newWidth, newHeight));
                    newImage.Save(outputFile);
                }
                var imagePath = imageFile.Split('/');
            }
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
            else if (value == "5")
            {
                return ("../images/fiveStar.png");
            }
            else
            {
                return ("../images/noStar.png");
            }

        }
        public void sendText(TempSkills skill, ApplicationUser user)
        {
            try
            {
                List<string> sending = new List<string>();
                string line;
                System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\Jack\Desktop\Twilio.txt");
                while ((line = file.ReadLine()) != null)
                {
                    sending.Add(line);
                }
                file.Close();
                string message = "Hey" + user.FirstName + "! " + skill.OrganizationId + " just added a new opportunity that needs your skills! Head to Help.Hub to check it out!";
                string toNumber = phoneFormat(user.PhoneNumber);
                string AccountSid = sending[0];
                string AuthToken = sending[1];
                var client = new TwilioRestClient(AccountSid, AuthToken);
                client.SendMessage("262-278-0866", toNumber, message);
            }
            catch { }
        }
        public string phoneFormat(string number)
        {
            string cleanNumber = number.Insert(3, "-");
            string cleanerNumber = cleanNumber.Insert(7, "-");
            return cleanNumber;
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