using System;
using System.Linq;
using System.Collections.Generic;

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

        public IQueryable<客戶資料> AllOfQuery(string 客戶名稱)
        {
            if (string.IsNullOrEmpty(客戶名稱))
                return All();
            else
                return All().Where(x => x.客戶名稱.Contains(客戶名稱));
        }

    }

    public interface I客戶資料Repository : IRepository<客戶資料>
    {

    }
}