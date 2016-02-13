using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Net;
using HtmlAgilityPack;
using System.Web;
using MvcApplication2.Models;



namespace MvcApplication2
{
    
    class ClsGetWebData
    {
        public string strWebUrl { get; set; }
        public DataTable DT { get; set; }
        public List<StockIndi> OutLiStockIndi { get; set; }
        public List<TWNInd> OutLiTWNInd { get; set; }
        public string Para1 { get; set; }
        public string Para2 { get; set; }
        public string Para3 { get; set; }

        public void GetIndiStockWebData() 
        {
            try
            {
                List<StockIndi> LiStockIndi = new List<StockIndi>();
                //DAO ClsDao = new DAO();
                //ClsDao.CnStringCode = "connstring3";
                string strWebSite;
                strWebSite = "http://www.twse.com.tw/ch/trading/exchange/STOCK_DAY/genpage/Report" + Para1 + Para2 + "/" + Para1 + Para2 + "_F3_1_8_" + Para3 + ".php?STK_NO=" + Para3 + "&myear=" + Para1 + "&mmon=" + Para2;
                WebClient url = new WebClient();
                MemoryStream ms = new MemoryStream(url.DownloadData(strWebSite));
                HtmlDocument doc = new HtmlDocument();
                ClsBaseFunction CBF = new ClsBaseFunction();
                doc.Load(ms, Encoding.Default);
                HtmlDocument hdc = new HtmlDocument();
                foreach (var cell in doc.DocumentNode.SelectNodes("/html[1]/body[1]/table[1]/tr[3]/td[1]/table[3]/tr[position()>2]"))
                {
                    LiStockIndi.Add(
                    new StockIndi
                    {
                        TxnDate = Convert.ToDateTime(CBF.TWNTransADDate(cell.SelectSingleNode("td[1]").InnerText)).ToShortDateString(),
                        DealStockCnt = Convert.ToInt32(cell.SelectSingleNode("td[2]").InnerText.Replace(",", "")),
                        DealStockAmt = Convert.ToInt64(cell.SelectSingleNode("td[3]").InnerText.Replace(",", "")),
                        OpenPrice = Convert.ToDecimal(cell.SelectSingleNode("td[4]").InnerText),
                        HighPrice = Convert.ToDecimal(cell.SelectSingleNode("td[5]").InnerText),
                        LowPrice = Convert.ToDecimal(cell.SelectSingleNode("td[6]").InnerText),
                        ClosePrice = Convert.ToDecimal(cell.SelectSingleNode("td[7]").InnerText),
                        PriceDiff = cell.SelectSingleNode("td[8]").InnerText,
                        DealCount = Convert.ToInt32(cell.SelectSingleNode("td[9]").InnerText.Replace(",", ""))
                    });
                }
                OutLiStockIndi = LiStockIndi;
            }
            catch (WebException)
            {

            }
            catch (Exception) 
            {
                //throw;
            }
        }

        private void InsertStockIndiDT(StockIndi vStockIndi, DataTable vDT) 
        {
            DataRow DR = vDT.NewRow();
            DR[0] = vStockIndi.TxnDate;
            DR[1] = vStockIndi.DealStockCnt;
            DR[2] = vStockIndi.DealStockAmt;
            DR[3] = vStockIndi.OpenPrice;
            DR[4] = vStockIndi.HighPrice;
            DR[5] = vStockIndi.LowPrice;
            DR[6] = vStockIndi.ClosePrice;
            DR[7] = vStockIndi.PriceDiff;
            DR[8] = vStockIndi.DealCount;

            vDT.Rows.Add(DR);
        }

        public void GetThreeJuristicBuySell() 
        {
            string strUrl;
            strUrl = "http://www.twse.com.tw/ch/trading/fund/T86/T86.php";
            string strDate;
            string strStockClassNo;

            strDate = "104/04/15";
            strStockClassNo = "02";

            ASCIIEncoding encoding = new ASCIIEncoding();
            string PostData;
            PostData = "";
            PostData += "input_date=" + strDate;
            PostData += "&select2=" + strStockClassNo;
            byte[] data = encoding.GetBytes(PostData);
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(strUrl);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = data.Length;
            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.Default);
            
