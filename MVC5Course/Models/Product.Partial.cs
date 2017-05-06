namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
    }
    
    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }
        [Required(ErrorMessage ="請輸入商品名稱")]
        [MinLength(3), MaxLength(30)]
        [StringLength(80, ErrorMessage="欄位長度不得大於 80 個字元")]
        //System.componet Model
        [DisplayName("商品名稱")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "請輸入商品ID")]
        //System.componet Model
        [DisplayName("商品價格")]
        public Nullable<decimal> Price { get; set; }
        //System.componet Model
        [DisplayName("是否上架")]
        public Nullable<bool> Active { get; set; }
        //System.componet Model
        [DisplayName("是否有庫存")]
        public Nullable<decimal> Stock { get; set; }
    
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}