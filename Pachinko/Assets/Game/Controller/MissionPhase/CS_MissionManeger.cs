using System;
using UnityEngine;

public class CS_MissionManeger : MonoBehaviour
{
    public CSO_MissionPhaseTable missionPhaseTable;
    private CSO_PlayerStatus playerStatus;
    private bool isReplayTriggered = false;

    [SerializeField]
    [Header("0:収集ミッション")]
    [Header("1:討伐ミッション")]
    [Header("2:鍛錬ミッション")]
    private GameObject[] missionPrefab;

    private CS_Controller bigController;

    //-----------------------イベントハンドラ-----------------------
    public delegate void Performance(int _performance);

    public static event Performance OnPlayPerformance;  // イベントハンドラ
    //-------------------------------------------------------------

    void Start()
    {
        bigController = GameObject.Find("BigController").GetComponent<CS_Controller>();
        int missionType = (int)bigController.GetComponent<CSO_MissionData>().MissionNumber;
        GameObject instance = Instantiate(missionPrefab[missionType], missionPrefab[missionType].transform.position, missionPrefab[missionType].transform.rotation);
        instance.name = missionPrefab[missionType].name;

        StartMissionPhase();
    }

    public void StartMissionPhase()
    {
        for (int i = 0; i < 20; i++)
        {
            int missionIndex = UnityEngine.Random.Range(0, missionPhaseTable.infomation.Count);
            MissionPhaseInfomation mission = missionPhaseTable.infomation[missionIndex];

            Debug.Log($"抽選されたミッション: {mission.name}");

            if (mission.win_lost == WIN_LOST.LOST)
            {
                HandleEscapeMission(mission);
            }
            else if (mission.win_lost == WIN_LOST.SMALL_WIN)
            {
                HandleDefeatMission(mission);
            }
            else
            {
                CallMission(mission, missionIndex); // インデックスを渡す
            }

            if (i == 19 && isReplayTriggered)
            {
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
            Debug.Log("再抽選が発生しました");
            isReplayTriggered = true;
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
        Debug.Log("再抽選実行中・・・");

        // 必要な再抽選処理をここに追加
        // 例: ステータス変更、リプレイ用データの設定など

        // 再抽選後にボスフェーズへ移行
        // StartBossPhase();


    }

    private void CallMission(MissionPhaseInfomation mission, int missionIndex)
    {
        Debug.Log($"ミッション実行: {mission.name}");
        playerStatus.UpdateStatus(hpChange: 5, attackChange: 2, defenseChange: 1);

        if (mission.performance != null)
        {
            Instantiate(mission.performance, Vector3.zero, Quaternion.identity);
            OnPlayPerformance?.Invoke(missionIndex);  // イベントトリガー
        }
    }

    public static void RemoveAllHandlers()
    {
        if (OnPlayPerformance != null)
        {
            Delegate[] handlers = OnPlayPerformance.GetInvocationList();

            foreach (Delegate handler in handlers)
            {
                OnPlayPerformance -= (Performance)handler;
            }
        }
    }
}

