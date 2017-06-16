using eHal.Models;
using eHal.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eHal.Controllers
{
    public class HomeController : Controller
    {
        private IApplicationDbContext db;

        public HomeController()
        {
            db = new ApplicationDbContext();
            
        }

        public HomeController(IApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public ActionResult Index()
        {
            var service = new CacheService(db);
            
            HomePageViewModel vm = service.getStatsCache();
            vm.productTypes = service.getProductTypeCache();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(string[] selectedFilters)
        {

            return RedirectToAction("Index", "Listing", new {selectedPT = selectedFilters });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}