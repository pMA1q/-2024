using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_HpGage : MonoBehaviour
{
    [SerializeField, Header("プレイヤーHP")]
    private Image[] mPlayerHpBlocks = new Image[10];
    [SerializeField, Header("敵HP")]
    private Image[] mBossHpBlocks = new Image[10];


    private CS_BossPhaseData mBossData;

    private float mNowHp;
    private float mGoalHp;
    private float mMaxPlayerHp;
    private float mMaxBossHp;

    private Coroutine mPlayerGageCol = null;
    private Coroutine mBossGageCol = null;

    private int mPlayerOneBlockHp = 1;
    private int mBossOneBlockHp = 1;

    int test = 1;

    bool mGuageUpdateFinish = false;
    public bool HpUpdateFinish { get { return mGuageUpdateFinish; } }

    private void Start()
    {
        Invoke("LateStart", 0.5f);
    }
    // Start is called before the first frame update
    void LateStart()
    {
        mBossData = GameObject.Find("BigController").GetComponent<CS_BossPhaseData>();
        if(mBossData.PlayerStatus==null) { Debug.Log("PlayerStatusはnullです"); }
        mMaxPlayerHp = mBossData.PlayerStatus.backupStatus.hp;
        mPlayerOneBlockHp = (int)mMaxPlayerHp / 10;
        mBossData.PlayerOneBlockHp = mPlayerOneBlockHp;
        mMaxBossHp = mBossData.BossStatus.infomations[mBossData.BossNumber].hp;
        mBossOneBlockHp = (int)mMaxBossHp / 10;
        mBossData.BossOneBlockHp = mBossOneBlockHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (mPlayerGageCol == null) {  }
        else { Debug.Log("コルーチンはnullではない"); }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayerHpDown();
        }
    }

    //プレイヤーのHPゲージを減らす
    public void PlayerHpDown()
    {
        mGuageUpdateFinish = false;
        if (mPlayerGageCol == null) { mPlayerGageCol = StartCoroutine(PlayerSubGage()); }
        
    }

    //ボスのHPゲージを減らす
    public void BossHpDown()
    {
        mGuageUpdateFinish = false;
        if (mBossGageCol == null) { mBossGageCol = StartCoroutine(PlayerSubGage()); }

    }

    public void PlayerHpRevival()
    {
        mGuageUpdateFinish = false;
        if (mPlayerGageCol == null) { mPlayerGageCol = StartCoroutine(PlayerRevaivalGage()); }

    }

   
    private IEnumerator PlayerSubGage()
    {

        int count = 0; // アクティブを無効にする個数をカウント
        float attackPow = Mathf.Ceil(mBossData.BossOneAttackPow / mPlayerOneBlockHp);
        
        int deleteNum = (int)attackPow;
        for (int i = 0; i < mPlayerHpBlocks.Length; i++)
        {
            // アクティブかつまだ num 個を無効化していない場合
            if (mPlayerHpBlocks[i].gameObject.activeSelf && count < deleteNum)
            {
                mPlayerHpBlocks[i].gameObject.SetActive(false);
                count++; // 無効化カウントを増やす
                yield return new WaitForSeconds(0.05f);
            }
            
        }
        int leftoverHp = 0;//残りHP数
        for (int i = 0; i < mPlayerHpBlocks.Length; i++)
        {
            if (mPlayerHpBlocks[i].gameObject.activeSelf)
            {
                leftoverHp++;
            }
        }
        
        if(!mBossData.IsPlayerRevaival && leftoverHp == 0) { mBossData.IsPlayerLose = true; }

        mBossData.PlayerStatus.hp = leftoverHp;
        mPlayerGageCol = null;
        mGuageUpdateFinish = true;
        yield return null;
    }
    private IEnumerator PlayerRevaivalGage()
    {

        int count = 0; // アクティブを無効にする個数をカウント
        int revCount = (int)mBossData.BackUpHP - mBossData.PlayerStatus.hp;
        for (int i = 0; i < mPlayerHpBlocks.Length; i++)
        {
            // アクティブかつまだ num 個を無効化していない場合
            if (!mPlayerHpBlocks[i].gameObject.activeSelf && count < revCount)
            {
                mPlayerHpBlocks[i].gameObject.SetActive(true);
                count++; // 無効化カウントを増やす
                yield return new WaitForSeconds(0.05f);
            }

        }
        int leftoverHp = 0;//残りHP数
        for (int i = 0; i < mPlayerHpBlocks.Length; i++)
        {
            if (mPlayerHpBlocks[i].gameObject.activeSelf)
            {
                leftoverHp++;
            }
        }

        mBossData.PlayerStatus.hp = leftoverHp;
        mPlayerGageCol = null;
        mGuageUpdateFinish = true;
        yield return null;
    }


    private IEnumerator BossSubGage()
    {

        int count = 0; // アクティブを無効にする個数をカウント
        float attackPow = Mathf.Ceil(mBossData.PlayerOneAttackPow / mBossOneBlockHp);

        int deleteNum = (int)attackPow;
        for (int i = 0; i < mBossHpBlocks.Length; i++)
        {
            // アクティブかつまだ num 個を無効化していない場合
            if (mBossHpBlocks[i].gameObject.activeSelf && count < deleteNum)
            {
                mBossHpBlocks[i].gameObject.SetActive(false);
                count++; // 無効化カウントを増やす
                yield return new WaitForSeconds(0.05f);
            }

        }
        int leftoverHp = 0;//残りHP数
        for (int i = 0; i < mBossHpBlocks.Length; i++)
        {
            if (mBossHpBlocks[i].gameObject.activeSelf)
            {
                leftoverHp++;
            }
        }

        if(leftoverHp == 0) { mBossData.IsSubjugation = true; }

        mBossData.BossStatus.infomations[mBossData.BossNumber].hp = leftoverHp;
        mBossGageCol = null;
        mGuageUpdateFinish = true;
        yield return null;
    }

   
}
