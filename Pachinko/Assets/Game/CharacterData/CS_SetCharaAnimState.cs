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

        // 遷移するStateを指定
        SetState(mState);  // 例: JumpStateへ遷移
    }

    void SetState(int stateID)
    {
        animator.SetInteger("StateID", stateID);
    }
}
