using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_RushPhaseController : MonoBehaviour
{
    [SerializeField, Header("大当たり演出プレハブ")]
    private GameObject mJackPotPerf;

    [SerializeField, Header("ゲームカウントプレハブ")]
    private GameObject mGameCountPerf;

    [SerializeField, Header("突入プレハブ")]
    private GameObject mRushIn;

    [SerializeField, Header("継続プレハブ")]
    private GameObject mRushConti;

    private static bool mContinue = false;

    private CS_BP_DrawCount mDrawCount;

    private int mGameCount = 100;//入賞数

    private CS_Controller mBigController;//司令塔(大)
    private CS_RushPhaseData mData;

    // 大当たりの確率
    private const float mBigWinProbability = 1.0f / 94.2f;

    Coroutine mWaitCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        mBigController = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_Controller>();//司令塔大を取得
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

        //入賞数が3？
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
                //ボーナスシーンへ
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

        //変動できるかを取得
        bool variationStart = mBigController.CanVariationStart();
        if (!variationStart) { return; }//falseなら終了

       
        //演出抽選
        bool jackPot = CS_LotteryFunction.LotJackpotFloat(mBigWinProbability);
        WIN_LOST winlos = WIN_LOST.LOST;
        if (jackPot) 
        { 
            winlos = WIN_LOST.BIG_WIN;
            mData.JackPot = true;
        }
        

        //保留玉使用（変動開始）
        mBigController.UseStock(winlos);

        PlayPerf(jackPot);
        mGameCount--;//入賞数減算
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
            mBigController.PerformanceFinish();//演出は行わないので終了フラグを立てる
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
