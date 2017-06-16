using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eHal.Models
{
    public class Listing : IElement
    {
        public Listing()
        {
            this.ilanTarihi = DateTime.Now;
            this.pageViews = 0;
            this.onDisplay = true;
            this.controlled = false;
        }

        public Listing(CreateListingViewModel vm)
        {


            if (vm.malinTamami || vm.minimumOrder >= vm.miktar)
            {
                this.malinTamami = true;
                this.minimumOrder = vm.miktar;
            }
            else
            {
                this.malinTamami = false;
                this.minimumOrder = vm.minimumOrder;
            }

            this.birimFiyat = vm.birimFiyat;
            this.miktar = vm.miktar;
            this.ProductTypeId = vm.ProductTypeId;
            this.birinciKalite = vm.kalite;
            this.placedInBoxed = vm.placedInBoxed;
            this.sera = vm.sera;

            this.onDisplay = true;
            this.controlled = false;

            this.ilanTarihi = DateTime.Now;
            this.pageViews = 0;
        }

        public int Id { get; set; }



        [Required]
        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }


        [Required]
        [Display(Name = "Satıştaki Miktar")]
        public float miktar { get; set; } //ton

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Kilogram Fiyatı (TL/kg)")]
        public float birimFiyat { get; set; }
        

        [DataType(DataType.Currency)]
        public float toplamFiyat
        {
            get
            {
                return this.miktar * this.birimFiyat;
            }


        }

        
        public virtual Week hasatZamani { get; set; }
        public int hasatZamaniId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ilanTarihi { get; set; }

        public bool malinTamami { get; set; }


        [Required]
        [Display(Name = "Minimum sipariş miktarı")]
        public float minimumOrder { get; set; }


        [Required]
        public virtual Seller ilanSahibi { get; set; }
        public int ilanSahibiId { get; set; }



        public string Aciklama
        {
            get
            {
                string aciklama = "";
                if (malinTamami)
                {
                    aciklama = "Bu ilanda belirtilan malın tamamı satılıktır";
                }
                else
                {

                }
                return aciklama;
            }


        }

        [Display(Name = "Görüntülenme sayısı")]
        public int pageViews { get; set; }

        [Display(Name = "Kutulara yerleştirilmiş")]
        public bool placedInBoxed { get; set; }

        [Display(Name = "Serada yetiştirilmiş")]
        public bool sera { get; set; }

        [Display(Name = "I. Kalite ürün")]
        public bool birinciKalite { get; set; }

        [Display(Name = "İlan gösterimde")]
        public bool onDisplay { get; set; }
        [Display(Name = "Hasat tarihi geçti")]
        public bool expired
        {
            get
            { 
                return hasatZamani.sunday < DateTime.Now;
            }
        }

        
        [Display(Name = "Kontrol Edildi")]
        public bool controlled { get; set; }
        [Display(Name = "Organik Sertifikalı")]
        public bool organik { get; set; }
        [Display(Name = "İyi Tarım Sertifikalı")]
        public bool iyiTarim { get; set; }

        

        void control(bool newState)
        {

            if (newState == false)
            {
                controlled = false;
                onDisplay = false;
            }
            else if (newState == true)
            {
                controlled = true;
                onDisplay = true;
            }
        }

        void updateOnDisplay()
        {
            if (expired)
            {
                onDisplay = false;
            }
            else
            {
                onDisplay = true;
            }
        }

        void incrementPageView()
        {
            this.pageViews = this.pageViews + 1;
        }
    }


    public class Week : IElement
    {
        
        public Week()
        {

        }

        public Week(DateTime monday, CultureInfo cul)
        {
            if(monday.DayOfWeek == DayOfWeek.Monday)
            {
                this.monday = monday;
                this.sunday = this.monday.AddDays(6);
                this.weekNumber = cul.Calendar.GetWeekOfYear( monday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);


                this.tag = weekNumber+". Hafta: "+monday.ToString("dd/MM/yyyy") +" - "+sunday.ToString("dd/MM/yyyy");
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
            

        }

        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        public DateTime monday { get; set; }

        public DateTime sunday { get; set; }

        public int weekNumber { get; set; }

        public string tag { get; set; }



    }

    public class FilterObject
    {
        public int?[] filteredTypes { get; set; }
        public bool fiyatToggle { get; set; }
        public bool miktarToggle { get; set; }
        public bool minimumOrderToggle { get; set; }
        public bool hasatToggle { get; set; }
        public double? fiyatMin { get; set; }
        public double? fiyatMax { get; set; }
        public double? miktarMin { get; set; }
        public double? miktarMax { get; set; }
        public double? minimumOrder { get; set; }
        public DateTime? hasatMin { get; set; }
        public DateTime? hasatMax { get; set; }
        public int? orderBy { get; set; }
        public int? pageNumber { get; set; }

        public int? sellerId { get; set; }

        public FilterObject()
        {
        }
        public FilterObject(ListingIndexViewModel vm)
        {
            this.filteredTypes = vm.selectedPT;
            this.fiyatToggle = vm.fiyatToggle;
            this.miktarToggle = vm.miktarToggle;
            this.minimumOrderToggle = vm.minimumOrderToggle;
            this.hasatToggle = vm.hasatToggle;

            this.fiyatMin = vm.fiyatMin;
            this.fiyatMax = vm.fiyatMax;
            this.miktarMin = vm.miktarMin;
            this.miktarMax = vm.miktarMax;
            this.minimumOrder = vm.minimumOrder;
            this.hasatMin = vm.hasatMin;
            this.hasatMax = vm.hasatMax;
            this.orderBy = vm.sortBy;
            this.pageNumber = vm.pageIndex;

        }
    }

}