            HtmlDocument doc = new HtmlDocument();
            doc.Load(reader);
            foreach (var cell in doc.DocumentNode.SelectNodes("/html[1]/body[1]/table[1]/tr[3]/td[1]/div[1]/table[1]/tbody[1]/tr[1]/td[1]")) 
            {

            }
            
            string content = reader.ReadToEnd();

            
            //myResponse.Write(content);
            
        }

        public void GetTWNIndex() 
        {
            try
            {
                string strUrl;
                
                strUrl = "http://www.twse.com.tw/ch/trading/indices/MI_5MINS_HIST/MI_5MINS_HIST.php";

                List<TWNInd> LiTWInd = new List<TWNInd>();
                WebClient url = new WebClient();
                MemoryStream ms = new MemoryStream(url.DownloadData(strUrl));
                HtmlDocument doc = new HtmlDocument();
                ClsBaseFunction CBF = new ClsBaseFunction();
                doc.Load(ms, Encoding.Default);
                HtmlDocument hdc = new HtmlDocument();
                ASCIIEncoding encoding = new ASCIIEncoding();

                string aa = (Convert.ToInt32(Para1)-1911).ToString();
                string bb = Para2;

                string PostData;
                PostData = "";
                PostData += "myear=" + aa;
                PostData += "&mmon=" + bb;
                byte[] data = encoding.GetBytes(PostData);
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(strUrl);
                myRequest.Method = "POST";
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentLength = data.Length;
                Stream newStream = myRequest.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Close();
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.Default);

                doc.Load(reader);

                foreach (var cell in doc.DocumentNode.SelectNodes("/html[1]/body[1]/table[1]/tr[3]/td[1]/table[3]/tr[position()>2]"))
                {
                    LiTWInd.Add(
                    new TWNInd
                    {
                        TxnDate = Convert.ToDateTime(CBF.TWNTransADDate(cell.SelectSingleNode("td[1]").InnerText)).ToShortDateString(),
                        OpenPrice = Convert.ToDecimal(cell.SelectSingleNode("td[2]").InnerText.Replace(",", "")),
                        HighPrice = Convert.ToDecimal(cell.SelectSingleNode("td[3]").InnerText.Replace(",", "")),
                        LowPrice = Convert.ToDecimal(cell.SelectSingleNode("td[4]").InnerText.Replace(",", "")),
                        ClosePrice = Convert.ToDecimal(cell.SelectSingleNode("td[5]").InnerText.Replace(",", ""))
                        
                    });
                }

                strUrl = "http://www.twse.com.tw/ch/trading/exchange/FMTQIK/genpage/Report" + Para1 + Para2 + "/" + Para1 + Para2 + "_F3_1_2.php?STK_NO=&myear=" + Para1 + "&mmon=" + Para2;
                HtmlDocument doc2 = HttpGetDataDoc(strUrl);
                int z = 0;
                foreach (var cell in doc2.DocumentNode.SelectNodes("/html[1]/body[1]/table[1]/tr[3]/td[1]/table[3]/tr[position()>2]")) 
                {
                    LiTWInd[z].DealAmt = Convert.ToInt64(cell.SelectSingleNode("td[3]").InnerText.Replace(",", ""));
                    z += 1;
                }




                OutLiTWNInd = LiTWInd;
            }
            catch 
            {

            }
            


        }

        private HtmlDocument HttpGetDataDoc(string vstrUrl) 
        {
            WebClient url = new WebClient();
            MemoryStream ms = new MemoryStream(url.DownloadData(vstrUrl));
            HtmlDocument doc = new HtmlDocument();
            ClsBaseFunction CBF2 = new ClsBaseFunction();
            doc.Load(ms, Encoding.Default);
            return doc;
        }
    }
}
