using MyMarketingBackEnd.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMarketingBackEnd.WebApp.Controllers
{
    public class HomeController : Controller
    {
        [ActionName("Login")]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [ActionName("SignIn")]
        [HttpPost]
        public ActionResult LoginSubmission(User userObj)
        {
            // Validate User and authorize
            return RedirectToAction("Index");
        }

        [ActionName("Index")]
        public ActionResult LandingPage()
        {
            return View();
        }
    }
}
