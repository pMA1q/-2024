using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SetCharaAnimState : MonoBehaviour
{
    public static string StateID = "StateID";
    [SerializeField]
    private int mState = 0;

    private Animator animator;
    private int previousStateID = -1;
    void Start()
    {
        animator = GetComponent<Animator>();

        // ‘JˆÚ‚·‚éState‚ğw’è
        SetState(mState);  // —á: JumpState‚Ö‘JˆÚ
    }

    public void SetState(int _stateID)
    {
        if (_stateID == previousStateID) return; // “¯‚¶’l‚È‚çÄİ’è‚µ‚È‚¢
        previousStateID = _stateID;
        animator.SetInteger(StateID, _stateID);
    }
}
