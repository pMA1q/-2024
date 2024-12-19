//---------------------------------
//司令塔（大）
//担当者：中島
//---------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CS_Controller : MonoBehaviour
{
    //パチンコの状態
    public enum PACHINKO_PHESE
    {
        SET,    //準備フェーズ
        MISSION,//ミッションフェーズ
        BOSS,   //ボスフェーズ       
        BOUNUS,
        RUSH    //ラッシュフェーズ
    }

    
    [SerializeField, Header("司令塔コントローラー")]
    List<GameObject> mCtrls = new List<GameObject>();
    [SerializeField, Header("各フェーズのBGM")]
    List<GameObject> mBGM = new List<GameObject>();


    [SerializeField, Header("ヘソ")]
    private CS_Stock mHeso;

    [SerializeField, Header("図柄表示")]
    private CS_DrawPattern mDrawNum;

    [SerializeField, Header("図柄表示")]
    private CS_NumberRailController mDrawNum2;

    private Vector3 mDefaultNumberScale;


    private Vector3 mDefaultNumberPosition;

    [SerializeField]
    private Vector3 mBigNumberScale;

    [SerializeField]
    private Vector3 mBigNumberPosition;

    public GameObject NumberRail { get{ return mDrawNum2.gameObject; } }
    public CS_NumberRailController NumberRailController { get{ return mDrawNum2; } }

    [SerializeField]
    private PACHINKO_PHESE mNowPhese = PACHINKO_PHESE.SET;//現在のフェーズ
    private PACHINKO_PHESE mPrevPhese = PACHINKO_PHESE.SET;//前ののフェーズ

    private GameObject mNowBGM;

    private int mStock = 0;//保留玉数
    
    private bool mPatternVariationFinish = true;//図柄変動終了フラグ
    private bool mPerformanceFinish = true;//演出終了フラグ
    private bool mPerformanceSemiFinish = true;//演出終了仮フラグ(演出が終わってから消えるまでのフラグ)
    private bool mWaitChoice = false;//選択待ちフラグ

    public bool PerformanceSemiFinish
    {
        set { mPerformanceSemiFinish = value; }
        get { return mPerformanceSemiFinish; }
    }

    private bool IsJackPotCutIn = false;
    public bool JackPotPerf
    {
        set { IsJackPotCutIn = value; }
        get { return IsJackPotCutIn; }
    }

    private int[] mPattern = new int[3];//図柄

    private float mVariationTimer;

    //変動時間の設定、取得
    public float VariationTimer
    {
        set { mVariationTimer = value; }
        get { return mVariationTimer; }
    }

    public bool WaitChoice
    {
        set { mWaitChoice = value; }
        get { return mWaitChoice; }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateController();

        RectTransform NumberTrans = mDrawNum2.GetComponent<RectTransform>();
        mDefaultNumberScale = NumberTrans.localScale;
        mDefaultNumberPosition = NumberTrans.localPosition ;
    }

    // Update is called once per frame
    void Update()
    {
        //現在のフェーズの前のフェーズが違うなら次のフェーズに行く
       // if(mNowPhese != mPrevPhese) { GoNextPhese(); }
    }

    //現在のフェーズ取得
    public PACHINKO_PHESE GetPhese()
    {
        return mNowPhese;
    }

    //フェーズ切り替え
    public void ChangePhase(PACHINKO_PHESE _nextPhese)
    {
        mPrevPhese = mNowPhese;
        mNowPhese = _nextPhese;
       // GoNextPhese();
    }

    //次のフェーズへ行く
    private void GoNextPhese()
    {
        mPrevPhese = mNowPhese;
       
    }

    public void CreateController()
    {
        if(mNowBGM != null)
        {
            AudioSource audio = mNowBGM.GetComponent<AudioSource>();
            if(audio.isPlaying)
            {
                audio.Stop();
            }
            Destroy(mNowBGM);
        }
        mPerformanceFinish = true;
        mPerformanceSemiFinish = true;
        mPatternVariationFinish = true;
        GameObject ctrl = Instantiate(mCtrls[(int)mNowPhese], transform.position, transform.rotation);
        ctrl.name = mCtrls[(int)mNowPhese].name;
        mNowBGM = Instantiate(mBGM[(int)mNowPhese], Vector3.zero, Quaternion.identity);
    }

    //保留玉を増やす
    public void AddStock()
    {
        mStock++;
    }

    public void UseStock(WIN_LOST _win_lost)
    {
        mStock--;
        mPatternVariationFinish = false;//図柄変動終了フラグをfalse
        mPerformanceFinish = false;//演出終了フラグをfalse
        mPerformanceSemiFinish = false;//演出終了(仮)フラグをfalse

        mPattern = CS_LotteryFunction.PatternLottery2(_win_lost);
        //Debug.Log("図柄:" + mHeso.stock[0][0] + "," + mHeso.stock[0][1] + "," + mHeso.stock[0][2] + ",");
        if (!mDrawNum2.gameObject.activeSelf) { mDrawNum2.gameObject.SetActive(true); }
        if(mDrawNum != null) {  mDrawNum.StartPatternVariation();}
        if(mDrawNum2 != null) {  mDrawNum2.StartPattenRail();}

    }

    public void Set777()
    {
        mDrawNum2.Rail777();
    }

    public void NumberRailBigger()
    {
        RectTransform NumberTrans = mDrawNum2.GetComponent<RectTransform>();
        NumberTrans.localScale = mBigNumberScale;
        NumberTrans.anchoredPosition = mBigNumberPosition;
    }

    public void NumberRailResetTrans()
    {
        RectTransform NumberTrans = mDrawNum2.GetComponent<RectTransform>();
        NumberTrans.localScale = mDefaultNumberScale;
        NumberTrans.localPosition = mDefaultNumberPosition;
    }

    public int[] GetPatterns()
    {
        return mPattern;
    }

    public bool GetJuckpot()
    {
        int[] nowstock = mPattern;
        return nowstock.Length == 3 && nowstock[0] == nowstock[1] && nowstock[1] == nowstock[2];
    }

    //保留玉を取得する
    public int GetStock()
    {
        mStock = mHeso.Count;
        return mStock;
    }

    //演出終了
    public void PerformanceFinish()
    {
        JackPotPerf = false;
        mPerformanceFinish = true;
    }

    public bool GetPerformanceFinish()
    {
        return mPerformanceFinish;
    }

    //図柄変動終了
    public void PatternVariationFinish()
    {
        if (GetJuckpot()) 
        { 
            if(mNowPhese == PACHINKO_PHESE.MISSION)
            {
               
            }
            mPerformanceFinish = false;
            mPerformanceSemiFinish = false;
            JackPotPerf = true;
            GetComponent<CS_CommonData>().LeftAttakerStart(3);
        }
        Debug.Log("図柄終了");
        mPatternVariationFinish = true;
        mHeso.DisableStock();//ストックを削除
    }

    //図柄変動が終了しているか
    public bool GetPatternVariationFinish() { return mPatternVariationFinish; }

    //変動が開始できるか
    public bool CanVariationStart()
    {
        if(GetStock() <= 0) { return false; }
        return mPatternVariationFinish && mPerformanceFinish;
    }

    public bool GetVariationFinish() 
    {
        return mPatternVariationFinish && mPerformanceFinish;
    }
}
