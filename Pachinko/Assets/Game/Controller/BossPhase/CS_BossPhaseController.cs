//---------------------------------
//ボスフェーズ
//担当者：野崎
//---------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using System.Diagnostics;

public class CS_BossPhaseController : MonoBehaviour
{
    [SerializeField, Header("ボスのテーブルリスト")]
    private List<CSO_BossPhaseTable> mBossTables;
    [SerializeField, Header("無発展時のプレハブ")]
    private GameObject mNodevlopmentPrehab;
    [SerializeField, Header("体力ゲージ")]
    private GameObject mHpGuage;

    [SerializeField, Header("競り合い")]
    private GameObject mCompetition;

    [SerializeField, Header("ラストアタック")]
    private GameObject mLastAttack;

    [SerializeField, Header("デバッグ番号(項目番号-1の値)")]
    [Header("デバッグしないなら-1")]
    private int mDebugNumber = -1;

    private GameObject mNoDevObj;
    private GameObject mCompetitionObj;
    private GameObject mGuageObj;

    private CSO_BossPhaseTable mNowBossTable;

    private CS_BossUnique mBossUnique;

    private CS_BossPhaseData mBossData;
    private CS_CommonData mData;

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
    private int mGameCount = 3;
    //public int GameCount
    //{
    //    set { mGameCount = value; }
    //    get { return mGameCount; }
    //}

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
        mController = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_Controller>();//司令塔大を取得
        mData = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_CommonData>();//共通データ取得
        mBossData = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_BossPhaseData>();
        mBossData.ResetData();//ボスフェーズデータの各フラグをリセットする
        mBossStatus = mBossData.BossStatus;
        mPlayerStatus = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_MissionPhaseData>().PlayerStatus;

        //体力ゲージ生成
        mGuageObj = Instantiate(mHpGuage, Vector3.zero, Quaternion.identity);
        mGuageObj.name = mHpGuage.name;//Cloneが付かないように変更
        mGuageObj.GetComponent<CS_HpGuage>().Init();
        //ボス番号に応じた情報を設定
        SetBossInfomation();

