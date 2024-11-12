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
        RUSH    //ラッシュフェーズ
    }

    
    [SerializeField, Header("司令塔コントローラー")]
    List<GameObject> mCtrls = new List<GameObject>();

    [SerializeField, Header("ヘソ")]
    private CS_Stock mHeso;

    [SerializeField, Header("図柄表示")]
    private CS_DrawPattern mDrawNum;

    [SerializeField, Header("図柄表示")]
    private CS_NumberRailController mDrawNum2;

    private PACHINKO_PHESE mNowPhese = PACHINKO_PHESE.SET;//現在のフェーズ
    private PACHINKO_PHESE mPrevPhese = PACHINKO_PHESE.SET;//前ののフェーズ

    private int mStock = 0;//保留玉数
    
    private bool mPatternVariationFinish = true;//図柄変動終了フラグ
    private bool mPerformanceFinish = true;//演出終了フラグ

    private int[] mPattern = new int[3];//図柄

    private float mVariationTimer;

    //変動時間の設定、取得
    public float VariationTimer
    {
        set { mVariationTimer = value; }
        get { return mVariationTimer; }
    }

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(mCtrls[(int)mNowPhese], transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        //現在のフェーズの前のフェーズが違うなら次のフェーズに行く
        if(mNowPhese != mPrevPhese) { GoNextPhese(); }
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
        GoNextPhese();
    }

    //次のフェーズへ行く
    private void GoNextPhese()
    {
        mPrevPhese = mNowPhese;
        //司令塔生成
        GameObject smallCtrl = Instantiate(mCtrls[(int)mNowPhese], transform.position, transform.rotation);
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

        mPattern = CS_LotteryFunction.PatternLottery2(_win_lost); 
        //Debug.Log("図柄:" + mHeso.stock[0][0] + "," + mHeso.stock[0][1] + "," + mHeso.stock[0][2] + ",");
        if(mDrawNum != null) {  mDrawNum.StartPatternVariation();}
        if(mDrawNum2 != null) {  mDrawNum2.StartPattenRail();}

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
        mPerformanceFinish = true;
    }

    //図柄変動終了
    public void PatternVariationFinish()
    {
        mPatternVariationFinish = true;
        mHeso.DisableStock();//ストックを削除
    }

    //変動が開始できるか
    public bool CanVariationStart()
    {
        if(GetStock() <= 0) { return false; }
        return mPatternVariationFinish && mPerformanceFinish;
    }
}
