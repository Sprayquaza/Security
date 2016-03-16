using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Webshop.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Productnaam { get; set; }
        public string Beschrijving { get; set; }
        public double Prijs { get; set; }

    }
}