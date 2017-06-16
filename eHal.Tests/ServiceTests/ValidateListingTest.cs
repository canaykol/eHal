using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eHal.Models;
using eHal.Service;

namespace eHal.Tests
{
    [TestClass]
    public class ValidateListingTest
    {
        FakeApplicationDbContext db = new FakeApplicationDbContext();
        ValidateService service;
        CalendarService cal;
        ProductType pType1;
        ProductType pType2;
        Seller seller;
        Week normalHasat;

        public ValidateListingTest()
        {
            this.service = new ValidateService(db);
            this.cal = new CalendarService(db);
            this.cal.updateWeeks(DateTime.Now);

            pType1 = new ProductType() { Id = 1, ProductTypeName = "Acjur" };
            pType2 = new ProductType() { Id = 2, ProductTypeName = "Kanana" };
            seller = new Seller() { tradeName = "Can Aykol" }; //daha sonra id ekle

            db.ProductTypes.Add(pType1);
            db.ProductTypes.Add(pType2);
            db.Sellers.Add(seller);
            db.SaveChanges();

            
            normalHasat = cal.getNextNthWeek(20);

        }

        [TestMethod]
        public void ValidateListingNormalCase()
        {
            Listing l = new Listing()
            {
                //ProductType, Hasat, seller, miktar, minimumOrder, birimFiyat
                ProductType = pType1,
                hasatZamani = normalHasat,
                ilanSahibi = seller,
                miktar = 3000,
                minimumOrder = 1000,
                birimFiyat = 3,

                malinTamami = false,

            };

            bool result = service.finalValidationNewListing(l);

            Assert.AreEqual(true, result);
        }


        [TestMethod]
        public void ValidateListingEarlierThanToday()
        {
            //Arrange
            Listing l = new Listing()
            {
                //ProductType, Hasat, seller, miktar, minimumOrder, birimFiyat
                ProductType = pType1,
                hasatZamani = cal.getNextNthWeek(-1),
                ilanSahibi = seller,
                miktar = 3000,
                minimumOrder = 1000,
                birimFiyat = 3,

                malinTamami = false,
            };
            //Act
            bool result = service.finalValidationNewListing(l);
            //Assert
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void ValidateListingMoreThanAYear()
        {
            //Arrange
            Week hasat = cal.getNextNthWeek(54);
            Listing l = new Listing()
            {
                //ProductType, hasatZamani, seller, ilanSahibi, minimumOrder, birimFiyat
                ProductType = pType1,
                hasatZamani = cal.getNextNthWeek(-1),
                ilanSahibi = seller,
                miktar = 3000,
                minimumOrder = 1000,
                birimFiyat = 3,

                malinTamami = false,
            };
            //Act
            bool result = service.finalValidationNewListing(l);
            //Assert
            Assert.AreEqual(false, result);
        }


        [TestMethod]
        public void ValidateListingNoQuantity()
        {
            //Arrange
            Listing l = new Listing()
            {
                //ProductType, Hasat, seller, miktar, minimumOrder, birimFiyat
                ProductType = pType1,
                hasatZamani = normalHasat,
                ilanSahibi = seller,
                minimumOrder = 1000,
                birimFiyat = 3,

                malinTamami = false,
            };
            //Act
            bool result = service.finalValidationNewListing(l);
            //Assert
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void ValidateListingNoPrice()
        {
            Listing l = new Listing()
            {
                //ProductType, Hasat, seller, miktar, minimumOrder, birimFiyat
                ProductType = pType1,
                hasatZamani = normalHasat,
                ilanSahibi = seller,
                miktar = 3000,
                minimumOrder = 1000,

                malinTamami = false,
            };

            bool result = service.finalValidationNewListing(l);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void ValidateListingNoProductType()
        {
            //Arrange
            Listing l = new Listing()
            {
                //ProductType, hasatZamani, seller, miktar, minimumOrder, birimFiyat
                hasatZamani = normalHasat,
                ilanSahibi = seller,
                miktar = 3000,
                minimumOrder = 100,
                birimFiyat = 3,

                malinTamami = false,
            };

            //Act
            bool result = service.finalValidationNewListing(l);
            //Assert
            Assert.AreEqual(false, result);
        }
        

        [TestMethod]
        public void ValidateListingMalinTamamiCaseLegitimate()
        {
            //Arrange
            Listing l = new Listing()
            {
                //ProductType, hasatZamani, seller, miktar, minimumOrder, birimFiyat
                ProductType = pType1,
                hasatZamani = normalHasat,
                ilanSahibi = seller,
                miktar = 3000,
                minimumOrder = 3000,
                birimFiyat = 3,

                malinTamami = true,
            };

            //Act
            bool result = service.finalValidationNewListing(l);
            //Assert
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void ValidateListingMalinTamamiCaseNonLegitimate()
        {
            //Arrange
            Listing l = new Listing()
            {
                //ProductType, hasatZamani, seller, miktar, minimumOrder, birimFiyat
                ProductType = pType1,
                hasatZamani = normalHasat,
                ilanSahibi = seller,
                miktar = 300,
                minimumOrder = 200,
                birimFiyat = 3,

                malinTamami = true,
            };

            //Act
            bool result = service.finalValidationNewListing(l);
            //Assert
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void ValidateListingMinimumOrderMoreThanQuantityCase()
        {
            //Arrange
            Listing l = new Listing()
            {
                //ProductType, hasatZamani, seller, miktar, minimumOrder, birimFiyat
                ProductType = pType1,
                hasatZamani = normalHasat,
                ilanSahibi = seller,
                miktar = 3000,
                minimumOrder = 4000,
                birimFiyat = 3,

                birinciKalite = true,
                organik = false,
                malinTamami = false,
                onDisplay = false,
            };

            //Act
            bool result = service.finalValidationNewListing(l);
            //Assert
            Assert.AreEqual(false, result);
        }

        public void ValidateListingNoMinimumOrderCase()
        {
            //Arrange
            Listing l = new Listing()
            {
                //ProductType, hasatZamani, ilanSahibi, miktar, minimumOrder, birimFiyat
                ProductType = pType1,
                hasatZamani = normalHasat,
                ilanSahibi = seller,
                miktar = 3000,
                minimumOrder = 0,
                birimFiyat = 3,

                malinTamami = false,
            };

            //Act
            bool result = service.finalValidationNewListing(l);
            //Assert
            Assert.AreEqual(false, result);
        }

        
        [TestMethod]
        public void ValidateListingNoOwnerCase()
        {
            //Arrange
            Listing l = new Listing()
            {
                //ProductType, hasatZamani, ilanSahibi, miktar, minimumOrder, birimFiyat
                ProductType = pType1,
                hasatZamani = normalHasat,
                miktar = 3000,
                birimFiyat = 3,
                minimumOrder = 100,

                malinTamami = true,
            };

            //Act
            bool result = service.finalValidationNewListing(l);
            //Assert
            Assert.AreEqual(false, result);
        }
    }
}
