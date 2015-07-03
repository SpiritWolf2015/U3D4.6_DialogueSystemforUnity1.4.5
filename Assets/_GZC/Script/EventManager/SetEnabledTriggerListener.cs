using UnityEngine;
using System.Collections;

public class SetEnabledTriggerListener : MonoBehaviour, IEventListener {

    public MonoBehaviour m_MonoBehaviour;

	// Use this for initialization
	void Start () {
        EventManager.instance.AddListener(this as IEventListener, Const.EVENT_NAME_SET_ENABLED);
        Debug.Log(string.Format("{0} 注册 {1} 事件完成", this.gameObject.name, Const.EVENT_NAME_SET_ENABLED));
	}

    #region 实现 IEventListener 接口

    bool IEventListener.HandleEvent (IEvent evt) {
        bool bEnabled = (bool)evt.GetData( );
        Debug.Log("Event received: " + evt.GetName( ) + " with data: " + bEnabled);

        m_MonoBehaviour.enabled = bEnabled;

        return true;
    }

    #endregion 实现 IEventListener 接口

}

//=====================

public class SetEnabledEvent : IEvent {

    private bool m_bEnabled = false;

    public SetEnabledEvent ( ) { }
    public SetEnabledEvent (bool bEnabled) {
        m_bEnabled = bEnabled;
    }

    string IEvent.GetName ( ) {
        return this.GetType( ).ToString( );
    }

    object IEvent.GetData ( ) {
        return m_bEnabled;
    }
}
