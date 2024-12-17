using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SetCharaAnimState : MonoBehaviour
{
    [SerializeField]
    private int mState = 0;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        // �J�ڂ���State���w��
        SetState(mState);  // ��: JumpState�֑J��
    }

    void SetState(int stateID)
    {
        animator.SetInteger("StateID", stateID);
    }
}
