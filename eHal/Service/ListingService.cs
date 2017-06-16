using eHal.Models;
using LinqKit;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Services.Description;

namespace eHal.Service
{
    public class ListingService
    {
        private IApplicationDbContext db;
        readonly Dictionary<Operation, Func<Expression, Expression, Expression>> Expressions;


        public ListingService(IApplicationDbContext db)
        {
            this.db = db;

        }
        

        public List<Listing> filterListings(FilterObject f)
        {
            
            var parameter = Expression.Parameter(typeof(Listing), "l");
            var member = Expression.Property(parameter, "Id"); //x.Id
            Expression<Func<Listing, bool>> body = x => false;
            var predicate = PredicateBuilder.True<Listing>();
            
            if (!(f.filteredTypes == null))
            {
                var typePredicate = PredicateBuilder.False<Listing>();
                foreach (int typeString in f.filteredTypes)
                {
                    int temp = Convert.ToInt32(typeString);
                    typePredicate = typePredicate.Or(p => p.ProductType.Id == temp);
                }
                predicate = predicate.And(typePredicate);
            }

            if (f.fiyatToggle)
            {
                predicate = predicate.And(p => p.birimFiyat <= f.fiyatMax);
                predicate = predicate.And(p => p.birimFiyat >= f.fiyatMin);
            }

            if (f.miktarToggle)
            {
                predicate = predicate.And(p => p.miktar <= f.miktarMax);
                predicate = predicate.And(p => p.miktar >= f.miktarMin);
            }

            if (f.minimumOrderToggle)
            {
                predicate = predicate.And(p => p.minimumOrder >= f.minimumOrder);
            }


            if (f.hasatToggle)
            {
                predicate = predicate.And(p => p.hasatZamani.monday <= f.hasatMax);
                predicate = predicate.And(p => p.hasatZamani.sunday >= f.hasatMin);
            }

            if (f.sellerId != null)
            {
                predicate = predicate.And(p => p.ilanSahibi.Id == f.sellerId);
            }


            return db.Listings.AsExpandable().Where(predicate).OrderByDescending(c => c.ilanTarihi).ToList();
        }


        public bool createListing(CreateListingViewModel vm)
        {
            Listing newListing = new Listing(vm);
            Seller ilansahibi = db.Sellers.Where(s => s.Id == vm.appuserid).FirstOrDefault(); //burası bozuk 
            Week hasatWeek = db.Weeks.Single(w => w.Id == vm.weekId);


            newListing.ilanSahibi = ilansahibi;
            newListing.hasatZamani = hasatWeek;

            var service = new ValidateService(db);

            if (service.finalValidationNewListing(newListing))
            {
                db.Listings.Add(newListing);
                ilansahibi.ilanlar.Add(newListing);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public ListingIndexViewModel ListingIndex()
        {
            ListingIndexViewModel vm = new ListingIndexViewModel();
            vm.lishingsToShow = db.Listings.Where(c => c.onDisplay == true && c.hasatZamani.sunday > DateTime.Now).OrderByDescending(c => c.ilanTarihi).ToList();


            vm.productTypes = db.ProductTypes.OrderBy(c => c.ProductTypeName).ToList();
            return vm;
        }
    }


    

    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }

}