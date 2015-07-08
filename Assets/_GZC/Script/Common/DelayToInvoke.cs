using UnityEngine;
using System.Collections;
using System;

public static class DelayToInvoke {

    public static IEnumerator DelayToInvokeDo (Action action, float delaySeconds) {
        yield return new WaitForSeconds(delaySeconds);
        if (null != action) {
            action( ); //最好IF判断一下不为空
        } else {
            Debug.LogError(string.Format("DelayToInvokeBehaviour类DelayToInvokeDo函数形参action is null !!"));
        }        
    }
}
