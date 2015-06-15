using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class ShowAlertBehaviourScript : MonoBehaviour {

    [Range(0.1F, 5F)]
    public float m_tipSecond = 3F;

	// Use this for initialization
	void Start () {
        tip( );
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.G)) {
            tip( );
        }
	}

    void tip ( ) {
        const string s = "C#脚本里写的提示消息，还可以LUA写提示消息";
        Debug.Log(s);
        // 第2个参数是显示秒数
        DialogueManager.ShowAlert(s, m_tipSecond);
    }

}
