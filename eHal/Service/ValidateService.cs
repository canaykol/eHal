
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eHal.Models;

namespace eHal.Service
{
    public class ValidateService
    {
        public IApplicationDbContext db;

        public ValidateService(IApplicationDbContext db)
        {
            this.db = db;
        }

        public bool finalValidationNewListing(Listing l) //db'ye eklenmeden hemen önce yapılacak
        {

            //Minimum Order
            if (l.malinTamami == true)
            {
                if (l.minimumOrder != l.miktar)
                {
                    return false;
                }
            }
            else
            {
                if (l.minimumOrder > l.miktar || l.minimumOrder <= 0)
                {
                    return false;
                }
            }

            //Birim Fiyat
            if (l.birimFiyat <= 0)
            {
                return false;
            }
            //Miktar
            if (l.miktar <= 0)
            {
                return false;
            }

            /* ppff bilemedim gerekli mi
            //Hasat Zamanı
            if (l.hasatZamani == null)
            {
                l.hasatZamani = db.Weeks.Single(w => w.Id == l.hasatZamaniId);
            }*/

            if (l.ProductType == null || l.ilanSahibi == null || l.hasatZamani == null)
            {
                return false;
            }
            DateTime now = DateTime.Now;
            DateTime max = now.AddYears(1);
            //Hasat Tarihi
            if (l.hasatZamani.sunday.Date < now || l.hasatZamani.monday.Date > max )
            {
                return false;
            }

            //Legitimate Product Type
            if(db.ProductTypes.Single(c => c.Id == l.ProductType.Id) == null)
            {
                return false;
            }
            //Legitimate Listing Owner
            if (db.Sellers.Single(c => c.Id == l.ilanSahibi.Id) == null)
            {
                return false;
            }


            return true;
        }
    }
}