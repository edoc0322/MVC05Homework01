namespace MVC05Homework01.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(客戶資料統計MetaData))]
    public partial class 客戶資料統計
    {
    }

    public partial class 客戶資料統計MetaData
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 客戶名稱 { get; set; }
        [Required]
        public int 聯絡人數量 { get; set; }
        [Required]
        public int 銀行帳戶數量 { get; set; }
    }
}
