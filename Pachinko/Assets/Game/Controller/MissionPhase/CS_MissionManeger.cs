//---------------------------------
//ミッションマネージャー
//担当者：野崎
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CS_MissionManeger : MonoBehaviour
{
    public CSO_MissionPhaseTable missionPhaseTable;  // Inspector で設定可能

    [SerializeField,Header("プレイヤーステータス")]
    private CSO_PlayerStatus playerStatus;

    [SerializeField, Header("カットイン")]
    private GameObject mCutIn;
    private GameObject mCutInPlay = null;

    private bool isReplayTriggered = false;  // 再抽選フラグ

    [SerializeField]
    [Header("0:収集ミッション")]
    [Header("1:討伐ミッション")]
    [Header("2:鍛錬ミッション")]
    private GameObject[] missionPrefab;

    private CS_Controller bigController;//司令塔(大)
    private CS_MissionPhaseData missionData;//司令塔(大)

    private int mGameCount = 20;//入賞数

    //ゲーム数の設定、取得
    public int GameCount
    {
        set { mGameCount = value; }
        get { return mGameCount; }
    }

    //敵討伐数
    private int mSubjugationNum = 0;
    public int SunjugationEnemy { get { return mSubjugationNum; } }



    //ユニークな演出
    CS_SM_Unique mSM_Unique;
    private int[] mUniquePF;

    int mNextMissionNum = -1;

    private int mBackupNumber = 0;

    private Coroutine mCoroutine = null;

    private int mHightEnemyCount = 0;

    //-----------------------イベントハンドラ-----------------------
    public delegate void Performance(int _performance);//引数：項目番号-1

    //登録時に使用
    public static event Performance OnPlayPerformance;
    //-------------------------------------------------------------

    void Start()
    {
        missionData = GameObject.Find("BigController").GetComponent<CS_MissionPhaseData>();
        missionData.ResetMissionData();//ミッションデータの各フラグをレセットする
        // プレイヤーステータスをデータから取得
        playerStatus = missionData.PlayerStatus;

        bigController = GameObject.Find("BigController").GetComponent<CS_Controller>();//司令塔大を取得

        //ミッションの種類を取得
        int missionType = (int)bigController.GetComponent<CS_MissionPhaseData>().MissionType;
        //ミッション選択オブジェクトを生成
        GameObject instance = Instantiate(missionPrefab[missionType], missionPrefab[missionType].transform.position, missionPrefab[missionType].transform.rotation);
        instance.name = missionPrefab[missionType].name; // (Clone)が付かないように名前をオリジナルの名前に戻す

        CS_MissionPhaseData.MISSION_TYPE mtype = missionData.MissionType;
        //if(mtype == CS_MissionPhaseData.MISSION_TYPE.SUBJUGATION) { mSM_Unique = new CS_SM_Unique(); }
        mSM_Unique = this.gameObject.AddComponent<CS_SM_Unique>();
        mUniquePF = new int[] { 11, 12, 18, 19, 22 };//ユニークな演出の項目番号配列

        //テスト
        OnPlayPerformance += PlayPerformance;
    }

    private void Update()
    {
        if(mCoroutine != null) { return; }
        
        UniquePerformance();//ユニークなミッションならば報酬または次のミッション番号を決める

        //入賞数が20？
        if (mGameCount <= 0 && mNextMissionNum == -1)
        {
            RemoveAllHandlers();
            StartBossPhase();
            //Destroy(this.gameObject);
            return;
        }

        //変動できるかを取得
        bool variationStart = bigController.CanVariationStart();
        if (!variationStart) { return; }//falseなら終了

        //Debug.Log("次のミッションフラグ" + mNextMissionNum);
       
        //イベントハンドラはnullなら終了
        if (OnPlayPerformance == null) { return; }

        //保留玉が無いなら終了
        if (bigController.GetStock() == 0) { return; }

       
        //演出抽選
        //int randomNumber = CS_LotteryFunction.LotNormalInt(missionPhaseTable.infomation.Count - 1);
        int randomNumber = CS_LotteryFunction.LotNormalInt(17);//一旦項目17までに限定する
       
        mGameCount--;//入賞数減算

        //Debug.Log("残りゲーム数" + mGameCount);

        string name = missionPhaseTable.infomation[randomNumber].name;
       
        //無発展
        if (randomNumber <= 2)
        {
            NoDevelopment(randomNumber);
            return;
        }
        else { bigController.VariationTimer = 4f; }

        missionData.NoDevelpment = false;//無発展フラグをfalse

        //再抽選確認。当選すれば次のミッション決定
        mNextMissionNum = CheckReLottely(missionPhaseTable.infomation[randomNumber]);
        //次の演出番号が-1じゃないなら再抽選結果を入れる
        if (mNextMissionNum != -1) { randomNumber = mNextMissionNum; }

        //保留玉使用（変動開始）
        bigController.UseStock(missionPhaseTable.infomation[randomNumber].win_lost);

        //抽せん番号を保存する
        mBackupNumber = randomNumber;

        name = missionPhaseTable.infomation[randomNumber].name;
        Debug.Log("演出内容" + name);
        mCoroutine = StartCoroutine(AfterLottery(randomNumber));//抽せん後処理を走らせる
    }

    //無発展処理
    private void NoDevelopment(int _perfNumber)
    {
        float[] valTime = new float[3] { 8f, 10f, 10f };
        bigController.VariationTimer = valTime[_perfNumber];//変動時間設定
        missionData.NoDevelpment = true;//無発展フラグをtrue
        mNextMissionNum = -1;
        mBackupNumber = _perfNumber;
        //保留玉使用（変動開始）
        bigController.UseStock(WIN_LOST.LOST);
        bigController.PerformanceFinish();//演出は行わないので終了フラグを立てる
        string name = missionPhaseTable.infomation[_perfNumber].name;
        Debug.Log("演出番号" + name);
    }
   

    //ユニークな演出時の処理
    private void UniquePerformance()
    {
        int next = -1;
        for (int i = 0; i < mUniquePF.Length; i++)
        {
            if (mBackupNumber == mUniquePF[i] - 1)
            {
                next = mSM_Unique.DesisionMission(i);
                mNextMissionNum = next;
            }
        }

        if(missionData.HighProbabEnemyMode)
        {
            missionData.HighProbabEnemyMode = false;
            mHightEnemyCount = 3;
        }

        //敵を討伐したなら
        if(missionData.SubjugationOneMission >= 1)
        {
            mSubjugationNum = missionData.SubjugationOneMission;//討伐数に加算
            missionData.SubjugationSum = mSubjugationNum;//ミッションデータの討伐数合計を更新
            int perfNumber = mBackupNumber + 1;//演出項目番号
            //次回ミッション遂行フラグ確定？の演出かを調べる
            bool perfNumber18or19 = (perfNumber == 18 || perfNumber == 19);
            //次回ミッション遂行フラグ確定？の演出でないなら１ミッション討伐数を0に戻す
            if (!perfNumber18or19) {   missionData.SubjugationOneMission = 0; }
           
        }
    }
    
    //再抽選確認
    private int CheckReLottely(MissionPhaseInfomation mission)
    {
        //再抽選無しなら終了
        if(mission.replay == REPLAY.FALSE) { return -1; }

        int[] lot;
        if(missionData.PlayerBuff == CS_MissionPhaseData.PLAYER_BUFF.WEAK)
        {
            lot = new int[2] { 6, 9 };
            missionData.PlayerBuff = CS_MissionPhaseData.PLAYER_BUFF.NONE;
            return lot[CS_LotteryFunction.LotNormalInt(2)]-1;
        }
        if (missionData.PlayerBuff == CS_MissionPhaseData.PLAYER_BUFF.STRONG)
        {
            lot = new int[4] { 6, 9, 14, 16 };
            missionData.PlayerBuff = CS_MissionPhaseData.PLAYER_BUFF.NONE;
            return lot[CS_LotteryFunction.LotNormalInt(4)]-1;
        }

        //敵高確率遭遇ゲーム数が1以上なら敵一体遭遇演出にする
        if(mHightEnemyCount >= 1)
        {
            mHightEnemyCount--;
            lot = new int[6] { 4 ,5, 6, 7, 8, 9 };
            return lot[CS_LotteryFunction.LotNormalInt(6)] - 1;
        }

        Debug.Log("再抽選開始");
        //先制攻撃の確率に設定
        float percentage = playerStatus.charaStatus.preemptiveAttack;
       
        //P2なら復活値にする
        if(mission.replay == REPLAY.TRUE_P2){ percentage = playerStatus.charaStatus.revaival; }

        float randomValue = UnityEngine.Random.Range(0f, 100f);
        if (randomValue < percentage)//当たった
        {
            Debug.Log("再抽選当選");
            //ランダムステータスUP
            if(mission.replayNum <= 16) 
            {
                RundomStatusUP(mission.replayNum);
            }
            return mission.replayNum - 1;
        }
        return -1;
    }

    //収集ミッション時以外のステータスUP処理
    private void RundomStatusUP(int _val)
    {
        CharacterStatus cStatus = playerStatus.charaStatus;
        float[] status = new float[5] { cStatus.charColorUP, cStatus.preemptiveAttack, cStatus.attack, cStatus.revaival, cStatus.cutIn};
        List<float> choicePercent = new List<float> {cStatus.charColorUpPow.conicePercent, cStatus.preemptiveAttackUpPow.conicePercent,cStatus.attackUpPow.conicePercent,
                                                     cStatus.revivalUpPow.conicePercent,cStatus.cutInUpPow.conicePercent};
        List<float> smallpower = new List<float> {cStatus.charColorUpPow.smallUP, cStatus.preemptiveAttackUpPow.smallUP,cStatus.attackUpPow.smallUP,
                                                     cStatus.revivalUpPow.smallUP,cStatus.cutInUpPow.smallUP};
        List<float> midllepower = new List<float> {cStatus.charColorUpPow.smallUP, cStatus.preemptiveAttackUpPow.smallUP,cStatus.attackUpPow.smallUP,
                                                     cStatus.revivalUpPow.smallUP,cStatus.cutInUpPow.smallUP};
        List<float> maxpower = new List<float> {cStatus.charColorUpPow.max, cStatus.preemptiveAttackUpPow.max,cStatus.attackUpPow.max,
                                                     cStatus.revivalUpPow.max,cStatus.cutInUpPow.max};
        int random = CS_LotteryFunction.LotPerformance(choicePercent);
        
        if(_val == 6 || _val == 9) { status[random] += smallpower[random]; }//ランダムステータスUP小
        else  { status[random] += midllepower[random]; }//ランダムステータスUP中
        //最大値を超えないようにする
        if (status[random] > maxpower[random]) { status[random] = maxpower[random]; }
        cStatus.charColorUP = status[0];
        cStatus.preemptiveAttack = status[1];
        cStatus.attack = status[2];
        cStatus.revaival = status[3];
        cStatus.cutIn = status[4];
        playerStatus.charaStatus = cStatus;
    }

    // ボスフェーズへの移行
    private void StartBossPhase()
    {
        Debug.Log("ボスフェーズへ移行します");
        Destroy(this.gameObject);
        // ボスフェーズの処理を開始
        bigController.ChangePhase(CS_Controller.PACHINKO_PHESE.BOSS);
    }

    //抽選後処理
    private IEnumerator AfterLottery(int _perfNum)
    {
        yield return new WaitForSeconds(0.5f);
        //イベントハンドラ実行
        OnPlayPerformance(_perfNum);

        //演出が終わるまで処理を進めない
        while (!bigController.GetPatternVariationFinish()) { yield return null; }
        //Debug.Log("演出終了(仮)" + bigController.PerformanceSemiFinish);

        GameObject JackPotPerf = null;
        //当たり演出フラグがtrueならその演出生成
        if (bigController.JackPotPerf)
        {
            JackPotPerf = Instantiate(mCutIn, mCutIn.transform.position, mCutIn.transform.rotation);
        }

        //演出が終わるまで処理を進めない
        while (JackPotPerf) { yield return null; }

        mCoroutine = null;
    }

    //イベントハンドラ削除
    public static void RemoveAllHandlers()
    {
        // OnPlayPerformanceに登録されている関数を消す
        if (OnPlayPerformance != null)
        {
            //登録されているものを取得
            Delegate[] handlers = OnPlayPerformance.GetInvocationList();

            foreach (Delegate handler in handlers)
            {
                OnPlayPerformance -= (Performance)handler;
            }
        }
    }

    private void PlayPerformance(int _num)
    {
        Instantiate(missionPhaseTable.infomation[_num].performance, Vector3.zero, Quaternion.identity);
    }
}
