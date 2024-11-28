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
        //�{�X�ԍ��ɉ���������ݒ�
        SetBossInfomation(); 
    }

    private void SetBossInfomation()
    {
        switch(mBossNumber)
        {
            case 0:
                mBossUnique = new CS_Boss1_Unique();
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

        int randomNumber = CS_LotteryFunction.LotNormalInt(mNowBossTable.infomation.Count);//0~��񐔕��̊ԂŒ�����

        mGameCount--;//�Q�[���J�E���g���ւ炷

        string name = mNowBossTable.infomation[randomNumber].name;

        //�����W
        if (randomNumber <= 2)
        {
            NoDevelopment(randomNumber);
            return;
        }
        else { mController.VariationTimer = 4f; }

        mBossData.NoDevelpment = false;//�����W�t���O��false

        //�Ē��I�m�F�B���I����Ύ��̃~�b�V��������
        mNextMissionNum = CheckReLottely(randomNumber);
        //���̉��o�ԍ���-1����Ȃ��Ȃ�Ē��I���ʂ�����
        if (mNextMissionNum != -1) { randomNumber = mNextMissionNum; }

        //�ۗ��ʎg�p�i�ϓ��J�n�j
        mController.UseStock(mNowBossTable.infomation[randomNumber].win_lost);

        //������ԍ���ۑ�����
        mBackupNumber = randomNumber;

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
        string name = mNowBossTable.infomation[_perfNumber].name;
        Debug.Log("���o�ԍ�" + name);
    }

    private int CheckReLottely(BossPhaseInfomation info)
    {
        
        return -1;

        {
            //REPLAY_B replay = info.replay;
            //float percentage = mPlayerStatus.charaStatus.preemptiveAttack;



            //switch (replay)
            //{
            //    case REPLAY_B.TRUE_P1:
            //        {
            //            int[] preemptive = new int[2] { 4, 10 };
            //            float playerHp = mPlayerStatus.hp;
            //            playerHp -= mBossStatus.infomations[mBossNumber].attack * 0.333f;//�{�X�̍U���́i��j��hp�������
            //            if (playerHp <= 0f)//�̗͂������Ȃ�Ε���������
            //            {
            //                percentage = mPlayerStatus.charaStatus.revaival;
            //                if (!ReLot(percentage))//���I���Ȃ�������s�k
            //                { 
            //                    playerHp = 0;

            //                }
            //                else 
            //                {

            //                    next = CS_LotteryFunction.LotNormalInt(preemptive.Length)-1; 
            //                }//���I����΍��ڔԍ�4�̂�Ԃ�
            //            }
            //            else//�搧�U���̒l�ōĒ��I
            //            {
            //                if (ReLot(percentage)) { next = CS_LotteryFunction.LotNormalInt(preemptive.Length) - 1; }//���I����΍��ڔԍ�4�̂�Ԃ�
            //            }

            //            //�v���C���[�̗͍X�V
            //            mPlayerStatus.hp = playerHp;
            //        }
            //        break;
            //    case REPLAY_B.TRUE_P2:
            //        {

            //            float playerHp = mPlayerStatus.hp;
            //            playerHp -= mBossStatus.infomations[mBossNumber].attack * 0.333f;//�{�X�̍U���́i��j��hp�������
            //            if (playerHp <= 0f)//�̗͂������Ȃ�Ε���������
            //            {
            //                percentage = mPlayerStatus.charaStatus.revaival;
            //                if (!ReLot(percentage))//���I���Ȃ�������s�k
            //                {
            //                    playerHp = 0;

            //                }
            //                else 
            //                {
            //                    //���I����ΐ搧�U���̍��ڔԍ���Ԃ�
            //                    next = 3; 
            //                }
            //            }
            //            //�v���C���[�̗͍X�V
            //            mPlayerStatus.hp = playerHp;
            //        }
            //        break;
            //    case REPLAY_B.TRUE_P3:

            //        break;
            //    case REPLAY_B.TRUE_P4:

            //        break;
            //    case REPLAY_B.TRUE_P5:

            //        break;
            //}
        }
    }

    private int CheckReLottely(int _val)
    {
        List<int> isRerotteryNums = new List<int> { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 17, 19, 20, 23, 24, 25, 26, 29, 30 };
        int next = -1;
        for (int i = 0; i < isRerotteryNums.Count; i++)
        {
            if (i == _val + 1)
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
        PlayPerformance(_perfNum);

        //���o���I���܂ŏ�����i�߂Ȃ�
        while (!mController.GetPatternVariationFinish()) { yield return null; }
        //Debug.Log("���o�I��(��)" + bigController.PerformanceSemiFinish);

        //GameObject JackPotPerf = null;
        ////�����艉�o�t���O��true�Ȃ炻�̉��o����
        //if (mController.JackPotPerf)
        //{
        //    JackPotPerf = Instantiate(mCutIn, mCutIn.transform.position, mCutIn.transform.rotation);
        //}

        ////���o���I���܂ŏ�����i�߂Ȃ�
        //while (JackPotPerf) { yield return null; }

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
