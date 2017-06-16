using eHal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eHal.Service
{
    public class SellerProfileService
    {
        private IApplicationDbContext db;

        public SellerProfileService(IApplicationDbContext db)
        {
            this.db = db;
        }


        public Seller CreateNewSeller(EditSellerViewModel vm)
        {

            CustomApplicationUser user = db.CustomApplicationUsers.Single(c => c.ApplicationUserId == vm.ApplicationUserId);
            var newSeller = new Seller(user, vm);


            user.seller = newSeller;

            db.Sellers.Add(newSeller);
            db.SaveChanges();

            return newSeller;
        }


    }



}