using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eHal.Service;

namespace eHal.Tests.ModelTests
{
    [TestClass]
    public class ViewServiceTest
    {
        [TestMethod]
        public void ViewServiceTimeDifferenceYılTest()
        {
            var since = DateTime.Now.AddYears(-3);
            string message = ViewService.timeDifference(since);
            Assert.AreEqual("3 yıl", message);
        }
        [TestMethod]
        public void ViewServiceTimeDifferenceAyTest()
        {
            var since = DateTime.Now.AddMonths(-5);
            string message = ViewService.timeDifference(since);
            Assert.AreEqual("5 ay", message);
        }
        [TestMethod]
        public void ViewServiceTimeDifferenceHaftaTest()
        {
            var since = DateTime.Now.AddDays(-14);
            string message = ViewService.timeDifference(since);
            Assert.AreEqual("2 hafta", message);
        }
        [TestMethod]
        public void ViewServiceTimeDifferenceGunTest()
        {
            var since = DateTime.Now.AddDays(-4);
            string message = ViewService.timeDifference(since);
            Assert.AreEqual("4 gün", message);
        }

        [TestMethod]
        public void ViewServiceTimeDifferenceSaatTest()
        {
            var since = DateTime.Now.AddHours(-2);
            string message = ViewService.timeDifference(since);
            Assert.AreEqual("2 saat", message);
        }
        [TestMethod]
        public void ViewServiceTimeDifferenceDakikaTest()
        {
            var since = DateTime.Now.AddMinutes(-46);
            string message = ViewService.timeDifference(since);
            Assert.AreEqual("46 dakika", message);
        }
        [TestMethod]
        public void ViewServiceTimeDifferenceSaniyeTest()
        {
            var since = DateTime.Now.AddSeconds(-30);
            string message = ViewService.timeDifference(since);
            Assert.AreEqual("30 saniye", message);
        }
    }


    [TestClass]
    public class GetMondayTest
    {
        [TestMethod]
        public void FromMonday1()
        {

            var theActualMonday = new DateTime(1935, 08, 5);

            var fromMonday = new DateTime(1935, 08, 5);
            var fromTuesday = new DateTime(1935, 08, 6);
            var fromWednesday = new DateTime(1935, 08, 7);
            var fromThursday = new DateTime(1935, 08, 8);
            var fromFriday = new DateTime(1935, 08, 9);
            var fromSaturday = new DateTime(1935, 08, 10);
            var fromSunday = new DateTime(1935, 08, 11);

            var guess1 = ViewService.getMonday(fromMonday);
            var guess2 = ViewService.getMonday(fromTuesday);
            var guess3 = ViewService.getMonday(fromWednesday);
            var guess4 = ViewService.getMonday(fromThursday);
            var guess5 = ViewService.getMonday(fromFriday);
            var guess6 = ViewService.getMonday(fromSaturday);
            var guess7 = ViewService.getMonday(fromSunday);


            Assert.AreEqual(theActualMonday, guess1);
            Assert.AreEqual(theActualMonday, guess2);
            Assert.AreEqual(theActualMonday, guess3);
            Assert.AreEqual(theActualMonday, guess4);
            Assert.AreEqual(theActualMonday, guess5);
            Assert.AreEqual(theActualMonday, guess6);
            Assert.AreEqual(theActualMonday, guess7);
        }


        [TestMethod]
        public void FromMonday2()
        {

            var theActualMonday = new DateTime(2017, 03, 20);

            var fromMonday = new DateTime(2017, 03, 21);
            var fromTuesday = new DateTime(2017, 03, 22);
            var fromWednesday = new DateTime(2017, 03, 22);
            var fromThursday = new DateTime(2017, 03, 23);
            var fromFriday = new DateTime(2017, 03, 24);
            var fromSaturday = new DateTime(2017, 03, 25);
            var fromSunday = new DateTime(2017, 03, 26);

            var guess1 = ViewService.getMonday(fromMonday);
            var guess2 = ViewService.getMonday(fromTuesday);
            var guess3 = ViewService.getMonday(fromWednesday);
            var guess4 = ViewService.getMonday(fromThursday);
            var guess5 = ViewService.getMonday(fromFriday);
            var guess6 = ViewService.getMonday(fromSaturday);
            var guess7 = ViewService.getMonday(fromSunday);


            Assert.AreEqual(theActualMonday, guess1);
            Assert.AreEqual(theActualMonday, guess2);
            Assert.AreEqual(theActualMonday, guess3);
            Assert.AreEqual(theActualMonday, guess4);
            Assert.AreEqual(theActualMonday, guess5);
            Assert.AreEqual(theActualMonday, guess6);
            Assert.AreEqual(theActualMonday, guess7);
        }
    }

    

}
