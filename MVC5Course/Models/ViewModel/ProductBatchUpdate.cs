using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModel
{
    public class ProductBatchUpdate
    {
        public int ProductId { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Stock { get; set; }
    }
}