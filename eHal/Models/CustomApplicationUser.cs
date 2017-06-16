using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eHal.Models
{

    public class CustomApplicationUser : IElement
    {
        public CustomApplicationUser()
        {
        }

        public CustomApplicationUser(int ApplicationUserId)
        {
            this.joinDate = DateTime.Now;
            this.ApplicationUserId = ApplicationUserId;
            this.watchList = new List<Listing>();
            this.ApplicationUserId = ApplicationUserId;

        }


        public int Id { get; set; }
        

        public int ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicaitonUser { get; set; }


        public string personName { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime joinDate { get; set; }

        
        public virtual ICollection<Listing> watchList { get; set; }

        
        public int sellerId { get; set; }
        public  virtual Seller seller { get; set; }
        
    }

    public class Seller 
    {
        public Seller()
        {

        }

        public Seller(CustomApplicationUser user, EditSellerViewModel vm)
        {
            this.ilanlar = new List<Listing>();
            this.CustomApplicationUser = user;
            this.Id = user.Id;

            this.tradeName = vm.tradeName;
            this.telefon = vm.telefon;
            this.webAddress = vm.webAddress;
            this.birthDate = vm.birthDate;
            this.geziKabul = vm.geziKabul;
            this.numune = vm.numune;
        }
        
        public int Id { get; set; }


        [Required]
        public int CustomApplicationUserId { get; set; }
        public virtual CustomApplicationUser CustomApplicationUser { get; set; }
        
        

        [Required]
        public string tradeName { get; set; }

        [Required]
        public DateTime birthDate { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:###-###-####}", ApplyFormatInEditMode = true)]
        public string telefon { get; set; }

        [DisplayFormat(NullDisplayText = "", ApplyFormatInEditMode = true)]
        public string acikAdres { get; set; }

        [DisplayFormat(NullDisplayText = "", ApplyFormatInEditMode = true)]
        public string logo { get; set; }

        [DisplayFormat(NullDisplayText = "", ApplyFormatInEditMode = true)]
        public string webAddress { get; set; }

        [Display(Name = "Bu kullanıcı müşterilerine üretim ve depolama alanını gezdirmeyi kabul ediyor.")]
        public bool geziKabul { get; set; }
        [Display(Name = "Bu kullanıcı müşterilerine numune yollamayı kabul ediyor.")]
        public bool numune { get; set; }

        
        

        public virtual ICollection<Listing> ilanlar { get; set; }



        public int positiveReviews { get; set; }
        public int negativeReviews { get; set; }


        public List<Review> Reviews { get; set; }

        public decimal score { get; set; }
    }
    
}

