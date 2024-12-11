using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_NumberRailController : MonoBehaviour
{
    [SerializeField, Header("0:Left,1:Center,2:Right")]
    private CS_NumberRail[] mNumRails = new CS_NumberRail[3];

    private CS_Controller mBigCtrl;

    private int[] mNumbers = new int[3];

    Coroutine mCoroutine = null;  // �R���[�`���̎��s���Ǘ�����ϐ�

    private float mVariationTime = 8.0f;

    private float mNowTime = 0.0f;
    private float mAlpha = 1.0f;

    private bool isVariation = false;

    private void Start()
    {
        mBigCtrl = GameObject.Find("BigController").GetComponent<CS_Controller>();
        if (!mBigCtrl) { Debug.LogError("BigController������"); }
        //for (int i = 0; i < 3; i++) { mNumRails[i].ChangeAlpha(1.0f); }
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
        if(mCoroutine == null)
        {
            mVariationTime = mBigCtrl.VariationTimer-1.0f;//�ϓ����Ԑݒ�
            mCoroutine = StartCoroutine(RealTex());
        }
    }

    //�}���ϓ�
    private IEnumerator RealTex()
    {
        for(int i = 0; i < 3; i++)
        {
            mNumRails[i].StartVariation();
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2f);
        isVariation = true;

        CS_CommonData missionData = GameObject.Find("BigController").GetComponent<CS_CommonData>();

        if (missionData.NoDevelpment) { yield return new WaitForSeconds(mVariationTime -2f); }//�ϓ����Ԃ��߂���܂ŏ�����i�߂Ȃ�
        else 
        {
            yield return new WaitForSeconds(mVariationTime - 2f);
            while (!mBigCtrl.PerformanceSemiFinish) { yield return null; } //���o�I�����t���O��true�ɂȂ�܂ŏ�����i�߂Ȃ�
        }
        

        mNumRails[0].StopStart(mBigCtrl.GetPatterns()[0]);
        yield return new WaitForSeconds(1.0f);
        mNumRails[2].StopStart(mBigCtrl.GetPatterns()[2]);
        yield return new WaitForSeconds(1.0f);
        while (!mBigCtrl.PerformanceSemiFinish) { yield return null; }
        mNumRails[1].StopStart(mBigCtrl.GetPatterns()[1]);

        yield return new WaitForSeconds(1.5f);
        //�i�ߓ��ɐ}���ϓ��I����`����
        mBigCtrl.PatternVariationFinish();

        isVariation = false;
        mNowTime = 0.0f;

        mCoroutine = null;

        yield return null;
    }
}
