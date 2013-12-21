using System;

namespace Phenix.Infrastructure.Cache
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class CacheProviderExtensions
    {    
        /// <summary>
        /// 默认30分钟,
        /// </summary> 
        public static T Get<T>(this ICacheProvider cacheManager, string key, Func<T> acquire)
        {
            return Get(cacheManager, key, 30, acquire);
        }
        /// <summary>
        /// 缓存单位是分钟
        /// </summary> 
        public static T Get<T>(this ICacheProvider cacheManager, string key, int cacheTime, Func<T> acquire) 
        {
            if (cacheManager.IsSet(key))
            {
                return cacheManager.Get<T>(key);
            }
            var result = acquire();
           //TODO 数据列表不为空时才写入缓存
            cacheManager.Set(key, result, cacheTime);
            return result;
        }
 
    }
}
