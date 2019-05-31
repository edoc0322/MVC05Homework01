using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using MVC05Homework01.ViewModels;
using System.Web.ModelBinding;
using MVC05Homework01.Models;

namespace MVC05Homework01.Services
{
    public class LoginService
    {
        private 客戶資料Repository rep;
        public LoginService()
        {
            rep = RepositoryHelper.Get客戶資料Repository();
        }

        public bool LoginValidation(LoginViewModel model)
        {
            if (model == null)
                return false;
            var encryPwd = EncryPasswd(model.密碼);
            return rep.All().Any(x => x.帳號.Equals(model.帳號) && x.密碼.Equals(encryPwd));
        }


        public string EncryPasswd(string Password)
        {
            if (string.IsNullOrEmpty(Password))
                throw new NullReferenceException();
            SHA256 sha256 = new SHA256CryptoServiceProvider();//建立一個SHA256
            byte[] source = Encoding.Default.GetBytes(Password);//將字串轉為Byte[]
            byte[] crypto = sha256.ComputeHash(source);//進行SHA256加密
            return Convert.ToBase64String(crypto);//把加密後的字串從Byte[]轉為字串
        }
        //public 

    }
}