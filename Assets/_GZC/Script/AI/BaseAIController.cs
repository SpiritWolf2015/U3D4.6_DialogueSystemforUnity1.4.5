using UnityEngine;
using System.Collections;
using AIStates;

[AddComponentMenu("Base/AI Controller")]
public class BaseAIController : ExtendedCustomMonoBehavior {

    public AIState currentAIState;

    public float moveSpeed = 30f;
    public float moveTime = 3F;

    public bool AIControlled;

    public PlayAnimateBehaviour m_PlayAnimateBehaviour;

    public void Start ( ) {
        Init( );
    }

    public virtual void Init ( ) {
        // cache ref to gameObject
        myGO = gameObject;
        // cache ref to transform
        myTransform = transform;    
        // cache a ref to our rigidbody
        myBody = myTransform.GetComponent<Rigidbody>();

        SetAIState2Idle( );

        // init done!
        didInit = true;
    }

    public void SetAIControl ( bool state ) {
        AIControlled = state;
    }

    // set up AI parameters --------------------
    
    public void SetMoveSpeed ( float aNum ) {
        moveSpeed = aNum;
    }
    public void SetMoveTime (float aNum) {
        moveTime = aNum;
    }


    // ----------------------------------------- 

    void SetAIState2Walk ( ) {
        SetAIState(AIState.moving);
    }

    void SetAIState2Idle ( ) {
        SetAIState(AIState.idle);
    }

    void SetAIState2Talk ( ) {
        SetAIState(AIState.talk);
    }

    void SetAIState2TalkSet ( ) {
        SetAIState(AIState.talk_set);
    }

    public virtual void SetAIState ( AIState newState ) {
        // update AI state
        currentAIState = newState;
    }

    public virtual void Update ( ) {
        // 初始化
        if ( !didInit )
            Init( );

        // AI是否可控。
        if ( !AIControlled )
            return;

        // do AI updates
        UpdateAI( );
    }

    public virtual void UpdateAI ( ) {
        switch ( currentAIState ) {
            case AIState.moving:
                Debug.Log("AIState.moving");
                // 播放走路动画
                m_PlayAnimateBehaviour.playAnimateWalk( );
                break;
            case AIState.idle:
                Debug.Log("AIState.idle");
                // 播放待机动画
                m_PlayAnimateBehaviour.playAnimateIdle( );
                break;
            case AIState.talk:
                m_PlayAnimateBehaviour.playAnimateTalk( );
                Debug.Log("AIState.talk");
                // 播放讲话动画
                break;
            case AIState.talk_set:
                m_PlayAnimateBehaviour.playAnimateTalkSet( );
                Debug.Log("AIState.talk_set");
                // 播放讲话动画
                break;     
            default:
                Debug.Log("AIState.default");
                // idle (do nothing)
                break;
        }
    }

}
