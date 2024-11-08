//---------------------------------
//ボスフェーズ
//担当者：野崎
//---------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using System.Diagnostics;

public class CS_BossPheseController : MonoBehaviour
{

    [SerializeField]
    CSO_MissionPhaseTable mProbabilityStatus;
    List<float> mProbabilities = new List<float>();

    //演出が終わったか否か
    private bool mPerformanceFinish = true;

    //-----------------------イベントハンドラ-----------------------
    public delegate void Performance(int _performance);

    //演出を流すトリガーイベント
    public static event Performance OnPlayPerformance;
    //-------------------------------------------------------------

    int debugCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        //
       
    }

    // Update is called once per frame
    void Update()
    {
        //CheckLottery();
        //演出が終わっていないなら終了
        if (!mPerformanceFinish) { return; }

        //保留玉を使用

        //演出抽選
        int randomNumber = CS_LotteryFunction.LotPerformance(mProbabilities);
        mPerformanceFinish = false;
        //演出開始トリガーをON
        OnPlayPerformance(randomNumber);
    }



    private void CheckLottery()
    {
        if (debugCount < 10000)
        {
            int randomNumber = CS_LotteryFunction.LotPerformance(mProbabilities);
            //Debug.Log("ランダムに選ばれた演出: " + mProbabilityStatus.performances[randomNumber].name);
            debugCount++;

            if (debugCount >= 10000)
            {
                Debug.Log("10000回終了");
            }
        }

    }

    //演出終了関数
    public void PerformanceFinish()
    {
        mPerformanceFinish = true;
    }

    //登録されているイベントハンドラをすべて削除
    public static void RemoveAllHandlers()
    {
        // OnPlayPerformance に何かしらのハンドラが登録されている場合
        if (OnPlayPerformance != null)
        {
            // OnPlayPerformance に登録されている全てのハンドラを取得
            Delegate[] handlers = OnPlayPerformance.GetInvocationList();

            // すべてのハンドラを解除
            foreach (Delegate handler in handlers)
            {
                OnPlayPerformance -= (Performance)handler;
            }
        }
    }
}
