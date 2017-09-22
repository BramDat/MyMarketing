﻿using MyMarketingBackEnd.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMarketingBackEnd.WebApp.Utility
{
    public class Utility
    {
        private static int uNameCheckCounter = 0;
        private static List<string> userIdList = new List<string>();
        private static ClientTransactions ctObj = new ClientTransactions();

        public static string GenerateUserName(string firstName, string lastName)
        {
            string newUName = default(string);

            if (uNameCheckCounter < firstName.Length)
                newUName = firstName[uNameCheckCounter].ToString() + lastName.Substring(0, 5);
            else
                newUName = firstName[0].ToString() + lastName.Substring(1, 6);

            if (userIdList == null || userIdList.Count < 1)
                return newUName;

            if (!CheckUserNameExists(newUName))
                return newUName;
            else
                return GenerateUserName(firstName, lastName);
        }

        public static string GeneratePassword()
        {
            Guid guidForPswrd = new Guid();
            string password = guidForPswrd.ToString().Split('-')[0];
            return password;
        }

        public static bool CheckUserNameExists(string userName)
        {
            if (userIdList == null)
                userIdList = ctObj.GetUserIDs();

            if (userIdList.Any(x => userName.Contains(x)))
            {
                uNameCheckCounter++;
                return true;
            }
            else
                return false;
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