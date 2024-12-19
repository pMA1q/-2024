using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_LastAttackPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject mNeraeCanvas;
  
    [SerializeField]
    private GameObject mEffect;

    [SerializeField]
    private GameObject mRedEffect;

    [SerializeField]
    private GameObject mRainbowEffect;

    [SerializeField]
    private GameObject mBack;

    [SerializeField]
    private GameObject Enemy;

    // Start is called before the first frame update
    void Start()
    {
        mNeraeCanvas.transform.SetParent(null);
        mBack.SetActive(true);
        Enemy.SetActive(false);
        mRedEffect.SetActive(false);
    }

    private IEnumerator LastAttack()
    {
        mRedEffect.SetActive(false);
        mRainbowEffect.SetActive(true);

        Destroy(mRedEffect);

        CS_SetCharaAnimState animState = GetComponent<CS_SetCharaAnimState>();
        animState.SetState(1);
        mEffect.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        Enemy.SetActive(true);
        mBack.SetActive(true);
        Destroy(this.gameObject);
    }

    public void StartLastAttack()
    {
        StartCoroutine(LastAttack());
    }
}
