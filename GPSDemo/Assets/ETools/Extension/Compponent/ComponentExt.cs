using System.Linq;
using UnityEngine;

namespace ETools.Extension {
    /// <summary>
    /// 组件拓展
    /// </summary>
    public static class ComponentExt {
        /// <summary>
        /// 添加指定组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        /// <returns></returns>
        public static T AddComponent<T> (this Component component) where T : Component
        {
            return component.gameObject.AddComponent<T>();
        }

        /// <summary>
        /// 判断是否存在该组件，有则返回该组件，否则添加组件并返回
        /// </summary>
        /// <param name="component">Component.</param>
        /// <returns>Previously or newly attached component.</returns>
        public static T GetOrAddComponent<T> (this Component component) where T : Component
        {
            return component.GetComponent<T>() ?? component.AddComponent<T>();
        }

        /// <summary>
        /// 判断是否存在该组件
        /// </summary>
        /// <param name="component">Component.</param>
        /// <returns>True when component is attached.</returns>
        public static bool HasComponent<T> (this Component component) where T : Component
        {
            return component.GetComponent<T>() != null;
        }


        /// <summary>
        /// 判断该对象身上是否存在空物体
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool HasMissingScript (this Component component)
        {
            return component
                .GetComponents<Component>()
                .Any(c => c == null);
        }
    }
}