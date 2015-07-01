using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;




public class UtilLoadAsset {

    #region static字段

    /// <summary>
    /// 已经Load过的配置文件 hash缓存, 【配置文件名字（带扩展名）——内容StringBuilder】
    /// </summary> 
    static Dictionary<string, string> s_hasLoadConfigTxt = new Dictionary<string, string>( );
    static System.Object s_lockConfig = new System.Object( );

    /// <summary>
    /// 已经Load过的AssetBundle hash缓存, 【AssetBundle名字——AssetBundle】
    /// </summary> 
    static Dictionary<string, AssetBundle> s_hasLoadAssetBundle = new Dictionary<string, AssetBundle>( );
    static System.Object s_lockThis = new System.Object( );
    

    /// <summary>
    /// 协议名称
    /// </summary>
    static string s_scheme = @"file://";

    #endregion static字段
    
    #region URL

    /// <summary>
    /// AssetBundle放的路径
    /// </summary> 
    static string AssetPath {
        get {
            string target = string.Empty;
            if (Application.platform == RuntimePlatform.OSXEditor ||
                Application.platform == RuntimePlatform.IPhonePlayer ||
                Application.platform == RuntimePlatform.OSXEditor) {
                target = "iphone";
            } else if (Application.platform == RuntimePlatform.Android) {
                target = "android";
            } else {
                target = "StandaloneWindows";
            }

            string assetPath = UtilAsset.GetDataDir( ) + "/StreamingAssets/" + target + "/";
            //Debug.Log("AssetPath = " + AssetPath);
            return assetPath;
        }
    }
        
    public static string Uri {
        get {
            return s_scheme + AssetPath;
        }
        set {
            if ("file" == value) {
                s_scheme = @"file://";
            } else if ("https" == value) {
                s_scheme = @"https://";
            } else {
                s_scheme = @"http://";
            }
        }
    }

    #endregion URL

    #region 配置文件

    /// <summary>
    /// 使用WWW读取一个配置文件
    /// </summary>
    /// <param name="configFileName">配置文件名，带扩展名</param>
    /// <returns>文件内容</returns>
    static IEnumerator readConfigTxtW3 (string configFileName) {
        string uri = Uri + configFileName;

        using (WWW w3 = new WWW(uri)) {
            yield return w3;
            if (!string.IsNullOrEmpty(w3.error)) {
                Debug.LogError("uri = " + uri + "使用WWW读取一个配置文件失败，" + w3.error);
            } else {
                lock (s_lockConfig) {               
                    string configTxt = w3.text;
                    if (!s_hasLoadConfigTxt.ContainsKey(configFileName)) {
                        // 加入到hash缓存
                        s_hasLoadConfigTxt.Add(configFileName, configTxt);
                        Debug.LogWarning("uri = " + uri + "使用WWW读取一个配置文件成功！，内容为" + configTxt);
                    }                   
                }
            }
        }
    }

    /// <summary>
    /// 将配置文件JSON反序列化还原，得到Key
    /// </summary>
    /// <param name="configJson"></param>
    /// <param name="assetName"></param>
    /// <returns></returns> 
    static List<object> configJsonDeserialize (string configJson, string assetName) {
        Dictionary<string, object> config = (Dictionary<string, object>)MiniJSON.Json.Deserialize(configJson);
        return (List<object>)config[configJsonDeserializeKey(configJson, assetName)];
    }
   
    /// <summary>
    /// 将配置文件JSON反序列化还原，得到依赖的资源名字List
    /// </summary>
    /// <param name="configJson"></param>
    /// <param name="assetName"></param>
    /// <returns></returns> 
    static string configJsonDeserializeKey (string configJson, string assetName) {
        Dictionary<string, object> config = (Dictionary<string, object>)MiniJSON.Json.Deserialize(configJson);
        string firstKey = "";
        int i = 0;
        foreach (var key in config.Keys) {
            if (0 == i) {
                firstKey = key.ToString( );
            }
            i++;
        }
        return firstKey;
    }
    
    #endregion 配置文件

    #region 载入AssetBundle

