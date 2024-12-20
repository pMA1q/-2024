using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �e�A�j���[�V�����i�s���A�q�A�j���[�V�����𓯊�������
public class ParentChildAnimatorSync : MonoBehaviour
{
    public Animator parentAnimator; // �e�I�u�W�F�N�g��Animator
    public Animator childAnimator;  // �q�I�u�W�F�N�g��Animator

    // �e�A�j���[�V�����C�x���g�ŌĂяo�����
    public void TriggerChildAnimation()
    {
        if (childAnimator != null)
        {
            // �q�A�j���[�V�����̓���̏�Ԃ��Đ�
            childAnimator.SetTrigger("Start");
            Debug.Log("Child animation triggered");
        }
    }
}