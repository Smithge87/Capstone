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
    public class VolunteerController : Controller
    {
        public ApplicationDbContext _context;
        public VolunteerController()
        {
            _context = new ApplicationDbContext();
        }


        // GET: Volunteer
        public ActionResult Index()
        {
            var currentUserName = User.Identity.Name;
            var currentUser = _context.Users.FirstOrDefault(m => m.UserName == currentUserName);
            var currentInfo = _context.Address.FirstOrDefault(m => m.UserId == currentUser.Email);
            var currentSettings = _context.VolunteerSettings.FirstOrDefault(m => m.UserId == currentUser.Email);
            var currentSkills = _context.Skill.FirstOrDefault(m => m.UserId == currentUser.Email);
            List<Opportunity> myOpportunities = new List<Opportunity>();
            List<Interest> interestToCompare = new List<Interest>();
            var myInterest = new Interest();
            foreach (var interest in _context.Interest)
            {
                if (interest.VolunteerId == currentUser.Email)
                {
                    interestToCompare.Add(interest);
                }
            }
            foreach (var opportunity in _context.Opportunity)
            {
                foreach (var interest in interestToCompare)
                {
                    if (opportunity.ID == interest.OpportunityId)
                    {
                        myOpportunities.Add(opportunity);
                    }
                }
            }
            var viewModel = new VolunteerProfileViewModel()
            {
                ApplicationUser = currentUser
            };
            if (currentInfo != null)
            {
                viewModel.Information = currentInfo;
            }
            if (currentSettings != null)
            {
                viewModel.VolunteerSettings = currentSettings;
            }
            if (currentSkills != null)
            {
                viewModel.Skill = currentSkills;
                viewModel.AnimalImageSrc = imagePull(currentSkills.AnimalSkill);
                viewModel.DisasterImageSrc = imagePull(currentSkills.DisasterSkill);
                viewModel.EducationImageSrc = imagePull(currentSkills.EducationSkill);
                viewModel.EnviornmentImageSrc = imagePull(currentSkills.EnviornmentSkill);
                viewModel.HealthImageSrc = imagePull(currentSkills.HealthSkill);
                viewModel.HumanServicesImageSrc = imagePull(currentSkills.HumanServicesSkill);
            }
            if (myOpportunities.Count>0)
            {
                viewModel.MyOpportunities = myOpportunities;
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
                var viewModel = new VolunteerInfoViewModel()
                {
                    StateList = states,
                    FirstName = currentUser.FirstName,
                    LastName = currentUser.LastName,
                    PhoneNumber = currentUser.PhoneNumber,
                    UserTitle = currentUser.UserTitle,
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
                var viewModel = new VolunteerInfoViewModel()
                {
                    StateList = states,
                    FirstName = currentUser.FirstName,
                    LastName = currentUser.LastName,
                    PhoneNumber = currentUser.PhoneNumber,
                    UserTitle = currentUser.UserTitle
                };

            return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Info(VolunteerInfoViewModel model)
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
                userInDatabase.UserTitle = model.UserTitle;

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
                return RedirectToAction("Index", "Volunteer");

            }
            return View(model);
        }
        public ActionResult Skills()
        {
            var currentUserName = User.Identity.Name;
            var currentUser = _context.Users.FirstOrDefault(m => m.UserName == currentUserName);
            var currentSkills = _context.Skill.FirstOrDefault(m => m.UserId == currentUser.Email);
            if (currentSkills != null)
            {
                var viewModel = new VolunteerSkillsViewModel()
                {
                    AboutSkills = currentSkills.AboutSkills,
                    Animal = currentSkills.AnimalSkill
                    
                };
                return View(viewModel);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Skills(VolunteerSkillsViewModel model, FormCollection form)
        {
            var currentUserName = User.Identity.Name;
            var currentUser = _context.Users.FirstOrDefault(m => m.UserName == currentUserName);
            var currentSkills = _context.Skill.FirstOrDefault(m => m.UserId == currentUser.Email);

            if (currentSkills != null)
            {
                currentSkills.AnimalSkill = model.Animal;
                currentSkills.DisasterSkill = model.Disaster;
                currentSkills.EducationSkill = model.Education;
                currentSkills.EnviornmentSkill = model.Enviornment;
                currentSkills.HealthSkill = model.Health;
                currentSkills.HumanServicesSkill = model.HumanServices;
                currentSkills.AboutSkills = model.AboutSkills;
                if (model.AboutAnimal != null)
                {
                    currentSkills.AboutAnimal = model.AboutAnimal;
                }
                if (model.AboutDisaster != null)
                {
                    currentSkills.AboutDisaster = model.Disaster;
                }
                if (model.AboutEducation != null)
                {
                    currentSkills.AboutEducation = model.AboutEducation;
                }
                if (model.AboutEnviornment != null)
                {
                    currentSkills.AboutEnviornment = model.AboutEnviornment;
                }
                if (model.AboutHealth != null)
                {
                    currentSkills.AboutHealth = model.AboutHealth;
                }
                if (model.AboutHumanServices != null)
                {
                    currentSkills.AboutHumanServices = model.AboutHumanServices;
                }
            }
            else
            {
                var newSkill = new Skill()
                {
                    UserId = currentUser.Email,
                    AnimalSkill = model.Animal,
                    DisasterSkill = model.Disaster,
                    EducationSkill = model.Education,
                    EnviornmentSkill = model.Enviornment,
                    HealthSkill = model.Health,
                    HumanServicesSkill = model.HumanServices,
                    AboutSkills = model.AboutSkills
                };
                if (model.AboutAnimal != null)
                {
                    newSkill.AboutAnimal = model.AboutAnimal;
                }
                if (model.AboutDisaster != null)
                {
                    newSkill.AboutDisaster = model.Disaster;
                }
                if (model.AboutEducation != null)
                {
                    newSkill.AboutEducation = model.AboutEducation;
                }
                if (model.AboutEnviornment != null)
                {
                    newSkill.AboutEnviornment = model.AboutEnviornment;
                }
                if (model.AboutHealth != null)
                {
                    newSkill.AboutHealth = model.AboutHealth;
                }
                if (model.AboutHumanServices != null)
                {
                    newSkill.AboutHumanServices = model.AboutHumanServices;
                }
                _context.Skill.Add(newSkill);
            }
            _context.SaveChanges();
            return RedirectToAction("Index","Volunteer");
        }
        public ActionResult Settings()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Settings(VolunteerSettingsViewModel model)
        {
            bool tempContact;
            bool tempRefer;
            bool tempSee;
            var currentUserName = User.Identity.Name;
            var currentUser = _context.Users.FirstOrDefault(m => m.UserName == currentUserName);
            var currentSettings = _context.VolunteerSettings.FirstOrDefault(m => m.UserId == currentUser.Email);
            if (model.CanContact == "0")
            {
                tempContact = false;
            }
            else
            {
                tempContact = true;
            }
            if (model.CanSee == "0")
            {
                tempSee = false;
            }
            else
            {
                tempSee = true;
            }
            if (model.CanRefer == "0")
            {
                tempRefer = false;
            }
            else
            {
                tempRefer = true;
            }

            if (currentSettings !=  null)
            {
                currentSettings.CanContact = tempContact ;
                currentSettings.CanSee = tempSee;
                currentSettings.CanRefer = tempRefer;
            }
            else
            {
                var newSettings = new VolunteerSettings()
                {
                    UserId = currentUser.Email,
                    CanContact = tempContact,
                    CanSee = tempSee,
                    CanRefer = tempRefer
                };
                _context.VolunteerSettings.Add(newSettings);
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Volunteer");
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

        public ActionResult AddPhoto (PhotoViewModel model, FormCollection form)
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
                return RedirectToAction("Index", "Volunteer");
        }
        public ActionResult PartialProfile(string id)
        {
            var wantedUser = _context.Users.FirstOrDefault(m => m.Email == id);
            var wantedInfo = _context.Address.FirstOrDefault(m => m.UserId == wantedUser.Email);
            var wantedInterest = _context.Interest.FirstOrDefault(m => m.VolunteerId == wantedUser.Email);
            var wantedSkill = _context.Skill.FirstOrDefault(m => m.UserId == wantedUser.Email);

            var viewModel = new PartialProfileVolunteerViewModel()
            {
                Volunteer = wantedUser,
                VulunteerInfo = wantedInfo,
                VolunteerSkills = wantedSkill,
                OpportunityInterest = wantedInterest
            };
            if (wantedSkill != null)
            {
                viewModel.AnimalImageSrc = imagePull(wantedSkill.AnimalSkill);
                viewModel.DisasterImageSrc = imagePull(wantedSkill.DisasterSkill);
                viewModel.EducationImageSrc = imagePull(wantedSkill.EducationSkill);
                viewModel.EnviornmentImageSrc = imagePull(wantedSkill.EnviornmentSkill);
                viewModel.HealthImageSrc = imagePull(wantedSkill.HealthSkill);
                viewModel.HumanServicesImageSrc = imagePull(wantedSkill.HumanServicesSkill);
            }
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