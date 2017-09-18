using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMarketingBackEnd.BusinessObjects
{
    public class ClientAuth : Client
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        //public DateTime AccountRegistrationDate { get; set; }
        // Use Client class's CreatedDate

        public DateTime AccountExpiryDate { get; set; }

        public DateTime LastLoginTime { get; set; }
    }
}
