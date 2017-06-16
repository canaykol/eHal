using eHal.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace eHal.Service
{
    public class CalendarService
    {
        private IApplicationDbContext db;

        public CalendarService(IApplicationDbContext db)
        {
            this.db = db;
        }

        public Week getThisWeek()
        {
            var today = DateTime.Now.Date;
            Week thisWeek = db.Weeks.Single(w => w.monday < today && w.sunday > today);
            
            return thisWeek;

        }

        public Week getNextNthWeek(int n)
        {
            var today = DateTime.Now.Date;
            var thatDay = today.AddDays(7*n);
            Week thisWeek = db.Weeks.Single(w => w.monday < thatDay && w.sunday > thatDay);
            return thisWeek;
        }



        public void updateWeeks(DateTime now)
        {

            var cult = CultureInfo.CurrentCulture;
            
            int haftaSonrasi = 100;
            int haftaO = 30;
            
            int gunOncesi = - (int) haftaO * 7;

            DateTime startingMonday = ViewService.getMonday(now).AddDays(gunOncesi);
            
            for (int i = 0; i<(haftaSonrasi + haftaO); i++)
            {
                
                if (!db.Weeks.Any(w => w.monday == startingMonday))
                {
                    Week newWeek = new Week(startingMonday.Date, cult);
                    db.Weeks.Add(newWeek);
                }
                startingMonday = startingMonday.AddDays(7);
                
            }
            
            db.SaveChanges();
        }

        
    } 
    
}