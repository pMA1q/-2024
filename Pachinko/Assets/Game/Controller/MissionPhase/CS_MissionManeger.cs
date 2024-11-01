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

    private int mPrizesNum = 0;//���ܐ�

    //private int mGameCount = 0;

    //private List<int> mMissionIndexes;
    //private List<MissionPhaseInfomation> mMissionInfomations;

    CS_SM_Unique mSM_Unique;
    private int[] mUniquePF;
    int mNextMissionNum = -1;

    //-----------------------�C�x���g�n���h��-----------------------
    public delegate void Performance(int _performance);

    public static event Performance OnPlayPerformance;  // �C�x���g�n���h��
    //-------------------------------------------------------------

    void Start()
    {
        // �v���C���[�X�e�[�^�X������
        playerStatus = new CSO_PlayerStatus(initialHp: 100, initialAttack: 10, initialDefense: 10, initialPreemptiveAttack: 30, initialRevaival: 20);

        bigController = GameObject.Find("BigController").GetComponent<CS_Controller>();//�i�ߓ����擾

        //�~�b�V�����̎�ނ�擾
        int missionType = (int)bigController.GetComponent<CSO_MissionData>().MissionNumber;
        GameObject instance = Instantiate(missionPrefab[missionType], missionPrefab[missionType].transform.position, missionPrefab[missionType].transform.rotation);
        instance.name = missionPrefab[missionType].name;

        mSM_Unique = this.gameObject.AddComponent<CS_SM_Unique>();
        mUniquePF = new int[] { 11, 12 };//���j�[�N�ȉ��o�̍��ڔԍ��z��

        // �~�b�V�����t�F�[�Y�̃V�i���I����߂�
       // StartMissionPhase();
    }

    //�~�b�V�����V�i���I������
    public void StartMissionPhase()
    {
        int max = 20;
        for (int i = 0; i < max; i++)  // 20��]�̒��I����
        {
            int missionIndex = UnityEngine.Random.Range(0, missionPhaseTable.infomation.Count - 1);  // ���X�g����~�b�V�����𒊑I
            MissionPhaseInfomation mission = missionPhaseTable.infomation[missionIndex];

            //mMissionInfomations.Add(mission);//�~�b�V������񃊃X�g�ǉ�
            //mMissionIndexes.Add(missionIndex);//�~�b�V�������ڔԍ����X�g�ɒǉ�

            Debug.Log($"���I���ꂽ�~�b�V����: {mission.name}");

            {
                //// �~�b�V�����̓������ɉ���������
                //if (mission.win_lost == WIN_LOST.LOST)
                //{
                //    Debug.Log("�~�b�V�������s: �G�ɓ�������");
                //    HandleEscapeMission(mission);
                //}
                //else if (mission.win_lost == WIN_LOST.SMALL_WIN)
                //{
                //    Debug.Log("��������~�b�V��������");
                //    HandleDefeatMission(mission);
                //}
                //else
                //{
                //    Debug.Log($"������~�b�V����: {mission.name}");
                //    CallMission(mission);
                //}

                //// 20��]�ڂ̏���
                //if (i == 19 && isReplayTriggered)
                //{
                //    Debug.Log("20��]�ڂɍĒ��I�����B�Ē��I��A�{�X�t�F�[�Y�ֈڍs�B");
                //    // �Ē��I��A�{�X�t�F�[�Y�ֈڍs
                //    HandleReplay();
                //    break;
                //}
            }
        }

       
    }

    private void Update()
    {
        //�ϓ��ł��邩��擾
        bool variationStart = bigController.CanVariationStart();
        if (!variationStart) { return; }//false�Ȃ�I��

        //���ܐ���20�H
        if (mPrizesNum == 20)
        {
            RemoveAllHandlers();
            //StartBossPhase();
            //Destroy(this.gameObject);
            return;
        }

        // �C�x���g�n���h����null�Ȃ�I��
        if (OnPlayPerformance == null) { return; }

        //�ۗ��ʂ������Ȃ�I��
        if (bigController.GetStock() == 0) { return; }

        //�ۗ��ʎg�p�i�ϓ��J�n�j
        bigController.UseStock();

        //���o���I
        //int randomNumber = CS_LotteryFunction.LotNormalInt(missionPhaseTable.infomation.Count - 1);
        int randomNumber = CS_LotteryFunction.LotNormalInt(16);//��U����17�܂łɌ��肷��

        if(mNextMissionNum != -1) { randomNumber = mNextMissionNum; }

        mPrizesNum++;//���ܐ����Z

        //�C�x���g�n���h�����s
        OnPlayPerformance(randomNumber);

        string name = missionPhaseTable.infomation[randomNumber].name;
        Debug.Log("���o�ԍ�" + name);

        //�Ē��I�m�F�B���I����Ύ��̃~�b�V��������
        mNextMissionNum = CheckReLottely(missionPhaseTable.infomation[randomNumber]);

        DesisionNextMissionNum(randomNumber);//���j�[�N�ȃ~�b�V�����Ȃ�Ύ��̃~�b�V�����ԍ�����߂�
    }

    //���݂̃~�b�V���������j�[�N�ȏꍇ
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
    
    //�Ē��I�m�F
    private int CheckReLottely(MissionPhaseInfomation mission)
    {
        //�Ē��I�����Ȃ�I��
        if(mission.replay == REPLAY.FALSE) { return -1; }

        //�搧�U���̊m���ɐݒ�
        float percentage = playerStatus.preemptiveAttack;
       
        //P2�Ȃ畜���l�ɂ���
        if(mission.replay == REPLAY.TRUE_P2){ percentage = playerStatus.revaival; }

        float randomValue = UnityEngine.Random.Range(0f, 100f);
        if (randomValue < percentage)
        {
            //����������ǉ�����
            MissionPhaseInfomation replayMission = missionPhaseTable.infomation[mission.replayNum - 1];
            //mMissionIndexes.Add(mission.replayNum - 1);
            //mMissionInfomations.Add(replayMission);
            return mission.replayNum - 1;
        }
        else { mNextMissionNum = -1; }
        return -1;
    }

    // �G�ɓ�������ꍇ�̏���
    private void HandleEscapeMission(MissionPhaseInfomation mission)
    {
        if (mission.replay == REPLAY.TRUE_P1 || mission.replay == REPLAY.TRUE_P2)
        {
            Debug.Log("�Ē��I���������܂���");
            isReplayTriggered = true;
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

        // �K�v�ȍĒ��I����������ɒǉ�
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

