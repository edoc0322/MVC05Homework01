using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC05Homework01.ViewModels
{
    public class 客戶銀行資訊ExportViewModel
    {
        public string 客戶名稱 { get; set; }

        public string 銀行名稱 { get; set; }
        public int 銀行代碼 { get; set; }
        public string 分行代碼 { get; set; }

        public string 帳戶名稱 { get; set; }

        public string 帳戶號碼 { get; set; }
    }
}