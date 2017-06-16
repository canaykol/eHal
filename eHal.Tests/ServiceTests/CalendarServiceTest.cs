using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eHal.Models;

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eHal.Models;
using eHal.Controllers;
using System.Web.Mvc;
using eHal.Service;
using System.Collections.Generic;
using System.Web;
using System.Linq;

namespace eHal.Tests.ServiceTests
{
    [TestClass]
    public class CalendarServiceTest

    {
        CalendarService service;
        FakeApplicationDbContext db = new FakeApplicationDbContext();

        public CalendarServiceTest()
        {
            service = new CalendarService(db);
        }

        [TestMethod]
        public void UpdateWeeksTest()
        {
            
            service.updateWeeks(DateTime.Now);

            DateTime nextMonday = db.Weeks.ToList().First().monday;
            DateTime nextSunday = db.Weeks.ToList().First().sunday;

            Assert.AreEqual(DayOfWeek.Monday, nextMonday.DayOfWeek);
            Assert.AreEqual(DayOfWeek.Sunday, nextSunday.DayOfWeek);
            for (int i = 0; i < 100; i++)
            {
                Week w = db.Weeks.ToList().ElementAt(i);
                Assert.AreEqual(nextMonday, w.monday);
                Assert.AreEqual(nextSunday, w.sunday);
                nextMonday = nextMonday.AddDays(7);
                nextSunday = nextSunday.AddDays(7);
                
            }
            //50 hafta ilerisi
            service.updateWeeks(DateTime.Now.AddDays(7*50));
        }
    }
}
