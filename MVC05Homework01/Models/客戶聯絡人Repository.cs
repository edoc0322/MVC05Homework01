using System;
using System.Linq;
using System.Collections.Generic;

namespace MVC05Homework01.Models
{
    public class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
    {
        public 客戶聯絡人 Find(int id)
        {
            return All().Where(p => p.Id == id).FirstOrDefault();
        }
        public IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(x => !x.已刪除);
        }

        public IQueryable<客戶聯絡人> JobTitleQuery(string 職稱)
        {
            if (string.IsNullOrEmpty(職稱))
                return All();
            else
                return All().Where(x => x.職稱.Equals(職稱));
        }


        public IQueryable<string> Get職稱List()
        {
            return All().GroupBy(x => x.職稱).Select(x => x.Key);
        }

    }

    public interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
    {

    }
}