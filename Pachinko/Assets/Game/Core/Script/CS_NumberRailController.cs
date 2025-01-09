using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_NumberRailController : MonoBehaviour
{
    [SerializeField, Header("0:Left,1:Center,2:Right")]
    private CS_NumberRail[] mNumRails = new CS_NumberRail[3];

    private CS_Controller mBigCtrl;
    private CS_RushPhaseData mRushData;

    private int[] mNumbers = new int[3];

    Coroutine mCoroutine = null;  // ƒRƒ‹[ƒ`ƒ“‚ÌÀs‚ğŠÇ—‚·‚é•Ï”

    IEnumerator enumerator;

    private float mVariationTime = 8.0f;

    private float mNowTime = 0.0f;
    private float mAlpha = 1.0f;

    private bool isVariation = false;

    private void Start()
    {
        mBigCtrl = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_Controller>();
        if (!mBigCtrl) { Debug.LogError("BigController‚ª–³‚¢"); }
        mRushData = mBigCtrl.GetComponent<CS_RushPhaseData>();
        enumerator = null;
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

        //for(int i = 0; i < 3; i++) { mNumRails[i].ChangeAlpha(mAlpha); }
    }

    public void StartPattenRail()
    {
        if(enumerator == null)
        { 
            if(mBigCtrl.GetPhese()== CS_Controller.PACHINKO_PHESE.RUSH)
            {
                enumerator = RealTexRush();
                Debug.Log("ƒ‰ƒbƒVƒ…•Ï“®");
            }
            else {enumerator = RealTex();}
        }
        if(mCoroutine == null && enumerator != null)
        {
            mVariationTime = mBigCtrl.VariationTimer-1.0f;//•Ï“®ŠÔİ’è
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

    private IEnumerator RealTexRush()
    {
        Vector3[] scaledefa = new Vector3[3];
        for (int i = 0; i < 3; i++) { scaledefa[i] = mNumRails[i].GetComponent<RectTransform>().localScale; }

        for (int i = 0; i < 3; i++)
        {
            Vector3 scale = mNumRails[i].GetComponent<RectTransform>().localScale;
            scale *= 1.5f;
            mNumRails[i].GetComponent<RectTransform>().localScale = scale;
        }
        int[] number = new int[] { mBigCtrl.GetPatterns()[0], mBigCtrl.GetPatterns()[2], mBigCtrl.GetPatterns()[1] };
        yield return new WaitForSeconds(0.2f);
        if (mRushData.JackPot)
        {
            Debug.Log("‘å“–‚½‚è");
            for (int i = 0; i < 3; i++) { mNumRails[i].ChangeAlpha(0.0f); }
            Debug.Log("F‚©‚¦");
            yield return new WaitForSeconds(2.8f);
            for (int i = 0; i < 3; i++)
            {
                mNumRails[i].StopStartRush(number[i]);
                mNumRails[i].ChangeAlpha(1.0f);
            }
            
            yield return new WaitForSeconds(5.0f);
            //i—ß“ƒ‚É}•¿•Ï“®I—¹‚ğ“`‚¦‚é
            mBigCtrl.PatternVariationFinish();

            isVariation = false;
            mNowTime = 0.0f;

            mCoroutine = null;
            enumerator = null;
            yield break;
        }
        
      
        //int[] number = new int[] { 1, 2, 3 };
        for (int i = 0; i < 3; i++)
        {
            Vector3 scale = mNumRails[i].GetComponent<RectTransform>().localScale;
            scale = scaledefa[i];
            mNumRails[i].StopStartRush(number[i]);
            mNumRails[i].GetComponent<RectTransform>().localScale = scale;
        }
        yield return new WaitForSeconds(0.7f);
        //i—ß“ƒ‚É}•¿•Ï“®I—¹‚ğ“`‚¦‚é
        mBigCtrl.PatternVariationFinish();

        isVariation = false;
        mNowTime = 0.0f;

        mCoroutine = null;
        enumerator = null;

    }

    private IEnumerator RealTex7()
    {
        while (!mBigCtrl.PerformanceSemiFinish) { yield return null; }
        mNumRails[0].StopStart(7);

        mNumRails[2].StopStart(7);

        mNumRails[1].StopStart(7);

        mBigCtrl.Is777 = true;

        yield return new WaitForSeconds(2.0f);
        //i—ß“ƒ‚É}•¿•Ï“®I—¹‚ğ“`‚¦‚é
        mBigCtrl.PatternVariationFinish();

        isVariation = false;
        mNowTime = 0.0f;
    }
    //}•¿•Ï“®
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

        if (missionData.NoDevelpment) { yield return new WaitForSeconds(mVariationTime -2f); }//•Ï“®ŠÔ‚ª‰ß‚¬‚é‚Ü‚Åˆ—‚ği‚ß‚È‚¢
        else 
        {
            yield return new WaitForSeconds(mVariationTime - 2f);
            while (!mBigCtrl.PerformanceSemiFinish) { yield return null; } //‰‰oI—¹‰¼ƒtƒ‰ƒO‚ªtrue‚É‚È‚é‚Ü‚Åˆ—‚ği‚ß‚È‚¢
        }
        

        mNumRails[0].StopStart(mBigCtrl.GetPatterns()[0]);
        yield return new WaitForSeconds(1.0f);
        mNumRails[2].StopStart(mBigCtrl.GetPatterns()[2]);
        yield return new WaitForSeconds(1.0f);
        while (!mBigCtrl.PerformanceSemiFinish) { yield return null; }
        mNumRails[1].StopStart(mBigCtrl.GetPatterns()[1]);

        yield return new WaitForSeconds(1.5f);
        //i—ß“ƒ‚É}•¿•Ï“®I—¹‚ğ“`‚¦‚é
        mBigCtrl.PatternVariationFinish();

        isVariation = false;
        mNowTime = 0.0f;

        mCoroutine = null;
        enumerator = null;

        yield return null;
    }
}
