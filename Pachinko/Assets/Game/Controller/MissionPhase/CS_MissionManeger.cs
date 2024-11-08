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

    private CSO_PlayerStatus playerStatus;

    private bool isReplayTriggered = false;  // 再抽選フラグ

    [SerializeField]
    [Header("0:収集ミッション")]
    [Header("1:討伐ミッション")]
    [Header("2:鍛錬ミッション")]
    private GameObject[] missionPrefab;

    private CS_Controller bigController;//司令塔(大)

    private int mPrizesNum = 0;//入賞数

    //ユニークな演出
    CS_SM_Unique mSM_Unique;
    private int[] mUniquePF;

    int mNextMissionNum = -1;

    //-----------------------イベントハンドラ-----------------------
    public delegate void Performance(int _performance);

    //登録時に使用
    public static event Performance OnPlayPerformance;
    //-------------------------------------------------------------

    void Start()
    {
        // プレイヤーステータス初期化
        playerStatus = new CSO_PlayerStatus(initialHp: 100, initialAttack: 10, initialDefense: 10, initialPreemptiveAttack: 30, initialRevaival: 20);

        bigController = GameObject.Find("BigController").GetComponent<CS_Controller>();//司令塔大を取得

        //ミッションの種類を取得
        int missionType = (int)bigController.GetComponent<CSO_MissionData>().MissionNumber;
        //ミッション選択オブジェクトを生成
        GameObject instance = Instantiate(missionPrefab[missionType], missionPrefab[missionType].transform.position, missionPrefab[missionType].transform.rotation);
        instance.name = missionPrefab[missionType].name; // (Clone)が付かないように名前をオリジナルの名前に戻す

        mSM_Unique = this.gameObject.AddComponent<CS_SM_Unique>();
        mUniquePF = new int[] { 11, 12 };//ユニークな演出の項目番号配列

        // ミッションフェーズのシナリオを決める
       // StartMissionPhase();
    }

    //ミッションシナリオ抽せん
    public void StartMissionPhase()
    {
        int max = 20;
        for (int i = 0; i < max; i++)  // 20回転の抽選処理
        {
            int missionIndex = UnityEngine.Random.Range(0, missionPhaseTable.infomation.Count - 1);  // リストからミッションを抽選
            MissionPhaseInfomation mission = missionPhaseTable.infomation[missionIndex];

            //mMissionInfomations.Add(mission);//ミッション情報リスト追加
            //mMissionIndexes.Add(missionIndex);//ミッション項目番号リストに追加

            Debug.Log($"抽選されたミッション: {mission.name}");

            {
                //// ミッションの当落情報に応じた処理
                //if (mission.win_lost == WIN_LOST.LOST)
                //{
                //    Debug.Log("ミッション失敗: 敵に逃げられる");
                //    HandleEscapeMission(mission);
                //}
                //else if (mission.win_lost == WIN_LOST.SMALL_WIN)
                //{
                //    Debug.Log("小当たりミッション発生");
                //    HandleDefeatMission(mission);
                //}
                //else
                //{
                //    Debug.Log($"当たりミッション: {mission.name}");
                //    CallMission(mission);
                //}

                //// 20回転目の処理
                //if (i == 19 && isReplayTriggered)
                //{
                //    Debug.Log("20回転目に再抽選発生。再抽選後、ボスフェーズへ移行。");
                //    // 再抽選後、ボスフェーズへ移行
                //    HandleReplay();
                //    break;
                //}
            }
        }

       
    }

    private void Update()
    {
        //変動できるかを取得
        bool variationStart = bigController.CanVariationStart();
        if (!variationStart) { return; }//falseなら終了

        //入賞数が20？
        if (mPrizesNum == 20)
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

        if(mNextMissionNum != -1) { randomNumber = mNextMissionNum; }

        mPrizesNum++;//入賞数加算

        //イベントハンドラ実行
        OnPlayPerformance(randomNumber);

        string name = missionPhaseTable.infomation[randomNumber].name;
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
        float percentage = playerStatus.preemptiveAttack;
       
        //P2なら復活値にする
        if(mission.replay == REPLAY.TRUE_P2){ percentage = playerStatus.revaival; }

        float randomValue = UnityEngine.Random.Range(0f, 100f);
        if (randomValue < percentage)
        {
            //当たったら追加する
            MissionPhaseInfomation replayMission = missionPhaseTable.infomation[mission.replayNum - 1];
            //mMissionIndexes.Add(mission.replayNum - 1);
            //mMissionInfomations.Add(replayMission);
            return mission.replayNum - 1;
        }
        else { mNextMissionNum = -1; }
        return -1;
    }

    // 敵に逃げられる場合の処理
    private void HandleEscapeMission(MissionPhaseInfomation mission)
    {
        if (mission.replay == REPLAY.TRUE_P1 || mission.replay == REPLAY.TRUE_P2)
        {
            Debug.Log("先制攻撃可否の再抽選発生");
            // 再抽選やステータスの変更処理を実行
            isReplayTriggered = true;  // 再抽選フラグを設定
        }
        
    }

    // 敵と戦って負ける場合の処理
    private void HandleDefeatMission(MissionPhaseInfomation mission)
    {
        if (mission.replay == REPLAY.TRUE_P1 || mission.replay == REPLAY.TRUE_P2)
        {
            Debug.Log("復活可否の再抽選発生");
            // 再抽選やステータスの変更処理を実行
            isReplayTriggered = true;  // 再抽選フラグを設定
        }
        else
        {
            Debug.Log("再抽選なし");
        }
    }

    // 再抽選後の処理
    private void HandleReplay()
    {
        // 再抽選の処理をここで実行
        Debug.Log("再抽選実行中・・・");

        // 再抽選後にボスフェーズへ移行
        StartBossPhase();
    }

    // ボスフェーズへの移行
    private void StartBossPhase()
    {
        Debug.Log("ボスフェーズへ移行します");
        // ボスフェーズの処理を開始
    }


    // ミッションの処理を実行
    private void CallMission(MissionPhaseInfomation mission)
    {
        Debug.Log($"ミッション実行: {mission.name}");
        playerStatus.UpdateStatus(hpChange: 5, attackChange: 2, defenseChange: 1);
        // 演出などの処理
        if (mission.performance != null)
        {
            Instantiate(mission.performance, Vector3.zero, Quaternion.identity);
        }
    }

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


}
