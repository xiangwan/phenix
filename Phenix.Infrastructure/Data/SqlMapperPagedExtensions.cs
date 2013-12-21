using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using NLog;

namespace Phenix.Infrastructure.Data{
    public static class SqlMapperPagedExtensions{ 
        public static PagedList<T> GetPagedList<T>(this IDbConnection connection
                , int pageIndex, int pageSize, string sqlWhere = "", string sqlOrderBy = " order by id ") {
            if (pageIndex <= 0) {
                pageIndex = 1;
            }
            if (pageSize <= 0) {
                pageSize = 1;
            }
            string sqlPage =string.Format(
                            "SELECT * FROM (SELECT ROW_NUMBER() OVER ({0}) AS __rn,* FROM [{1}] {2}) as result WHERE __rn>{3} AND __rn<={4}",
                            sqlOrderBy, typeof (T).Name, sqlWhere, (pageIndex - 1) * pageSize, pageIndex * pageSize);
            string sqlItemCount = string.Format(" ;SELECT count(*) FROM [{0}] {1}", typeof (T).Name, sqlWhere);
            var sql = sqlPage + " " + sqlItemCount; 
            using (var multi = connection.QueryMultiple(sql))
            {
                var items = multi.Read<T>();
                int itemsCount = multi.Read<int>().FirstOrDefault();
                var paged = new PagedList<T>(items, pageIndex, pageSize, itemsCount);
                return paged;
            }
        }
    }
}