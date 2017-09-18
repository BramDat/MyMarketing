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
        public Dictionary<int, string> BizTimingStartHours { get; set; }
        public Dictionary<int, string> BizTimingEndHours { get; set; }
        public Dictionary<string, string> BizTimingStartDay { get; set; }
        public Dictionary<string, string> BizTimingEndDay { get; set; }

        public ClientVM()
        {
            this.BusinessCategoryList = new Dictionary<int, string>();
            this.PayPeriodList = new Dictionary<int, string>();

            BizTimingStartHours = InitializeHours();
            BizTimingEndHours = InitializeHours();
            BizTimingStartDay = InitializeDays();
            BizTimingEndDay = InitializeDays();
        }

        private Dictionary<int, string> InitializeHours()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();

            dic.Add(0, "00 AM");
            dic.Add(1, "01 AM");
            dic.Add(2, "02 AM");
            dic.Add(3, "03 AM");
            dic.Add(4, "04 AM");
            dic.Add(5, "05 AM");
            dic.Add(6, "06 AM");
            dic.Add(7, "07 AM");
            dic.Add(8, "08 AM");
            dic.Add(9, "09 AM");
            dic.Add(10, "10 AM");
            dic.Add(11, "11 AM");
            dic.Add(12, "12 PM");
            dic.Add(13, "01 PM");
            dic.Add(14, "02 PM");
            dic.Add(15, "03 PM");
            dic.Add(16, "04 PM");
            dic.Add(17, "05 PM");
            dic.Add(18, "06 PM");
            dic.Add(19, "07 PM");
            dic.Add(20, "08 PM");
            dic.Add(21, "09 PM");
            dic.Add(22, "10 PM");
            dic.Add(23, "11 PM");

            return dic;
        }

        private Dictionary<string, string> InitializeDays()
        {
            Dictionary<string, string> days = new Dictionary<string, string>();

            days.Add("MON", "Monday");
            days.Add("TUE", "Monday");
            days.Add("WED", "Monday");
            days.Add("THU", "Monday");
            days.Add("FRI", "Monday");
            days.Add("SAT", "Monday");
            days.Add("SUN", "Monday");

            return days;
        }
    }


}