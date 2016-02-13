using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using MvcApplication2.Models;

namespace MvcApplication2.Controllers
{
    public class TIOMOController : Controller
    {
        //
        // GET: /TIOMO/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(InputData vm)
        {
            try
            {
                ClsOperate COP = new ClsOperate();
                COP.Para1 = vm.strStockCode;
                COP.Para2 = vm.strDataDate;
                COP.Para3 = vm.strEndDate;

                if (vm.YearCnt > 24)
                {
                    return View("OverCount");
                }

                COP.WDT = ClsOperate.WebDataType.Individual;
                COP.Main_Batch();

               


                ViewData["StockCode"] = vm.strStockCode;
                ViewData["StartDate"] = vm.strDataDate;
                ViewData["LiStockIndi"] = COP.OutputList;
                ExportClientsListToCSV(COP.OutputList);
                return View(vm);
            }
            catch 
            {
                return View("Error");
            }
            

        }

        private void ExportClientsListToCSV(List<Models.StockIndi> vLiIndi)
        {
            StringWriter sw = new StringWriter();

            sw.WriteLine("\"TxnDate\",\"DealStockCnt\",\"DealStockAmt\",\"OpenPrice\",\"HighPrice\",\"LowPrice\",\"ClosePrice\",\"PriceDiff\",\"DealCount\"");

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=TW_Stock_Indi.csv");
            Response.ContentType = "text/csv";


            foreach (var line in vLiIndi)
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\"",
                                            line.TxnDate,
                                            line.DealStockCnt,
                                            line.DealStockAmt,
                                            line.OpenPrice,
                                            line.HighPrice,
                                            line.LowPrice,
                                            line.ClosePrice,
                                            line.PriceDiff,
                                            line.DealCount
                                            ));
            }

            Response.Write(sw.ToString());

            Response.End();
        }

      
    }
}
