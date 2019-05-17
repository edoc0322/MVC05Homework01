using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC05Homework01.Models
{
    public static class EnumModel
    {
        public enum 客戶分類
        {
            拒絕往來戶 = 1,
            A級客戶 = 2,
            S級客戶 = 3,
            VIP級客戶 = 4,
            VVIP級客戶 = 5
        }
    }
}