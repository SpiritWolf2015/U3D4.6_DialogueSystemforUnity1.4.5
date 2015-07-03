using UnityEngine;
using System.Collections;
using System;

public abstract class BaseTriggerBehaviour : MonoBehaviour {

    private Action m_OnTriggerEnterAction;

    #region U3D API

    void Start ( ) {
        initAction( );
    }

    void OnTriggerEnter (Collider other) {
        if (isFireOnTriggerEnterEvent(other)) {
            // 发出事件
            fireOnTriggerEnterEvent( );
        }
    }

    #endregion U3D API

    /// <summary>
    /// 初始化m_OnTriggerEnterAction
    /// </summary>
    private void initAction ( ) {
        if (null != m_OnTriggerEnterAction) {
            m_OnTriggerEnterAction += onTriggerEnterAction;
        } else {
            m_OnTriggerEnterAction = onTriggerEnterAction;
        }        
    }

    /// <summary>
    /// OnTriggerEnter时调用具体内容
    /// </summary>
    public abstract void onTriggerEnterAction ( );

    /// <summary>
    /// 发出OnTriggerEnterEvent条件，默认为进入者为玩家发出
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool isFireOnTriggerEnterEvent (Collider other) {
        if (other.CompareTag(Const.COMPARE_TAG)) {           
            return true;
        }
        return false;
    }

    void fireOnTriggerEnterEvent ( ) {
        if (null != m_OnTriggerEnterAction) {
            m_OnTriggerEnterAction( );
        } else {
            Debug.LogError("m_OnTriggerEnterAction is null !!");
        }
    }

}
