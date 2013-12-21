using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phenix.Infrastructure.Cache
{
    public static class EFCacheExtensions {
        private static ICacheProvider cacheProvider;
        public static ICacheProvider CacheProvider {
         get {

             return cacheProvider??(cacheProvider =new MemoryCacheProvider());
         }
        } 
  
         /// <summary>
         /// 默认缓存30分钟
         /// </summary> 
        public static IQueryable<T> AsCacheable<T>(this IQueryable<T> query, string key = "")
        { 
             if (string.IsNullOrEmpty(key)) {
                 key = GetKey(query); 
             } 
              return CacheProvider.Get(key,() => query.ToList().AsQueryable()); 
        }
        /// <summary>
        /// 单位是分钟
        /// </summary> 
        public static IEnumerable<T> AsCacheable<T>(this IQueryable<T> query, int cacheDuration)
        { 
            var key = GetKey(query);
            return CacheProvider.Get(key, cacheDuration, () => query.ToList()); 
        }

        public static void RemoveFromCache<T>(IQueryable<T> query)
        { 
            var key = GetKey(query);
             CacheProvider.Remove(key);
        }

        private static string GetKey<T>(IQueryable<T> query)
        {  
            var keyBuilder = new StringBuilder(query.ToString()); 
            keyBuilder.Append("\n\r");
            keyBuilder.Append(typeof(T).AssemblyQualifiedName);

            return keyBuilder.ToString();
        }
    }
}
