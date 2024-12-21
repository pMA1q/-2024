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

        // 遷移するStateを指定
        SetState(mState);  // 例: JumpStateへ遷移
    }

    public void SetState(int _stateID)
    {
        //if (_stateID == previousStateID) return; // 同じ値なら再設定しない
        previousStateID = _stateID;
        animator.SetInteger(StateID, _stateID);
        Debug.Log(this.gameObject.name + "の" + "アニメーションステートが変わりました");
    }
}
