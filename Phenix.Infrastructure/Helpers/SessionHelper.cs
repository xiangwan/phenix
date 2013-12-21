using System.Web;

namespace Phenix.Infrastructure.Helpers {
    /// <summary>
    /// Session操作类
    /// </summary>
    public static class SessionHelper
    {
        /// <summary>
        /// 添加Session 
        /// </summary>
        /// <param name="name">Session对象名称</param>
        /// <param name="value">Session值</param>
        public static void Add(string name, object value)
        {
            HttpContext.Current.Session[name] = value;
            //TODO 测试session默认timeout
            // HttpContext.Current.Session.Timeout = 23;
        }

        /// <summary>
        /// 添加Session
        /// </summary>
        /// <param name="name">Session对象名称</param>
        /// <param name="value">Session值</param>
        /// <param name="expires">调动有效期（分钟）</param>
        public static void Add(string name, string value, int expires)
        {
            HttpContext.Current.Session[name] = value;
            HttpContext.Current.Session.Timeout = expires;
        }

        /// <summary>
        /// 读取某个Session对象值
        /// </summary>
        /// <param name="name">Session对象名称</param>
        /// <returns>Session对象值</returns>
        public static object Get(string name)
        {
            return HttpContext.Current.Session[name];
        }

        /// <summary>
        /// 删除某个Session对象
        /// </summary>
        /// <param name="name">Session对象名称</param>
        public static void Delete(string name)
        {
            HttpContext.Current.Session[name] = null;
        }
    }
}