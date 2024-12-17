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

        // 遷移するStateを指定
        SetState(mState);  // 例: JumpStateへ遷移
    }

    public void SetState(int _stateID)
    {
        animator.SetInteger(StateID, _stateID);
    }
}
