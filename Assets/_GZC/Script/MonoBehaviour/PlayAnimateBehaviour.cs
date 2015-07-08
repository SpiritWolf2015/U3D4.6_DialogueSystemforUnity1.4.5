using UnityEngine;
using System.Collections;

public class PlayAnimateBehaviour : MonoBehaviour {
 
    public Animation m_Animation; 
    public string m_animateWalk = "walk";
    public string m_animateIdle = "idle";
    public string m_animateTalk = "talk";
    public string m_animateTalkSet = "talk_set";

    public void playAnimateWalk ( ) {
        if (!string.IsNullOrEmpty(m_animateWalk)) {
            m_Animation.CrossFade(m_animateWalk);
        } else {
            Debug.LogError(string.Format("{0}的{1} is null or empty !!", this.gameObject.name, this.m_animateWalk));
        }
    }

    public void playAnimateIdle ( ) {
        if (!string.IsNullOrEmpty(m_animateIdle)) {
            m_Animation.CrossFade(m_animateIdle);
        } else {
            Debug.LogError(string.Format("{0}的{1} is null or empty !!", this.gameObject.name, this.m_animateIdle));
        }
    }

    public void playAnimateTalk ( ) {
        if (!string.IsNullOrEmpty(m_animateTalk)) {
            m_Animation.CrossFade(m_animateTalk);
        } else {
            Debug.LogError(string.Format("{0}的{1} is null or empty !!", this.gameObject.name, this.m_animateTalk));
        }
    }

    public void playAnimateTalkSet ( ) {
        if (!string.IsNullOrEmpty(m_animateTalkSet)) {
            m_Animation.CrossFade(m_animateTalkSet);
        } else {
            Debug.LogError(string.Format("{0}的{1} is null or empty !!", this.gameObject.name, this.m_animateTalkSet));
        }
    }

}
