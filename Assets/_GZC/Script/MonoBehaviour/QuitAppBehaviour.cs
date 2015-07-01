using UnityEngine;
using System.Collections;

public class QuitAppBehaviour : MonoBehaviour {

    public void QuitApp ( ) {
        Debug.Log("退出程序");
        Application.Quit( );
    }
}
