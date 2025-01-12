using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_RushPhaseController : MonoBehaviour
{
    [SerializeField, Header("�哖���艉�o�v���n�u")]
    private GameObject mJackPotPerf;

    [SerializeField, Header("�Q�[���J�E���g�v���n�u")]
    private GameObject mGameCountPerf;

    [SerializeField, Header("�˓��v���n�u")]
    private GameObject mRushIn;

    [SerializeField, Header("�p���v���n�u")]
    private GameObject mRushConti;

    private static bool mContinue = false;

    private CS_BP_DrawCount mDrawCount;

    private int mGameCount = 100;//���ܐ�

    private CS_Controller mBigController;//�i�ߓ�(��)
    private CS_RushPhaseData mData;

    // �哖����̊m��
    private const float mBigWinProbability = 1.0f / 94.2f;

    Coroutine mWaitCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        mBigController = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_Controller>();//�i�ߓ�����擾
        mBigController.NumberRailBigger();
        mData = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_RushPhaseData>();
        mData.JackPot = false;
        mGameCountPerf.transform.SetParent(null);
        mDrawCount = mGameCountPerf.GetComponent<CS_BP_DrawCount>();
        mDrawCount.SetCount(mGameCount);

        mWaitCoroutine = StartCoroutine(WaitStart());

        if (mContinue) { Instantiate(mRushConti, Vector3.zero, Quaternion.identity); }
        else{ Instantiate(mRushIn, Vector3.zero, Quaternion.identity); }
    }

    // Update is called once per frame
    void Update()
    { 
        if(mWaitCoroutine != null) { return; }

        //���ܐ���3�H
        if (mGameCount <= 0 && mBigController.GetVariationFinish())
        {
            mContinue = false;
            CS_Denchu denchu = GameObject.Find("Denchu").GetComponent<CS_Denchu>();
            denchu.ActiveStop();
            mBigController.ChangePhase(CS_Controller.PACHINKO_PHESE.SET);
            mBigController.CreateController();
            mBigController.NumberRailResetTrans();
            Destroy(this.gameObject);
            Destroy(mGameCountPerf);
            return;
        }

        if(mBigController.GetVariationFinish())
        {
            if(mData.JackPot)
            {
                //�{�[�i�X�V�[����
                mBigController.ChangePhase(CS_Controller.PACHINKO_PHESE.BONUS);
                mBigController.CreateController();
                mBigController.NumberRailResetTrans();
                mContinue = true;
                CS_Denchu denchu = GameObject.Find("Denchu").GetComponent<CS_Denchu>();
                denchu.ActiveStop();
                Destroy(this.gameObject);
                Destroy(mGameCountPerf);
                return;
            }
        }

        //�ϓ��ł��邩���擾
        bool variationStart = mBigController.CanVariationStart();
        if (!variationStart) { return; }//false�Ȃ�I��

       
        //���o���I
        bool jackPot = CS_LotteryFunction.LotJackpotFloat(mBigWinProbability);
        WIN_LOST winlos = WIN_LOST.LOST;
        if (jackPot) 
        { 
            winlos = WIN_LOST.BIG_WIN;
            mData.JackPot = true;
        }
        

        //�ۗ��ʎg�p�i�ϓ��J�n�j
        mBigController.UseStock(winlos);

        PlayPerf(jackPot);
        mGameCount--;//���ܐ����Z
        mDrawCount.SetCount(mGameCount);
    }
    private void PlayPerf(bool _jackpot)
    {
        if(_jackpot)
        {
            mBigController.Is777 = true;
            if (mJackPotPerf != null)
            {
                StartCoroutine(WaitStartPerf());
            }
        }
        else
        {
            mBigController.PerformanceSemiFinish = true;
            mBigController.PerformanceFinish();//���o�͍s��Ȃ��̂ŏI���t���O�𗧂Ă�
        }
    }
    private IEnumerator WaitStartPerf()
    {
        yield return new WaitForSeconds(0.2f);
        Instantiate(mJackPotPerf, Vector3.zero, mJackPotPerf.transform.rotation);
    }

    private IEnumerator WaitStart()
    {
        yield return new WaitForSeconds(6.0f);
        mWaitCoroutine = null;
        CS_Denchu denchu = GameObject.Find("Denchu").GetComponent<CS_Denchu>();
        denchu.ActiveStart();
    }
}
