using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_HpGuage : MonoBehaviour
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

    public string pefName;

    bool mGuageDownUpdateFinish = true;
    bool mGuageRevUpdateFinish = true;

    public bool HpDownUpdateFinish { get { return mGuageDownUpdateFinish; } }
    public bool HpRevaivalUpdateFinish { get { return mGuageRevUpdateFinish; } }

    public bool HpUpdateFinish { get { return mGuageDownUpdateFinish && mGuageRevUpdateFinish; } }

    public Text testTx; 

    private void Start()
    {
        //Init();
        //Invoke("LateStart", 0.5f);
    }
    // Start is called before the first frame update
    public void Init()
    {
        mBossData = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_BossPhaseData>();
        if(mBossData.PlayerStatus==null) { Debug.Log("PlayerStatusはnullです"); }
        mMaxPlayerHp = mBossData.PlayerStatus.backupStatus.hp;
        Debug.Log("プレイヤーHP" + mMaxPlayerHp);
        mPlayerOneBlockHp = (int)mMaxPlayerHp / 10;
        mBossData.PlayerOneBlockHp = mPlayerOneBlockHp;
        mMaxBossHp = mBossData.BossStatus.infomations[mBossData.BossNumber].hp;
        mBossOneBlockHp = (int)mMaxBossHp / 10;
        mBossData.BossOneBlockHp = mBossOneBlockHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (!mBossData) { return; }
        testTx.text = "";
        //if(mBossData.IsPlayerRevaival)
        //{
        //    testTx.text = pefName + "復活あり";
        //}
    }

    //プレイヤーのHPゲージを減らす
    public void PlayerHpDown()
    {
        mBossData.IsBossAttack = true;
        mGuageDownUpdateFinish = false;
        StartCoroutine(PlayerSubGuage());
        //if (mPlayerGageCol == null) { mPlayerGageCol = StartCoroutine(PlayerSubGage()); }

    }

    //ボスのHPゲージを減らす
    public void BossHpDown()
    {

        mBossData.IsPlayerAttack = true;
        mGuageDownUpdateFinish = false;
        // if (mBossGageCol == null) { mBossGageCol = StartCoroutine(BossSubGage()); }
        mBossGageCol = StartCoroutine(BossSubGuage());
    }

    public void PlayerHpRevival()
    {
        
        mPlayerGageCol = StartCoroutine(PlayerRevaivalGuage());
        //if (mPlayerGageCol == null) { mPlayerGageCol = StartCoroutine(PlayerRevaivalGage()); }

    }

    public void PlayerHpHeal()
    {

        mPlayerGageCol = StartCoroutine(PlayerHealGage());
        //if (mPlayerGageCol == null) { mPlayerGageCol = StartCoroutine(PlayerRevaivalGage()); }

    }

    private IEnumerator PlayerSubGuage()
    {
        Debug.Log("プレイヤー体力減少");
        int count = 0; // アクティブを無効にする個数をカウント
        float attackPow = Mathf.Ceil(mBossData.BossOneAttackPow / (float)mPlayerOneBlockHp);
        
        int deleteNum = (int)attackPow;

        Debug.Log("消去数:" + deleteNum + "ボス攻撃力:" + mBossData.BossOneAttackPow + "プレイヤ1ブロックHP" + mPlayerOneBlockHp);
        for (int i = mPlayerHpBlocks.Length　-1; i >= 0; i--)
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

        Debug.Log("残りhp" + leftoverHp);

        if(!mBossData.IsPlayerRevaival && leftoverHp == 0) { mBossData.IsPlayerLose = true; }
        if (mBossData.IsPlayerRevaival)
        {
            Debug.Log("復活");
            mGuageRevUpdateFinish = false; 
        }

        mBossData.PlayerStatus.hp = leftoverHp;
        mPlayerGageCol = null;
        mGuageDownUpdateFinish = true;
       
        yield return null;
    }
    private IEnumerator PlayerRevaivalGuage()
    {
        Debug.Log("プレイヤー体力回復（復活）");
        int count = 0; // アクティブを無効にする個数をカウント
        int revCount = (int)mBossData.BackUpHP - mBossData.PlayerStatus.hp;
        Debug.Log("回復量" + revCount);
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
        mBossData.IsPlayerRevaival = false;
        mPlayerGageCol = null;
        mGuageRevUpdateFinish = true;
        yield return null;
    }

    private IEnumerator PlayerHealGage()
    {
        Debug.Log("プレイヤー体力回復");
        int count = 0; // アクティブを無効にする個数をカウント
        int revCount = (int)Mathf.Ceil((mMaxPlayerHp * 0.3f));
        Debug.Log("回復量" + revCount);
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
        mBossData.IsPlayerRevaival = false;
        mPlayerGageCol = null;
        mGuageRevUpdateFinish = true;
        yield return null;
    }

    private IEnumerator BossSubGuage()
    {
        Debug.Log("ボス体力減少");
        int count = 0; // アクティブを無効にする個数をカウント
        float attackPow = Mathf.Ceil(mBossData.PlayerOneAttackPow / mBossOneBlockHp);

        int deleteNum = (int)attackPow;
        Debug.Log("消去数:" + deleteNum + "プレイヤ攻撃力:" + mBossData.PlayerOneAttackPow + "ボス1ブロックHP" + mBossOneBlockHp);
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
        mGuageDownUpdateFinish = true;
        yield return null;
    }

   
}
