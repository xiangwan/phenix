using System.Collections.Generic;
using Phenix.Core.Repository;
using Phenix.Infrastructure.Data;
using Phenix.Infrastructure.Extensions;

namespace Phenix.Data.Repository{
    public class Repository<T> : IRepository<T> where T : class{
        private readonly string _tableName;

        public Repository() {
            _tableName = typeof (T).Name;
        }

        public virtual long Add(T item) {
            using (var conn = ConnectionFactory.CreateConnection()) {
                return conn.Insert(item);
            }
        }

        public virtual bool Update(T item) {
            using (var conn = ConnectionFactory.CreateConnection())
            {
                return conn.Update(item);
            }
        }

        public virtual T GetById(long id)
        {
            using (var conn = ConnectionFactory.CreateConnection()) {
                return conn.Get<T>(id);
            }
        }

        public virtual IEnumerable<T> GetList(string sqlTop, string sqlWhere, string sqlOrderBy = "")
        {
            using (var conn = ConnectionFactory.CreateConnection()) {
                var sql = "SELECT {0} * FROM {1} {2} {3}";
                return conn.Query<T>(sql.FormatWith(sqlTop,_tableName,sqlWhere,sqlOrderBy));
            }
        }

        public virtual PagedList<T> GetPagedList(int pageIndex, int pageSize, string sqlWhere = "",
                string sqlOrderBy = " order by id ") {
                    using (var conn = ConnectionFactory.CreateConnection()) {
                        return conn.GetPagedList<T>(pageIndex, pageSize, sqlWhere, sqlOrderBy);
                    }
        }

        public virtual bool DeleteById(long id)
        {
            using (var conn = ConnectionFactory.CreateConnection()) {
                var sql = "DELETE FROM [{0}]  WHERE ID=@ID";
                return conn.Execute(sql.FormatWith(_tableName), new { ID = id })>0;
            }
        }
    }
}