using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop.Models
{
    public class Aanbieding
    {
        public int  ProductId { get; set; }
        public double Percentage { get; set; }
        public DateTime EindDatum { get; set; }
    }
}