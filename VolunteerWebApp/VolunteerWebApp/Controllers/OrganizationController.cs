using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            return View();
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




            return RedirectToAction("Index", "Organization");
        }
    }

}