using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SetCharaAnimState : MonoBehaviour
{
    public static string StateID = "StateID";
    [SerializeField]
    private int mState = 0;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        // ‘JˆÚ‚·‚éState‚ðŽw’è
        SetState(mState);  // —á: JumpState‚Ö‘JˆÚ
    }

    public void SetState(int _stateID)
    {
        animator.SetInteger(StateID, _stateID);
    }
}
