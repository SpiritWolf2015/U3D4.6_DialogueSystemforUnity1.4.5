using UnityEngine;
using System.Collections;

public class SetEnabledTriggerBehaviour : BaseTriggerBehaviour {	

    public override void onTriggerEnterAction ( ) {
        EventManager.instance.QueueEvent(new SetEnabledEvent( ));
    }

}
