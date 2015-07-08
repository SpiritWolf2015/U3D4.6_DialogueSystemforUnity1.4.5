using UnityEngine;
using System.Collections;

public class LoadSceneTriggerBehaviour : BaseTriggerBehaviour {

    public string m_sceneName;

    public override void onTriggerEnterAction ( ) {
        Debug.Log(string.Format("m_sceneName = {0}", m_sceneName));
        EventManager.instance.QueueEvent(new LoadSceneEvent(m_sceneName));
    }
}
