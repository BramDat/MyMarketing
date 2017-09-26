using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMarketingBackEnd.WebApp.Models
{
    public class BusinessNameListVM
    {
        public Dictionary<int,string> BusinessList { get; set; }

        public int SelectedBusinessId { get; set; }
    }
}