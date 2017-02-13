using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            return View();
        }
        public ActionResult Info()
        {
            var currentUserName = User.Identity.Name;
            var states = _context.State.ToList();
            var currentUser = _context.Users.FirstOrDefault(m => m.UserName == currentUserName);
            var viewModel = new VolunteerInfoViewModel()
            {
                StateList = states,
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                PhoneNumber = currentUser.PhoneNumber,
                UserTitle = currentUser.UserTitle,
                
            };

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Info(VolunteerInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }
            else
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
                }
                else
                {
                    var newAddress = new Address()
                    {
                        StreetAddress = model.StreetAddress,
                        City = model.City,
                        StateId = model.Address.StateId,
                        Zipcode = model.Zipcode,
                        UserId = currentUser.Email
                    };
                    _context.Address.Add(newAddress);
                }
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Volunteer");
        }
        public ActionResult Skills()
        {
            return View();
        }
        public ActionResult Settings()
        {
            return View();
        }
    }
}