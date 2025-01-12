using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Denchu : MonoBehaviour
{
    [SerializeField]
    private Transform mTargetTrans;

    private Vector3 mDefaultPos;

    private float mOpenTimer = 2.0f;

    private float[] mCloseTimers = new float[4] { 0.5f, 0.5f, 0.5f, 2.0f };

    private int mIndex = 0;

    private bool mIsActive = false;

    private float mTimer = 0.0f;

    private bool mIsOpening = true; // 開閉状態を管理

    // Start is called before the first frame update
    void Start()
    {
        mDefaultPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!mIsActive) return;

        mTimer += Time.deltaTime;

        if (mIsOpening)
        {
            // mTargetTrans の位置まで移動
            this.transform.position = Vector3.Lerp(mDefaultPos, mTargetTrans.position, mTimer / mOpenTimer *100f);

            if (mTimer >= mOpenTimer)
            {
                mTimer = 0.0f;
                mIsOpening = false;
            }
        }
        else
        {
            // mDefaultPos の位置まで移動
            this.transform.position = Vector3.Lerp(mTargetTrans.position, mDefaultPos, mTimer / mCloseTimers[mIndex] * 100f);

            if (mTimer >= mCloseTimers[mIndex])
            {
                mTimer = 0.0f;
                mIndex++;

                if (mIndex >= mCloseTimers.Length)
                {
                    mIndex = 0; // インデックスをリセット
                }

                mIsOpening = true;
            }
        }
    }

    public void ActiveStart()
    {
        mIsActive = true;
        mTimer = 0.0f;
        mIndex = 0;
        mIsOpening = true;
    }

    public void ActiveStop()
    {
        mIsActive = false;
        mTimer = 0.0f;
        mIndex = 0;
        this.transform.position = mDefaultPos;
    }
}
