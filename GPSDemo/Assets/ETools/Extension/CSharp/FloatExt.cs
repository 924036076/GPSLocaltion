using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETools.Extension
{
    public static class FloatExt
    {
        /// <summary>
        /// 返回浮点数的小数
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static float GetAfterDecimalPoint(this float self)
        {
            return self % 1;
        }

        // Example: Debug.Log(1.25f.GetAfterDecimalPoint());     //0.25
        
        /// <summary>
        /// 判断是否有小数
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsExistAfterDecimalPoint( this float self )
        {
            return self % 1 != 0;
        }
        // Example: Debug.Log(1.25f.IsExistAfterDecimalPoint());     //true
        // Debug.Log(1f.IsExistAfterDecimalPoint());     //false
    }
}