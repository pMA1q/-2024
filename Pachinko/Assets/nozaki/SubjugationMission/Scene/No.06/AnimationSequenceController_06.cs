using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimationController_06 : MonoBehaviour
{
    public Animator parentAnimator; // 親のAnimator
    public Animator childAnimator;  // 子のAnimator

    // 親のSM_Cutin終了時に呼び出される
    public void TriggerChildAttack()
    {
        if (childAnimator != null)
        {
            childAnimator.SetTrigger("Attack"); // 子のAttackアニメーションを再生
            Debug.Log("Child 'Attack' animation triggered.");
        }
    }

    // 子のAttack終了時に呼び出される
    public void TriggerParentWinAndChildApper()
    {
        if (parentAnimator != null)
        {
            parentAnimator.SetTrigger("Win"); // 親のWinアニメーションを再生
            Debug.Log("Parent 'Win' animation triggered.");
        }

        if (childAnimator != null)
        {
            childAnimator.SetTrigger("Apper"); // 子のApperアニメーションを再生
            Debug.Log("Child 'Apper' animation triggered.");
        }
    }
}
