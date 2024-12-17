//-----------------------------
//敵からから見たプレイヤー攻撃が当たった時の処理(ボスフェーズ項目番号:5)
//-----------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BP_4_Enemy : MonoBehaviour
{
    CS_HpGuage mHpGuage;
    // Start is called before the first frame update
    void Start()
    {
        mHpGuage = GameObject.Find("HpGuage").GetComponent<CS_HpGuage>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter(Collision collision)
    {

    }

    private void PlayerAttackHit()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.right * 1000); // 後ろに倒れる力を加える

        //敵のHPゲージを減らす
        mHpGuage.BossHpDown();

        //終了処理
        StartCoroutine(Finish());
    }


    private IEnumerator Finish()
    {
        //体力ゲージの処理が終了するまでループ
        while (!mHpGuage.HpUpdateFinish) { yield return null; }
        //演出終了を知らせる
        GameObject rootObject = transform.root.gameObject;
        if (rootObject.GetComponent<CS_PerformanceFinish>() == null)
        {
            //3秒後に演出を消す
            rootObject.AddComponent<CS_PerformanceFinish>().DestroyConfig(true, 3f);
        }
        Debug.Log("演出終了");
    }
}
