using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CHARACTER_ANIMATION_STATE
{
    IDLE = 0,
    ATTACK,
    DOWN,
    DAMAGE,
    WALK,
    WINNING = 7,
    BONUS = 777,
}

public class CS_SetCharaAnimState : MonoBehaviour
{
    public readonly static string StateID = "StateID";
    [SerializeField]
    private CHARACTER_ANIMATION_STATE mCharacterState = CHARACTER_ANIMATION_STATE.IDLE;

    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();

        // 遷移するStateを指定
        SetState(mCharacterState);
    }

    //ステート番号を変更
    public void SetState(CHARACTER_ANIMATION_STATE _stateID)
    {
        //if (_stateID == previousStateID) return; // 同じ値なら再設定しない

        animator.SetInteger(StateID, (int)_stateID);
    }
}
