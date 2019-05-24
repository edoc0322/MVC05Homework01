namespace MVC05Homework01.Models
{
    using MVC05Homework01.Attribute.DataTypes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var rep = RepositoryHelper.Get客戶聯絡人Repository();
            var tData = rep.Where(x => x.客戶Id == this.客戶Id && x.Email.Trim().ToUpper().Equals(this.Email)).FirstOrDefault();
            if (tData != null)
                yield return new ValidationResult("同一個客戶的聯絡人Email不可重複", new string[] { "客戶Id", "Email" });
        }
    }

    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Remote("客戶的聯絡人Email重覆確認", "Api", AdditionalFields = "客戶Id", ErrorMessage = "同一個客戶的聯絡人Email不可重複")]
        public int 客戶Id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }

        [StringLength(250, ErrorMessage = "欄位長度不得大於 250 個字元")]
        [Required]
        [Remote("客戶的聯絡人Email重覆確認", "Api",AdditionalFields = "客戶Id",ErrorMessage ="同一個客戶的聯絡人Email不可重複")]
        public string Email { get; set; }

        [MobilePhoneNumber]
        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string 手機 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }

        public bool 已刪除 { get; set; }

        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