        mNoDevObj = Instantiate(mNodevlopmentPrehab, Vector3.zero, Quaternion.identity);
        mNoDevObj.GetComponent<CS_SetPositionPerfPos>().Start();
        //mNoDevObj.GetComponent<CS_CameraWander>().Init();
    }

    private void SetBossInfomation()
    {
        switch(mBossNumber)
        {
            case 0:
                mBossUnique = this.gameObject.AddComponent<CS_Boss1_Unique>();
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
        if(mCoroutine != null) { return; }

        //ボス討伐
        if(mBossData.IsSubjugation)
        {
            if (!mController.CanVariationStart()) { return; }
            Subjugation();
            return;
        }

        //負け処理
        if(mBossData.IsPlayerLose)
        {
            PlayerLose();
            return;
        }

        //残りゲーム数が0以下で次のミッションのフラグも立っていない？
        if (mGameCount <= 0 && mNextMissionNum <= -1)
        {
            Debug.Log("カウント" + mGameCount);
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

        //5秒間待機してから下の処理に
        mCoroutine = StartCoroutine(Lottery());

       
    }

    //討伐成功処理
    private void Subjugation()
    {
        Debug.Log("目標殲滅しました");
        StartNextPhase();
    }

    //負け
    private void PlayerLose()
    {
        Debug.Log("負けました");
        StartNextPhase();
    }

    private IEnumerator Lottery()
    {
        int randomNumber = CS_LotteryFunction.LotNormalInt(mNowBossTable.infomation.Count);//0~情報数分の間で抽せん
        if (mDebugNumber >= 0) { randomNumber = mDebugNumber; }
        mGameCount--;//ゲームカウントをへらす

        string name = mNowBossTable.infomation[randomNumber].name;

        //無発展
        if (randomNumber <= 2)
        {
            NoDevelopment(randomNumber);
            yield break;
        }

        //プレイヤーが攻撃するか確認
        if (CheackPlayerAttack(randomNumber))
        {
            mNoDevObj.SetActive(false);
            mCompetitionObj = Instantiate(mCompetition, Vector3.zero, Quaternion.identity); //攻撃するなら競り合いのシーンを入れる
            Debug.Log("競り合いシーンを生成しました");
            CS_BP_CompetitionController competition = mCompetitionObj.GetComponent<CS_BP_CompetitionController>();

            float t = 0f;
            while(t <= 6f)
            {
                t += Time.deltaTime;
                if(competition.isActiveAndEnabled)  
                {
                    if (competition.NoHaveTikets()) { break; }
                    else { yield return null; }
                   
                }
            }

            randomNumber = ChangePerfNumber(randomNumber);//選んだチケットに応じて演出番号変更
        }
      
        mData.NoDevelpment = false;

        mController.VariationTimer = 4f;

        //この時点で次の番号が決まっているなら今回の変動番号決定
        if (mNextMissionNum > -1)
        {
            randomNumber = mNextMissionNum;
        }

        if (mDebugNumber >= 0) { randomNumber = mDebugNumber; }

        //再抽選確認。当選すれば次のミッション決定
        mNextMissionNum = CheckReLottely(randomNumber);
        //次の演出番号が-1じゃないなら再抽選結果を入れる
        if (mNextMissionNum > -1) { randomNumber = mNextMissionNum; }

       

        Debug.Log("再抽選番号:" + mNextMissionNum);
        Debug.Log("番号:" + randomNumber);
        name = mNowBossTable.infomation[randomNumber].name;
        //保留玉使用（変動開始）
        mController.UseStock(mNowBossTable.infomation[randomNumber].win_lost);

        //抽せん番号を保存する
        mBackupNumber = randomNumber;

        CS_HpGuage guage = GameObject.Find("HpGuage").GetComponent<CS_HpGuage>();
        guage.pefName = name;

        mCoroutine = StartCoroutine(AfterLottery(randomNumber));//抽せん後処理を走らせる
        yield return null;
    }

    bool CheackPlayerAttack(int _random)
    {
        int[] competitionNum = new int[] { 4, 5,6,10,12,14,17,19,20,24 };
        for (int i = 0; i < competitionNum.Length; i++)
        {
            if (competitionNum[i] == _random + 1)
            {
                return true;
                Debug.Log("競り合いシーンを生成します");
            }
        }
        return false;
    }

    bool CheckLastAttack()
    {
        float attackPow = Mathf.Ceil(mBossData.PlayerOneAttackPow / mBossData.BossOneBlockHp);
        float bossHp = mBossData.BossStatus.infomations[mBossData.BossNumber].hp - attackPow;
        return bossHp <= 0.0f;

    }

    private int ChangePerfNumber(int _randomNum)
    {
        _randomNum++;//一旦項目番号にする
        switch(mBossData.UseTiket)
        {
            case CS_BossPhaseData.USE_TIKET.SPECOAL:
                _randomNum = 25;
                break;
            case CS_BossPhaseData.USE_TIKET.PARTNER:
                _randomNum = 29;
                break;
            case CS_BossPhaseData.USE_TIKET.PREEMPTIVE_ATTACK:
                _randomNum = 4;
                break;
        }
      
        return _randomNum -1;
    }

    private void StartNextPhase()
    {
        //HPを元に戻す
        mPlayerStatus.hp = mPlayerStatus.backupStatus.hp;
        mBossStatus.infomations[mBossNumber].hp = mBossStatus.initialValues[mBossNumber].hp;
        Destroy(mNoDevObj);
        Destroy(mGuageObj);
        Debug.Log("次のフェーズへ移行します");
        mController.NumberRailResetTrans();
        mController.ChangePhase(CS_Controller.PACHINKO_PHESE.SET);
        mController.CreateController();
        Destroy(this.gameObject);

    }

    //無発展処理
    private void NoDevelopment(int _perfNumber)
    {
        float[] valTime = new float[3] { 8f, 10f, 12f };
        mController.VariationTimer = valTime[_perfNumber];//変動時間設定
        mData.NoDevelpment = true;//無発展フラグをtrue
        mNextMissionNum = -1;
        mBackupNumber = _perfNumber;
        //保留玉使用（変動開始）
        mController.UseStock(WIN_LOST.LOST);
        mController.PerformanceSemiFinish = true;
        mController.PerformanceFinish();//演出は行わないので終了フラグを立てる
        string name = mNowBossTable.infomation[_perfNumber].name;
        CS_HpGuage guage = GameObject.Find("HpGuage").GetComponent<CS_HpGuage>();
        guage.pefName = name;

        mData.NoDevelpment = true;

        Debug.Log("演出番号" + name);
    }

    
    private int CheckReLottely(int _val)
    {
        List<int> isRerotteryNums = new List<int> { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13,14, 17,18, 19, 20, 23, 24, 25, 26, 29, 30 };
        int next = -1;
        for (int i = 0; i < isRerotteryNums.Count; i++)
        {
            if (isRerotteryNums[i] == _val + 1)
            {
                next = mBossUnique.ReLottery(i);
                break;
            }
        }
        return next;
    }

    
    private IEnumerator AfterLottery(int _perfNum)
    {
        yield return new WaitForSeconds(2f);
        if(mCompetitionObj != null) 
        {
            Destroy(mCompetitionObj); //競り合いのオブジェクトを消す
            GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_CommonData>().ButtonsInteractable();//ボタンを有効にする
        }
        mNoDevObj.SetActive(false);
        //イベントハンドラ実行
        PlayPerformance(_perfNum);
        //mController.PerformanceSemiFinish = true;
        //演出が終わるまで処理を進めない
        while (!mController.GetPatternVariationFinish()) { yield return null; }
        //Debug.Log("演出終了(仮)" + bigController.PerformanceSemiFinish);

        mNextMissionNum = mBossUnique.DesisionFlag(_perfNum - 3);

        CS_HpGuage guage = GameObject.Find("HpGuage").GetComponent<CS_HpGuage>();

        while (!guage.HpUpdateFinish) { yield return null; }

        Debug.Log("HP更新終了");


        //演出終了を知らせる
        GameObject rootObject = transform.root.gameObject;
        if (rootObject.GetComponent<CS_PerformanceFinish>() == null)
        {
            //3秒後に演出を消す
            rootObject.AddComponent<CS_PerformanceFinish>().DestroyConfig(false, 0f);
        }
        //mController.PerformanceFinish();
        while (!mController.GetPerformanceFinish()) { yield return null; }

        mNoDevObj.SetActive(true);

        mCoroutine = null;
    }

    private void PlayPerformance(int _num)
    {
        if(mNowBossTable.infomation[_num].performance != null)
        {
            if (CheckLastAttack())
            {
                mCompetitionObj.SetActive(false);
                mNoDevObj.SetActive(false);
                Instantiate(mLastAttack, Vector3.zero, mLastAttack.transform.rotation);
                return;
            }
            GameObject obj = Instantiate(mNowBossTable.infomation[_num].performance, Vector3.zero, mNowBossTable.infomation[_num].performance.transform.rotation);
            obj.name = mNowBossTable.infomation[_num].performance.name;//Cloneが付かないようにする
            obj.GetComponent<CS_SetPositionPerfPos>().Start();
        }
        else
        {
            mController.PerformanceSemiFinish = true;
            mController.PerformanceFinish();
        }
        
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
