using System;
using System.Collections.Generic;
using System.Reflection;
using Phenix.Infrastructure.Data;

namespace Phenix.Infrastructure
{

    #region flexigrid

    public class FlexiGridDataJson
    {
        public FlexiGridDataJson()
        {
        }

        public FlexiGridDataJson(
            int pageIndex, int totalCount, IList<FlexiGridRow> data)
        {
            page = pageIndex;
            total = totalCount;
            rows = data;
        }

        public int page { get; set; }
        public int total { get; set; }
        public IList<FlexiGridRow> rows { get; set; }

        public FlexiGridError error { get; set; }

        #region ConvertFromList

        /* public static FlexiGridDataJson ConvertFromList<T>(List<T> list, string key, string[] cols) where T : class
        {
            var data = new FlexiGridDataJson();
            data.page = 1;
            if (list != null)
            {
                data.total = list.Count;
                data.rows = new List<FlexiGridRow>();
                foreach (T t in list)
                {
                    var row = new FlexiGridRow();
                    row.id = getValue(t, key);
                    row.cell = new List<string>();
                    foreach (string col in cols)
                    {
                        row.cell.Add(getValue(t, col));
                    }
                    data.rows.Add(row);
                }
            }
            else
            {
                data.total = 0;
            }
            return data;
        }*/

        #endregion

        private static string getValue<T>(T t, string pname) where T : class
        {
            Type type = t.GetType();
            PropertyInfo pinfo = type.GetProperty(pname);
            var p = type.GetProperties();
            if (pinfo != null)
            {
                object v = pinfo.GetValue(t, null);
                return v != null ? v.ToString() : "";
            }
            return "";
        }

        public static FlexiGridDataJson ConvertFromPagedList<T>(PagedList<T> pagelist, string key, string[] cols)
            where T : class
        {
            var data = new FlexiGridDataJson();
            data.page = pagelist.CurrentPageIndex; 
            data.total = pagelist.TotalItemCount; 
            data.rows = new List<FlexiGridRow>();
            foreach (T t in pagelist)
            {
                var row = new FlexiGridRow();
                row.id = getValue(t, key);
                row.cell = new List<string>();
                foreach (string col in cols)
                {
                    row.cell.Add(getValue(t, col));
                }
                data.rows.Add(row);
            }
            return data;
        }
    }

    public class FlexiGridRow
    {
        public string id { get; set; }
        public List<string> cell { get; set; }
    }

    public class FlexiGridError
    {
        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
    }

    public class JsonReturnMessages
    {
        public bool IsSuccess { get; set; }


        public string Msg { get; set; }

        public object Data { get; set; }
    }

    #endregion
}