﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;

namespace MVC05Homework01.Extensions
{
    public static class DatatableAndListExtensions
    {
        public static DataTable ListToDataTable<TResult>(this IEnumerable<TResult> ListValue) where TResult : class, new()
        {
            //建立一個回傳用的 DataTable
            DataTable dt = new DataTable();

            //取得映射型別
            Type type = typeof(TResult);

            //宣告一個 PropertyInfo 陣列，來接取 Type 所有的共用屬性
            PropertyInfo[] PI_List = null;

            foreach (var item in ListValue)
            {
                //判斷 DataTable 是否已經定義欄位名稱與型態
                if (dt.Columns.Count == 0)
                {
                    //取得 Type 所有的共用屬性
                    PI_List = item.GetType().GetProperties();

                    //將 List 中的 名稱 與 型別，定義 DataTable 中的欄位 名稱 與 型別
                    foreach (var item1 in PI_List)
                        dt.Columns.Add(item1.Name, item1.PropertyType);
                }

                //在 DataTable 中建立一個新的列
                DataRow dr = dt.NewRow();

                //將資料足筆新增到 DataTable 中
                foreach (var item2 in PI_List)
                {
                    dr[item2.Name] = item2.GetValue(item, null);
                }

                dt.Rows.Add(dr);
            }

            dt.AcceptChanges();

            return dt;
        }
    }
}