using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class ConditionObserverSetVariable_BehaviourScript : MonoBehaviour {

	
	void Start () {
	
	}
	
	
	void Update () {
     
	}

    void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("SetVariable->PlayerIn为真");
            // 设置LUA变量
            DialogueLua.SetVariable("player_in", true);
        }
    }


}
