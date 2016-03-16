using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int KlantId { get; set; }
        public List<Product> Producten { get; set; }

    }
}