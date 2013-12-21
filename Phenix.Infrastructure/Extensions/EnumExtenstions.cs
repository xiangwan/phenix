using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Phenix.Infrastructure.Extensions{
    public static class EnumExtenstions{
        public static Dictionary<string, string> DicEnum = new Dictionary<string, string>();

        ///   <summary>   
        ///   获取枚举值的详细文本   
        ///   </summary>    
        public static string GetDescription(this Enum item) {
            Type enumType = item.GetType();
            string key = string.Format("{0}.{1}", enumType.Name, item);

            if (DicEnum.ContainsKey(key)) {
                return DicEnum[key];
            }

            //获取字段信息   
            MemberInfo[] memebers = enumType.GetMember(item.ToString());
            if (memebers.Length > 0) {
                object[] attrs = memebers[0].GetCustomAttributes(typeof (DescriptionAttribute), false);
                if (attrs.Length > 0) {
                    string desc = ((DescriptionAttribute) attrs[0]).Description;
                    DicEnum.Add(key, desc);
                    return desc;
                }
            }
            //如果没有检测到合适的注释，则用默认名称   
            DicEnum.Add(key, item.ToString());
            return item.ToString();
        }
    }

    /// <summary>
    /// T 类型枚举列表
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    public class Enum<T> : IEnumerable<T>{
     
        public static IEnumerable<T> AsEnumerable() {
            return new Enum<T>();
        }
        /// <summary>
        /// 返回一个循环访问集合的枚举数。
        /// </summary>
        /// <returns>可用于循环访问集合的 IEnumerator&lt;T&gt; 。</returns>
        public IEnumerator<T> GetEnumerator()
        {
            var values = Enum.GetValues(typeof(T));
            foreach (T item in values)
            {
                yield return item;
            }
        } 

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}