using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyMarketingBackEnd.BusinessObjects;

namespace MyMarketingBackEnd.WebApp.Models
{
    public class ClientVM : ClientAuth
    {
        public List<ClientBusinessGallery> Business { get; set; }

        public Dictionary<int, string> BusinessCategoryList { get; set; }
        public Dictionary<int, string> PayPeriodList { get; set; }

        public ClientVM()
        {
            this.BusinessCategoryList = new Dictionary<int, string>();
        }
    }
}