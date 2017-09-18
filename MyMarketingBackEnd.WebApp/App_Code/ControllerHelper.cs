using MyMarketingBackEnd.Business;
using MyMarketingBackEnd.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMarketingBackEnd.WebApp.App_Code
{
    public class ControllerHelper
    {
        public static void PrefillBusinessDetails(BusinessTransactions btObj, ref ClientVM clientVM)
        {
            clientVM.BusinessCategoryList = btObj.GetBusinessCategory();
            clientVM.PayPeriodList = btObj.GetPayPeriods();
        }
    }
}