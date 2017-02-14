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
                    AboutSkills = currentSkills.AboutSkills
                };
                return View(viewModel);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Skills(VolunteerSkillsViewModel model)
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
            var currentUserName = User.Identity.Name;
            var currentUser = _context.Users.FirstOrDefault(m => m.UserName == currentUserName);
            var currentSettings = _context.VolunteerSettings.FirstOrDefault(m => m.UserId == currentUser.Email);

            if (currentSettings !=  null)
            {
                currentSettings.CanContact = model.CanContact;
                currentSettings.CanSee = model.CanSee;
                currentSettings.CanRefer = model.CanRefer;
            }
            else
            {
                var newSettings = new VolunteerSettings()
                {
                    UserId = currentUser.Email,
                    CanContact = model.CanContact,
                    CanSee = model.CanSee,
                    CanRefer = model.CanRefer
                };
                _context.VolunteerSettings.Add(newSettings);
            }
            _context.SaveChanges();

            return View();
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
    }
}