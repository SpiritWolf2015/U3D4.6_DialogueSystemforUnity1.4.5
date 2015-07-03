using UnityEngine;
using System.Collections;

public class SetActiveTriggerListener : MonoBehaviour, IEventListener {

    public GameObject m_targetGameObject;

	// Use this for initialization
	void Start () {
        string eventName = Const.EVENT_NAME_SET_ACTIVE;
        EventManager.instance.AddListener(this as IEventListener, eventName);
        Debug.Log(string.Format("{0} 注册 {1} 事件完成", this.gameObject.name, eventName));        
	}

    #region 实现 IEventListener 接口

    bool IEventListener.HandleEvent (IEvent evt) {
        bool bActive = (bool)evt.GetData( );
        Debug.Log("Event received: " + evt.GetName( ) + " with data: " + bActive);

        m_targetGameObject.SetActive(bActive);

        return true;
    }

    #endregion 实现 IEventListener 接口

}

//=====================

public class SetActiveEvent : IEvent {

    private bool m_bActive = false;

    public SetActiveEvent ( ) { }
    public SetActiveEvent (bool bActive) {
        m_bActive = bActive;
    }

    string IEvent.GetName ( ) {
        return this.GetType( ).ToString( );
    }

    object IEvent.GetData ( ) {
        return m_bActive;
    }
}
