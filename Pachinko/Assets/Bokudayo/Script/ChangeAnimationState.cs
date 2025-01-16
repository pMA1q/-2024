using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAnimationState : MonoBehaviour
{
    CS_SetCharaAnimState animState;

    [SerializeField]
    CHARACTER_ANIMATION_STATE next;
    [SerializeField]
    float nextTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        animState = GetComponent<CS_SetCharaAnimState>();
        StartCoroutine(ChangeAnimation());
    }

    private IEnumerator  ChangeAnimation()
    {
        yield return new WaitForSeconds(nextTime);
        animState.SetState(next);
    }
}
