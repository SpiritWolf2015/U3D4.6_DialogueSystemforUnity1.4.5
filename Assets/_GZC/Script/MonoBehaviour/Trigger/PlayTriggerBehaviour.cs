using UnityEngine;
using System.Collections;

public class PlayTriggerBehaviour : BaseTriggerBehaviour {   
    
    public string m_AnimateName = "Take 001";

    public override void onTriggerEnterAction ( ) {
        EventManager.instance.QueueEvent(new PlayAnimateEvent(m_AnimateName));
    }

}
