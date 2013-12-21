using System;

namespace Phenix.Infrastructure.Cache
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class CacheProviderExtensions
    {    
        /// <summary>
        /// Ĭ��30����,
        /// </summary> 
        public static T Get<T>(this ICacheProvider cacheManager, string key, Func<T> acquire)
        {
            return Get(cacheManager, key, 30, acquire);
        }
        /// <summary>
        /// ���浥λ�Ƿ���
        /// </summary> 
        public static T Get<T>(this ICacheProvider cacheManager, string key, int cacheTime, Func<T> acquire) 
        {
            if (cacheManager.IsSet(key))
            {
                return cacheManager.Get<T>(key);
            }
            var result = acquire();
           //TODO �����б�Ϊ��ʱ��д�뻺��
            cacheManager.Set(key, result, cacheTime);
            return result;
        }
 
    }
}
