using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 应用类型
/// </summary>
[Serializable]
public enum PlaySceneType
{
    Fanatic = 0,    // 健身
    Station = 1,    // 空间站
    Family = 2,     // 亲子

}

/// <summary>
/// 配置文件
/// </summary>
public class ScenesConfig
{
    /// <summary>
    /// AssetBundle路径
    /// </summary>
    public static string AssetBundlePath = Application.streamingAssetsPath;//"/mnt/sdcard/Download"
    /// <summary>
    /// assetBundle名字
    /// </summary>
    public static string AssetBundleName;
    /// <summary>
    /// 场景索引
    /// </summary>
    public static int FristSceneId    = 0;  // FristScene
    public static int MainSceneId   = 0;  // MainScene
    public static int PlaySceneId   = 1;  // HoloPlayScene
    /// <summary>
    /// 应用场景类型
    /// </summary>
    public static PlaySceneType SceneType;
    /// <summary>
    /// 场景
    /// </summary>
    public static string SceneTitle;
    /// <summary>
    /// 全息影像的位置、旋转、缩放信息
    /// </summary>
    //public static int HoloPersonScale = 0;
    //public static Vector3 HoloPersonPos;
    //public static Vector3 HoloPersonRotation;

    /// <summary>
    /// 是否持续追踪
    /// </summary>
    public static bool IsKeepTracking;


    /// <summary>
    /// Event
    /// </summary>
    public static  Action OnPrefabCreate;


}
