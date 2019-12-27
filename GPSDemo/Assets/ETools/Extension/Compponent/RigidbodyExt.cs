using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETools.Extension {
    /// <summary>
    /// 
    /// </summary>
    public static class RigidbodyExt {
        /// <summary>
        /// 更改刚体的方向而不更改其速度
        /// </summary>
        /// <param name="rigidbody">Rigidbody.</param>
        /// <param name="direction">New direction.</param>
        public static void ChangeDirection (this Rigidbody rigidbody, Vector3 direction)
        {
            rigidbody.velocity = direction * rigidbody.velocity.magnitude;
        }
    }
}