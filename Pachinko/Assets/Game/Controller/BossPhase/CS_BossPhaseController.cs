//---------------------------------
//�{�X�t�F�[�Y
//�S���ҁF���
//---------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using System.Diagnostics;

public class CS_BossPhaseController : MonoBehaviour
{
    [SerializeField, Header("�{�X�̃e�[�u�����X�g")]
    private List<CSO_BossPhaseTable> mBossTables;
    [SerializeField, Header("�����W���̃v���n�u")]
    private GameObject mNodevlopmentPrehab;
    [SerializeField, Header("�̗̓Q�[�W")]
    private GameObject mHpGuage;

    [SerializeField, Header("���荇��")]
    private GameObject mCompetition;

    [SerializeField, Header("���X�g�A�^�b�N")]
    private GameObject mLastAttack;

    [SerializeField, Header("�f�o�b�O�ԍ�(���ڔԍ�-1�̒l)")]
    [Header("�f�o�b�O���Ȃ��Ȃ�-1")]
    private int mDebugNumber = -1;

    private GameObject mNoDevObj;
    private GameObject mCompetitionObj;
    private GameObject mGuageObj;

    private CSO_BossPhaseTable mNowBossTable;

    private CS_BossUnique mBossUnique;

    private CS_BossPhaseData mBossData;
    private CS_CommonData mData;

    private CS_Controller mController;

    private CSO_PlayerStatus mPlayerStatus;
    private CSO_BossStatus mBossStatus;

    private int mBossNumber = 0;
    public int BossNumber
    {
        set { mBossNumber = value; }
        get { return mBossNumber; }
    }

    private int mNextMissionNum = -1;
    private int mGameCount = 3;
    //public int GameCount
    //{
    //    set { mGameCount = value; }
    //    get { return mGameCount; }
    //}

    private int mBackupNumber = 0;

    private Coroutine mCoroutine = null;

    //-----------------------�C�x���g�n���h��-----------------------
    public delegate void Performance(int _performance);//�����F���ڔԍ�-1

    //�o�^���Ɏg�p
    public static event Performance OnPlayPerformance;
    //-------------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        mController = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_Controller>();//�i�ߓ�����擾
        mData = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_CommonData>();//���ʃf�[�^�擾
        mBossData = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_BossPhaseData>();
        mBossData.ResetData();//�{�X�t�F�[�Y�f�[�^�̊e�t���O�����Z�b�g����
        mBossStatus = mBossData.BossStatus;
        mPlayerStatus = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_MissionPhaseData>().PlayerStatus;

        //�̗̓Q�[�W����
        mGuageObj = Instantiate(mHpGuage, Vector3.zero, Quaternion.identity);
        mGuageObj.name = mHpGuage.name;//Clone���t���Ȃ��悤�ɕύX
        mGuageObj.GetComponent<CS_HpGuage>().Init();
        //�{�X�ԍ��ɉ���������ݒ�
        SetBossInfomation();

