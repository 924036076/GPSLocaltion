/*
 * @author Valentin Simonov / http://va.lent.in/
 */

using UnityEngine;
using TouchScript.Gestures.TransformGestures;

namespace TouchScript.Examples.CameraControl
{
    /// <exclude />
    public class CameraController : MonoBehaviour
    {
        public ScreenTransformGesture TwoFingerMoveGesture;
        public ScreenTransformGesture ManipulationGesture;
        public float PanSpeed = 200f;
        public float RotationSpeed = 200f;
        public float ZoomSpeed = 10f;

        private Transform pivot;
        private Transform cam;

        private void Awake()
        {
            pivot = transform.Find("Pivot");
            cam = pivot.GetChild(0);
            cam.transform.LookAt(pivot);
        }

        private void OnEnable()
        {
            TwoFingerMoveGesture.Transformed += twoFingerTransformHandler;
            ManipulationGesture.Transformed += manipulationTransformedHandler;
        }

        private void OnDisable()
        {
            TwoFingerMoveGesture.Transformed -= twoFingerTransformHandler;
            ManipulationGesture.Transformed -= manipulationTransformedHandler;
        }

        private void manipulationTransformedHandler(object sender, System.EventArgs e)
        {
            pivot.transform.localScale -= Vector3.one * (ManipulationGesture.DeltaScale - 1f) * ZoomSpeed;
        }

        private void twoFingerTransformHandler(object sender, System.EventArgs e)
        {
            var rotation = Quaternion.Euler(pivot.localRotation.x,
                TwoFingerMoveGesture.DeltaPosition.x / Screen.width * RotationSpeed,
                pivot.localRotation.z);
            pivot.localRotation *= rotation;
        }
    }
}