using MyMarketingBackEnd.Business;
using MyMarketingBackEnd.BusinessObjects;
using MyMarketingBackEnd.WebApp.Models;
using MyMarketingBackEnd.WebApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMarketingBackEnd.WebApp.Controllers
{
    public class BusinessManagementController : Controller
    {
        BusinessTransactions businessBAObject = new BusinessTransactions();
        private GenericDataReader genericDRSingleton = GenericDataReader.GetInstance();

        public ActionResult Index()
        {
            // Get the list of businesses
            return View();
        }

        [ActionName("EditBusiness")]
        public ActionResult UpdateBusinessDetails(string businessId)
        {
            // Display biz details in a form
            // List down the gallery items
            // Display the logo
            BusinessNameListVM model = new BusinessNameListVM();
            model.BusinessList = businessBAObject.GetBusinessList();
            model.SelectedBusinessId = model.BusinessList.FirstOrDefault().Key;
            return View("EditBusiness", model);
        }

        public PartialViewResult GetBusinessDetails(string id)
        {
            ClientVM obj = new ClientVM();
            obj.PayPeriodList = genericDRSingleton.PayPeriodList;
            obj.BusinessCategoryList = genericDRSingleton.BusinessCategoryList;

            if (businessBAObject == null)
                businessBAObject = new BusinessTransactions();

            ClientBusiness cb = new ClientBusiness();

            businessBAObject.GetBusinessDetails(Convert.ToInt32(id), ref cb);

            if (obj.Business == null)
                obj.Business = new List<ClientBusiness>();

            obj.Business.Add(cb);
            return PartialView("_BusinessDetailsFull", obj);
        }

        [HttpPost]
        public ActionResult SaveBusinessDetails(ClientVM obj)
        {
            BusinessNameListVM model = new BusinessNameListVM();
            try
            {
                if (businessBAObject.UpdateBusinessDetails(obj.Business[0]))
                    ViewData["Message"] = DBTransactionStatus.Success;
                else
                    ViewData["Message"] = DBTransactionStatus.Failure;
            }
            catch (Exception ex)
            {
                ViewData["ErrMessage"] = ex.Message;
            }
            finally
            {
                model.BusinessList = businessBAObject.GetBusinessList();
                model.SelectedBusinessId = model.BusinessList.FirstOrDefault().Key;
            }
            return View("EditBusiness", model);
        }

    }
}