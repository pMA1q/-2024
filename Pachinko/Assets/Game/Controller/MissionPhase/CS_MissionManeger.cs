//---------------------------------
//�~�b�V�����}�l�[�W���[
//�S���ҁF���
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using UnityEditor.SceneManagement;

public class CS_MissionManeger : MonoBehaviour
{
    public CSO_MissionPhaseTable missionPhaseTable;  // Inspector �Őݒ�\

    [SerializeField,Header("�v���C���[�X�e�[�^�X")]
    private CSO_PlayerStatus playerStatus;

    [SerializeField, Header("�J�b�g�C��")]
    private GameObject mCutIn;

    [SerializeField]
    [Header("0:���W�~�b�V����")]
    [Header("1:�����~�b�V����")]
    [Header("2:�b�B�~�b�V����")]
    private GameObject[] missionPrefab;

    [SerializeField, Header("�����W���̃v���n�u")]
    private GameObject mNodevlopmentPrehab;

    [SerializeField, Header("�f�o�b�O�ԍ�(���ڔԍ�-1�̒l)")]
    [Header("�f�o�b�O���Ȃ��Ȃ�-1")]
    private int mDebugNumber = -1;

    private CS_Controller bigController;//�i�ߓ�(��)
    private CS_MissionPhaseData missionData;//�i�ߓ�(��)
    private CS_CommonData mData;//���ʃf�[�^

    private int mGameCount = 5;//���ܐ�

    private GameObject mNoDevObj;

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

    private int mBackupNumber = 0;

    private Coroutine mCoroutine = null;

    private int mHightEnemyCount = 0;

    //-----------------------�C�x���g�n���h��-----------------------
    public delegate void Performance(int _performance);//�����F���ڔԍ�-1

    //�o�^���Ɏg�p
    public static event Performance OnPlayPerformance;
    //-------------------------------------------------------------

    void Start()
    {
        mData = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_CommonData>();//���ʃf�[�^�擾
        missionData = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_MissionPhaseData>();
        missionData.ResetMissionData();//�~�b�V�����f�[�^�̊e�t���O�����Z�b�g����
       
        // �v���C���[�X�e�[�^�X���f�[�^����擾
        playerStatus = missionData.PlayerStatus;

        bigController = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_Controller>();//�i�ߓ�����擾

        //�~�b�V�����̎�ނ��擾
        int missionType = (int)bigController.GetComponent<CS_MissionPhaseData>().MissionType;
        //�~�b�V�����I���I�u�W�F�N�g�𐶐�
        GameObject instance = Instantiate(missionPrefab[missionType], missionPrefab[missionType].transform.position, missionPrefab[missionType].transform.rotation);
        instance.name = missionPrefab[missionType].name; // (Clone)���t���Ȃ��悤�ɖ��O���I���W�i���̖��O�ɖ߂�

        CS_MissionPhaseData.MISSION_TYPE mtype = missionData.MissionType;
        //if(mtype == CS_MissionPhaseData.MISSION_TYPE.SUBJUGATION) { mSM_Unique = new CS_SM_Unique(); }
        mSM_Unique = this.gameObject.AddComponent<CS_SM_Unique>();
        mUniquePF = new int[] { 11, 12, 18, 19, 22 };//���j�[�N�ȉ��o�̍��ڔԍ��z��

        mNoDevObj = Instantiate(mNodevlopmentPrehab, Vector3.zero, Quaternion.identity);
        mNoDevObj.GetComponent<CS_SetPositionPerfPos>().Start();
        mNoDevObj.GetComponent<CS_CameraWander>().Init();

        //�e�X�g
        OnPlayPerformance += PlayPerformance;
    }

