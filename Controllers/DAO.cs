using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml;


namespace MvcApplication2
{
    class DAO
    {
        const string strSettingPath = "D:\\Program\\DB_Setting.xml";
        public string CnStringCode { get; set; }
        public string Para1 { get; set; }
        public string Para2 { get; set; }
        public string Para3 { get; set; }


        private string GetConnstring() 
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(strSettingPath);
            XmlNode cNode1 = XmlDoc.FirstChild;
            XmlNode cNode2 = cNode1.SelectSingleNode(CnStringCode);
            return cNode2.InnerText;
        }

        public DataTable GetSchema(string vDBName) 
        {
            SqlConnection cn = new SqlConnection(GetConnstring());
            var SqlCmd = new SqlCommand("exec GetSchema '" + vDBName  + "'", cn);
            DataTable DT = new DataTable();
            cn.Open();
            DT.Load(SqlCmd.ExecuteReader());
            return DT;
        }

        public DataTable GetStockIndiData() 
        {
            SqlConnection cn = new SqlConnection(GetConnstring());
            var SqlCmd = new SqlCommand("exec GetGetStockIndiData 'TW1101Tb','2015/01/01','2015/01/01'", cn);
            DataTable DT = new DataTable();
            cn.Open();
            DT.Load(SqlCmd.ExecuteReader());
            return DT;
        }

        public void BulkWriteDB(DataTable vDT) 
        {
            SqlConnection cn = new SqlConnection(GetConnstring());
            cn.Open();
            SqlTransaction trans = cn.BeginTransaction();
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(cn, SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers, trans)) 
            {

            }
        }

    }
}
