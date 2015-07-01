using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class ShowAlertBehaviourScript : MonoBehaviour {

    [Range(0.1F, 5F)]
    public float m_tipSecond = 3F;
    public string m_tipString = "tip";

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
        // 第2个参数是显示秒数
        DialogueManager.ShowAlert(m_tipString, m_tipSecond);
    }

}
