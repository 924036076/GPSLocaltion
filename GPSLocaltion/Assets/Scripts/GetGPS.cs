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
using Utf8Json;
using System.Collections.Generic;
using System.IO;
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

        public List<Localtion> localtionList = new List<Localtion>();
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

        private void GetFileBtn()
        {
            DirectoryInfo folder = new DirectoryInfo(Application.persistentDataPath);
            foreach (var file in folder.GetFiles("*.bytes"))
            {
                logInfoTxt.text += "\n" + file.FullName;
            }
        }

        /// <summary>
        /// 开启监听
        /// </summary>
        private void StartLocaltionClick()
        {
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
                pointList.Add(new Vector3((float) x, 0, (float) z));
            }

            var bytes = JsonSerializer.Serialize(pointList);
#if UNITY_EDITOR
            using (FileStream fs = new FileStream(
                Application.dataPath + "/StreamingAssets/" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".bytes"
                , FileMode.OpenOrCreate, FileAccess.Write))
            {
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                fs.Dispose();
            }
#else
            using (FileStream fs = new FileStream(
                Application.persistentDataPath + "/" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".bytes"
                , FileMode.OpenOrCreate, FileAccess.Write))
            {
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                fs.Dispose();
            }
#endif
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
                localtionList.Add(localtion);
                Debug.Log("sdasdadas");
            }
        }

        #endregion

        public struct Localtion
        {
            public double Longitude, Latitude;
        }
    }
}