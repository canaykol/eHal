using eHal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eHal.Service
{
    public class CacheService
    {
        private IApplicationDbContext db;

        public CacheService(IApplicationDbContext db)
        {
            this.db = db;
        }

        
        public List<ProductType> getProductTypeCache()
        {
            List<ProductType> productTypes;
            object p5 = HttpRuntime.Cache.Get("ProductTypes");
            if (p5 == null)
            {
                productTypes = db.ProductTypes.OrderBy(c => c.ProductTypeName).ToList();
                HttpRuntime.Cache.Insert("ProductTypes", productTypes, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1));
            }
            else
            {
                productTypes = (List<ProductType>) p5;
            }
            return productTypes;
        }

        public HomePageViewModel getStatsCache()
        {
            var vm = new HomePageViewModel();

            object p2 = HttpRuntime.Cache.Get("SellerNumber");
            if (p2 == null)
            {
                vm.sellerNo = db.Sellers.Count();
                HttpRuntime.Cache.Insert("SellerNumber", vm.sellerNo, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1));
            }
            else
            {
                vm.sellerNo = (int)p2;
            }

            object p3 = HttpRuntime.Cache.Get("ListingNumber");
            if (p3 == null)
            {
                vm.listingNo = db.Listings.Where(c => c.onDisplay == true).Count();
                HttpRuntime.Cache.Insert("ListingNumber", vm.listingNo, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1));
            }
            else
            {
                vm.listingNo = (int)p3;
            }


            object p4 = HttpRuntime.Cache.Get("TotalTon");
            if (p4 == null)
            {
                //vm.listingTon = (int) (db.Listings.Where(c => c.onDisplay == true).Sum(c => c.Miktar) /1000);
                HttpRuntime.Cache.Insert("TotalTon", vm.listingTon, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1));
            }
            else
            {
                vm.listingTon = (int)p4;
            }



            return vm;
        }


        //To-be triggered during app-start and every ten min?
        public void updateStatsCache()
        {
            int productTypeNo = db.ProductTypes.Count();
            int sellerNo = db.Sellers.Count();
            int listingNo = db.Listings.Count();
            int listingTon = (int) db.Listings.Sum(c => c.miktar) / 1000;


            HttpRuntime.Cache.Insert("ProductTypeNumber", productTypeNo, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1));
            HttpRuntime.Cache.Insert("SellerNumber", sellerNo, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1));
            HttpRuntime.Cache.Insert("ListingNumber", listingNo, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1));
            HttpRuntime.Cache.Insert("TotalTon", listingTon, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1));
        }

    }

}