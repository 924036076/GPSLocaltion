using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETools.Extension {
    /// <summary>
    /// 
    /// </summary>
    public static class TransformExt {
        /// <summary>
        /// 以该对象作为父节点添加子节点
        /// </summary>
        /// <param name="transform">Parent transform.</param>
        /// <param name="children">Game objects to make children.</param>
        public static void AddChildren (this Transform transform, GameObject[] children)
        {
            Array.ForEach(children, child => child.transform.parent = transform);
        }

        /// <summary>
        /// 为该对象添加多个组件
        /// </summary>
        /// <param name="transform">Parent transform.</param>
        /// <param name="children">Components of game objects to make children.</param>
        public static void AddChildren (this Transform transform, Component[] children)
        {
            Array.ForEach(children, child => child.transform.parent = transform);
        }

        /// <summary>
        /// 归零所有子节点坐标位置
        /// </summary>
        /// <param name="transform">Parent transform.</param>
        /// <param name="recursive">Also reset ancestor positions?</param>
        public static void ResetChildPositions (this Transform transform, bool recursive = false)
        {
            foreach (Transform child in transform)
            {
                child.position = Vector3.zero;

                if (recursive)
                {
                    child.ResetChildPositions(recursive);
                }
            }
        }

        /// <summary>
        /// 设置所有子节点对应层级
        /// </summary>
        /// <param name="transform">Parent transform.</param>
        /// <param name="layerName">Name of layer.</param>
        /// <param name="recursive">Also set ancestor layers?</param>
        public static void SetChildLayers (this Transform transform, string layerName, bool recursive = false)
        {
            var layer = LayerMask.NameToLayer(layerName);
            SetChildLayersHelper(transform, layer, recursive);
        }

        static void SetChildLayersHelper (Transform transform, int layer, bool recursive)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.layer = layer;

                if (recursive)
                {
                    SetChildLayersHelper(child, layer, recursive);
                }
            }
        }

        /// <summary>
        /// 设置该对象的X轴位置
        /// </summary>
        /// <param name="x">Value of x.</param>
        public static void SetX (this Transform transform, float x)
        {
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }

        /// <summary>
        /// 设置该对象的Y轴位置
        /// </summary>
        /// <param name="y">Value of y.</param>
        public static void SetY (this Transform transform, float y)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }

        /// <summary>
        /// 设置该对象的Z轴位置
        /// </summary>
        /// <param name="z">Value of z.</param>
        public static void SetZ (this Transform transform, float z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }
    }
}
