using MVC05Homework01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC05Homework01.Extensions
{
    public static class IQuerableExtension
    {
        public static IOrderedEnumerable<客戶聯絡人> Sort(this IQueryable<客戶聯絡人> query, string sortOrder, string currentSort)
        {
            Func<客戶聯絡人, object> func;
            if (!(new 客戶聯絡人().GetType().GetProperty(sortOrder) == null))
                func = (客戶聯絡人 x) => typeof(客戶聯絡人).GetProperty(sortOrder).GetValue(x, null);
            else
                func = (客戶聯絡人 x) => x.客戶Id;

            if (sortOrder.Equals(currentSort))
                return query.OrderByDescending(func);
            else
                return query.OrderBy(func);
        }

        public static IOrderedEnumerable<客戶資料> Sort(this IQueryable<客戶資料> query, string sortOrder, string currentSort)
        {
            Func<客戶資料, object> func;
            if (!(new 客戶資料().GetType().GetProperty(sortOrder) == null))
                func = (客戶資料 x) => typeof(客戶資料).GetProperty(sortOrder).GetValue(x, null);
            else
                func = (客戶資料 x) => x.客戶名稱;

            if (sortOrder.Equals(currentSort))
                return query.OrderByDescending(func);
            else
                return query.OrderBy(func);
        }

        // public static IOrderedQueryable<T> Sort(IQueryable<T> query, string currentSort, string sortOrder)
        // {
        //     var Typeq = typeof(T);
        //     Func<T, object> func;
        //     if (!(typeof(T).GetType().GetProperty(sortOrder) == null))
        //         func = (T x) => typeof(T).GetProperty(sortOrder).GetValue(x, null);
        //     else
        //         func = (T x) => typeof(T).GetProperty(sortOrder).GetValue(x, null);
        //
        //     if (sortOrder.Equals(currentSort))
        //         return query.OrderByDescending(func);
        //     else
        //         return query.OrderBy(func);
        // }
    }
}