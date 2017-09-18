using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMarketingBackEnd.BusinessObjects
{
    public class PayPeriod
    {
        public int PayPeriodId { get; set; }

        public string PayPeriodType { get; set; }

        public bool IsActive { get; set; }
    }
}
