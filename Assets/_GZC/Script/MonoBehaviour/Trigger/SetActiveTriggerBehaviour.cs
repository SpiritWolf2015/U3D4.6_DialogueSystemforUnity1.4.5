using UnityEngine;
using System.Collections;

public class SetActiveTriggerBehaviour : BaseTriggerBehaviour {	

    public override void onTriggerEnterAction ( ) {
        EventManager.instance.QueueEvent(new SetActiveEvent(true));
    }



}
