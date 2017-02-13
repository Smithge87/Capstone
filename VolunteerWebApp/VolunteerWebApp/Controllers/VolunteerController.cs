using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VolunteerWebApp.Controllers
{
    public class VolunteerController : Controller
    {
        // GET: Volunteer
        public ActionResult VolunteerIndex()
        {
            return View();
        }
    }
}