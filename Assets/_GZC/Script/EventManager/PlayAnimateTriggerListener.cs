using UnityEngine;
using System.Collections;

public class PlayAnimateTriggerListener : MonoBehaviour, IEventListener {
   
    public Animation[] m_Animations;

	void Start () {
        EventManager.instance.AddListener(this as IEventListener, Const.EVENT_NAME_PLAY_ANIMATE);
        Debug.Log(string.Format("{0} 注册 {1} 事件完成", this.gameObject.name, Const.EVENT_NAME_PLAY_ANIMATE));
	}

    #region 实现 IEventListener 接口

    bool IEventListener.HandleEvent (IEvent evt) {
        string animationName = evt.GetData( ) as string;
        Debug.Log("Event received: " + evt.GetName( ) + " with data: " + animationName);

        playAnimation(animationName);

        return true;
    }

    #endregion 实现 IEventListener 接口

    void playAnimation (string animationName) {
        if (!string.IsNullOrEmpty(animationName)) {
            for (int i = 0; i < m_Animations.Length; i++) {
                Debug.Log(string.Format("播放动画：{0}, 监听器 {1}", animationName, this.gameObject.name));
                m_Animations[i].CrossFade(animationName);
            }               
        } else {
            Debug.LogError(string.Format("{0} is null or empty!!", animationName));
        }
    }

}

//=====================

public class PlayAnimateEvent : IEvent {

    private string m_strAnimateName = "Take 001";
    public PlayAnimateEvent ( ) { }
    public PlayAnimateEvent (string strAnimateName) {
        m_strAnimateName = strAnimateName;
    }

     string IEvent.GetName ( ) {
        return this.GetType( ).ToString( );
    }

     object IEvent.GetData ( ) {
        return m_strAnimateName;
    }
}
