using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace InRixDataManager
{
    public static class RouteFilter
    {
        public static DataIncome DCFilter(DataIncome di, List<String> meta)
        {
            LogManager.writeLog(4020); //start
            try
            {
                SortByName(di);
                int j = 0;
                int count = 0;
                List<InrixData> tmpInrixList = new List<InrixData>();

                for (int i = 0; i < meta.Count; i++)
                {
                    int tmp;

                    while ((tmp = meta[i].CompareTo(di.List[j].TMCCode)) > 0)
                    {
                        ++j;
                    }

                    if (tmp == 0)
                    {
                        tmpInrixList.Add(di.List[j]);
                        count++;
                    }
                }

                di.List = tmpInrixList;

                LogManager.writeLog(4021);//done
                return di;
            }
            catch (Exception e)
            {
                LogManager.writeLog(4120,e);//exception
                return null;
            }
        }

        private static void SortByName(DataIncome di)
        {
            di.List.Sort(delegate(InrixData i1, InrixData i2) { return i1.TMCCode.CompareTo(i2.TMCCode); });
        }
    }
}