    private void Update()
    {
        if(mCoroutine != null) { return; }
        
        UniquePerformance();//���j�[�N�ȃ~�b�V�����Ȃ�Ε�V�܂��͎��̃~�b�V�����ԍ������߂�

        Debug.Log("aaa");

        //���ܐ���20�H
        if (mGameCount <= 0 && mNextMissionNum == -1)
        {
            RemoveAllHandlers();
            StartBossPhase();
            //Destroy(this.gameObject);
            return;
        }

        //�ϓ��ł��邩���擾
        bool variationStart = bigController.CanVariationStart();
        if (!variationStart) { return; }//false�Ȃ�I��

        //Debug.Log("���̃~�b�V�����t���O" + mNextMissionNum);
       
        //�C�x���g�n���h����null�Ȃ�I��
        if (OnPlayPerformance == null) { return; }

        //�ۗ��ʂ������Ȃ�I��
        if (bigController.GetStock() == 0) { return; }

       
        //���o���I
        //int randomNumber = CS_LotteryFunction.LotNormalInt(missionPhaseTable.infomation.Count - 1);
        int randomNumber = CS_LotteryFunction.LotNormalInt(10);//��U����17�܂łɌ��肷��
        if (mDebugNumber >= 0) { randomNumber = mDebugNumber; }

        mGameCount--;//���ܐ����Z

        //Debug.Log("�c��Q�[����" + mGameCount);

        string name = missionPhaseTable.infomation[randomNumber].name;
       
        //�����W
        if (randomNumber <= 2)
        {
            NoDevelopment(randomNumber);
            return;
        }
 
        mData.NoDevelpment = false;//�����W�t���O��false

        bigController.VariationTimer = 4f;


        //�Ē��I�m�F�B���I����Ύ��̃~�b�V��������
        mNextMissionNum = CheckReLottely(missionPhaseTable.infomation[randomNumber]);
        //���̉��o�ԍ���-1����Ȃ��Ȃ�Ē��I���ʂ�����
        if (mNextMissionNum != -1) { randomNumber = mNextMissionNum; }

        //�ۗ��ʎg�p�i�ϓ��J�n�j
        bigController.UseStock(missionPhaseTable.infomation[randomNumber].win_lost);

        //������ԍ���ۑ�����
        mBackupNumber = randomNumber;

        name = missionPhaseTable.infomation[randomNumber].name;
        Debug.Log("���o���e" + name);
        mCoroutine = StartCoroutine(AfterLottery(randomNumber));//������㏈���𑖂点��
    }

    //�����W����
    private void NoDevelopment(int _perfNumber)
    {
        float[] valTime = new float[3] { 8f, 10f, 10f };
        bigController.VariationTimer = valTime[_perfNumber];//�ϓ����Ԑݒ�
        mData.NoDevelpment = true;//�����W�t���O��true
        mNextMissionNum = -1;
        mBackupNumber = _perfNumber;
        //�ۗ��ʎg�p�i�ϓ��J�n�j
        bigController.UseStock(WIN_LOST.LOST);
        mCoroutine = StartCoroutine(AfterLotteryNodev());
        bigController.PerformanceSemiFinish = true;
        bigController.PerformanceFinish();//���o�͍s��Ȃ��̂ŏI���t���O�𗧂Ă�
        
        string name = missionPhaseTable.infomation[_perfNumber].name;
        Debug.Log("���o�ԍ�" + name);
    }
   

    //���j�[�N�ȉ��o���̏���
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

