using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcApplication2
{
    class ClsBaseFunction
    {
        public string TWNTransADDate(string vstrDate) 
        {
            string result;
            result = "";

            string aa;
            string az;
            DateTime ac;
            aa = vstrDate;


            if (aa.IndexOf("19") == 1) 
            {
                aa = aa.Remove(0, 2);
            }
            
            int intYear;
            intYear = Convert.ToInt32(vstrDate.Substring(0, vstrDate.IndexOf("/")));
            intYear = intYear + 1911;

            az = aa.Substring(aa.IndexOf("/"),aa.Length-aa.IndexOf("/"));
            ac = Convert.ToDateTime(intYear.ToString() + az);
            result = ac.Year.ToString() + "/" + PrefixZero(ac.Month) + "/" + PrefixZero(ac.Day);

            return result;
        }

        public string ADTransTWNDate(string vstrDate) 
        {
            string result;
            result = "";
            int intYear;
            string az;
            intYear = Convert.ToInt32(vstrDate.Substring(0, vstrDate.IndexOf("/")));
            intYear = intYear - 1911;
            az = vstrDate.Substring(vstrDate.IndexOf("/"), vstrDate.Length - vstrDate.IndexOf("/"));
            result = intYear.ToString() + "/" + PrefixZero(Convert.ToInt32(az));
            


            return result;

            

        }

        public string PrefixZero(int vint) 
        {
            string result;

            if (vint <= 9)
            {
                result = "0" + vint.ToString();
            }
            else 
            {
                result = vint.ToString();
            }
            return result;
        }

       

        public List<string> GetDateRoute(string vstrDateStart,string vstrDateEnd) 
        {
            List<string> result = new List<string>();

            string strYear;
            string strMonth;
            string strEndYear;
            string strEndMonth;

            string strAddToList;

            int intYear;
            int intMonth;
            int intThisYear;
            int intThisMonth;

            strAddToList = "";

            strYear = vstrDateStart.Substring(0, 4);
            strMonth = vstrDateStart.Substring(4, 2);

            intYear = Convert.ToInt32(strYear);
            intMonth = Convert.ToInt32(strMonth);

            strEndYear = vstrDateEnd.Substring(0, 4);
            strEndMonth = vstrDateEnd.Substring(4, 2);

            intThisYear = Convert.ToInt32(strEndYear);
            intThisMonth = Convert.ToInt32(strEndMonth);
            

            if (intYear != intThisYear)
            {
                for (int i = intYear; i <= intThisYear; i++)
                {
                    if (i == intYear)
                    {
                        for (int j = intMonth; j <= 12; j++)
                        {
                            strAddToList = i.ToString() + PrefixZero(j).ToString();
                            result.Add(strAddToList);
                        }
                    }
                    else if (i != intThisYear)
                    {
                        for (int j = 1; j <= 12; j++)
                        {
                            strAddToList = i.ToString() + PrefixZero(j).ToString();
                            result.Add(strAddToList);
                        }
                    }
                    else
                    {
                        for (int j = 1; j <= intThisMonth; j++)
                        {
                            strAddToList = i.ToString() + PrefixZero(j).ToString();
                            result.Add(strAddToList);
                        }
                    }
                }
            }
            else 
            {
                for (int j = intMonth; j <= intThisMonth; j++) 
                {
                    strAddToList = intYear.ToString() + PrefixZero(j).ToString();
                    result.Add(strAddToList);
                }
            }
           
            return result;
        }
    }
}
