using MyMarketingBackEnd.Business;
using MyMarketingBackEnd.BusinessObjects;
using MyMarketingBackEnd.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace MyMarketingBackEnd.WebApp.App_Code
{
    public class ControllerHelper
    {
        private readonly string IMAGE_ROOT_FOLDER_PATH = ConfigurationManager.AppSettings["ImagesRootFolder"].ToString();

        public static void PrefillBusinessLists(BusinessTransactions btObj, ref ClientVM clientVM)
        {
            clientVM.BusinessCategoryList = btObj.GetBusinessCategory();
            clientVM.PayPeriodList = btObj.GetPayPeriods();
        }

        public string GetFullLogoDirectory(int clientId)
        {
            return Path.Combine(HostingEnvironment.MapPath(IMAGE_ROOT_FOLDER_PATH), clientId.ToString(), "Logo");
        }

        public string GetLogoDirectory(int clientId)
        {
            string rootPath = IMAGE_ROOT_FOLDER_PATH.TrimStart('~');
            return Path.Combine(rootPath, clientId.ToString(), "Logo");
        }

        public string GetGalleryDirectory(int clientId)
        {
            string rootPath = IMAGE_ROOT_FOLDER_PATH.TrimStart('~');
            return Path.Combine(rootPath, clientId.ToString(), "Gallery");
        }

        public string GetFullGalleryDirectory(int clientId)
        {
            return Path.Combine(HostingEnvironment.MapPath(IMAGE_ROOT_FOLDER_PATH), clientId.ToString(), "Gallery");
        }

        public string GetLogoFileRelativePath(int clientId, string fileName)
        {
            return Path.Combine(GetLogoDirectory(clientId), Path.GetFileName(fileName));
        }

        public bool SaveLogo(ClientBusiness clientObj, HttpPostedFileBase fileBase)
        {
            try
            {
                string logoDir = this.GetFullLogoDirectory(clientObj.ClientId);
                string logoFileFullPath = Path.Combine(logoDir, Path.GetFileName(fileBase.FileName));
                DirectoryInfo di = new DirectoryInfo(logoDir);
                if (!di.Exists)
                    di.Create();
                else
                {
                    foreach (var item in di.GetFiles())
                    {
                        item.Delete();
                    }
                }
                fileBase.SaveAs(logoFileFullPath);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteLogoOnDBUpdateFail(ClientBusiness clientObj, HttpPostedFileBase fileBase)
        {
            string logoDir = this.GetFullLogoDirectory(clientObj.ClientId);
            string logoFileFullPath = Path.Combine(logoDir, Path.GetFileName(fileBase.FileName));
            FileInfo fi = new FileInfo(logoFileFullPath);
            if (fi.Exists)
            {
                fi.Delete();
            }
            return true;
        }
    }
}