        //�G�𓢔������Ȃ�
        if(missionData.SubjugationOneMission >= 1)
        {
            mSubjugationNum = missionData.SubjugationOneMission;//�������ɉ��Z
            missionData.SubjugationSum = mSubjugationNum;//�~�b�V�����f�[�^�̓��������v���X�V
            int perfNumber = mBackupNumber + 1;//���o���ڔԍ�
            //����~�b�V�������s�t���O�m��H�̉��o���𒲂ׂ�
            bool perfNumber18or19 = (perfNumber == 18 || perfNumber == 19);
            //����~�b�V�������s�t���O�m��H�̉��o�łȂ��Ȃ�P�~�b�V������������0�ɖ߂�
            if (!perfNumber18or19) {   missionData.SubjugationOneMission = 0; }
           
        }
    }
    
    //�Ē��I�m�F
    private int CheckReLottely(MissionPhaseInfomation mission)
    {
        //�Ē��I�����Ȃ�I��
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
            return lot[CS_LotteryFunction.LotNormalInt(4)];
        }

        //�G���m�������Q�[������1�ȏ�Ȃ�G��̑������o�ɂ���
        if(mHightEnemyCount >= 1)
        {
            mHightEnemyCount--;
            lot = new int[6] { 4 ,5, 6, 7, 8, 9 };
            return lot[CS_LotteryFunction.LotNormalInt(6)];
        }

        Debug.Log("�Ē��I�J�n");
        //�搧�U���̊m���ɐݒ�
        float percentage = playerStatus.charaStatus.preemptiveAttack;
       
        //P2�Ȃ畜���l�ɂ���
        if(mission.replay == REPLAY.TRUE_P2){ percentage = playerStatus.charaStatus.revaival; }

        float randomValue = UnityEngine.Random.Range(0f, 100f);
        if (randomValue < percentage)//��������
        {
            Debug.Log("�Ē��I���I");
            //�����_���X�e�[�^�XUP
            if(mission.replayNum <= 16) 
            {
                RundomStatusUP(mission.replayNum);
            }
            return mission.replayNum - 1;
        }
        return -1;
    }

    //���W�~�b�V�������ȊO�̃X�e�[�^�XUP����
    private void RundomStatusUP(int _val)
    {
        CharacterStatus cStatus = playerStatus.charaStatus;
        float[] status = new float[5] { cStatus.charColorUP, cStatus.preemptiveAttack, cStatus.attack, cStatus.revaival, cStatus.cutIn};
        List<float> choicePercent = new List<float> {cStatus.charColorUpPow.conicePercent, cStatus.preemptiveAttackUpPow.conicePercent,cStatus.attackUpPow.conicePercent,
                                                     cStatus.revivalUpPow.conicePercent,cStatus.cutInUpPow.conicePercent};
        List<float> smallpower = new List<float> {cStatus.charColorUpPow.smallUP, cStatus.preemptiveAttackUpPow.smallUP,cStatus.attackUpPow.smallUP,
                                                     cStatus.revivalUpPow.smallUP,cStatus.cutInUpPow.smallUP};
        List<float> midllepower = new List<float> {cStatus.charColorUpPow.middleUP, cStatus.preemptiveAttackUpPow.middleUP,cStatus.attackUpPow.middleUP,
                                                     cStatus.revivalUpPow.middleUP,cStatus.cutInUpPow.middleUP};
        List<float> maxpower = new List<float> {cStatus.charColorUpPow.max, cStatus.preemptiveAttackUpPow.max,cStatus.attackUpPow.max,
                                                     cStatus.revivalUpPow.max,cStatus.cutInUpPow.max};
        int random = CS_LotteryFunction.LotPerformance(choicePercent);
        
        if(_val == 6 || _val == 9) { status[random] += smallpower[random]; }//�����_���X�e�[�^�XUP��
        else  { status[random] += midllepower[random]; }//�����_���X�e�[�^�XUP��
        //�ő�l�𒴂��Ȃ��悤�ɂ���
        if (status[random] > maxpower[random]) { status[random] = maxpower[random]; }
        cStatus.charColorUP = status[0];
        cStatus.preemptiveAttack = status[1];
        cStatus.attack = status[2];
        cStatus.revaival = status[3];
        cStatus.cutIn = status[4];
        playerStatus.charaStatus = cStatus;
    }

    private void StartBossPhase()
    {
        Debug.Log("�{�X�t�F�[�Y�ֈڍs���܂�");
        Destroy(mNoDevObj); // �����W�I�u�W�F�N�g�폜

        //Remuneration();//��V�l������

        // ���̃t�F�[�Y�̏�������
        CS_BossPhaseHandler bossPhaseHandler = gameObject.GetComponent<CS_BossPhaseHandler>();
        bossPhaseHandler.Initialize(bigController);

        // �~�b�V�����}�l�[�W���[���폜
        Destroy(this);
    }

    //��V����
    void Remuneration()
    {
        //�~�b�V�����f�[�^�擾
        CS_MissionPhaseData.MISSION_TYPE mType = missionData.MissionType;

        int missionCnontents = missionData.GetMissionContent((int)mType);//�~�b�V�����̎�ނ̒��̗v�f�ԍ����擾
        CSO_PlayerStatus pStatus = missionData.PlayerStatus;//�v���C���[���擾
        int addTiket = 3;//�`�P�b�g������
        switch(mType)
        {
            case CS_MissionPhaseData.MISSION_TYPE.COLLECT://���W(�`�P�b�g)
                if(missionCnontents == missionData.tPreAttack)
                {
                    pStatus.ticket.preemptiveAttack += addTiket;
                    
                }
                else if (missionCnontents == missionData.tPreRev)
                {
                    pStatus.ticket.revaival += addTiket;
                }
               
                break;
            case CS_MissionPhaseData.MISSION_TYPE.SUBJUGATION://����(�X�L��)
                if (missionCnontents == missionData.eSkill)
                {
                    pStatus.ticket.special += 3;
                }
                break;
            case CS_MissionPhaseData.MISSION_TYPE.TRAINING://�b�B(�X�e�[�^�X)
                //�X�e�[�^�X10%Up
                if (missionCnontents == missionData.sCut)
                {
                    pStatus.charaStatus.cutIn *= 1.10f;
                    //�ő�l�𒴂��Ȃ��悤�ɂ���
                    if(pStatus.charaStatus.cutIn > pStatus.charaStatus.cutInUpPow.max) { pStatus.charaStatus.cutIn = pStatus.charaStatus.cutInUpPow.max; }
                }
                if (missionCnontents == missionData.sColor)
                { 
                    pStatus.charaStatus.charColorUP *= 1.10f;
                    //�ő�l�𒴂��Ȃ��悤�ɂ���
                    if (pStatus.charaStatus.charColorUP > pStatus.charaStatus.charColorUpPow.max) { pStatus.charaStatus.charColorUP = pStatus.charaStatus.charColorUpPow.max; }
                }
                if (missionCnontents == missionData.sPreAttack)
                { 
                    pStatus.charaStatus.preemptiveAttack *= 1.10f;
                    //�ő�l�𒴂��Ȃ��悤�ɂ���
                    if (pStatus.charaStatus.preemptiveAttack > pStatus.charaStatus.preemptiveAttackUpPow.max) { pStatus.charaStatus.preemptiveAttack = pStatus.charaStatus.preemptiveAttackUpPow.max; }
                }
                if (missionCnontents == missionData.sRev)
                {
                    pStatus.charaStatus.revaival *= 1.10f;
                    //�ő�l�𒴂��Ȃ��悤�ɂ���
                    if (pStatus.charaStatus.revaival > pStatus.charaStatus.revivalUpPow.max) { pStatus.charaStatus.revaival = pStatus.charaStatus.revivalUpPow.max; }
                }
                if (missionCnontents == missionData.sEquipmentRank)
                {
                    pStatus.charaStatus.equipmentRank += 1;
                    //�ő�l�𒴂��Ȃ��悤�ɂ���
                    if (pStatus.charaStatus.revaival > pStatus.charaStatus.revivalUpPow.max) { pStatus.charaStatus.revaival = pStatus.charaStatus.revivalUpPow.max; }
                }

                //�U����UP
                pStatus.charaStatus.attack += 5;
                if(pStatus.charaStatus.attack > pStatus.charaStatus.attackUpPow.max) { pStatus.charaStatus.attack = pStatus.charaStatus.attackUpPow.max; }
                break;
        }
    }

    private IEnumerator AfterLotteryNodev()
    {
        //���o���I���܂ŏ�����i�߂Ȃ�
        while (!bigController.GetPatternVariationFinish()) { yield return null; }
        Debug.Log("�}���ϓ����I�����܂���");
        mCoroutine = null;
    }
    //���I�㏈��
    private IEnumerator AfterLottery(int _perfNum)
    {
        yield return new WaitForSeconds(0.5f);
        mNoDevObj.SetActive(false);
        //�C�x���g�n���h�����s
        OnPlayPerformance(_perfNum);

        //���o���I���܂ŏ�����i�߂Ȃ�
        while (!bigController.GetPatternVariationFinish()) { yield return null; }
        //Debug.Log("���o�I��(��)" + bigController.PerformanceSemiFinish);

        GameObject JackPotPerf = null;
        //�����艉�o�t���O��true�Ȃ炻�̉��o����
        if (bigController.JackPotPerf)
        {
            JackPotPerf = Instantiate(mCutIn, mCutIn.transform.position, mCutIn.transform.rotation);
        }

        //���o���I���܂ŏ�����i�߂Ȃ�
        while (JackPotPerf) { yield return null; }

        mNoDevObj.SetActive(true);

        mCoroutine = null;
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
        if(missionPhaseTable.infomation[_num].performance != null)
        {
            Instantiate(missionPhaseTable.infomation[_num].performance, Vector3.zero, Quaternion.identity);
        }
        else
        {
            bigController.PerformanceFinish();
        }
        
    }
}
