using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eHal.Models;
using eHal.Controllers;
using System.Web.Mvc;
using eHal.Service;
using System.Collections.Generic;
using System.Web;
using System.Linq;

namespace eHal.Tests
{
    [TestClass]
    public class CacheServiceTests
    {

        [TestMethod]
        public void GetStatisticsCache()
        {
            //Arrange
            var fakeDb = new FakeApplicationDbContext();

            fakeDb.Sellers = new FakeDbSet<Seller>();
            fakeDb.Listings = new FakeDbSet<Listing>();
            fakeDb.ProductTypes = new FakeDbSet<ProductType>();

            var sulu = new Seller() { tradeName = "Sülü" };
            var kadir = new Seller() { tradeName = "Kadir" };
            var memo = new Seller() { tradeName = "Memo" };

            var acjur = new ProductType() { ProductTypeName = "Acjur" };
            var kanana = new ProductType() { ProductTypeName = "Kanana" };


            fakeDb.Sellers.Add(sulu);
            fakeDb.Sellers.Add(memo);
            fakeDb.Sellers.Add(kadir);

            fakeDb.ProductTypes.Add(acjur);
            fakeDb.ProductTypes.Add(kanana);

            fakeDb.Listings.Add(new Listing() { onDisplay = true, ilanSahibi = sulu, ProductType = acjur });
            fakeDb.Listings.Add(new Listing() { onDisplay = true, ilanSahibi = kadir, ProductType = kanana });
            fakeDb.Listings.Add(new Listing() { onDisplay = true, ilanSahibi = memo, ProductType = acjur });
            fakeDb.Listings.Add(new Listing() { onDisplay = false, ilanSahibi = memo, ProductType = kanana });

            fakeDb.SaveChanges();



            //Act
            var service = new CacheService(fakeDb);
            HomePageViewModel vm = service.getStatsCache();


            //Assert
            Assert.AreEqual(3, vm.listingNo);
            Assert.AreEqual(0, vm.listingTon);
            Assert.AreEqual(3, vm.sellerNo);

        }

        [TestMethod]
        public void GetProductTypeCache()
        {
            //Arrange
            var fakeDb = new FakeApplicationDbContext();
            fakeDb.ProductTypes = new FakeDbSet<ProductType>();

            var acjur = new ProductType() { ProductTypeName = "Acjur" };
            var kanana = new ProductType() { ProductTypeName = "Kanana" };
            fakeDb.ProductTypes.Add(acjur);
            fakeDb.ProductTypes.Add(kanana);

            fakeDb.SaveChanges();




            //Act
            
            var service = new CacheService(fakeDb);
            var fromMethod = service.getProductTypeCache();
            var fromDb = fakeDb.ProductTypes.ToList();

            //Assert


            CollectionAssert.AreEqual(fromDb, fromMethod);






        }

        
    }




}
