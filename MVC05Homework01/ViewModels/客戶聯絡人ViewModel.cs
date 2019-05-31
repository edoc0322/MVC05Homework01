using MVC05Homework01.Attribute.DataTypes;
using MVC05Homework01.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC05Homework01.ViewModels
{
    public class 客戶聯絡人批次更新ViewModel
    {
        public int Id { get; set; }

        public int 客戶Id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }


        public string 姓名 { get; set; }

        public string Email { get; set; }

        [MobilePhoneNumber]
        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string 手機 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }


        public virtual 客戶資料 客戶資料 { get; set; }
    }
}