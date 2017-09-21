using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMarketingBackEnd.WebApp.Utility
{
    public class Utility
    {
        public static string GenerateUserName(string firstName, string lastName)
        {
            return "";
        }

        public static string GeneratePassword()
        {
            Guid guidForPswrd = new Guid();
            string password = guidForPswrd.ToString().Split('-')[0];
            return password;
        }
    }

    public enum TransactionMode
    {
        Create,
        Update
    }

    public enum DBTransactionStatus
    {
        Success,
        Failure
    }
}