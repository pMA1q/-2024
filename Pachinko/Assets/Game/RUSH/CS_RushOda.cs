using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_RushOda : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeAnimation());
    }

    private IEnumerator ChangeAnimation()
    {
        yield return new WaitForSeconds(3.0f);
        GetComponent<CS_SetCharaAnimState>().SetState(CHARACTER_ANIMATION_STATE.ATTACK);
    }
}
