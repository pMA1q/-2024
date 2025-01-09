using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_AnimChangeTimer : MonoBehaviour
{
    [SerializeField]
    private CHARACTER_ANIMATION_STATE mNextState = CHARACTER_ANIMATION_STATE.IDLE;
    [SerializeField]
    private float mTimer = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeAnimationState());
    }

   
    private IEnumerator ChangeAnimationState()
    {
        yield return new WaitForSeconds(mTimer);
        CS_SetCharaAnimState state = GetComponent<CS_SetCharaAnimState>();
        state.SetState(mNextState);
    }
}
