//---------------------------------
//�~�b�V�����}�l�[�W���[
//�S���ҁF���
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CS_MissionManeger : MonoBehaviour
{
    public CSO_MissionPhaseTable missionPhaseTable;  // Inspector �Őݒ�\

    private CSO_PlayerStatus playerStatus;

    private bool isReplayTriggered = false;  // �Ē��I�t���O

    [SerializeField]
    [Header("0:���W�~�b�V����")]
    [Header("1:�����~�b�V����")]
    [Header("2:�b�B�~�b�V����")]
    private GameObject[] missionPrefab;

    private CS_Controller bigController;//�i�ߓ�(��)

    //-----------------------�C�x���g�n���h��-----------------------
    public delegate void Performance(int _performance);

    //�o�^���Ɏg�p
    public static event Performance OnPlayPerformance;
    //-------------------------------------------------------------

    void Start()
    {
        // �v���C���[�X�e�[�^�X������
        //playerStatus = new PlayerStatus(initialHp: 100, initialAttack: 10, initialDefense: 10);

        

        bigController = GameObject.Find("BigController").GetComponent<CS_Controller>();//�i�ߓ�����擾

        //�~�b�V�����̎�ނ��擾
        int missionType = (int)bigController.GetComponent<CSO_MissionData>().MissionNumber;
        //�~�b�V�����I���I�u�W�F�N�g�𐶐�
        GameObject instance = Instantiate(missionPrefab[missionType], missionPrefab[missionType].transform.position, missionPrefab[missionType].transform.rotation);
        instance.name = missionPrefab[missionType].name; // (Clone)���t���Ȃ��悤�ɖ��O���I���W�i���̖��O�ɖ߂�

        // �~�b�V�����t�F�[�Y�J�n
        StartMissionPhase();
    }

    // �~�b�V�����t�F�[�Y�̐i�s
    public void StartMissionPhase()
    {
        for (int i = 0; i < 20; i++)  // 20��]�̒��I����
        {
            int missionIndex = UnityEngine.Random.Range(0, missionPhaseTable.infomation.Count);  // ���X�g����~�b�V�����𒊑I
            MissionPhaseInfomation mission = missionPhaseTable.infomation[missionIndex];

            Debug.Log($"���I���ꂽ�~�b�V����: {mission.name}");

            // �~�b�V�����̓������ɉ���������
            if (mission.win_lost == WIN_LOST.LOST)
            {
                Debug.Log("�~�b�V�������s: �G�ɓ�������");
                HandleEscapeMission(mission);
            }
            else if (mission.win_lost == WIN_LOST.SMALL_WIN)
            {
                Debug.Log("��������~�b�V��������");
                HandleDefeatMission(mission);
            }
            else
            {
                Debug.Log($"������~�b�V����: {mission.name}");
                CallMission(mission);
            }

            // 20��]�ڂ̏���
            if (i == 19 && isReplayTriggered)
            {
                Debug.Log("20��]�ڂɍĒ��I�����B�Ē��I��A�{�X�t�F�[�Y�ֈڍs�B");
                // �Ē��I��A�{�X�t�F�[�Y�ֈڍs
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
            Debug.Log("�搧�U���ۂ̍Ē��I����");
            // �Ē��I��X�e�[�^�X�̕ύX���������s
            isReplayTriggered = true;  // �Ē��I�t���O��ݒ�
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
            // �Ē��I��X�e�[�^�X�̕ύX���������s
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
        // �Ē��I�̏����������Ŏ��s
        Debug.Log("�Ē��I���s���E�E�E");

        // �Ē��I��Ƀ{�X�t�F�[�Y�ֈڍs
        StartBossPhase();
    }

    // �{�X�t�F�[�Y�ւ̈ڍs
    private void StartBossPhase()
    {
        Debug.Log("�{�X�t�F�[�Y�ֈڍs���܂�");
        // �{�X�t�F�[�Y�̏������J�n
    }


    // �~�b�V�����̏��������s
    private void CallMission(MissionPhaseInfomation mission)
    {
        Debug.Log($"�~�b�V�������s: {mission.name}");
        playerStatus.UpdateStatus(hpChange: 5, attackChange: 2, defenseChange: 1);
        // ���o�Ȃǂ̏���
        if (mission.performance != null)
        {
            Instantiate(mission.performance, Vector3.zero, Quaternion.identity);
        }
    }

    public static void RemoveAllHandlers()
    {
        // OnPlayPerformance�ɓo�^����Ă���֐�������
        if (OnPlayPerformance != null)
        {
            //�o�^����Ă�����̂��擾
            Delegate[] handlers = OnPlayPerformance.GetInvocationList();

            foreach (Delegate handler in handlers)
            {
                OnPlayPerformance -= (Performance)handler;
            }
        }
    }


}
