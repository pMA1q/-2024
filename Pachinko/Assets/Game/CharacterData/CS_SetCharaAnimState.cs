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

        // ‘JˆÚ‚·‚éState‚ðŽw’è
        SetState(mState);  // —á: JumpState‚Ö‘JˆÚ
    }

    void SetState(int stateID)
    {
        animator.SetInteger("StateID", stateID);
    }
}
