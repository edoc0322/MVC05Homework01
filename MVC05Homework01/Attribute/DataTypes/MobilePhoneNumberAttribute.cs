using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MVC05Homework01.Attribute.DataTypes
{
    public class MobilePhoneNumberAttribute : DataTypeAttribute
    {
        public MobilePhoneNumberAttribute() : base(DataType.Text)
        {
            ErrorMessage = "{0}欄位格式不正確，(e.g. 0911-111111)";
        }
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;
            return Regex.IsMatch(value.ToString(), @"\d{4}-\d{6}");
        }
    }
}