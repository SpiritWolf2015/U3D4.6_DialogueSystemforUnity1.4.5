using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class ShowAlertBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("脚本里写的提示消息");
            DialogueManager.ShowAlert("脚本里写的提示消息");
        }
	}
}
