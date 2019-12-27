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
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

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
//        [SerializeField] private GameObject obj;

        #endregion

        #region Private Method

        private void Start()
        {
            var p1 = new Person {Age = 12, Name = "GDX"};

            var p = new Path();
            p.pointList.Add(new Vector3(0, 1));
            p.pointList.Add(new Vector3(1, 0));
            p.pointList.Add(new Vector3(5, 0));
            var result = JsonConvert.SerializeObject(p);
            Debug.Log("1. =====>" + result);
//            File.WriteAllText(
//                System.IO.Path.Combine(Application.streamingAssetsPath,
//                    DateTime.Now.ToString("yyyyMMddhhmmss") + ".txt"), result);
//            var p2 = JsonConvert.DeserializeObject<List<Vector3>>(result);
//            Debug.Log("2. =====>" + p2);
//            var json = JsonConvert.ToString(result);
//            Debug.Log("3. =====>" + json);
//            File.WriteAllBytes(
//                Application.dataPath + "/StreamingAssets/" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".txt",
//                result);
            var path = new Path();
            DirectoryInfo folder = new DirectoryInfo(Application.streamingAssetsPath);
            foreach (var file in folder.GetFiles("*.txt"))
            {
                var str = File.ReadAllText(file.FullName);
                path = JsonConvert.DeserializeObject<Path>(str);
            }

            lineRenderer.positionCount = path.pointList.Count;
            lineRenderer.SetPositions(path.pointList.ToArray());
//            using (var fs = File.OpenRead(Application.dataPath + "/StreamingAssets/" + "20191227095708.dat"))
//            {
//                using (var reader = new BsonReader(fs))
//                {
//                    var serializer = new Newtonsoft.Json.JsonSerializer();
//                    var s = serializer.Deserialize<Person>(reader);
//                    Debug.Log(s.Name);
//                    
//                }
//            }
//            using (var fs =
//                File.Open(Application.dataPath + "/StreamingAssets/" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".dat",
//                    FileMode.Create))
//            {
//                using (var writer = new BsonWriter(fs))
//                {
//                    var serializer = new JsonSerializer();
//                    serializer.Serialize(writer, p);
//                }
//            }

//            using (FileStream fs = new FileStream(
//                Application.dataPath + "/StreamingAssets/" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".txt"
//                , FileMode.OpenOrCreate, FileAccess.Write))
//            {
//                using (var writer = new BsonWriter(fs))
//                {
//                    var serializer = new JsonSerializer();
//                    serializer.Serialize(writer, result);
//                }
//            }

//            using (FileStream fs =
//                new FileStream(
//                    Application.dataPath + "/StreamingAssets/20191225113548.bytes",
//                    FileMode.Open, FileAccess.Read))
//            {
//                var bytes = new byte[fs.Length];
//                fs.Read(bytes, 0, bytes.Length);
//
//                using (var reader = new BsonReader(fs))
//                {
//                    var serializer = new Newtonsoft.Json.JsonSerializer();
//                    vList = serializer.Deserialize<List<Vector3>>(reader);
//                    Debug.Log(vList);
////                    foreach (var v in vList)
////                    {
////                        Debug.Log(v);
////                    }
//                }

//                Newtonsoft.Json.JsonSerializer.Deserialize<List<Vector3>>(bytes);
//                vList = JsonConvert.de<List<Vector3>>(bytes);
//                lineRenderer.positionCount = list.Count;
//                lineRenderer.SetPositions(list.ToArray());
//            fs.Close();
//            fs.Dispose();
//        }
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
//            for (int i = 0; i < vList.Count; i++)
//            {
//                Debug.DrawLine(vList[i], vList[i + 1], Color.green);
//            }
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

//        https://20191116-095306/svn/txkj

        #endregion
    }
}