using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Phenix.Infrastructure.Extensions{
    public static class StringExtensions{

        public static string FormatWith(this string format, params object[] args){
            return string.Format(format, args);
        }
        public static bool HasValue(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }
        public static bool IsCaseInsensitiveEqual(this string instance, string comparing)
        {
            return string.Compare(instance, comparing, StringComparison.OrdinalIgnoreCase) == 0;
        }
        #region  类型转换

        public static long ToLong(this string str, long i = 0){
            if (string.IsNullOrEmpty(str)){
                return i;
            }
            long d;
            return long.TryParse(str, out d) ? d : i;
        }

        public static int ToInt(this string str, int i = 0){
            if (string.IsNullOrEmpty(str)){
                return i;
            }
            int d;
            return int.TryParse(str, out d) ? d : i;
        }

        public static decimal ToDecimal(this string str, decimal i = 0){
            if (string.IsNullOrEmpty(str)){
                return i;
            }
            decimal d;
            return decimal.TryParse(str, out d) ? d : i;
        }

        #endregion

        #region 辅助

        /// <summary>
        /// 计算字符串的 MD5 哈希。若字符串为空，则返回空，否则返回计算结果。
        /// </summary>
        public static string ToMd5Hash(this string str){
            if (string.IsNullOrEmpty(str)){
                return string.Empty;
            }
            var hash = new StringBuilder();
            var md5 = new MD5CryptoServiceProvider();
            byte[] data = Encoding.Default.GetBytes(str);
            data = md5.ComputeHash(data);
            for (int i = 0; i < data.Length; i++){
                hash.Append(data[i].ToString("x2"));
            }
            return hash.ToString();
        }

        /// <summary>
        /// 移除最后一个字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveLastChar(this string str){
            if (string.IsNullOrEmpty(str)) return "";
            return str.Substring(0, str.Length - 1);
        }

        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string ToPhysicalPath(this string strPath){
            if (HttpContext.Current == null){
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
            return HttpContext.Current.Server.MapPath(strPath);
        }

      
        #endregion
 


    }
}