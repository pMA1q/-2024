using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_LastAttackPlayer : MonoBehaviour
{
    private CS_CommonData mData;//ã§í ÉfÅ[É^

    [SerializeField]
    private GameObject mNeraeCanvas;
    [SerializeField]
    private GameObject mNerae;
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
        mData = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_CommonData>();
        mData.V_SpotOpen();
        mNeraeCanvas.transform.SetParent(null);
        mBack.SetActive(false);
        Enemy.SetActive(false);
        StartCoroutine(LastAttack());
        
    }

    private IEnumerator LastAttack()
    {
        while (!mData.RightAttaker.IsInV_Spot) { yield return null; }

        mRainbowEffect.SetActive(true);

        Destroy(mNerae);
        Destroy(mRedEffect);

        CS_SetCharaAnimState animState = GetComponent<CS_SetCharaAnimState>();
        animState.SetState(1);
        mEffect.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        Enemy.SetActive(true);
        mBack.SetActive(true);
        Destroy(this.gameObject);
    }

    
}
