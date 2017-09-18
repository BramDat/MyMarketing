using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMarketingBackEnd.Common
{
    public class StringHelper
    {
        public static string FormatBusinessHours(int startHours, int endHours, string startDay, string endDay)
        {
            string formatterTiming = default(string);

            formatterTiming = startDay + " TO " + endDay + " FROM ";

            formatterTiming = formatterTiming + startHours.ToString().PadLeft(2, '0') + FindAMPM(startHours) + " - " + endHours.ToString().PadLeft(2, '0') + FindAMPM(endHours);

            return formatterTiming;
        }

        private static string FindAMPM(int hours)
        {
            if (hours < 12)
                return "AM";
            else
                return "PM";
        }
    }
}
