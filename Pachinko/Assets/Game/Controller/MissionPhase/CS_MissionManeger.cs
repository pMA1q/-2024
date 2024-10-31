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

    //-----------------------イベントハンドラ-----------------------
    public delegate void Performance(int _performance);

    //登録時に使用
    public static event Performance OnPlayPerformance;
    //-------------------------------------------------------------

    void Start()
    {
        // プレイヤーステータス初期化
        //playerStatus = new PlayerStatus(initialHp: 100, initialAttack: 10, initialDefense: 10);

        

        bigController = GameObject.Find("BigController").GetComponent<CS_Controller>();//司令塔大を取得

        //ミッションの種類を取得
        int missionType = (int)bigController.GetComponent<CSO_MissionData>().MissionNumber;
        //ミッション選択オブジェクトを生成
        GameObject instance = Instantiate(missionPrefab[missionType], missionPrefab[missionType].transform.position, missionPrefab[missionType].transform.rotation);
        instance.name = missionPrefab[missionType].name; // (Clone)が付かないように名前をオリジナルの名前に戻す

        // ミッションフェーズ開始
        StartMissionPhase();
    }

    // ミッションフェーズの進行
    public void StartMissionPhase()
    {
        for (int i = 0; i < 20; i++)  // 20回転の抽選処理
        {
            int missionIndex = UnityEngine.Random.Range(0, missionPhaseTable.infomation.Count);  // リストからミッションを抽選
            MissionPhaseInfomation mission = missionPhaseTable.infomation[missionIndex];

            Debug.Log($"抽選されたミッション: {mission.name}");

            // ミッションの当落情報に応じた処理
            if (mission.win_lost == WIN_LOST.LOST)
            {
                Debug.Log("ミッション失敗: 敵に逃げられる");
                HandleEscapeMission(mission);
            }
            else if (mission.win_lost == WIN_LOST.SMALL_WIN)
            {
                Debug.Log("小当たりミッション発生");
                HandleDefeatMission(mission);
            }
            else
            {
                Debug.Log($"当たりミッション: {mission.name}");
                CallMission(mission);
            }

            // 20回転目の処理
            if (i == 19 && isReplayTriggered)
            {
                Debug.Log("20回転目に再抽選発生。再抽選後、ボスフェーズへ移行。");
                // 再抽選後、ボスフェーズへ移行
                HandleReplay();
                break;
            }
        }
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
        else
        {
            Debug.Log("再抽選なし");
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
