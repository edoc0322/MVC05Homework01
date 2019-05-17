using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC05Homework01.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public 客戶銀行資訊 Find(int id)
        {
            return All().Where(p => p.Id == id).FirstOrDefault();
        }

        public IQueryable<客戶銀行資訊> AllOfNonDel()
        {
            return base.All().Where(x => !x.已刪除);
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}