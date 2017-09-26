using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMarketingBackEnd.WebApp.Controllers
{
    public class BusinessController : Controller
    {
        public ActionResult Index()
        {
            // Get the list of businesses
            return View();
        }

        public ActionResult EditBusiness(string businessId)
        {
            return View();
        }

    }
}
