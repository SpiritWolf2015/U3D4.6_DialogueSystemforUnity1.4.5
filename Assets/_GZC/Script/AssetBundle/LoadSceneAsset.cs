using UnityEngine;
using System.Collections;

public class LoadSceneAsset : MonoBehaviour {
    
    public string[ ] m_assetNames;
    public GameObject[ ] m_destroyInitSceneGos;

    //异步对象  
    private AsyncOperation mAsyn;
    //更新Tip的时间  
    private const float UpdateTime = 0.1F;
    //上一次更细的时间  
    private float lastTime = 0.0F;  

    IEnumerator Start ( ) {
        // 载入所有assetBundle
        yield return StartCoroutine(loadAssets( ));

        // 载入完assetBundle后，异步加载场景
        StartCoroutine(SceneManager.Instance.LoadSceneAdditiveAsync(0, null));
        // 角色的主场景载入完了，就把现在的这个场景的东西删了。
        StartCoroutine(SceneManager.Instance.LoadSceneAdditiveAsync(1, DestroyInitSceneGos));          
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

    void DestroyInitSceneGos ( ) {
        for (int i = 0; i < m_destroyInitSceneGos.Length; i++) {
            Destroy(m_destroyInitSceneGos[i]);
        }
    }

}
