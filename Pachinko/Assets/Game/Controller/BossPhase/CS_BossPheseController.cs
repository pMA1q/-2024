//---------------------------------
//�{�X�t�F�[�Y
//�S���ҁF���
//---------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using System.Diagnostics;

public class CS_BossPheseController : MonoBehaviour
{
    [SerializeField, Header("�{�X�̃e�[�u�����X�g")]
    private List<CSO_BossPhaseTable> mBossTables;

    [SerializeField, Header("�̗̓Q�[�W")]
    private GameObject mHpGuage;

    private CSO_BossPhaseTable mNowBossTable;

    private CS_BossUnique mBossUnique;

    private CS_BossPhaseData mBossData;

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
    private int mGameCount = 10;
    public int GameCount
    {
        set { mGameCount = value; }
        get { return mGameCount; }
    }

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
        mController = GameObject.Find("BigController").GetComponent<CS_Controller>();//�i�ߓ�����擾
        mBossData = GameObject.Find("BigController").GetComponent<CS_BossPhaseData>();
        mBossData.ResetData();//�{�X�t�F�[�Y�f�[�^�̊e�t���O�����Z�b�g����
        mBossStatus = mBossData.BossStatus;
        mPlayerStatus = GameObject.Find("BigController").GetComponent<CS_MissionPhaseData>().PlayerStatus;

        //�̗̓Q�[�W����
        GameObject guage = Instantiate(mHpGuage, Vector3.zero, Quaternion.identity);
        guage.name = mHpGuage.name;//Clone���t���Ȃ��悤�ɕύX
        guage.GetComponent<CS_HpGuage>().Init();
        //�{�X�ԍ��ɉ���������ݒ�
        SetBossInfomation();

        
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

        //�c��Q�[������0�ȉ��Ŏ��̃~�b�V�����̃t���O�������Ă��Ȃ��H
        if (mGameCount <= 0 && mNextMissionNum == -1)
        {
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

        //int randomNumber = CS_LotteryFunction.LotNormalInt(mNowBossTable.infomation.Count);//0~��񐔕��̊ԂŒ�����
        int randomNumber = 4;

        mGameCount--;//�Q�[���J�E���g���ւ炷

        string name = mNowBossTable.infomation[randomNumber].name;

        //�����W
        if (randomNumber <= 2)
        {
            NoDevelopment(randomNumber);
            return;
        }
        else
        {
            CS_BossPhaseData data = GameObject.Find("BigController").GetComponent<CS_BossPhaseData>();
            data.NoDevelpment = false;
            mController.VariationTimer = 4f; 
        }

        mBossData.NoDevelpment = false;//�����W�t���O��false

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
    }
    private void StartNextPhase()
    {
        //HP�����ɖ߂�
        mPlayerStatus.hp = mPlayerStatus.backupStatus.hp;
        mBossStatus.infomations[mBossNumber].hp = mBossStatus.initialValues[mBossNumber].hp;
        Debug.Log("���̃t�F�[�Y�ֈڍs���܂�");
        //mController.ChangePhase(CS_Controller.PACHINKO_PHESE);
        Destroy(this.gameObject);

    }

    //�����W����
    private void NoDevelopment(int _perfNumber)
    {
        float[] valTime = new float[3] { 8f, 10f, 10f };
        mController.VariationTimer = valTime[_perfNumber];//�ϓ����Ԑݒ�
        mBossData.NoDevelpment = true;//�����W�t���O��true
        mNextMissionNum = -1;
        mBackupNumber = _perfNumber;
        //�ۗ��ʎg�p�i�ϓ��J�n�j
        mController.UseStock(WIN_LOST.LOST);
        mController.PerformanceFinish();//���o�͍s��Ȃ��̂ŏI���t���O�𗧂Ă�
        mController.PerformanceSemiFinish = true;
        string name = mNowBossTable.infomation[_perfNumber].name;
        CS_HpGuage guage = GameObject.Find("HpGuage").GetComponent<CS_HpGuage>();
        guage.pefName = name;

        CS_BossPhaseData data = GameObject.Find("BigController").GetComponent<CS_BossPhaseData>();
        data.NoDevelpment = true;

        Debug.Log("���o�ԍ�" + name);
    }

    
    private int CheckReLottely(int _val)
    {
        List<int> isRerotteryNums = new List<int> { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 17,18, 19, 20, 23, 24, 25, 26, 29, 30 };
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
        yield return new WaitForSeconds(0.5f);
        //�C�x���g�n���h�����s
        //PlayPerformance(_perfNum);
        mController.PerformanceSemiFinish = true;
        //���o���I���܂ŏ�����i�߂Ȃ�
        while (!mController.GetPatternVariationFinish()) { yield return null; }
        //Debug.Log("���o�I��(��)" + bigController.PerformanceSemiFinish);

        mNextMissionNum = mBossUnique.DesisionFlag(_perfNum - 3);

        CS_HpGuage guage = GameObject.Find("HpGuage").GetComponent<CS_HpGuage>();

        while (!guage.HpUpdateFinish) { yield return null; }

        mController.PerformanceFinish();

        mCoroutine = null;
    }

    private void PlayPerformance(int _num)
    {
        Instantiate(mNowBossTable.infomation[_num].performance, Vector3.zero, Quaternion.identity);
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
