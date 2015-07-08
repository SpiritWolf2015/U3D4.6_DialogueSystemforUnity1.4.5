using UnityEngine;
using System.Collections;

public class LoadSceneTriggerListener : MonoBehaviour, IEventListener {

    // 
    void Start ( ) {
        string eventName = Const.EVENT_NAME_LOAD_SCENE;
        EventManager.instance.AddListener(this as IEventListener, eventName);
        Debug.Log(string.Format("{0} 注册 {1} 事件完成", this.gameObject.name, eventName));
    }

    #region 实现 IEventListener 接口

    bool IEventListener.HandleEvent (IEvent evt) {
        string sceneName = (string)evt.GetData( );
        Debug.Log("Event received: " + evt.GetName( ) + " with data: " + sceneName);

        // 删了主场景的物体，载入到办公室场景。
        StartCoroutine(SceneManager.Instance.LoadSceneAdditiveAsync(2, LoadScene.Instance.DestroyInitSceneGos));

        return true;
    }

    #endregion 实现 IEventListener 接口
}

//=====================

public class LoadSceneEvent : IEvent {

    private string m_sceneName = null;

    public LoadSceneEvent ( ) { }
    public LoadSceneEvent (string sceneName) {
        m_sceneName = sceneName;
    }

    string IEvent.GetName ( ) {
        return this.GetType( ).ToString( );
    }

    object IEvent.GetData ( ) {
        return m_sceneName;
    }
}