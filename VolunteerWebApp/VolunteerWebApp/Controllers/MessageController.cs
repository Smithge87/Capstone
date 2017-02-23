using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using VolunteerWebApp.Models;

namespace VolunteerWebApp.Controllers
{
    public class MessageController : Controller
    {
        public ApplicationDbContext _context;
        public MessageController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpGet]
        public ActionResult Index(List<string> users)
        {
            List<ApplicationUser> sendToList = new List<ApplicationUser>();
            var currentUserName = User.Identity.Name;
            var currentUser = _context.Users.FirstOrDefault(m => m.UserName == currentUserName);
            foreach (string email in users)
            {
                var sendTo = _context.Users.FirstOrDefault(m => m.Email == email);
                sendToList.Add(sendTo);
            }
            var viewModel = new MessageViewModel()
            {
                SendFrom = currentUser,
                SendList = sendToList,
                Subject = "",
                Message = "",
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Index(MessageViewModel model, FormCollection form, List<string> sendTo)
        {
            List<string> sending = new List<string>();
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\Jack\Desktop\helphubemail.txt");
            while ((line = file.ReadLine()) != null)
            {
                sending.Add(line);
            }
            file.Close();
            var subject = "";
            var message = model.Message;
            var senderId = form.AllKeys[2];
            var sender = _context.Users.FirstOrDefault(m => m.Id == senderId);
            if (User.IsInRole("organization"))
            {
                subject = "Help.Hub message from " + sender.OrganizationName + " regarding " + model.Subject;
            }
            else if (User.IsInRole("volunteer"))
            {
                subject = "Help.Hub message from " + sender.Email + " regarding " + model.Subject;
            }

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(sending[0], sending[1]),
                EnableSsl = true
            };

            foreach (var person in sendTo)
            {
                var actualPerson = _context.Users.FirstOrDefault(m => m.Id == person);
                client.Send("helphubmessageservice@gmail.com", actualPerson.Email, subject, message);
            }

            if (User.IsInRole("volunteer"))
            {
                return RedirectToAction("Index", "Volunteer");
            }
            else if (User.IsInRole("organization"))
            {
                return RedirectToAction("Index", "Organization");
            }
            return RedirectToAction("Index", "Home");

        }

    }
}