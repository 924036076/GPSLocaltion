using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using Random = UnityEngine.Random;

namespace ETools.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public static class IEnumerableExt
    {
        /// <summary>
        /// 遍历方法，返回下标与对应的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<(int index, T value)> WithIndex<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            IEnumerable<(int dex, T value)> Imp()
            {
                var i = 0;
                foreach (var value in source)
                {
                    yield return (i, value);
                    i++;
                }
            }

            return Imp();
        }

        // Example:
        // foreach ( var ( index, value ) in list.WithIndex() ){
        // Debug.Log( index + ":" + value );
        // }

        /// <summary>
        /// 可自由增加数组长度方法
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> first, params T[] second)
        {
            return Enumerable.Concat(first, second);
        }

        // Example:
        // var list = new[] {1, 2, 3};
        // var result = list.Concat(4, 5, 6);
        // result: {1, 2, 3, 4, 5, 6}

        /// <summary>
        /// 根据条件筛选出符合条件的数组与不符合条件的数组
        /// </summary>
        /// <param name="self"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Tuple<IEnumerable<T>, IEnumerable<T>> Partition<T>(
            this IEnumerable<T> self, Func<T, bool> predicate)
        {
            var ok = new List<T>();
            var ng = new List<T>();

            foreach (var n in self)
            {
                if (predicate(n))
                    ok.Add(n);
                else
                    ng.Add(n);
            }

            return Tuple.Create((IEnumerable<T>)ok, (IEnumerable<T>)ng);
        }

        // Example:
        // var list = new[] {1, 2, 3, 4, 5, 6, 7};
        // var (ok, ng ) = list.Partition(c => c % 2 == 0);
        // ok: 2, 4, 6; ng: 1, 3, 5, 7;

        /// <summary>
        /// 从数组中获取随机值
        /// </summary>
        /// <param name="self"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T RandomAt<T>(this IEnumerable<T> self)
        {
            return self.Any() ? self.ElementAt(Random.Range(0, self.Count())) : default;
        }

        // Example:
        // var list = new[] {1, 2, 3, 4, 5, 6, 7};
        // var value = list.RandomAt();

        /// <summary>
        /// 移除所有空元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static IEnumerable<T> NotNull<T>(this IEnumerable<T> self)
        {
            return self.Where(c => c != null);
        }
    }
}