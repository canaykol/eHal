using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eHal.Models;
using eHal.Service;
using System.Collections.Generic;

namespace eHal.Tests.ModelTests
{
    [TestClass]
    public class SellerModelTest
    {
        FakeApplicationDbContext db = new FakeApplicationDbContext();

        [TestMethod]
        public void SellerConstructorTest()
        {
            var service = new SellerProfileService(db);
            var vm = new EditSellerViewModel();
            vm.ApplicationUserId = 1;
            vm.birthDate = new DateTime(1995, 04, 21);
            vm.geziKabul = true;
            vm.numune = false;
            vm.telefon = "5414660873";
            vm.tradeName = "Cancan Inc.";
            vm.webAdress = "canaykol.com";


            var cau = new CustomApplicationUser();
            cau.Id = 1;
            

            Seller newSeller = new Seller(cau, vm);


            
            cau.seller = newSeller;


            Assert.AreEqual(new DateTime(1995, 04, 21), newSeller.birthDate);
            Assert.AreEqual(true, newSeller.geziKabul);

            Assert.AreEqual(false, newSeller.numune);
            Assert.AreEqual(new List<Listing>(), newSeller.ilanlar);
            Assert.AreEqual("5414660873", newSeller.telefon);
            Assert.AreEqual("Cancan Inc.", newSeller.tradeName);

            Assert.AreEqual("canaykol.com", newSeller.webAddress);
            


        }


        [TestMethod]
        public void SellerRelationalTest()
        {
            //db içinde cau'ya ve au'ya ulaşma bilgi çekme
            

            




        

        }
    }
}
