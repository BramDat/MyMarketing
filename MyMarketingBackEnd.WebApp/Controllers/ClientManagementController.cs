using MyMarketingBackEnd.Business;
using MyMarketingBackEnd.BusinessObjects;
using MyMarketingBackEnd.WebApp.App_Code;
using MyMarketingBackEnd.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMarketingBackEnd.WebApp.Controllers
{
    public class ClientManagementController : Controller
    {

        private ClientTransactions ClientBAObject = new ClientTransactions();
        private BusinessTransactions BusinessBAObject = new BusinessTransactions();
        ControllerHelper ControllerHelper = new ControllerHelper();
        private const int WORKFLOW_START_NUM = 1;

        public ActionResult Index()
        {
            ViewData["StartStepNum"] = WORKFLOW_START_NUM;
            ClientVM clientObject = new ClientVM();
            return View(clientObject);
        }


        [HttpPost]
        [ActionName("SaveClient")]
        public ActionResult SaveClientDetails(ClientVM clientObj, string currentStep)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ClientBAObject.CreateClient(clientObj))
                        return GetBusinessDetails(clientObj, (Convert.ToInt32(currentStep) + 1).ToString());
                    else
                        throw new Exception("Some error occurred");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                    ModelState.AddModelError("More Details", ex);
                    ViewData["StartStepNum"] = currentStep;
                    return View("Index");
                }
            }
            else
            {
                ViewData["StartStepNum"] = currentStep;
                return View("Index");
            }

            return GetBusinessDetails(clientObj, (Convert.ToInt32(currentStep) + 1).ToString());
        }

        [HttpGet]
        [ActionName("BusinessDetails")]
        [ChildActionOnly]
        public ActionResult GetBusinessDetails(ClientVM clientObj, string currentStep)
        {
            ControllerHelper.PrefillBusinessLists(BusinessBAObject, ref clientObj);
            ViewData["StartStepNum"] = currentStep;
            return View("Index", clientObj);
        }

        [HttpPost]
        [ActionName("SaveBusinessDetails")]
        public ActionResult SaveBizDetails(ClientVM clientObj, string currentStep)
        {
            // #remove
            if (ModelState.IsValidField("Business[0].BizDescription") && ModelState.IsValidField("Business[0].NegotiatedPrice"))
            {
                clientObj.Business[0].BizGalleryPath = ControllerHelper.GetGalleryDirectory(clientObj.Business[0].ClientId);
                if (BusinessBAObject.SaveBusinessDetails(clientObj.Business[0]))
                {
                    ViewData["StartStepNum"] = (Convert.ToInt32(currentStep) + 1).ToString();
                    return View("Index", clientObj);
                }
                else
                {
                    ModelState.AddModelError("Error", "Something went wrong while saving data!");
                }
            }
            else
            {
                ModelState.AddModelError("Error", "Please fill in all the mandatory details");
            }
            ViewData["StartStepNum"] = currentStep;
            ControllerHelper.PrefillBusinessLists(BusinessBAObject, ref clientObj);
            return View("Index", clientObj);
        }

        [HttpPost]
        [ActionName("UploadLogo")]
        public ActionResult SaveLogoToDirectory(ClientBusiness clientObj, string currentStep, HttpPostedFileBase fileBase)
        {

            if (fileBase != null && fileBase.ContentLength > 0)
            {
                try
                {
                    clientObj.BizGalleryPath = ControllerHelper.GetGalleryDirectory(clientObj.ClientId);
                    if (ControllerHelper.SaveLogo(clientObj, fileBase))
                    {
                        if (BusinessBAObject.UploadLogo(clientObj))
                        {
                            ClientVM clientVMObj = new ClientVM(); // Passing the view model itself to the GET of GalleryAction
                            ClientBusinessGallery cbg = new ClientBusinessGallery();
                            cbg.BizId = clientObj.BizId;
                            cbg.ClientId = clientObj.ClientId;
                            clientVMObj.Business = new List<ClientBusinessGallery>();
                            clientVMObj.Business.Add(cbg);
                            return SaveImagesToGallery(clientVMObj, (Convert.ToInt32(currentStep) + 1).ToString());
                        }
                        else
                        {
                            ControllerHelper.DeleteLogoOnDBUpdateFail(clientObj, fileBase);
                            throw new Exception("Error while updating DB.");
                        }
                    }
                    else
                        throw new Exception("Error while saving image.");
                }
                catch (Exception ex)
                {
                    ViewData["StartStepNum"] = currentStep;
                    ModelState.AddModelError("Error", ex.Message);
                    return View("Index", clientObj);
                }
            }
            else
            {
                ViewData["StartStepNum"] = currentStep;
                ModelState.AddModelError("Error", "Please select a file to upload");
                return View("Index", clientObj);
            }
        }

        [ActionName("UploadToGallery")]
        public ActionResult SaveImagesToGallery(ClientVM clientObj, string currentStep)
        {
            ViewData["StartStepNum"] = currentStep;
            return View("Index");
        }
    }
}
