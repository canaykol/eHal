using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eHal.Models;
using eHal.Service;
using System.Linq;

namespace eHal.Tests.ModelTests
{
    /// <summary>
    /// Summary description for ListingModelTest
    /// </summary>

    

    [TestClass]
    public class ListingModelTest
    {

        FakeApplicationDbContext db = new FakeApplicationDbContext();

        public ListingModelTest()
        {

            var fakeDb = new FakeApplicationDbContext();

            db.Sellers = new FakeDbSet<Seller>();
            db.Listings = new FakeDbSet<Listing>();
            db.ProductTypes = new FakeDbSet<ProductType>();

            var sulu = new Seller() { tradeName = "Sülü" };
            var kadir = new Seller() { tradeName = "Kadir" };
            var memo = new Seller() { tradeName = "Memo" };

            var acjur = new ProductType() { ProductTypeName = "Acjur" };
            var kanana = new ProductType() { ProductTypeName = "Kanana" };


            db.Sellers.Add(sulu);
            db.Sellers.Add(memo);
            db.Sellers.Add(kadir);

            db.ProductTypes.Add(acjur);
            db.ProductTypes.Add(kanana);

            db.Listings.Add(new Listing() { ilanSahibi = sulu, ProductType = acjur });
            db.Listings.Add(new Listing() { ilanSahibi = kadir, ProductType = kanana });
            db.Listings.Add(new Listing() { ilanSahibi = memo, ProductType = acjur });
            db.Listings.Add(new Listing() { ilanSahibi = memo, ProductType = kanana });

            fakeDb.SaveChanges();

        }



        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ListingConstructor()
        {
            //Arrange
            var seller = new Seller() { tradeName = "Can" };
            var productType = new ProductType() { ProductTypeName = "Elma" };
            db.Sellers.Add(seller);
            db.ProductTypes.Add(productType);

            //Act
            Listing l = new Listing()
            {
                ilanSahibi = seller,
                ProductType = productType
            };
            //Assert

            Assert.AreEqual(null,l.birimFiyat);
            Assert.AreEqual(null,l.toplamFiyat);
            Assert.AreEqual(null,l.minimumOrder);
            Assert.AreEqual(null,l.miktar);
            Assert.AreEqual(true, l.onDisplay);
            Assert.AreEqual(false, l.organik);
            Assert.AreEqual(false, l.birinciKalite);
            Assert.AreEqual(0, l.pageViews);
            Assert.AreEqual(DateTime.Now.Date,l.ilanTarihi.Date);
            Assert.AreEqual(seller,l.ilanSahibi);
            Assert.AreEqual(productType, l.ProductType);









        }
    }
}
