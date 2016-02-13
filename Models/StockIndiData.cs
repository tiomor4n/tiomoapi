using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication2.Models
{
    public class StockIndi
    {
        public string TxnDate { get; set; }
        public int DealStockCnt { get; set; }
        public long DealStockAmt { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal ClosePrice { get; set; }
        public string PriceDiff { get; set; }
        public int DealCount { get; set; }
    }

    public class TWNInd 
    {
        public string TxnDate { get; set; }
        public long DealAmt { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal ClosePrice { get; set; }
    }
}