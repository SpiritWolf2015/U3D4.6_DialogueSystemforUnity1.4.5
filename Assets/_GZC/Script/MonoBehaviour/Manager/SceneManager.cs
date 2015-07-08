using UnityEngine;
using System.Collections;
using System;


public class SceneManager : MonoBehaviour {

    public string[ ] levelNames;
    public int gameLevelNum;

    //异步对象  
    private AsyncOperation mAsyn = null;
    //更新Tip的时间  
    private const float UpdateTime = 0.1F;
    //上一次更细的时间  
    private float lastTime = 0.0F;

    #region 单例
    
    private static SceneManager s_Instance = null;

    public static SceneManager Instance {
        get {
            if (null == s_Instance) {
                s_Instance = (SceneManager)GameObject.FindObjectOfType(typeof(SceneManager));
                if (!s_Instance)
                    Debug.LogError("找不到挂SceneManager脚本物体");
            }
            return s_Instance;
        }
    }

    #endregion 单例

    public void Start ( ) {
        // keep this object alive
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadLevel (string sceneName) {
        Application.LoadLevel(sceneName);
    }

    public void GoNextLevel ( ) {
        // if our index goes over the total number of levels in the 
        // array, we reset it  
        if (gameLevelNum >= levelNames.Length)
            gameLevelNum = 0;
        // load the level (the array index starts at 0, but we start  
        // counting game levels at 1 for clarity’s sake)  
        LoadLevel(gameLevelNum);
        // increase our game level index counter
        gameLevelNum++;
    }

    private void LoadLevel (int indexNum) {
        // load the game level  
        LoadLevel(levelNames[indexNum]);
    }

    public void ResetGame ( ) {
        // reset the level index counter 
        gameLevelNum = 0;
    }

    void LoadLevelAdditive (int indexNum) {
        Application.LoadLevelAdditive(levelNames[indexNum]);
        Debug.Log(string.Format("增量加载场景 {0} 完毕！", levelNames[indexNum]));
    }

    public IEnumerator LoadSceneAdditiveAsync (int indexNum, Action finish) {
        string sceneName = levelNames[indexNum];
        Debug.Log(string.Format("异步增量加载的场景名：{0}", sceneName));

        mAsyn = Application.LoadLevelAdditiveAsync(sceneName);
        // 显示加载进度
        StartCoroutine(LoadSceneAsyncOperationProgress(mAsyn, sceneName, finish));
        yield return mAsyn;
    }

    // 显示加载进度
    IEnumerator LoadSceneAsyncOperationProgress (AsyncOperation asyn, string sceneName, Action finish) {
        if (null != asyn) {
            while (!asyn.isDone) {
                // 如果达到了更新的时间  
                if (Time.time - lastTime >= UpdateTime) {
                    lastTime = Time.time;
                    // 显示进度
                    Debug.Log("已经加载了(" + (int)(asyn.progress * 100) + "%" + ")");
                }
                yield return 0;
            }
            Debug.Log(string.Format("异步加载场景 {0} 完毕！", sceneName));

            // 载入完成回调
            if (null != finish) {
                finish( );
            } else {
                Debug.LogWarning("finish callback is null !!");
            }
        } else {
            Debug.LogError("asyn is null !!");
        }       
    }

}