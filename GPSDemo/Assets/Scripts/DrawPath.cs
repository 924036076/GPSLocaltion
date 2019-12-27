/*************************************************************************
 *  Copyright © #COPYRIGHTYEAR# Eyang. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  DrawPath.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Eyang
 *  Version      :  0.1.0
 *  Date         :  #CREATEDATE#
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameMain.Script
{
    public class DrawPath : MonoBehaviour
    {
        #region Field and Property

        [SerializeField] private Button drawPathPointBtn;
        [SerializeField] private LineRenderer lineRenderer;
        private GetGPS gps;

        #endregion

        #region Private Method

        private void Start()
        {
            drawPathPointBtn.onClick.AddListener(DrawPathPoint);
            gps = GetComponent<GetGPS>();
        }

        private void DrawPathPoint()
        {
            if (gps.pointList.Count == 0)
            {
                NativeToolkit.ShowAlert("", "没有定位数据，请先录入路径");
                return;
            }

            lineRenderer.positionCount = gps.pointList.Count;
            lineRenderer.SetPositions(gps.pointList.ToArray());
        }

        #endregion

        #region Public Method

        #endregion
    }

    public class Path
    {
        public List<Vector3> pointList = new List<Vector3>();
    }
}