        mNoDevObj = Instantiate(mNodevlopmentPrehab, Vector3.zero, Quaternion.identity);
        mNoDevObj.GetComponent<CS_SetPositionPerfPos>().Start();
        //mNoDevObj.GetComponent<CS_CameraWander>().Init();
    }

    private void SetBossInfomation()
    {
        switch(mBossNumber)
        {
            case 0:
                mBossUnique = this.gameObject.AddComponent<CS_Boss1_Unique>();
                break;
            //case 1:
            //    mBossUnique = new CS_Boss2_Unique();
            //    break;
            //case 2:
            //    mBossUnique = new CS_Boss3_Unique();
            //    break;
            //case 3:
            //    mBossUnique = new CS_Boss4_Unique();
            //    break;
        }

        mNowBossTable = mBossTables[mBossNumber];
    }

    // Update is called once per frame
    void Update()
    {
        if(mCoroutine != null) { return; }

        //�{�X����
        if(mBossData.IsSubjugation)
        {
            if (!mController.CanVariationStart()) { return; }
            Subjugation();
            return;
        }

        //��������
        if(mBossData.IsPlayerLose)
        {
            PlayerLose();
            return;
        }

        //�c��Q�[������0�ȉ��Ŏ��̃~�b�V�����̃t���O�������Ă��Ȃ��H
        if (mGameCount <= 0 && mNextMissionNum <= -1)
        {
            Debug.Log("�J�E���g" + mGameCount);
            RemoveAllHandlers();
            StartNextPhase();
            //Destroy(this.gameObject);
            return;
        }

        //�ϓ��ł��邩���擾
        bool variationStart = mController.CanVariationStart();
         if (!variationStart) { return; }//false�Ȃ�I��

        //�ۗ��ʂ������Ȃ�I��
        if (mController.GetStock() == 0) { return; }

        //5�b�ԑҋ@���Ă��牺�̏�����
        mCoroutine = StartCoroutine(Lottery());

       
    }

    //������������
    private void Subjugation()
    {
        Debug.Log("�ڕW�r�ł��܂���");
        StartNextPhase();
    }

    //����
    private void PlayerLose()
    {
        Debug.Log("�����܂���");
        StartNextPhase();
    }

    private IEnumerator Lottery()
    {
        int randomNumber = CS_LotteryFunction.LotNormalInt(mNowBossTable.infomation.Count);//0~��񐔕��̊ԂŒ�����
        if (mDebugNumber >= 0) { randomNumber = mDebugNumber; }
        mGameCount--;//�Q�[���J�E���g���ւ炷

        string name = mNowBossTable.infomation[randomNumber].name;

        //�����W
        if (randomNumber <= 2)
        {
            NoDevelopment(randomNumber);
            yield break;
        }

        //�v���C���[���U�����邩�m�F
        if (CheackPlayerAttack(randomNumber))
        {
            mNoDevObj.SetActive(false);
            mCompetitionObj = Instantiate(mCompetition, Vector3.zero, Quaternion.identity); //�U������Ȃ狣�荇���̃V�[��������
            Debug.Log("���荇���V�[���𐶐����܂���");
            CS_BP_CompetitionController competition = mCompetitionObj.GetComponent<CS_BP_CompetitionController>();

            float t = 0f;
            while(t <= 6f)
            {
                t += Time.deltaTime;
                if(competition.isActiveAndEnabled)  
                {
                    if (competition.NoHaveTikets()) { break; }
                    else { yield return null; }
                   
                }
            }

            randomNumber = ChangePerfNumber(randomNumber);//�I�񂾃`�P�b�g�ɉ����ĉ��o�ԍ��ύX
        }
      
        mData.NoDevelpment = false;

        mController.VariationTimer = 4f;

        //���̎��_�Ŏ��̔ԍ������܂��Ă���Ȃ獡��̕ϓ��ԍ�����
        if (mNextMissionNum > -1)
        {
            randomNumber = mNextMissionNum;
        }

        if (mDebugNumber >= 0) { randomNumber = mDebugNumber; }

        //�Ē��I�m�F�B���I����Ύ��̃~�b�V��������
        mNextMissionNum = CheckReLottely(randomNumber);
        //���̉��o�ԍ���-1����Ȃ��Ȃ�Ē��I���ʂ�����
        if (mNextMissionNum > -1) { randomNumber = mNextMissionNum; }

       

        Debug.Log("�Ē��I�ԍ�:" + mNextMissionNum);
        Debug.Log("�ԍ�:" + randomNumber);
        name = mNowBossTable.infomation[randomNumber].name;
        //�ۗ��ʎg�p�i�ϓ��J�n�j
        mController.UseStock(mNowBossTable.infomation[randomNumber].win_lost);

        //������ԍ���ۑ�����
        mBackupNumber = randomNumber;

        CS_HpGuage guage = GameObject.Find("HpGuage").GetComponent<CS_HpGuage>();
        guage.pefName = name;

        mCoroutine = StartCoroutine(AfterLottery(randomNumber));//������㏈���𑖂点��
        yield return null;
    }

    bool CheackPlayerAttack(int _random)
    {
        int[] competitionNum = new int[] { 4, 5,6,10,12,14,17,19,20,24 };
        for (int i = 0; i < competitionNum.Length; i++)
        {
            if (competitionNum[i] == _random + 1)
            {
                return true;
                Debug.Log("���荇���V�[���𐶐����܂�");
            }
        }
        return false;
    }

    bool CheckLastAttack()
    {
        float attackPow = Mathf.Ceil(mBossData.PlayerOneAttackPow / mBossData.BossOneBlockHp);
        float bossHp = mBossData.BossStatus.infomations[mBossData.BossNumber].hp - attackPow;
        return bossHp <= 0.0f;

    }

    private int ChangePerfNumber(int _randomNum)
    {
        _randomNum++;//��U���ڔԍ��ɂ���
        switch(mBossData.UseTiket)
        {
            case CS_BossPhaseData.USE_TIKET.SPECOAL:
                _randomNum = 25;
                break;
            case CS_BossPhaseData.USE_TIKET.PARTNER:
                _randomNum = 29;
                break;
            case CS_BossPhaseData.USE_TIKET.PREEMPTIVE_ATTACK:
                _randomNum = 4;
                break;
        }
      
        return _randomNum -1;
    }

    private void StartNextPhase()
    {
        //HP�����ɖ߂�
        mPlayerStatus.hp = mPlayerStatus.backupStatus.hp;
        mBossStatus.infomations[mBossNumber].hp = mBossStatus.initialValues[mBossNumber].hp;
        Destroy(mNoDevObj);
        Destroy(mGuageObj);
        Debug.Log("���̃t�F�[�Y�ֈڍs���܂�");
        mController.NumberRailResetTrans();
        mController.ChangePhase(CS_Controller.PACHINKO_PHESE.SET);
        mController.CreateController();
        Destroy(this.gameObject);

    }

    //�����W����
    private void NoDevelopment(int _perfNumber)
    {
        float[] valTime = new float[3] { 8f, 10f, 12f };
        mController.VariationTimer = valTime[_perfNumber];//�ϓ����Ԑݒ�
        mData.NoDevelpment = true;//�����W�t���O��true
        mNextMissionNum = -1;
        mBackupNumber = _perfNumber;
        //�ۗ��ʎg�p�i�ϓ��J�n�j
        mController.UseStock(WIN_LOST.LOST);
        mController.PerformanceSemiFinish = true;
        mController.PerformanceFinish();//���o�͍s��Ȃ��̂ŏI���t���O�𗧂Ă�
        string name = mNowBossTable.infomation[_perfNumber].name;
        CS_HpGuage guage = GameObject.Find("HpGuage").GetComponent<CS_HpGuage>();
        guage.pefName = name;

        mData.NoDevelpment = true;

        Debug.Log("���o�ԍ�" + name);
    }

    
    private int CheckReLottely(int _val)
    {
        List<int> isRerotteryNums = new List<int> { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13,14, 17,18, 19, 20, 23, 24, 25, 26, 29, 30 };
        int next = -1;
        for (int i = 0; i < isRerotteryNums.Count; i++)
        {
            if (isRerotteryNums[i] == _val + 1)
            {
                next = mBossUnique.ReLottery(i);
                break;
            }
        }
        return next;
    }

    
    private IEnumerator AfterLottery(int _perfNum)
    {
        yield return new WaitForSeconds(2f);
        if(mCompetitionObj != null) 
        {
            Destroy(mCompetitionObj); //���荇���̃I�u�W�F�N�g������
            GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_CommonData>().ButtonsInteractable();//�{�^����L���ɂ���
        }
        mNoDevObj.SetActive(false);
        //�C�x���g�n���h�����s
        PlayPerformance(_perfNum);
        //mController.PerformanceSemiFinish = true;
        //���o���I���܂ŏ�����i�߂Ȃ�
        while (!mController.GetPatternVariationFinish()) { yield return null; }
        //Debug.Log("���o�I��(��)" + bigController.PerformanceSemiFinish);

        mNextMissionNum = mBossUnique.DesisionFlag(_perfNum - 3);

        CS_HpGuage guage = GameObject.Find("HpGuage").GetComponent<CS_HpGuage>();

        while (!guage.HpUpdateFinish) { yield return null; }

        Debug.Log("HP�X�V�I��");


        //���o�I����m�点��
        GameObject rootObject = transform.root.gameObject;
        if (rootObject.GetComponent<CS_PerformanceFinish>() == null)
        {
            //3�b��ɉ��o������
            rootObject.AddComponent<CS_PerformanceFinish>().DestroyConfig(false, 0f);
        }
        //mController.PerformanceFinish();
        while (!mController.GetPerformanceFinish()) { yield return null; }

        mNoDevObj.SetActive(true);

        mCoroutine = null;
    }

    private void PlayPerformance(int _num)
    {
        if(mNowBossTable.infomation[_num].performance != null)
        {
            if (CheckLastAttack())
            {
                mCompetitionObj.SetActive(false);
                mNoDevObj.SetActive(false);
                Instantiate(mLastAttack, Vector3.zero, mLastAttack.transform.rotation);
                return;
            }
            GameObject obj = Instantiate(mNowBossTable.infomation[_num].performance, Vector3.zero, mNowBossTable.infomation[_num].performance.transform.rotation);
            obj.name = mNowBossTable.infomation[_num].performance.name;//Clone���t���Ȃ��悤�ɂ���
            obj.GetComponent<CS_SetPositionPerfPos>().Start();
        }
        else
        {
            mController.PerformanceSemiFinish = true;
            mController.PerformanceFinish();
        }
        
    }

   
    //�o�^����Ă���C�x���g�n���h�������ׂč폜
    public static void RemoveAllHandlers()
    {
        // OnPlayPerformance �ɉ�������̃n���h�����o�^����Ă���ꍇ
        if (OnPlayPerformance != null)
        {
            // OnPlayPerformance �ɓo�^����Ă���S�Ẵn���h�����擾
            Delegate[] handlers = OnPlayPerformance.GetInvocationList();

            // ���ׂẴn���h��������
            foreach (Delegate handler in handlers)
            {
                OnPlayPerformance -= (Performance)handler;
            }
        }
    }
}
