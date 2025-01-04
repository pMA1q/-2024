using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CHARACTER_ANIMATION_STATE
{
    IDLE = 0,
    ATTACK,
    DOWN,
    DAMAGE,
    WINNING = 7
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

        // �J�ڂ���State���w��
        SetState(mCharacterState);
    }

    //�X�e�[�g�ԍ���ύX
    public void SetState(CHARACTER_ANIMATION_STATE _stateID)
    {
        //if (_stateID == previousStateID) return; // �����l�Ȃ�Đݒ肵�Ȃ�

        animator.SetInteger(StateID, (int)_stateID);
        Debug.Log(this.gameObject.name + "��" + "�A�j���[�V�����X�e�[�g���ς��܂���");
    }
}
