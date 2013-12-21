using System;
using System.Net;
using System.Web;

namespace Phenix.Infrastructure.Helpers {
    public static class CookieHelper
    {
        /// <summary>
        /// 创建COOKIE对象并赋Value值，修改COOKIE的Value值也用此方法，因为对COOKIE修改必须重新设Expires
        /// </summary> 
        /// <param name="expires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数 </param> 
        public static void Add(string name, string value, int expires)
        {
            var cookie = new HttpCookie(name.Trim()) { Value = HttpUtility.UrlEncode(value.Trim()) };
            if (expires > 0)
            {
                cookie.Expires = expires == 1 ? DateTime.MaxValue : DateTime.Now.AddSeconds(expires);
            }
            Add(cookie);
        } 
     
        /// <summary>
        /// 创建COOKIE对象并赋Value值，修改COOKIE的Value值也用此方法，因为对COOKIE修改必须重新设Expires
        /// </summary> 
        /// <param name="expires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)，</param> 
        public static void Add(string name, string value, int expires, string domain)
        {
            var cookie = new HttpCookie(name.Trim()) { Value = HttpUtility.UrlEncode(value.Trim()), Domain = domain.Trim() };
            if (expires > 0)
            {
                cookie.Expires = expires == 1 ? DateTime.MaxValue : DateTime.Now.AddSeconds(expires);
            }
            Add(cookie);
        }
         
 
        public static string GetString(string strCookieName) {
            var httpCookie = HttpContext.Current.Request.Cookies[strCookieName];
            return httpCookie != null ? HttpUtility.UrlDecode(httpCookie.Value) : "";
        } 
   
        public static void Delete(string name) {
            var cookie = new HttpCookie(name.Trim()) { Expires = DateTime.MinValue };
            HttpContext.Current.Response.Cookies.Set(cookie);
        } 

        public static void Add(HttpCookie cookie)
        {
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void Set(Cookie cookie)
        {
            if (cookie != null) {
                HttpContext.Current.Response.Cookies.Add(cookie.ToHttpCookie());
            }
        }

        public static HttpCookie ToHttpCookie(this Cookie cookie) {
            var httpCookie = new HttpCookie(cookie.Name);
            /*Copy keys and values*/
            if (cookie.Value.Contains("&")) {
                foreach (string value in cookie.Value.Split('&')) {
                    string[] val = value.Split('=');
                    httpCookie.Values.Add(val[0], val[1]); /* or httpCookie[val[0]] = val[1];  */
                }
            }
            else {
                httpCookie.Value = cookie.Value;
            }

            /*Copy Porperties*/
            httpCookie.Domain = cookie.Domain;
            httpCookie.Expires = cookie.Expires;
            httpCookie.HttpOnly = cookie.HttpOnly;
            httpCookie.Path = cookie.Path;
            httpCookie.Secure = cookie.Secure;

            return httpCookie;
        }
    }
}