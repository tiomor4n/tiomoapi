using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcApplication2.Models
{
    public class InputData
    {
        [Required(ErrorMessage = "此為必要欄位")]
        [RegularExpression("^/d{4}$", ErrorMessage = "請輸入股票代碼4碼")]
        public string strStockCode { get; set; }


        public string strDataDate { get; set; }
        public string strEndDate { get; set; }

        public int YearCnt 
        {
            get {
                try
                {
                    ClsBaseFunction CBF = new ClsBaseFunction();
                    var temp = CBF.GetDateRoute(strDataDate, strEndDate);
                    return temp.Count;
                }
                catch 
                {
                    return 0;
                }
                
            }
        }
    }
}