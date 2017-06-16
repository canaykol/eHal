using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eHal.Models
{
    public class Review : IElement
    {

        public Review()
        {

        }
        public int Id { get; set; }


        public float score { get; set;}

        public string reviewMessage { get; set; }

        public int positiveNegativeNeutral { get; set; }


        public Seller sender { get; set; }
        public Seller reviewe { get; set; }

    }
}