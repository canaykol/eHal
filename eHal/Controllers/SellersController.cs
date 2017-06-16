using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eHal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using eHal.Service;

namespace eHal.Controllers
{
    public class SellersController : Controller
    {
        private IApplicationDbContext db;

        public SellersController()
        {
            db = new ApplicationDbContext();
        }

        public SellersController(IApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        // GET: Sellers
        public ActionResult Index()
        {
            return View(db.Sellers.ToList());
        }

        // GET: Sellers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = db.Sellers.Find(id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            return View(seller);
        }

        [Authorize]
        public ActionResult Create()
        {
            var um = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            int appuserid = User.Identity.GetUserId<int>();
            if (um.IsInRole(appuserid, "Seller"))
            {
                return RedirectToAction("Index", "Home"); //throw exception page with message "You already have a Seller Profile click here to edit it"
            }
            else
            {
                var vm = new EditSellerViewModel();
                return View(vm);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(EditSellerViewModel vm)
        {

            var service = new SellerProfileService(db);
            
            vm.ApplicationUserId = User.Identity.GetUserId<int>();

            
            
            var newSeller = service.CreateNewSeller(vm);

            var um = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            um.AddToRole(newSeller.CustomApplicationUser.ApplicationUserId, "Seller");




            
            return RedirectToAction("Index", "Home");
        }
    }
}
