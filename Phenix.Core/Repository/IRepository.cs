using System.Collections.Generic;
using System.Data;
using Phenix.Infrastructure.Data;

namespace Phenix.Core.Repository
{
    public interface IRepository<T> where T :class
    {
        long Add(T item);
        bool Update(T item); 
        T GetById(long id);
        IEnumerable<T> GetList(string sqlTop,string sqlWhere,string sqlOrderBy="");
        PagedList<T> GetPagedList(int pageIndex, int pageSize, string sqlWhere = "", string sqlOrderBy = " order by id ");
        bool DeleteById(long id);

    }
}