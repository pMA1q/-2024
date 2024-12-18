using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_LastAttackEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject winningPlayer;

    [SerializeField]
    private GameObject winningSE;

    CS_HpGuage mHpGuage;
    // Start is called before the first frame update
    void Start()
    {
        mHpGuage = GameObject.Find("HpGuage").GetComponent<CS_HpGuage>();
        StartCoroutine(Finish());
        Instantiate(winningSE, Vector3.zero, Quaternion.identity);
    }

    private IEnumerator Finish()
    {
        //ìGÇÃHPÉQÅ[ÉWÇå∏ÇÁÇ∑
        mHpGuage.BossHpDown();
        yield return new WaitForSeconds(2f);

        winningPlayer.SetActive(true);
        Destroy(this.gameObject);
    }
}
