using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class ConditionObserverSetVariable_BehaviourScript : MonoBehaviour {

    public string m_DialogueLuaVariableKey = "player_in";
	
	void Start () {
	
	}
	
	
	void Update () {
     
	}

    void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log(string.Format("SetVariable->{0}为真", m_DialogueLuaVariableKey));
            // 设置LUA变量
            DialogueLua.SetVariable(m_DialogueLuaVariableKey, true);
        }
    }


}
