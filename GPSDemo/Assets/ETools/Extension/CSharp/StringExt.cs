using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ETools.Extension
{
    public static class StringExt
    {
        /// <summary>
        /// 从一段字符串里面提取出所有数字
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ExtractNumber(this string self)
        {
            return Regex.Replace(self, @"[^0-9]", string.Empty);
        }

        //Example: var str = "s12das55"; Debug.Log(str.ExtractNumber); //"1255"

        /// <summary>
        /// 判断是否全是小写字母
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsLower(this string self)
        {
            foreach (var t in self)
            {
                if (char.IsUpper(t))
                {
                    return false;
                }
            }

            return true;
        }

        //Example: var str = "song";  true     var str = "Song";  false

        /// <summary>
        /// 判断是否全是大写字母
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsUpper(this string self)
        {
            foreach (var t in self)
            {
                if (char.IsLower(t))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 返回字符串第一行
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string GetFirstLine(this string self)
        {
            var separator = new[] {Environment.NewLine};
            return self.Split(separator, StringSplitOptions.None).FirstOrDefault();
        }
    }
}