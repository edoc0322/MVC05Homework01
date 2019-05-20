using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Dynamic;
using System.Text;

namespace MVC05Homework01.Models
{
    public class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
    {
        public 客戶資料 Find(int id)
        {
            return All().Where(p => p.Id == id).FirstOrDefault();
        }

        public IQueryable<客戶資料> All()
        {
            return base.All().Where(x => !x.已刪除);
        }

        public string test(string a, params object[] qaq) { return ""; }
        public IQueryable<客戶資料> AllOfQuery(int? 分類篩選, string 客戶名稱)
        {
            var condition = new List<string>();
            var paramsObj = new List<object>();
            int i = 0;

            if (分類篩選.HasValue)
            {
                condition.Add($"客戶分類.Value == @{i++}");
                paramsObj.Add(分類篩選.Value);
            }
            if (!string.IsNullOrEmpty(客戶名稱))
            {
                condition.Add($"客戶名稱.Contains(@{i++})");
                paramsObj.Add(客戶名稱);
            }

            if (paramsObj.Count > 0)
                return All().Where(string.Join(" AND ", condition), paramsObj.ToArray());
            else
                return All();

            // //這邊要怎麼整理阿 我的天啊
            // if (string.IsNullOrEmpty(客戶名稱) && (!分類篩選.HasValue || 分類篩選.Value == 0))
            //     return All();
            // else if (string.IsNullOrEmpty(客戶名稱))
            //     return All().Where(x => x.客戶分類.Value == (分類篩選));
            // else if (!分類篩選.HasValue || 分類篩選.Value == 0)
            //     return All().Where(x => x.客戶名稱.Contains(客戶名稱));
            // else
            //     return All().Where(x => x.客戶名稱.Contains(客戶名稱) && x.客戶分類.Value == 分類篩選);
        }
    }

    public interface I客戶資料Repository : IRepository<客戶資料>
    {

    }
}