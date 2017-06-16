using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web.Mvc;
using System;

namespace eHal.Models
{
    public class CreateListingViewModel
    {
        public int ProductTypeId { get; set; }
        public bool malinTamami { get; set; }
        public int miktar { get; set; }
        public float birimFiyat { get; set; }
        public int minimumOrder { get; set; }
        public bool sera { get; set; }
        public bool kalite { get; set; }
        public bool placedInBoxed { get; set; }
        public List<ProductType> pTypeList { get; set; }
        public int appuserid { get; set; }
        public int weekId { get; set; }
        public List<Week> availableWeeks { get; set; }

    }


    public class EditSellerViewModel
    {
        public string tradeName { get; set; }
        public DateTime birthDate { get; set; }
        public string telefon { get; set; }
        public string webAddress { get; set; }
        public bool geziKabul { get; set; }
        public bool numune { get; set; }
        public int ApplicationUserId { get; set; }

    }



    public class ListingIndexViewModel
    {

        
        //display
        public IEnumerable<Listing> lishingsToShow { get; set; }
        public IEnumerable<ProductType> productTypes { get; set; }

        //response
            //filters
        public double? miktarMin { get; set; }
        public double? miktarMax { get; set; }
        public double? fiyatMin { get; set; }
        public double? fiyatMax { get; set; }
        public double? minimumOrder { get; set; }

        public bool miktarToggle { get; set; }
        public bool fiyatToggle { get; set; }
        public bool hasatToggle { get; set; }
        public bool minimumOrderToggle { get; set; }

        public DateTime? hasatMin { get; set; }
        public DateTime? hasatMax { get; set; }

        public int?[] selectedPT { get; set; }

        public int? sellerId { get; set; }
            //sorting
        public int pageIndex { get; set; }
        public int sortBy { get; set; }



        public ListingIndexViewModel() {

            this.selectedPT = null;
            this.fiyatMax = null;
            this.fiyatMin = null;
            this.hasatMax = null;
            this.hasatMin = null;
            this.minimumOrder = null;
            this.miktarMax = null;
            this.miktarMin = null;
            this.fiyatToggle = false;
            this.miktarToggle = false;
            this.hasatToggle = false;
            this.minimumOrderToggle = false;



        }

        public ListingIndexViewModel(ListingIndexViewModel returnedVM)
        {
            this.selectedPT = returnedVM.selectedPT;
            this.fiyatMax = returnedVM.fiyatMax;
            this.fiyatMin = returnedVM.fiyatMin;
            this.hasatMax = returnedVM.hasatMax;
            this.hasatMin = returnedVM.hasatMin;
            this.minimumOrder = returnedVM.minimumOrder;
            this.miktarMax = returnedVM.miktarMax;
            this.miktarMin = returnedVM.miktarMin;

            this.fiyatToggle = returnedVM.fiyatToggle;
            this.miktarToggle = returnedVM.miktarToggle;
            this.hasatToggle = returnedVM.hasatToggle;
            this.minimumOrderToggle = returnedVM.minimumOrderToggle;
        }
    }


    public class HomePageViewModel
    {
        public IEnumerable<ProductType> productTypes;
        public int sellerNo;
        public int listingNo;
        public int listingTon;
        public int productTypeNo;
    }
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}