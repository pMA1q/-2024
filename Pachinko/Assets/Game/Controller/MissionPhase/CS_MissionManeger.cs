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

    [SerializeField,Header("�v���C���[�X�e�[�^�X")]
    private CSO_PlayerStatus playerStatus;

    private bool isReplayTriggered = false;  // �Ē��I�t���O

    [SerializeField]
    [Header("0:���W�~�b�V����")]
    [Header("1:�����~�b�V����")]
    [Header("2:�b�B�~�b�V����")]
    private GameObject[] missionPrefab;

    private CS_Controller bigController;//�i�ߓ�(��)
    private CS_MissionData missionData;//�i�ߓ�(��)

    private int mGameCount = 20;//���ܐ�

    //�Q�[�����̐ݒ�A�擾
    public int GameCount
    {
        set { mGameCount = value; }
        get { return mGameCount; }
    }

    //�G������
    private int mSubjugationNum = 0;
    public int SunjugationEnemy { get { return mSubjugationNum; } }

    //���j�[�N�ȉ��o
    CS_SM_Unique mSM_Unique;
    private int[] mUniquePF;

    int mNextMissionNum = -1;

    //-----------------------�C�x���g�n���h��-----------------------
    public delegate void Performance(int _performance);//�����F���ڔԍ�-1

    //�o�^���Ɏg�p
    public static event Performance OnPlayPerformance;
    //-------------------------------------------------------------

    void Start()
    {
        missionData = GameObject.Find("BigController").GetComponent<CS_MissionData>();
        // �v���C���[�X�e�[�^�X���f�[�^����擾
        playerStatus = missionData.PlayerStatus;

        bigController = GameObject.Find("BigController").GetComponent<CS_Controller>();//�i�ߓ�����擾

        //�~�b�V�����̎�ނ��擾
        int missionType = (int)bigController.GetComponent<CS_MissionData>().MissionNumber;
        //�~�b�V�����I���I�u�W�F�N�g�𐶐�
        GameObject instance = Instantiate(missionPrefab[missionType], missionPrefab[missionType].transform.position, missionPrefab[missionType].transform.rotation);
        instance.name = missionPrefab[missionType].name; // (Clone)���t���Ȃ��悤�ɖ��O���I���W�i���̖��O�ɖ߂�

        mSM_Unique = this.gameObject.AddComponent<CS_SM_Unique>();
        mUniquePF = new int[] { 11, 12 };//���j�[�N�ȉ��o�̍��ڔԍ��z��

        //�e�X�g
        OnPlayPerformance += PlayPerformance;
    }

    private void Update()
    {
        //�ϓ��ł��邩���擾
        bool variationStart = bigController.CanVariationStart();
        if (!variationStart) { return; }//false�Ȃ�I��

        //���ܐ���20�H
        if (mGameCount == 0 && mNextMissionNum != -1)
        {
            RemoveAllHandlers();
            StartBossPhase();
            //Destroy(this.gameObject);
            return;
        }

        //�C�x���g�n���h����null�Ȃ�I��
        if (OnPlayPerformance == null) { return; }

        //�ۗ��ʂ������Ȃ�I��
        if (bigController.GetStock() == 0) { return; }

        //�ۗ��ʎg�p�i�ϓ��J�n�j
        bigController.UseStock();

        //���o���I
        //int randomNumber = CS_LotteryFunction.LotNormalInt(missionPhaseTable.infomation.Count - 1);
        int randomNumber = CS_LotteryFunction.LotNormalInt(16);//��U����17�܂łɌ��肷��
       
        mGameCount--;//���ܐ����Z

        string name = missionPhaseTable.infomation[randomNumber].name;
        //�����W
        if (randomNumber <= 2)
        {
            float[] valTime = new float[3] { 8f, 10f, 10f };
            bigController.VariationTimer = valTime[randomNumber];//�ϓ����Ԑݒ�
            bigController.PerformanceFinish();//���o�͍s��Ȃ��̂ŏI���t���O�𗧂Ă�
            mNextMissionNum = -1;
            
            Debug.Log("���o�ԍ�" + name);
            return;
        }

        //���̉��o�ԍ���-1����Ȃ��Ȃ璊����𖳎�����
        if(mNextMissionNum != -1) { randomNumber = mNextMissionNum; }
        
        //�C�x���g�n���h�����s
        OnPlayPerformance(randomNumber);

        name = missionPhaseTable.infomation[randomNumber].name;
        Debug.Log("���o�ԍ�" + name);

        //�Ē��I�m�F�B���I����Ύ��̃~�b�V��������
        mNextMissionNum = CheckReLottely(missionPhaseTable.infomation[randomNumber]);

        DesisionNextMissionNum(randomNumber);//���j�[�N�ȃ~�b�V�����Ȃ�Ύ��̃~�b�V�����ԍ������߂�
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
        float percentage = playerStatus.charaStatus.preemptiveAttack;
       
        //P2�Ȃ畜���l�ɂ���
        if(mission.replay == REPLAY.TRUE_P2){ percentage = playerStatus.charaStatus.revaival; }

        float randomValue = UnityEngine.Random.Range(0f, 100f);
        if (randomValue < percentage)//��������
        {
            //�����_���X�e�[�^�X
            if(mission.replayNum <= 16) 
            {
                //�~�b�V�����̎�ނ��擾
                int missionType = (int)bigController.GetComponent<CS_MissionData>().MissionNumber;
                if(missionType == 0) { StatusUP(mission.replayNum); }
                else{ RundomStatusUP(mission.replayNum); }

            }
            return mission.replayNum - 1;
        }
        return -1;
    }

    //���W�~�b�V�����ӊO�̂Ƃ��̃X�e�[�^�XUP����
    private void RundomStatusUP(int _val)
    {
        CharacterStatus cStatus = playerStatus.charaStatus;
        float[] status = new float[5] { cStatus.charColorUP, cStatus.preemptiveAttack, cStatus.revaival, cStatus.equipmentRank, cStatus.cutIn};
        int random = CS_LotteryFunction.LotNormalInt(4);
        
        if(_val == 6 || _val == 9) { status[random] *= 2f; }//�����_���X�e�[�^�XUP��
        else  { status[random] *= 3f; }//�����_���X�e�[�^�XUP��
        playerStatus.charaStatus = cStatus;
    }

    //���W�~�b�V�����̂Ƃ��̃X�e�[�^�XUP����
    private void StatusUP(int _val)
    {
        int missionType = (int)missionData.MissionNumber;//�~�b�V�����̎��
        int mContents = bigController.GetComponent<CS_MissionData>().GetMissionContent(missionType);//�~�b�V�����̓��e
        CharacterStatus cStatus = playerStatus.charaStatus;
        float[] status = new float[5] { cStatus.charColorUP, cStatus.preemptiveAttack, cStatus.revaival, cStatus.equipmentRank, cStatus.cutIn };
        if (_val == 6 || _val == 9) { status[mContents] *= 2f; }//�X�e�[�^�XUP��
        else { status[mContents] *= 3f; }//�X�e�[�^�XUP��
        playerStatus.charaStatus = cStatus;
    }

    //���ڔԍ�18��19�̂Ƃ��̃��U���g
    public void Result18_19(int _res)
    {
        mSubjugationNum += _res;
    }

    // �{�X�t�F�[�Y�ւ̈ڍs
    private void StartBossPhase()
    {
        Debug.Log("�{�X�t�F�[�Y�ֈڍs���܂�");
        // �{�X�t�F�[�Y�̏������J�n
    }

    //�C�x���g�n���h���폜
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

    private void PlayPerformance(int _num)
    {
        Instantiate(missionPhaseTable.infomation[_num].performance, Vector3.zero, Quaternion.identity);
    }
}
