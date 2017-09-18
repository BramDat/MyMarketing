using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyMarketingBackEnd.BusinessObjects
{
    /// <summary>
    /// For Logged in Users
    /// </summary>
    public class User
    {
        private string _password;

        [DisplayName("USERNAME")]
        public string UserName { get; set; }

        [DisplayName("PASSWORD")]
        public string Password
        {
            set
            {
                _password = value;
            }
        }

        public string UserId { get; set; }

    }
}
