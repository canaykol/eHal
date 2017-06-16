using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eHal.Models
{
    public class ProductType : IElement
    {
        public int Id { get; set; }

        [Display(Name = "Ürünün adı")]
        public string ProductTypeName { get; set; }

        public string headerImage { get; set; }

        public virtual ICollection<Listing> listingsOfTheType { get; set; }

        
    }
}