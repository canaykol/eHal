using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eHal.Models;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using eHal.Service;

namespace eHal.Controllers
{
    public class ListingsController : Controller
    {
        private IApplicationDbContext db;

        public ListingsController()
        {
            db = new ApplicationDbContext();
        }

        public ListingsController(IApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        // GET: Listings
        

        public ActionResult Index(int?[] selectedPT, int? orderBy, int? pageNumber, int sellerId, float? miktarMax, float? miktarMin, float? fiyatMax, float? fiyatMin, float? minimumOrder)
        {
            var service = new ListingService(db);
            var cservice = new CacheService(db);

            ListingIndexViewModel vm = new ListingIndexViewModel();
            var filter = new FilterObject();
            if (sellerId != null)
            {
                filter.sellerId = sellerId;
            }
            if (selectedPT != null)
            {
                filter.filteredTypes = selectedPT;
            }
            if (fiyatMin != null)
            {
                filter.fiyatMin = (float) fiyatMin;
            }
            if (fiyatMax != null)
            {
                filter.fiyatMax = (float) fiyatMax;
            }
            if (miktarMax != null)
            {
                filter.miktarMax = (float) miktarMax;
            }
            if (miktarMin != null)
            {
                filter.miktarMin = (float) miktarMin;
            }
            if (minimumOrder != null)
            {
                filter.minimumOrder = (float) minimumOrder;
            }


            vm.productTypes = cservice.getProductTypeCache();
            vm.lishingsToShow = service.filterListings(filter);
            return View(vm);
        }


        [HttpPost]
        public ActionResult Index(ListingIndexViewModel returnedVM)
        {
            //threading yapılacak burada;
            var service = new ListingService(db);
            var cservice = new CacheService(db);
            ListingIndexViewModel newVM = new ListingIndexViewModel(returnedVM);


            newVM.productTypes = cservice.getProductTypeCache();
            FilterObject filterObj = new Models.FilterObject(returnedVM);
            newVM.lishingsToShow = service.filterListings(filterObj);
            


            string theUrl; // method to obtain the url using the return VM
            //return RedirectToAction("Index", "Listings", routeValues: new { selectedPT = returnedVM.selectedPT });
            return View(newVM);
        }

        // GET: Listings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listing listing = db.Listings.Find(id);
            var sahip = listing.ilanSahibi; /*http://stackoverflow.com/questions/6038541/ef-validation-failing-on-update-when-using-lazy-loaded-required-properties */
            var tur = listing.ProductType;
            listing.pageViews = listing.pageViews + 1;
            


            db.SaveChanges();

            if (listing == null)
            {
                return HttpNotFound();
            }
            return View(listing);
        }

        [Authorize]
        public ActionResult Create()
        {
            
            var um = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            int appuserid = User.Identity.GetUserId<int>();
            if (!um.IsInRole(appuserid, "Seller"))
            {
                return RedirectToAction("Create", "Sellers");
            }
            else
            {
                
                CreateListingViewModel vm = new CreateListingViewModel();
                

                var service = new CacheService(this.db);
                
                vm.pTypeList = service.getProductTypeCache();
                var now = DateTime.Now;
                var aYearAhead = now.AddYears(1);
                vm.availableWeeks = this.db.Weeks.Where(w => w.monday > now && w.monday < aYearAhead).ToList();
                return View(vm);
            }
            
        }

        
        [HttpPost]
        public ActionResult Create(CreateListingViewModel vm)
        {
            var um = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            vm.appuserid = User.Identity.GetUserId<int>();

            if (vm.appuserid == 0)
            {
                //return badass error   
            }
            var serv = new ListingService(db);
            if (serv.createListing(vm))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                throw new Exception();
            }
            

        }

    }
}
