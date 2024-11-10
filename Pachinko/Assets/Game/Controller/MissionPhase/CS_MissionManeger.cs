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

    private bool isReplayTriggered = false;  // 再抽選フラグ

    [SerializeField]
    [Header("0:収集ミッション")]
    [Header("1:討伐ミッション")]
    [Header("2:鍛錬ミッション")]
    private GameObject[] missionPrefab;

    private CS_Controller bigController;//司令塔(大)
    private CS_MissionData missionData;//司令塔(大)

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

    //-----------------------イベントハンドラ-----------------------
    public delegate void Performance(int _performance);//引数：項目番号-1

    //登録時に使用
    public static event Performance OnPlayPerformance;
    //-------------------------------------------------------------

    void Start()
    {
        missionData = GameObject.Find("BigController").GetComponent<CS_MissionData>();
        // プレイヤーステータスをデータから取得
        playerStatus = missionData.PlayerStatus;

        bigController = GameObject.Find("BigController").GetComponent<CS_Controller>();//司令塔大を取得

        //ミッションの種類を取得
        int missionType = (int)bigController.GetComponent<CS_MissionData>().MissionNumber;
        //ミッション選択オブジェクトを生成
        GameObject instance = Instantiate(missionPrefab[missionType], missionPrefab[missionType].transform.position, missionPrefab[missionType].transform.rotation);
        instance.name = missionPrefab[missionType].name; // (Clone)が付かないように名前をオリジナルの名前に戻す

        mSM_Unique = this.gameObject.AddComponent<CS_SM_Unique>();
        mUniquePF = new int[] { 11, 12 };//ユニークな演出の項目番号配列

        //テスト
        OnPlayPerformance += PlayPerformance;
    }

    private void Update()
    {
        //変動できるかを取得
        bool variationStart = bigController.CanVariationStart();
        if (!variationStart) { return; }//falseなら終了

        //入賞数が20？
        if (mGameCount == 0 && mNextMissionNum != -1)
        {
            RemoveAllHandlers();
            StartBossPhase();
            //Destroy(this.gameObject);
            return;
        }

        //イベントハンドラはnullなら終了
        if (OnPlayPerformance == null) { return; }

        //保留玉が無いなら終了
        if (bigController.GetStock() == 0) { return; }

        //保留玉使用（変動開始）
        bigController.UseStock();

        //演出抽選
        //int randomNumber = CS_LotteryFunction.LotNormalInt(missionPhaseTable.infomation.Count - 1);
        int randomNumber = CS_LotteryFunction.LotNormalInt(16);//一旦項目17までに限定する
       
        mGameCount--;//入賞数減算

        string name = missionPhaseTable.infomation[randomNumber].name;
        //無発展
        if (randomNumber <= 2)
        {
            float[] valTime = new float[3] { 8f, 10f, 10f };
            bigController.VariationTimer = valTime[randomNumber];//変動時間設定
            bigController.PerformanceFinish();//演出は行わないので終了フラグを立てる
            mNextMissionNum = -1;
            
            Debug.Log("演出番号" + name);
            return;
        }

        //次の演出番号が-1じゃないなら抽せんを無視する
        if(mNextMissionNum != -1) { randomNumber = mNextMissionNum; }
        
        //イベントハンドラ実行
        OnPlayPerformance(randomNumber);

        name = missionPhaseTable.infomation[randomNumber].name;
        Debug.Log("演出番号" + name);

        //再抽選確認。当選すれば次のミッション決定
        mNextMissionNum = CheckReLottely(missionPhaseTable.infomation[randomNumber]);

        DesisionNextMissionNum(randomNumber);//ユニークなミッションならば次のミッション番号を決める
    }

    //現在のミッションがユニークな場合
    private void DesisionNextMissionNum(int _nowMissionNum)
    {
        int next = -1;
        for (int i = 0; i < mUniquePF.Length; i++)
        {
            if (_nowMissionNum == mUniquePF[i] - 1)
            {
                next = mSM_Unique.DesisionMission(i);
                mNextMissionNum = next;
            }
        }
    }
    
    //再抽選確認
    private int CheckReLottely(MissionPhaseInfomation mission)
    {
        //再抽選無しなら終了
        if(mission.replay == REPLAY.FALSE) { return -1; }

        //先制攻撃の確率に設定
        float percentage = playerStatus.charaStatus.preemptiveAttack;
       
        //P2なら復活値にする
        if(mission.replay == REPLAY.TRUE_P2){ percentage = playerStatus.charaStatus.revaival; }

        float randomValue = UnityEngine.Random.Range(0f, 100f);
        if (randomValue < percentage)//当たった
        {
            //ランダムステータス
            if(mission.replayNum <= 16) 
            {
                //ミッションの種類を取得
                int missionType = (int)bigController.GetComponent<CS_MissionData>().MissionNumber;
                if(missionType == 0) { StatusUP(mission.replayNum); }
                else{ RundomStatusUP(mission.replayNum); }

            }
            return mission.replayNum - 1;
        }
        return -1;
    }

    //収集ミッション意外のときのステータスUP処理
    private void RundomStatusUP(int _val)
    {
        CharacterStatus cStatus = playerStatus.charaStatus;
        float[] status = new float[5] { cStatus.charColorUP, cStatus.preemptiveAttack, cStatus.revaival, cStatus.equipmentRank, cStatus.cutIn};
        int random = CS_LotteryFunction.LotNormalInt(4);
        
        if(_val == 6 || _val == 9) { status[random] *= 2f; }//ランダムステータスUP小
        else  { status[random] *= 3f; }//ランダムステータスUP大
        playerStatus.charaStatus = cStatus;
    }

    //収集ミッションのときのステータスUP処理
    private void StatusUP(int _val)
    {
        int missionType = (int)missionData.MissionNumber;//ミッションの種類
        int mContents = bigController.GetComponent<CS_MissionData>().GetMissionContent(missionType);//ミッションの内容
        CharacterStatus cStatus = playerStatus.charaStatus;
        float[] status = new float[5] { cStatus.charColorUP, cStatus.preemptiveAttack, cStatus.revaival, cStatus.equipmentRank, cStatus.cutIn };
        if (_val == 6 || _val == 9) { status[mContents] *= 2f; }//ステータスUP小
        else { status[mContents] *= 3f; }//ステータスUP中
        playerStatus.charaStatus = cStatus;
    }

    //項目番号18と19のときのリザルト
    public void Result18_19(int _res)
    {
        mSubjugationNum += _res;
    }

    // ボスフェーズへの移行
    private void StartBossPhase()
    {
        Debug.Log("ボスフェーズへ移行します");
        // ボスフェーズの処理を開始
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
