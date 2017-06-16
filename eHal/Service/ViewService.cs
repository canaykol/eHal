using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eHal.Service
{
    public static class ViewService
    {

        public static string timeDifference(DateTime startDate)
        {
            TimeSpan duration = DateTime.Now - startDate;
            if (duration.TotalDays > 365.25)
            {
                return String.Concat(Math.Round(duration.TotalDays / 365.25), " yıl");
            }
            else if (duration.TotalDays > 30)
            {
                return String.Concat(Math.Round(duration.TotalDays / 30), " ay");
            }
            else if (duration.TotalDays > 7)
            {
                return String.Concat(Math.Round(duration.TotalDays / 7), " hafta");
            }
            else if (duration.TotalDays > 1)
            {
                return String.Concat(Math.Round(duration.TotalDays), " gün");
            }
            else if (duration.TotalHours > 1)
            {
                return String.Concat(Math.Round(duration.TotalHours), " saat");
            }
            else if (duration.TotalMinutes > 1)
            {
                return String.Concat(Math.Round(duration.TotalMinutes), " dakika");
            }
            else
            {
                return String.Concat(Math.Round(duration.TotalSeconds), " saniye");
            }
        }
        
        public static DateTime getMonday(this DateTime from)
        {
            int start = (int)from.DayOfWeek;
            if(start == 0){ start = 7;}
            int target = (int)DayOfWeek.Monday;
            return from.AddDays(target - start).Date;

        }
    }
}