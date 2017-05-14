namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using ValidationAttribute;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product :IValidatableObject
    {
        public int 訂單數量 {
            get
            {
                //return this.OrderLine.Count;
                //因為不是直接從Entity時做出來的東東，所以沒有導覽屬性可以用，須改寫如下:
                using (var db = new FabricsEntities())
                {
                    return db.Product.Find(this.ProductId).OrderLine.Count;
                }
            }
                }

        //設計一個能夠自我驗證商業邏輯的 Model
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //此時已經ModelBinding完成，輸入驗證屬性也完成
            if (this.Price > 100 && this.Stock < 5)
            {
                //var db = new FabricsEntities();
                //有錯誤則Return ValidtaionResult
                yield return new ValidationResult("價格與庫存數量不合理", new string[] { "Price", "Stock" });
            }

            using (var db = new FabricsEntities())
            {
                var prod = db.Product.FirstOrDefault(p => p.ProductId == this.ProductId);
                if (prod != null && prod.OrderLine.Count() > 5 && this.Stock == 0)
                {
                        yield return new ValidationResult("Stock 與訂單數量不匹配",
                                new string[] { "Stock" });
                }
            }

            yield break;
            //throw new NotImplementedException();
        }
    }

    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }

        [Required(ErrorMessage ="請輸入商品名稱")]
        //[MinLength(3), MaxLength(30)]
        [StringLength(80, ErrorMessage="欄位長度不得大於 80 個字元")]
        //System.componet Model
        [DisplayName("商品名稱")]
        [商品名稱必須包含Test字串(ErrorMessage = "商品名稱必須包含Test字串")]
        [MaxWordsAttribute(30,ErrorMessage ="超出10個字串")]
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
