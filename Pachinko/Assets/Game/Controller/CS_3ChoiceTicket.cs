using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CS_3ChoiceTicket : MonoBehaviour
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
    private Heso mHeso;

    [SerializeField, Header("図柄表示")]
    private CS_DrawPattern mDrawNum;

    private PACHINKO_PHESE mNowPhese = PACHINKO_PHESE.SET;//現在のフェーズ
    private PACHINKO_PHESE mPrevPhese = PACHINKO_PHESE.SET;//前ののフェーズ

    private int mStock = 0;//保留玉数

    private bool mPatternVariationFinish = true;//図柄変動終了フラグ
    private bool mPerformanceFinish = true;//演出終了フラグ



    // Start is called before the first frame update
    void Start()
    {
        Instantiate(mCtrls[(int)mNowPhese], transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        //現在のフェーズの前のフェーズが違うなら次のフェーズに行く
        if (mNowPhese != mPrevPhese) { GoNextPhese(); }
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

    public void UseStock()
    {
        mStock--;
        mPatternVariationFinish = false;//図柄変動終了フラグをfalse
        mPerformanceFinish = false;//演出終了フラグをfalse

        //Debug.Log("図柄:" + mHeso.stock[0][0] + "," + mHeso.stock[0][1] + "," + mHeso.stock[0][2] + ",");
        mDrawNum.StartPatternVariation();//
    }



    //保留玉を取得する
    public int GetStock()
    {
        mStock = mHeso.stock.Count;
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
    }

    //変動が開始できるか
    public bool CanVariationStart()
    {
        return mPatternVariationFinish && mPerformanceFinish;
    }
}