    /// <summary>
    /// 载入有依赖关系的push asset
    /// </summary>
    /// <param name="pushAssetName">压入资源名字，带扩展名，如AAA.unity3d</param>
    /// <returns></returns> 
    public static IEnumerator loadPushAssetBundle (string pushAssetName) {
         int nEnumeResult = 0;
        // 去掉文件扩展名
        string noExtAssetName = pushAssetName.Remove(pushAssetName.LastIndexOf('.'));
        string configFileName = noExtAssetName + ".txt";

        // 读取该push asset的配置文件
        IEnumerator eTxt = readConfigTxtW3(configFileName);
        while (eTxt.MoveNext( ) && eTxt != null) {
            yield return ++nEnumeResult;
        }
        // 有配置文件，载入其依赖资源
        if (s_hasLoadConfigTxt.ContainsKey(configFileName)) {
            string config = s_hasLoadConfigTxt[configFileName];
            List<object> sharedAssetNames = configJsonDeserialize(config, noExtAssetName);

            // 载入该push asset资源的所有依赖资源       
            foreach (string sharedAssetName in sharedAssetNames) {
                IEnumerator e = loadSingleAssetBundle(sharedAssetName);
                while (e.MoveNext( ) && e != null) {
                    yield return ++nEnumeResult;
                }
                Debug.Log("sharedAssetName = " + sharedAssetName);
            }
            lock (s_lockConfig) {
                s_hasLoadConfigTxt.Remove(configFileName);
            }
        }        

        // 载入该push asset
        {
            IEnumerator e = loadSingleAssetBundle(pushAssetName);
            while (e.MoveNext( ) && e != null) {
                yield return ++nEnumeResult;
            }
            Debug.Log("pushAssetName = " + pushAssetName);
        }
        yield return ++nEnumeResult;
    }
    
    /// <summary>
    /// 载入一个AssetBundle
    /// </summary>
    /// <param name="assetName"></param>
    /// <returns></returns> 
    static IEnumerator loadSingleAssetBundle (string assetName) {
        bool bContain = false;
        lock (s_lockThis) {
            bContain = s_hasLoadAssetBundle.ContainsKey(assetName);
        }
        if (!bContain) {
            using (WWW w3 = new WWW(Uri + assetName)) {
               
                Debug.Log(Uri + assetName);
                yield return w3;
                if (!string.IsNullOrEmpty(w3.error)) {
                    Debug.LogError(w3.error);
                } else {
                    // 加入到hash缓存
                    lock (s_lockThis) {
                        AssetBundle assetBundle = w3.assetBundle;
                        s_hasLoadAssetBundle.Add(assetName, assetBundle);
                        Debug.LogWarning("AssetBundle hash缓存:" + "【" + assetName + "——" + assetBundle + "】");
                    }
                }       
            }
        }     
    }

    #endregion 载入AssetBundle
    
    /// <summary>
    /// 得到AssetBundle中GameObject
    /// </summary>
    /// <param name="assetName">AssetBundle名，带扩展名如AAA.assetBundle</param>
    /// <returns>AssetBundle中GameObject</returns> 
    public static GameObject getAssetBundleGameObject (string assetName) {        
        GameObject go = Object.Instantiate(s_hasLoadAssetBundle[assetName].mainAsset) as GameObject;
        Debug.LogWarning("从" + assetName + "get : " + go.name);
        destroyAssetBundle(assetName);

        return go;
    }
    
    #region destroyAssetBundle
    
    /// <summary>
    /// 释放AssetBundle
    /// </summary>
    /// <param name="assetName">AssetBundle名，带扩展名</param> 
    public static void destroyAssetBundle (string assetName) {    
        lock (s_lockThis) {
            if (s_hasLoadAssetBundle.ContainsKey(assetName)) {
                s_hasLoadAssetBundle[assetName].Unload(false);
                s_hasLoadAssetBundle.Remove(assetName);
                Debug.LogWarning("卸载" + assetName + "的内存镜像");
            } else {
                Debug.LogWarning("未加载" + assetName + "，无法卸载！");
            }
        }
    }
    /// <summary>
    /// 卸载所有已经加载的AssetBundle
    /// </summary>
    /// <param name="unloadAllLoadedObjects"></param>
    public static void destroyAllHasLoadAssetBundle (bool unloadAllLoadedObjects ) {
        lock (s_lockThis) {
            foreach (AssetBundle loadedAssetBundle in s_hasLoadAssetBundle.Values) {
                loadedAssetBundle.Unload(unloadAllLoadedObjects);                
            }
            s_hasLoadAssetBundle.Clear( );
            Debug.LogWarning("卸载所有已经加载的AssetBundle！");
        }
    }

    #endregion destroyAssetBundle

}
