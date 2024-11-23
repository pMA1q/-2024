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
        mNextMissionNum = CheckReLottely(mNowBossTable.infomation[randomNumber]);
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
