using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MvcApplication2.Models;


namespace MvcApplication2
{
    class ClsOperate
    {
        public enum WebDataType{Individual,TWNIndex};
        public delegate void MyDeligate(string vstr);

        static ClsOperate() 
        {
           
        }

        public WebDataType WDT
        {
            get;
            set;
        }

        public DataTable OutputDT
        {
            get;
            set;
        }

        public List<StockIndi> OutputList { get; set; }
        public List<TWNInd> OutputIndList { get; set; }

        public string Para1 { get; set; }
        public string Para2 { get; set; }
        public string Para3 { get; set; }

        public void Main_Batch() 
        {
            //WebDataType WDT = new WebDataType();
            switch (WDT)
            {
                case WebDataType.Individual:
                    //OutputList = GetIndiStockData(Para1, Para2);
                    GetIndiStockData(Para1, Para2, Para3);
                    break;
                case WebDataType.TWNIndex:
                    GetTWNIndex(Para2, Para3);
                    break;
            }
        }


        public void GetIndiStockData(string vstrStockCode, string vstrDate,string vstrEndDate) 
        {
            string strYear;
            string strMonth;
            List<string> ListDataMonth = new List<string>();
            List<StockIndi> LiTotal = new List<StockIndi>();
            ClsBaseFunction CBF = new ClsBaseFunction();
            DataTable TotalDt = new DataTable();
            DAO clsDao = new DAO();
            ClsGetWebData ClsGW = new ClsGetWebData();

            ListDataMonth = CBF.GetDateRoute(vstrDate,vstrEndDate);

            foreach (string str in ListDataMonth) 
            {
                //Para1:strYear
                //Para2:steMonth
                //Para3:vstrStocCode
                strYear = str.Substring(0, 4);
                strMonth = str.Substring(4, 2);
                ClsGW.Para1 = strYear;
                ClsGW.Para2 = strMonth;
                ClsGW.Para3 = vstrStockCode;
                ClsGW.GetIndiStockWebData();
                //OutputList.AddRange(ClsGW.OutLiStockIndi);
                LiTotal.AddRange(ClsGW.OutLiStockIndi);
            }

            OutputList = LiTotal;
            //return ClsGW.OutLiStockIndi;
        }

        public void GetTWNIndex(string vstrDate, string vstrEndDate) 
        {
            string strYear;
            string strMonth;
            List<string> ListDataMonth = new List<string>();
            List<TWNInd> LiTotal = new List<TWNInd>();
            ClsBaseFunction CBF = new ClsBaseFunction();
            DataTable TotalDt = new DataTable();
            DAO clsDao = new DAO();
            ClsGetWebData ClsGW = new ClsGetWebData();

            ListDataMonth = CBF.GetDateRoute(vstrDate, vstrEndDate);

            foreach (string str in ListDataMonth)
            {
                //Para1:strYear
                //Para2:steMonth
                //Para3:vstrStocCode
                strYear = str.Substring(0, 4);
                strMonth = str.Substring(4, 2);
                ClsGW.Para1 = strYear;
                ClsGW.Para2 = strMonth;
                ClsGW.GetTWNIndex();
                //OutputList.AddRange(ClsGW.OutLiStockIndi);
                LiTotal.AddRange(ClsGW.OutLiTWNInd);
            }
            OutputIndList = LiTotal;

        }

        /*
        public void ShowMessage(string vstr) 
        {
            ClsBaseFunction CBF = new ClsBaseFunction();
            MyDeligate MD = new MyDeligate(CBF.ShowMessage);
            MD(vstr);
        }
        */

        

        

        

    }
}
