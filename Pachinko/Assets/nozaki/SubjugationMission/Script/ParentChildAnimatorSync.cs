using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 親アニメーション進行中、子アニメーションを同期させる
public class ParentChildAnimatorSync : MonoBehaviour
{
    public Animator parentAnimator; // 親オブジェクトのAnimator
    public Animator childAnimator;  // 子オブジェクトのAnimator

    // 親アニメーションイベントで呼び出される
    public void TriggerChildAnimation()
    {
        if (childAnimator != null)
        {
            // 子アニメーションの特定の状態を再生
            childAnimator.SetTrigger("Start");
            Debug.Log("Child animation triggered");
        }
    }
}