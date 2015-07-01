using UnityEngine;
using System.Collections;

public class PlayTriggerBehaviour : MonoBehaviour {
   
    public const string COMPARE_TAG = "Player";
    public string m_AnimateName = "Take 001";

    void OnTriggerEnter (Collider other) {
        if (other.CompareTag(COMPARE_TAG)) {
            EventManager.instance.QueueEvent(new PlayAnimateEvent(m_AnimateName));
        }
    }

}
