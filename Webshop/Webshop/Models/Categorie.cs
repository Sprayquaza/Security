using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop.Models
{
    public class Categorie
    {
        public string Categorienaam { get; set; }
        public List<Product> Producten { get; set; }
    }
}