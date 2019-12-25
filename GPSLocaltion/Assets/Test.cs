/*************************************************************************
 *  Copyright © #COPYRIGHTYEAR# Eyang. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Test.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Eyang
 *  Version      :  0.1.0
 *  Date         :  #CREATEDATE#
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Utf8Json;

namespace GameMain.Script
{
    public class Person
    {
        public int Age { get; set; }
        public string Name { get; set; }
    }

    public class Test : MonoBehaviour
    {
        #region Field and Property

        private List<Vector3> vList = new List<Vector3>();
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private GameObject obj;

        #endregion

        #region Private Method

        private void Start()
        {
//            var p = new Person {Age = 20, Name = "GDX"};
//            vList.Add(new Vector3(0, 1));
//            vList.Add(new Vector3(1, 0));
//            var result = JsonSerializer.Serialize(vList);
//            Debug.Log("1. =====>" + result);
//            var p2 = JsonSerializer.Deserialize<List<Vector3>>(result);
//            Debug.Log("2. =====>" + p2);
//            var json = JsonSerializer.ToJsonString(p2);
//            Debug.Log("3. =====>" + json);
//            File.WriteAllBytes(
//                Application.dataPath + "/StreamingAssets/" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".txt",
//                result);

//            using (FileStream fs = new FileStream(
//                Application.dataPath + "/StreamingAssets/" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".bytes"
//                , FileMode.OpenOrCreate, FileAccess.Write))
//            {
//                fs.Write(result, 0, result.Length);
//                fs.Close();
//                fs.Dispose();
//            }

            using (FileStream fs =
                new FileStream(
                    Application.dataPath + "/StreamingAssets/20191225113548.bytes",
                    FileMode.Open, FileAccess.Read))
            {
                var bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                vList = JsonSerializer.Deserialize<List<Vector3>>(bytes);
                Debug.Log(JsonSerializer.ToJsonString(vList));
//                lineRenderer.positionCount = list.Count;
//                lineRenderer.SetPositions(list.ToArray());
                fs.Close();
                fs.Dispose();
            }
//            DirectoryInfo folder = new DirectoryInfo(Application.dataPath + "/StreamingAssets");
//
//            Debug.Log(folder.GetFiles("*.bytes")[0]);

//            JsonSerializer.Deserialize<List<Vector3>>(result)
//            foreach (var file in folder.GetFiles("*.bytes"))
//            {
////                Debug.Log(file.FullName);
//            }

//            FileStream fileStream = File.Create(Application.dataPath + "/StreamingFile" + "/byBin.byte");
//            File.WriteAllBytes(Application.dataPath + "/1.btyes", result);
//            StartCoroutine(Testmethod());
        }

        private void Update()
        {
            for(int i = 0; i < vList.Count; i++)
            {
                Debug.DrawLine(vList[i], vList[i+1], Color.green);
            }
        }

        IEnumerator Testmethod()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                Debug.Log("111111");
            }
        }

        #endregion

        #region Public Method

        #endregion
    }
}