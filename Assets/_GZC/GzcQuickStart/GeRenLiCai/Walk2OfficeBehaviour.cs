using UnityEngine;
using System.Collections;

public class Walk2OfficeBehaviour : MonoBehaviour {

    public GameObject m_go;
    public float m_Time = 5F;

    string m_pathName = "New Path 2";


    void walk2Player ( ) {
        Debug.Log("walk2Office");
        iTween.MoveTo(m_go, iTween.Hash("path", iTweenPath.GetPath("New Path 1"), "time", m_Time, "looptype", "none", "easytype", "linear"));
    }

    void walk2Office ( ) {
        Debug.Log("walk2Office");
        iTween.MoveTo(m_go, iTween.Hash("path", iTweenPath.GetPath(m_pathName), "time", m_Time, "looptype", "none", "easytype", "linear", "orienttopath",true));
    }
}
