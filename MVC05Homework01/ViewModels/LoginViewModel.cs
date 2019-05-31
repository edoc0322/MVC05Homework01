using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC05Homework01.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string 帳號 { get; set; }

        [Required]
        public string 密碼 { get; set; }
    }
}