using UnityEngine;
using System.Collections;

public class RecMsg_BehaviourScript : MonoBehaviour {

    void RecMsg1 (string str) {
        Debug.Log(this + ", 参数str = " + str);
    }

    void RecMsg2 ( ) {
        Debug.Log(this);
    }

}
