using UnityEngine;
using System.Collections;
using System;
using System.IO;

/// <summary>
/// 资源工具类
/// </summary>
public static class UtilAsset  {

    /// <summary>
    /// 应用程序内容路径
    /// </summary>
    public static string AppContentPath ( ) {
        string path = string.Empty;
        switch (Application.platform) {
            case RuntimePlatform.Android:
                path = "jar:file://" + Application.dataPath + "!/assets/";
                break;
            case RuntimePlatform.IPhonePlayer:
                path = Application.dataPath + "/Raw/";
                break;
            default:
                path = "file://" + Application.dataPath + "/StreamingAssets/";
                break;
        }
        return path;
    }

    /// <summary>
    /// 取得App包里面的读取目录
    /// </summary>
    public static Uri AppContentDataUri {
        get {
            string dataPath = Application.dataPath;
            if (Application.platform == RuntimePlatform.IPhonePlayer) {
                var uriBuilder = new UriBuilder( );
                uriBuilder.Scheme = "file";
                uriBuilder.Path = Path.Combine(dataPath, "Raw");
                return uriBuilder.Uri;
            } else if (Application.platform == RuntimePlatform.Android) {
                return new Uri("jar:file://" + dataPath + "!/assets");
            } else {
                var uriBuilder = new UriBuilder( );
                uriBuilder.Scheme = "file";
                uriBuilder.Path = Path.Combine(dataPath, "StreamingAssets");
                return uriBuilder.Uri;
            }
        }
    }

    /// <summary>
    /// 取得数据存放目录
    /// </summary>
    public static string GetDataDir ( ) {
        string dataPath = Application.dataPath;

        if (Application.platform == RuntimePlatform.IPhonePlayer) {
            string path = Path.GetDirectoryName(Path.GetDirectoryName(dataPath));
            return Path.Combine(path, "Documents/testgame/");

        } else if (Application.platform == RuntimePlatform.Android) {
            return "/sdcard/testgame/";
        }

        return dataPath;
    }



}
