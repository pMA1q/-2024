using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_NumberRailController : MonoBehaviour
{
    [SerializeField, Header("0:Left,1:Center,2:Right")]
    private CS_NumberRail[] mNumRails = new CS_NumberRail[3];

    private CS_Controller mBigCtrl;

    private int[] mNumbers = new int[3];

    Coroutine mCoroutine = null;  // コルーチンの実行を管理する変数

    IEnumerator enumerator;

    private float mVariationTime = 8.0f;

    private float mNowTime = 0.0f;
    private float mAlpha = 1.0f;

    private bool isVariation = false;

    private void Start()
    {
        mBigCtrl = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_Controller>();
        if (!mBigCtrl) { Debug.LogError("BigControllerが無い"); }
        //for (int i = 0; i < 3; i++) { mNumRails[i].ChangeAlpha(1.0f); }
        enumerator = RealTex();
    }
    // Update is called once per frame
    void Update()
    {
        
       // if (isVariation) { ChangeAlpha(); }

    }

    private void ChangeAlpha()
    {
        float newAlpha = 0.5f - Time.deltaTime / mVariationTime / 2;

        mNowTime += Time.deltaTime;
        if(mNowTime >= mVariationTime / 2) { newAlpha = mAlpha + Time.deltaTime*2 / mVariationTime / 2; }

        mAlpha = newAlpha;

        for(int i = 0; i < 3; i++) { mNumRails[i].ChangeAlpha(mAlpha); }
    }

    public void StartPattenRail()
    {
        if(enumerator == null) { enumerator = RealTex(); }
        if(mCoroutine == null && enumerator != null)
        {
            mVariationTime = mBigCtrl.VariationTimer-1.0f;//変動時間設定
            mCoroutine = StartCoroutine(enumerator);
        }
    }

    public void Rail777()
    {
        StopCoroutine(enumerator);
        enumerator = null;
        mCoroutine = null;

        StartCoroutine(RealTex7()); 
    }


    private IEnumerator RealTex7()
    {
        while (!mBigCtrl.PerformanceSemiFinish) { yield return null; }
        mNumRails[0].StopStart(7);

        mNumRails[2].StopStart(7);

        
        mNumRails[1].StopStart(7);

        mBigCtrl.Is777 = true;

        yield return new WaitForSeconds(2.0f);
        //司令塔に図柄変動終了を伝える
        mBigCtrl.PatternVariationFinish();

        isVariation = false;
        mNowTime = 0.0f;
    }
    //図柄変動
    private IEnumerator RealTex()
    {
        for(int i = 0; i < 3; i++)
        {
            mNumRails[i].StartVariation();
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2f);
        isVariation = true;

        CS_CommonData missionData = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_CommonData>();

        if (missionData.NoDevelpment) { yield return new WaitForSeconds(mVariationTime -2f); }//変動時間が過ぎるまで処理を進めない
        else 
        {
            yield return new WaitForSeconds(mVariationTime - 2f);
            while (!mBigCtrl.PerformanceSemiFinish) { yield return null; } //演出終了仮フラグがtrueになるまで処理を進めない
        }
        

        mNumRails[0].StopStart(mBigCtrl.GetPatterns()[0]);
        yield return new WaitForSeconds(1.0f);
        mNumRails[2].StopStart(mBigCtrl.GetPatterns()[2]);
        yield return new WaitForSeconds(1.0f);
        while (!mBigCtrl.PerformanceSemiFinish) { yield return null; }
        mNumRails[1].StopStart(mBigCtrl.GetPatterns()[1]);

        yield return new WaitForSeconds(1.5f);
        //司令塔に図柄変動終了を伝える
        mBigCtrl.PatternVariationFinish();

        isVariation = false;
        mNowTime = 0.0f;

        mCoroutine = null;
        enumerator = null;

        yield return null;
    }
}
