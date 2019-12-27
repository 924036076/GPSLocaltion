/*************************************************************************
 *  Copyright © #COPYRIGHTYEAR# Eyang. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  GetGPS.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Eyang
 *  Version      :  0.1.0
 *  Date         :  #CREATEDATE#
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using ETools.Extension;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using UnityEngine.UI;

namespace GameMain.Script
{
    public class GetGPS : MonoBehaviour
    {
        #region Field and Property

        [SerializeField] private Button startLocaltionBtn;
        private Text startLocaltionTxt;
        private bool isRecording = false;

        [SerializeField] private Button startMovementBtn;
        private Text startMovementTxt;
        private bool isMovement = false;

        [SerializeField] private Button getFileBtn;
        [SerializeField] private Text logInfoTxt;
        [SerializeField] private GameObject fullNameBtn;

        private Button closeBtn;

        [SerializeField] private Transform Group;
        private List<string> pathNameList = new List<string>();
        [SerializeField] private LineRenderer lineRenderer;

        [HideInInspector] public List<Localtion> localtionList = new List<Localtion>();
        private float currentTime;

        [SerializeField] private Transform movement;
        public List<Vector3> pointList = new List<Vector3>();

        #endregion

        #region Private Method

        private void Awake()
        {
            startLocaltionBtn.onClick.AddListener(StartLocaltionClick);
            startLocaltionTxt = startLocaltionBtn.GetComponentInChildren<Text>();
            startMovementBtn.onClick.AddListener(StartMovement);
            startMovementTxt = startMovementBtn.GetComponentInChildren<Text>();
            getFileBtn.onClick.AddListener(GetFileBtn);
            logInfoTxt.text += "\nGPS开启状态: " + NativeToolkit.StartLocation();
        }

        private void Update()
        {
            if (isMovement)
            {
                movement.position =
                    CatmullRom.EasyInterp3D(pointList.ToArray(),
                        Mathf.Clamp01((Time.time - currentTime) / pointList.Count));
            }
        }

        private void StartMovement()
        {
            if (pointList.Count == 0)
            {
                NativeToolkit.ShowAlert("", "没有定位数据，请先录入路径");
                return;
            }

            if (isMovement)
            {
                isMovement = !isMovement;
                startMovementTxt.text = "开始运动";
            }
            else
            {
                currentTime = Time.time;
                isMovement = !isMovement;
                startMovementTxt.text = "停止运动";
            }
        }

        /// <summary>
        /// 绘制路径按钮
        /// </summary>
        private void GetFileBtn()
        {
#if UNITY_EDITOR
            DirectoryInfo folder = new DirectoryInfo(Application.streamingAssetsPath);
#else
            DirectoryInfo folder = new DirectoryInfo(Application.persistentDataPath);
#endif
            foreach (var file in folder.GetFiles("*.txt"))
            {
                if (!pathNameList.Contains(file.Name))
                {
                    pathNameList.Add(file.Name);
                    var obj = Instantiate(fullNameBtn);
                    Debug.Log(pathNameList.Contains(file.Name));
                    obj.transform.SetParent(Group, true);
                    obj.GetComponentInChildren<Text>().text = file.Name;
                    obj.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        var str = File.ReadAllText(file.FullName);
                        var path = JsonConvert.DeserializeObject<Path>(str);
                        lineRenderer.positionCount = 0;
                        lineRenderer.positionCount = path.pointList.Count;
                        foreach (var (index, value) in path.pointList.WithIndex())
                        {
                            lineRenderer.SetPosition(index, value);
                        }

                        Debug.Log("读取成功" + path.pointList[0]);
                    });
                }
            }
        }

        /// <summary>
        /// 开启监听
        /// </summary>
        private void StartLocaltionClick()
        {
            if (!NativeToolkit.StartLocation())
            {
                NativeToolkit.ShowAlert("", "没有打开GPS定位");
                return;
            }

            if (isRecording)
            {
                isRecording = !isRecording;
                startLocaltionTxt.text = "开启定位";
                StopCoroutine("StartLocaltion");

                SaveData();
            }
            else
            {
                isRecording = !isRecording;
                startLocaltionTxt.text = "关闭定位";
                StartCoroutine("StartLocaltion");
            }
        }

        /// <summary>
        /// 不要管怎么计算的，反正结果是对的
        /// </summary>
        private void SaveData()
        {
            var lon_0 = localtionList[0].Longitude; //经度
            var lat_0 = localtionList[0].Latitude; //纬度 
            var C = 6371000 * Math.Cos(lat_0 * Mathf.Deg2Rad) * Math.PI / 180;
            lon_0 *= C;
            lat_0 *= 111136.1;

            foreach (var t in localtionList)
            {
                var lon = t.Longitude;
                var lat = t.Latitude;

                lon *= C;
                lat *= 111136.1;

                var x = lon - lon_0;
                var z = lat - lat_0;
                pointList.Add(new Vector3((float) x, t.Altitude, (float) z));
            }

            var path = new Path();
            path.pointList = pointList;

#if UNITY_EDITOR
            var filePath = System.IO.Path.Combine(Application.streamingAssetsPath,
                DateTime.Now.ToString("yyyyMMddhhmmss") + ".txt");
#else
            var filePath = System.IO.Path.Combine(Application.persistentDataPath,
                DateTime.Now.ToString("yyyyMMddhhmmss") + ".txt");
#endif
            var result = JsonConvert.SerializeObject(path);
            File.WriteAllText(filePath, result);

            logInfoTxt.text += "\n数据存储成功";
        }

        #endregion

        #region Public Method

        public IEnumerator StartLocaltion()
        {
            localtionList.Clear();
            while (true)
            {
                yield return new WaitForSeconds(1);
                Localtion localtion = new Localtion();
                localtion.Longitude = NativeToolkit.GetLongitude();
                localtion.Latitude = NativeToolkit.GetLatitude();
                localtion.Altitude = Input.location.lastData.altitude;
                localtionList.Add(localtion);
                Debug.Log("sdasdadas");
            }
        }

        #endregion

        public struct Localtion
        {
            public double Longitude, Latitude;
            public float Altitude;
        }
    }
}