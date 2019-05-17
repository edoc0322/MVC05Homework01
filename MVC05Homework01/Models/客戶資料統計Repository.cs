using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC05Homework01.Models
{   
	public  class 客戶資料統計Repository : EFRepository<客戶資料統計>, I客戶資料統計Repository
	{
    }

	public  interface I客戶資料統計Repository : IRepository<客戶資料統計>
	{

	}
}