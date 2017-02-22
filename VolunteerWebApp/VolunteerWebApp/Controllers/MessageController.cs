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
        // GET: Message
        public ActionResult Index(string email)
        {
            List<ApplicationUser> sendToList = new List<ApplicationUser>();
            var currentUserName = User.Identity.Name;
            var currentUser = _context.Users.FirstOrDefault(m => m.UserName == currentUserName);
            var sendTo = _context.Users.FirstOrDefault(m => m.Email == email);
            sendToList.Add(sendTo);
            var viewModel = new MessageViewModel()
            {
                SendFrom = currentUser,
                SendList = sendToList,
                Subject = "",
                Message = "",
            };
            return View(viewModel);
        }
        [ActionName("MessageAll")]
        public ActionResult Index(List<string> users)
        {

            return View();
        }
        [HttpPost]
        public ActionResult Index(MessageViewModel model, ApplicationUser sender)
        {
            var Sender = _context.Users.FirstOrDefault(m => m.Id == sender.Id);
            var message = model.Message;
            var subject = "Help.Hub message from" + model.SendFrom.Email + "regarding" + model.Subject;


            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential("helphubmessageservice@gmail.com", "Blockaway87."),
                EnableSsl = true
            };
            //foreach (var user in model.SendList)
            //{
                client.Send("helphubmessageservice@gmail.com", "smithge87@gmail.com", "test" , "testbody");
            //}



            if (User.IsInRole("volunteer"))
            {
                return RedirectToAction("Index", "Volunteer");
            }
            else if (User.IsInRole("organization"))
            {
                return RedirectToAction("Index", "Organization");
            }
            return RedirectToAction("Index", "Home");


            //try
            //{
            //    WebMail.EnableSsl = true;
            //    WebMail.SmtpUseDefaultCredentials = true;
            //    WebMail.SmtpServer = "smtp.gmail.com";
            //    WebMail.SmtpPort = 465;
            //    WebMail.UserName = "smithge87";
            //    WebMail.Password = "blockawaytub";
            //    WebMail.From = model.SendFrom;

            //    // Create array containing file name
            //    //var filesList = new string[] { fileAttachment };

            //    // Attach file and send email
            //    WebMail.Send(to: customerEmail,
            //        subject: subjectLine,
            //        body: fileAttachment + customerName
            //        /*filesToAttach: filesList*/);
            //}
            //catch (Exception ex)
            //{
            //    errorMessage = ex.Message;
            //}
        }

    }
}