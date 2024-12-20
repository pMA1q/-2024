using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimationController_06 : MonoBehaviour
{
    public Animator parentAnimator; // �e��Animator
    public Animator childAnimator;  // �q��Animator

    // �e��SM_Cutin�I�����ɌĂяo�����
    public void TriggerChildAttack()
    {
        if (childAnimator != null)
        {
            childAnimator.SetTrigger("Attack"); // �q��Attack�A�j���[�V�������Đ�
            Debug.Log("Child 'Attack' animation triggered.");
        }
    }

    // �q��Attack�I�����ɌĂяo�����
    public void TriggerParentWinAndChildApper()
    {
        if (parentAnimator != null)
        {
            parentAnimator.SetTrigger("Win"); // �e��Win�A�j���[�V�������Đ�
            Debug.Log("Parent 'Win' animation triggered.");
        }

        if (childAnimator != null)
        {
            childAnimator.SetTrigger("Apper"); // �q��Apper�A�j���[�V�������Đ�
            Debug.Log("Child 'Apper' animation triggered.");
        }
    }
}
