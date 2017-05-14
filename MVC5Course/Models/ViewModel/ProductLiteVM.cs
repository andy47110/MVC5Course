using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModel
{

    /// <summary>
    /// 這是一個精簡版的 Product 資料，主要用於建立商品資料用
    /// </summary>
    public class ProductLiteVM
    {
        public int ProductId { get; set; }
        [Required]
        [MinLength(5)]
            public string ProductName { get; set; }
        [Required]
        [Range(0, 9999,ErrorMessage ="價格不可小於0或大於9999")]
        public Nullable<decimal> Price { get; set; }
        [Required]
        [Range(0, 9999, ErrorMessage = "庫存不可小於0或大於9999")]
        public Nullable<decimal> Stock { get; set; }
    }
}