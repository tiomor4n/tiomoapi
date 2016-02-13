using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication2.Models;
using Newtonsoft.Json;

namespace MvcApplication2.Controllers
{
    public class TWINDController : Controller
    {
        //
        // GET: /TWIND/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(DatePrompt dtp) 
        {
            return RedirectToAction("showdataTWIND", new { strDataDate = dtp.strDataDate, strEndDate = dtp.strEndDate });
        }

        public ActionResult showdataTWIND(DatePrompt dtp) 
        {
            //ViewData["strStockCode"] = vm.strStockCode;
            //ViewData["strDataDate"] = vm.strDataDate;
            //ViewData["strEndDate"] = vm.strEndDate;
            string Ax;
            Ax = "[";

            try
            {
                ClsOperate COP = new ClsOperate();
                COP.Para2 = dtp.strDataDate;
                COP.Para3 = dtp.strEndDate;

                /*
                if (dtp.YearCnt > 24)
                {
                    //return View("OverCount");
                }
                */

                COP.WDT = ClsOperate.WebDataType.TWNIndex;
                COP.Main_Batch();

                ViewData["StartDate"] = dtp.strDataDate;
                ViewData["EndDate"] = dtp.strEndDate;
                ViewData["LiTWNInd"] = COP.OutputIndList;

                int x = 1;
                foreach (var line in COP.OutputIndList)
                {
                    var buildjsonResult = new
                    {
                        TxnDate = line.TxnDate,
                        DealAmt = line.DealAmt,
                        OpenPrice = line.OpenPrice,
                        HighPrice = line.HighPrice,
                        LowPrice = line.LowPrice,
                        ClosePrice = line.ClosePrice
                    };
                    //Response.Write(JsonConvert.SerializeObject(buildjsonResult);
                    //Ax = Ax + line.TxnDate + "," + line.DealStockCnt + "," + line.DealStockAmt + "," + line.OpenPrice + "," + line.HighPrice + "," + line.LowPrice + "," + line.ClosePrice + "," + line.PriceDiff + "," + line.DealCount + Server.HtmlDecode("&lt;br/&gt;");
                    Ax = Ax + JsonConvert.SerializeObject(buildjsonResult);
                    x += 1;
                    if (x <= COP.OutputIndList.Count)
                    {
                        Ax = Ax + ",";
                    }

                }

                Ax = Ax + "]";
                ViewData["Ax"] = Ax;
                return View(dtp);
            }
            catch
            {
                return View("Error");
            }

        }

    }
}
