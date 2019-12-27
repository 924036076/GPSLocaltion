using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Assets 用于复制“项目”视图上文件夹中现有资产的相对路径的类
/// </summary>
public static class AssetPathCopyer {
    private enum Priority   // 优先度
    {
        FROM_ASSETS = 10000,                // Assets 从文件夹复制相对路径的命令
        FROM_ASSETS_WITH_DOUBLE_QUATES,     // Assets 从双引号复制文件夹中相对路径的命令
        FROM_RESOURCES,                     // Resources 从文件夹复制相对路径的命令
        FROM_RESOURCES_WITH_DOUBLE_QUATES,  // Resources 从双引号复制文件夹中相对路径的命令
    }

    private const string FROM_ASSETS_ITEM_NAME = @"Assets/Copy Path/From Assets";             
    private const string FROM_ASSETS_WITH_DOUBLE_QUATES_ITEM_NAME = @"Assets/Copy Path/From Assets With """"";   
    private const string FROM_RESOURCES_ITEM_NAME = @"Assets/Copy Path/From Resources";  
    private const string FROM_RESOURCES_WITH_DOUBLE_QUATES_ITEM_NAME = @"Assets/Copy Path/From Resources With """""; 

    private static string SelectAssetPath { get { return AssetDatabase.GetAssetPath(Selection.objects[0]); } } // 从所选对象的Assets文件夹中获取相对路径
    private static bool IsSelectAsset { get { return Selection.objects != null && 0 < Selection.objects.Length; } } // 返回所选对象是否存在

    /// <summary>
    /// Assets 从文件夹复制相对路径
    /// </summary>
    [MenuItem(FROM_ASSETS_ITEM_NAME, false, (int)Priority.FROM_ASSETS)]
    private static void FromAssets ()
    {
        EditorGUIUtility.systemCopyBuffer = SelectAssetPath;
    }

    /// <summary>
    /// Assets 从双引号复制文件夹中相对路径的命令
    /// </summary>
    [MenuItem(FROM_ASSETS_WITH_DOUBLE_QUATES_ITEM_NAME, false, (int)Priority.FROM_ASSETS_WITH_DOUBLE_QUATES)]
    private static void FromAssetsWithDoubleQuates ()
    {
        EditorGUIUtility.systemCopyBuffer = AddDoubleQuates(SelectAssetPath);
    }

    /// <summary>
    /// Resources 从文件夹复制相对路径
    /// </summary>
    [MenuItem(FROM_RESOURCES_ITEM_NAME, false, (int)Priority.FROM_RESOURCES)]
    private static void FromResources ()
    {
        EditorGUIUtility.systemCopyBuffer = ToPathFromResources(SelectAssetPath);
    }

    /// <summary>
    /// Resources 从双引号复制文件夹中相对路径的命令
    /// </summary>
    [MenuItem(FROM_RESOURCES_WITH_DOUBLE_QUATES_ITEM_NAME, false, (int)Priority.FROM_RESOURCES_WITH_DOUBLE_QUATES)]
    private static void FromResourcesWithDoubleQuates ()
    {
        EditorGUIUtility.systemCopyBuffer = AddDoubleQuates(ToPathFromResources(SelectAssetPath));
    }

    /// <summary>
    /// 检查资产路径是否可以复制
    /// </summary>
    [MenuItem(FROM_ASSETS_ITEM_NAME, true)]
    [MenuItem(FROM_ASSETS_WITH_DOUBLE_QUATES_ITEM_NAME, true)]
    [MenuItem(FROM_RESOURCES_ITEM_NAME, true)]
    [MenuItem(FROM_RESOURCES_WITH_DOUBLE_QUATES_ITEM_NAME, true)]
    private static bool Validate ()
    {
        return IsSelectAsset;
    }

    /// <summary>
    /// 将指定的字符串转换为Resources文件夹中的相对路径
    /// </summary>
    private static string ToPathFromResources (string str)
    {
        str = Regex.Replace(str, @"^.*Resources/", ""); // 删除资源文件夹的路径
        str = string.Format("{0}/{1}", Path.GetDirectoryName(str), Path.GetFileNameWithoutExtension(str)); // 删除扩展名
        str = str.StartsWith(@"/") ? str.Remove(0, 1) : str;
        return str;
    }

    /// <summary>
    /// 在指定的字符串上添加双引号
    /// </summary>
    private static string AddDoubleQuates (string str)
    {
        return string.Format(@"""{0}""", str);
    }
}