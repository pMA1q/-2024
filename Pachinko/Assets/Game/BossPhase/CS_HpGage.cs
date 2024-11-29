using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_HpGage : MonoBehaviour
{
    [SerializeField, Header("プレイヤーHP")]
    private Slider mPlayerHpGage;
    [SerializeField, Header("敵HP")]
    private Slider mBossHpGage;


    private CS_BossPhaseData mBossData;

    private float mNowHp;
    private float mGoalHp;
    private float mMaxPlayerHp;

    private Coroutine mPlayerGageCol = null;

    // Start is called before the first frame update
    void Start()
    {
        mBossData = GameObject.Find("BigController").GetComponent<CS_BossPhaseData>();
        mMaxPlayerHp = mBossData.PlayerStatus.backupStatus.hp;
        //mPlayerHpGage.value = mMaxPlayerHp/mMaxPlayerHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (mPlayerGageCol == null) { Debug.Log("コルーチンはnullです"); }
        else { Debug.Log("コルーチンはnullではない"); }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayerHpDown();
        }
    }

    //プレイヤーのHPゲージを減らす
    public void PlayerHpDown()
    {
        if(mPlayerGageCol == null) { mPlayerGageCol = StartCoroutine(PlayerSubGage()); }
        
    }
    private IEnumerator PlayerSubGage()
    {
        mNowHp = mBossData.PlayerStatus.hp;
        Debug.Log("現在のプレイヤーHP" + mNowHp);
        mGoalHp = (mNowHp - mBossData.PlayerOneAttackPow);
        Debug.Log("ゴール" + mGoalHp);
        float subSpeed = mMaxPlayerHp * 0.1f;

        while(mNowHp > mGoalHp)
        {
            mNowHp -= subSpeed * Time.deltaTime;
            //if(mNowHp <= mGoalHp) { mNowHp = mGoalHp; }
            mPlayerHpGage.value = mNowHp / mMaxPlayerHp;
            yield return null; // 1フレーム待機

        }

        //テスト
        mBossData.PlayerStatus.hp = mNowHp;

        mPlayerGageCol = null;

        Debug.Log("ゲージげ減少終了");
        yield return null;
    }
}
