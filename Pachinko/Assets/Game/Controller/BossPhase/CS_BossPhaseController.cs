//---------------------------------
//ボスフェーズ
//担当者：野崎
//---------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using System.Diagnostics;

public class CS_BossPheseController : MonoBehaviour
{
    [SerializeField, Header("ボスのテーブルリスト")]
    private List<CSO_BossPhaseTable> mBossTables;

    private CSO_BossPhaseTable mNowBossTable;

    private CS_BossUnique mBossUnique;

    private CS_BossPhaseData mBossData;

    private CS_Controller mController;

    private CSO_PlayerStatus mPlayerStatus;
    private CSO_BossStatus mBossStatus;

    private int mBossNumber = 0;
    public int BossNumber
    {
        set { mBossNumber = value; }
        get { return mBossNumber; }
    }

    private int mNextMissionNum = -1;
    private int mGameCount = 10;
    public int GameCount
    {
        set { mGameCount = value; }
        get { return mGameCount; }
    }

    private int mBackupNumber = 0;

    private Coroutine mCoroutine = null;

    //-----------------------イベントハンドラ-----------------------
    public delegate void Performance(int _performance);//引数：項目番号-1

    //登録時に使用
    public static event Performance OnPlayPerformance;
    //-------------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        mController = GameObject.Find("BigController").GetComponent<CS_Controller>();//司令塔大を取得
        mBossData = GameObject.Find("BigController").GetComponent<CS_BossPhaseData>();
        mBossData.ResetData();//ボスフェーズデータの各フラグをリセットする
        mBossStatus = mBossData.BossStatus;
        mPlayerStatus = GameObject.Find("BigController").GetComponent<CS_MissionPhaseData>().PlayerStatus;
        //ボス番号に応じた情報を設定
        SetBossInfomation(); 
    }

    private void SetBossInfomation()
    {
        switch(mBossNumber)
        {
            case 0:
                mBossUnique = new CS_Boss1_Unique();
                break;
            //case 1:
            //    mBossUnique = new CS_Boss2_Unique();
            //    break;
            //case 2:
            //    mBossUnique = new CS_Boss3_Unique();
            //    break;
            //case 3:
            //    mBossUnique = new CS_Boss4_Unique();
            //    break;
        }

        mNowBossTable = mBossTables[mBossNumber];
    }

    // Update is called once per frame
    void Update()
    {
        //残りゲーム数が0以下で次のミッションのフラグも立っていない？
        if (mGameCount <= 0 && mNextMissionNum == -1)
        {
            RemoveAllHandlers();
            StartNextPhase();
            //Destroy(this.gameObject);
            return;
        }

        //変動できるかを取得
        bool variationStart = mController.CanVariationStart();
        if (!variationStart) { return; }//falseなら終了

        //保留玉が無いなら終了
        if (mController.GetStock() == 0) { return; }

        int randomNumber = CS_LotteryFunction.LotNormalInt(mNowBossTable.infomation.Count);//0~情報数分の間で抽せん

        mGameCount--;//ゲームカウントをへらす

        string name = mNowBossTable.infomation[randomNumber].name;

        //無発展
        if (randomNumber <= 2)
        {
            NoDevelopment(randomNumber);
            return;
        }
        else { mController.VariationTimer = 4f; }

        mBossData.NoDevelpment = false;//無発展フラグをfalse

        //再抽選確認。当選すれば次のミッション決定
        mNextMissionNum = CheckReLottely(randomNumber);
        //次の演出番号が-1じゃないなら再抽選結果を入れる
        if (mNextMissionNum != -1) { randomNumber = mNextMissionNum; }

        //保留玉使用（変動開始）
        mController.UseStock(mNowBossTable.infomation[randomNumber].win_lost);

        //抽せん番号を保存する
        mBackupNumber = randomNumber;

        mCoroutine = StartCoroutine(AfterLottery(randomNumber));//抽せん後処理を走らせる
    }
    private void StartNextPhase()
    {
        //HPを元に戻す
        mPlayerStatus.hp = mPlayerStatus.backupStatus.hp;
        mBossStatus.infomations[mBossNumber].hp = mBossStatus.initialValues[mBossNumber].hp;
        Debug.Log("次のフェーズへ移行します");
        //mController.ChangePhase(CS_Controller.PACHINKO_PHESE);
        Destroy(this.gameObject);

    }

    //無発展処理
    private void NoDevelopment(int _perfNumber)
    {
        float[] valTime = new float[3] { 8f, 10f, 10f };
        mController.VariationTimer = valTime[_perfNumber];//変動時間設定
        mBossData.NoDevelpment = true;//無発展フラグをtrue
        mNextMissionNum = -1;
        mBackupNumber = _perfNumber;
        //保留玉使用（変動開始）
        mController.UseStock(WIN_LOST.LOST);
        mController.PerformanceFinish();//演出は行わないので終了フラグを立てる
        string name = mNowBossTable.infomation[_perfNumber].name;
        Debug.Log("演出番号" + name);
    }

    private int CheckReLottely(BossPhaseInfomation info)
    {
        
        return -1;

        {
            //REPLAY_B replay = info.replay;
            //float percentage = mPlayerStatus.charaStatus.preemptiveAttack;



            //switch (replay)
            //{
            //    case REPLAY_B.TRUE_P1:
            //        {
            //            int[] preemptive = new int[2] { 4, 10 };
            //            float playerHp = mPlayerStatus.hp;
            //            playerHp -= mBossStatus.infomations[mBossNumber].attack * 0.333f;//ボスの攻撃力（弱）をhpから引く
            //            if (playerHp <= 0f)//体力が無くなれば復活抽せん
            //            {
            //                percentage = mPlayerStatus.charaStatus.revaival;
            //                if (!ReLot(percentage))//当選しなかったら敗北
            //                { 
            //                    playerHp = 0;

            //                }
            //                else 
            //                {

            //                    next = CS_LotteryFunction.LotNormalInt(preemptive.Length)-1; 
            //                }//当選すれば項目番号4のを返す
            //            }
            //            else//先制攻撃の値で再抽選
            //            {
            //                if (ReLot(percentage)) { next = CS_LotteryFunction.LotNormalInt(preemptive.Length) - 1; }//当選すれば項目番号4のを返す
            //            }

            //            //プレイヤー体力更新
            //            mPlayerStatus.hp = playerHp;
            //        }
            //        break;
            //    case REPLAY_B.TRUE_P2:
            //        {

            //            float playerHp = mPlayerStatus.hp;
            //            playerHp -= mBossStatus.infomations[mBossNumber].attack * 0.333f;//ボスの攻撃力（弱）をhpから引く
            //            if (playerHp <= 0f)//体力が無くなれば復活抽せん
            //            {
            //                percentage = mPlayerStatus.charaStatus.revaival;
            //                if (!ReLot(percentage))//当選しなかったら敗北
            //                {
            //                    playerHp = 0;

            //                }
            //                else 
            //                {
            //                    //当選すれば先制攻撃の項目番号を返す
            //                    next = 3; 
            //                }
            //            }
            //            //プレイヤー体力更新
            //            mPlayerStatus.hp = playerHp;
            //        }
            //        break;
            //    case REPLAY_B.TRUE_P3:

            //        break;
            //    case REPLAY_B.TRUE_P4:

            //        break;
            //    case REPLAY_B.TRUE_P5:

            //        break;
            //}
        }
    }

    private int CheckReLottely(int _val)
    {
        List<int> isRerotteryNums = new List<int> { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 17, 19, 20, 23, 24, 25, 26, 29, 30 };
        int next = -1;
        for (int i = 0; i < isRerotteryNums.Count; i++)
        {
            if (i == _val + 1)
            {
                next = mBossUnique.ReLottery(i);
                break;
            }
        }
        return next;

      
    }

    
    private IEnumerator AfterLottery(int _perfNum)
    {
        yield return new WaitForSeconds(0.5f);
        //イベントハンドラ実行
        PlayPerformance(_perfNum);

        //演出が終わるまで処理を進めない
        while (!mController.GetPatternVariationFinish()) { yield return null; }
        //Debug.Log("演出終了(仮)" + bigController.PerformanceSemiFinish);

        //GameObject JackPotPerf = null;
        ////当たり演出フラグがtrueならその演出生成
        //if (mController.JackPotPerf)
        //{
        //    JackPotPerf = Instantiate(mCutIn, mCutIn.transform.position, mCutIn.transform.rotation);
        //}

        ////演出が終わるまで処理を進めない
        //while (JackPotPerf) { yield return null; }

        mCoroutine = null;
    }

    private void PlayPerformance(int _num)
    {
        Instantiate(mNowBossTable.infomation[_num].performance, Vector3.zero, Quaternion.identity);
    }

    //登録されているイベントハンドラをすべて削除
    public static void RemoveAllHandlers()
    {
        // OnPlayPerformance に何かしらのハンドラが登録されている場合
        if (OnPlayPerformance != null)
        {
            // OnPlayPerformance に登録されている全てのハンドラを取得
            Delegate[] handlers = OnPlayPerformance.GetInvocationList();

            // すべてのハンドラを解除
            foreach (Delegate handler in handlers)
            {
                OnPlayPerformance -= (Performance)handler;
            }
        }
    }
}
