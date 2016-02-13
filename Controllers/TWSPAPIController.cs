using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication2.Models;
using Newtonsoft.Json;


namespace MvcApplication2.Controllers
{
    public class TWSPAPIController : Controller
    {
        //
        // GET: /TWSPAPI/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(InputData vm)
        {
            return RedirectToAction("showdata", new { strStockCode = vm.strStockCode, strDataDate = vm.strDataDate, strEndDate = vm.strEndDate });
        }

        public ActionResult showdata(InputData vm) 
        {
            //ViewData["strStockCode"] = vm.strStockCode;
            //ViewData["strDataDate"] = vm.strDataDate;
            //ViewData["strEndDate"] = vm.strEndDate;
            string Ax;
            Ax="[";

            try
            {
                ClsOperate COP = new ClsOperate();
                COP.Para1 = vm.strStockCode;
                COP.Para2 = vm.strDataDate;
                COP.Para3 = vm.strEndDate;

                if (vm.YearCnt > 24)
                {
                    //return View("OverCount");
                }

                COP.WDT = ClsOperate.WebDataType.Individual;
                COP.Main_Batch();

                ViewData["StockCode"] = vm.strStockCode;
                ViewData["StartDate"] = vm.strDataDate;
                ViewData["LiStockIndi"] = COP.OutputList;

                int x = 1;
                foreach (var line in COP.OutputList) 
                {
                    var buildjsonResult = new
                    {
                        TxnDate=line.TxnDate,
                        DealStockAmt=line.DealStockAmt,
                        OpenPrice=line.OpenPrice,
                        HighPrice = line.HighPrice,
                        LowPrice = line.LowPrice,
                        ClosePrice = line.ClosePrice
                    };
                    //Response.Write(JsonConvert.SerializeObject(buildjsonResult);
                    //Ax = Ax + line.TxnDate + "," + line.DealStockCnt + "," + line.DealStockAmt + "," + line.OpenPrice + "," + line.HighPrice + "," + line.LowPrice + "," + line.ClosePrice + "," + line.PriceDiff + "," + line.DealCount + Server.HtmlDecode("&lt;br/&gt;");
                    Ax = Ax + JsonConvert.SerializeObject(buildjsonResult);
                    x += 1;
                    if (x <= COP.OutputList.Count) 
                    {
                        Ax = Ax + ",";
                    }
                    
                }

                Ax = Ax + "]";
                ViewData["Ax"] = Ax;
                return View(vm);
            }
            catch
            {
                return View("Error");
            }
        }

    }
}
