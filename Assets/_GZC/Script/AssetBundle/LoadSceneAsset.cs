using UnityEngine;
using System.Collections;

public class LoadSceneAsset : MonoBehaviour {

    public string m_sceneName = "MyScene";
    public string[ ] m_assetNames;

    //异步对象  
    private AsyncOperation mAsyn;
    //更新Tip的时间  
    private const float UpdateTime = 0.1F;
    //上一次更细的时间  
    private float lastTime = 0.0F;  

    IEnumerator Start ( ) {
        // 载入所有assetBundle
        yield return StartCoroutine(loadAssets( ));
        StartCoroutine(LoadAbSceneAsync( ));      
    }

    void Update ( ) {
        //如果场景没有加载完则显示Tip  
        if (mAsyn != null && !mAsyn.isDone) {
            //如果达到了更新的时间  
            if (Time.time - lastTime >= UpdateTime) {
                lastTime = Time.time;
                Debug.Log("已经加载了(" + (int)(mAsyn.progress * 100) + "%" + ")");
            }
        } else {
            Debug.Log(string.Format("从assetBundle异步加载场景{0}完毕！", m_sceneName));
            this.enabled = false;
        }
    }  

    void OnDestroy ( ) {
        UtilLoadAsset.destroyAllHasLoadAssetBundle(false);
    }

    // 载入所有assetBundle
    IEnumerator loadAssets ( ) {
        foreach (string assetName in m_assetNames) {
            IEnumerator e = UtilLoadAsset.loadPushAssetBundle(assetName);
            while (e.MoveNext( ) && e != null) {
                yield return e.Current;
            }
        }
    }

    void LoadAbScene ( ) {
        Application.LoadLevelAdditive(m_sceneName);
        Debug.Log(string.Format("从assetBundle加载场景{0}完毕！", m_sceneName));
    }

    IEnumerator LoadAbSceneAsync ( ) {
        mAsyn = Application.LoadLevelAdditiveAsync(m_sceneName);
        yield return mAsyn;
    }

}
