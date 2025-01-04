using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BP_14_SccessEnemy : MonoBehaviour
{
    [SerializeField, Header("倒れるまでの時間")]
    private float mStartDown = 0.3f;
    CS_HpGuage mHpGuage;
    // Start is called before the first frame update
    void Start()
    {
        mHpGuage = GameObject.Find("HpGuage").GetComponent<CS_HpGuage>();
        StartCoroutine(PlayerAttackHit());
    }

   

    private IEnumerator PlayerAttackHit()
    {
        yield return new WaitForSeconds(mStartDown);

        GetComponent<CS_SetCharaAnimState>().SetState(CHARACTER_ANIMATION_STATE.DAMAGE);

        //敵のHPゲージを減らす
        mHpGuage.BossHpDown();

        //終了処理
        StartCoroutine(Finish());
    }

    private IEnumerator Finish()
    {
        //体力ゲージの処理が終了するまでループ
        while (!mHpGuage.HpUpdateFinish) { yield return null; }

        yield return new WaitForSeconds(5f);

       
        CS_BossPhaseData data = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_BossPhaseData>();
        //演出終了を知らせる
        GameObject rootObject = transform.root.gameObject;
        if (data.GetChoiceSuccess())
        {
            GameObject BossPerf14 = GameObject.Find("14_PlayerAttackChoice");
            if (!BossPerf14) { Debug.LogError("演出14がない(名前確認)"); }
            BossPerf14.GetComponent<CS_BP_AttackChoice>().Init();
            Destroy(rootObject);
        }
        else
        {
           
            if (rootObject.GetComponent<CS_PerformanceFinish>() == null)
            {
                //3秒後に演出を消す
                rootObject.AddComponent<CS_PerformanceFinish>().DestroyConfig(true, 3f);
            }
        }
       
    }
}
