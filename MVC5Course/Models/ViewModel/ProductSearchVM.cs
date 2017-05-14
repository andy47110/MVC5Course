using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;

namespace MVC5Course.Models.ViewModel
{
    public class ProductSearchVM :IValidatableObject
    {
        public ProductSearchVM()
        {
            this.vstockCntStart = 0;
            this.vstockCntEnd = 9999;
        }


         public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.vstockCntEnd < this.vstockCntStart)
            {
                yield return new ValidationResult("庫存數量設定錯誤", new string[] { "vstockCntStart, vstockCntEnd" });
            }
        }
        public string vproductName { get; set; }
        public int vstockCntStart { get; set; }
        public int vstockCntEnd { get; set; }
        
    }
}