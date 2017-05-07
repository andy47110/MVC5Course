using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModel
{

    /// <summary>
    /// 精簡版的Product資料
    /// </summary>
    public class ProductLiteVM
    {
        public int ProductId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [MinLength(5)]
            public string ProductName { get; set; }
        [Required]

        public Nullable<decimal> Price { get; set; }
        [Required]

        public Nullable<decimal> Stock { get; set; }
    }
}