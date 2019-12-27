using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETools.Extension {
    /// <summary>
    /// List 拓展类 
    /// </summary>
    public static class ListExt {
        public struct ForEachData<T> {
            public int Index { get; }
            public T Value { get; }
            public bool IsLast { get; }

            public ForEachData (int index, T value, bool isLast)
            {
                Index = index;
                Value = value;
                IsLast = isLast;
            }
        }

        /// <summary>
        /// 拓展foreach流程
        /// </summary>
        /// <param name="self"></param>
        /// <param name="callback"></param>
        /// <typeparam name="T"></typeparam>
        public static void ForEach<T> (this IList<T> self, Action<ForEachData<T>> callback)
        {
            var count = self.Count;
            for (int i = 0; i < count; i++)
            {
                var data = new ForEachData<T>
                (
                    index: i,
                    value: self[i],
                    isLast: i == count - 1
                );

                callback(data);
            }
        }

        // Example:
        // var list = new [] { 1, 2, 3 };
        // list.ForEach( data => { Console.WriteLine( "{0}: {1}", data.Value, data.IsLast );});

        /// <summary>
        /// 取出重复的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T[] GetDistinct<T> (this IList<T> self)
        {
            var uniqueList = new List<T>();
            var result = new List<T>();

            foreach (var n in self)
            {
                if (uniqueList.Contains(n))
                {
                    result.Add(n);
                }
                else
                {
                    uniqueList.Add(n);
                }
            }

            return result.ToArray();
        }
    }
}