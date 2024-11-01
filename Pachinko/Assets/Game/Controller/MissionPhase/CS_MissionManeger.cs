using System;
using UnityEngine;

public class CS_MissionManeger : MonoBehaviour
{
    public CSO_MissionPhaseTable missionPhaseTable;
    private CSO_PlayerStatus playerStatus;
    private bool isReplayTriggered = false;

    [SerializeField]
    [Header("0:���W�~�b�V����")]
    [Header("1:�����~�b�V����")]
    [Header("2:�b�B�~�b�V����")]
    private GameObject[] missionPrefab;

    private CS_Controller bigController;

    //-----------------------�C�x���g�n���h��-----------------------
    public delegate void Performance(int _performance);

    public static event Performance OnPlayPerformance;  // �C�x���g�n���h��
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

            Debug.Log($"���I���ꂽ�~�b�V����: {mission.name}");

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
                CallMission(mission, missionIndex); // �C���f�b�N�X��n��
            }

            if (i == 19 && isReplayTriggered)
            {
                HandleReplay();
                break;
            }
        }
    }

    // �G�ɓ�������ꍇ�̏���
    private void HandleEscapeMission(MissionPhaseInfomation mission)
    {
        if (mission.replay == REPLAY.TRUE_P1 || mission.replay == REPLAY.TRUE_P2)
        {
            Debug.Log("�Ē��I���������܂���");
            isReplayTriggered = true;
        }
        else
        {
            Debug.Log("�Ē��I�Ȃ�");
        }
    }

    // �G�Ɛ���ĕ�����ꍇ�̏���
    private void HandleDefeatMission(MissionPhaseInfomation mission)
    {
        if (mission.replay == REPLAY.TRUE_P1 || mission.replay == REPLAY.TRUE_P2)
        {
            Debug.Log("�����ۂ̍Ē��I����");
            isReplayTriggered = true;  // �Ē��I�t���O��ݒ�
        }
        else
        {
            Debug.Log("�Ē��I�Ȃ�");
        }
    }


    // �Ē��I��̏���
    private void HandleReplay()
    {
        Debug.Log("�Ē��I���s���E�E�E");

        // �K�v�ȍĒ��I�����������ɒǉ�
        // ��: �X�e�[�^�X�ύX�A���v���C�p�f�[�^�̐ݒ�Ȃ�

        // �Ē��I��Ƀ{�X�t�F�[�Y�ֈڍs
        // StartBossPhase();


    }

    private void CallMission(MissionPhaseInfomation mission, int missionIndex)
    {
        Debug.Log($"�~�b�V�������s: {mission.name}");
        playerStatus.UpdateStatus(hpChange: 5, attackChange: 2, defenseChange: 1);

        if (mission.performance != null)
        {
            Instantiate(mission.performance, Vector3.zero, Quaternion.identity);
            OnPlayPerformance?.Invoke(missionIndex);  // �C�x���g�g���K�[
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

