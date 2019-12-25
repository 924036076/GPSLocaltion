using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatmullRom 
{
    //曲线插值函数 3D : 如果要顯示所有陣列數據這較方便 ( 可顯示頭尾, 兩個點也能顯示 )
    public static Vector3 EasyInterp3D(Vector3[] pts, float t)
    {
        Vector3[] v = new Vector3[pts.Length + 2];
        v[0] = pts[0];
        v[v.Length - 1] = pts[pts.Length - 1];
        for (int i = 0; i < pts.Length; i++)
        {
            v[i + 1] = pts[i];
        }

        return Interp3D(v, t);
    }

    //曲线插值函数 3D : 原始方法 ( 頭尾不顯示 )
    private static Vector3 Interp3D(Vector3[] pts, float t)
    {
        int numSections = pts.Length - 3;
        int currPt = Mathf.Min(Mathf.FloorToInt(t * (float) numSections), numSections - 1);
        float u = t * (float) numSections - (float) currPt;

        Vector3 a = pts[currPt];
        Vector3 b = pts[currPt + 1];
        Vector3 c = pts[currPt + 2];
        Vector3 d = pts[currPt + 3];

        return .5f * (
                   (-a + 3f * b - 3f * c + d) * (u * u * u)
                   + (2f * a - 5f * b + 4f * c - d) * (u * u)
                   + (-a + c) * u
                   + 2f * b
               );
    }
}