//---------------------------------
//準備フェーズ司令塔
//担当者：中島
//---------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;  // LINQ��g�����߂ɕK�v
using UnityEngine.UI;
using Unity.VisualScripting;


public class CS_SetPheseController : MonoBehaviour
{
    //[SerializeField]
    //CSO_SetPhaseStatus mProbabilityStatus;
    //List<float> mProbabilities = new List<float>();

    [SerializeField,Header("準備フェーズのテーブル")]
    private CSO_SetPhaseTable mMissionStatus;

    [SerializeField, Header("ミッションselectのプレハブ")]
    private GameObject mMisstionSelect;

    [SerializeField, Header("キュイン(テスト用なので後で消す)")]
    private GameObject mCuinSE;

    private int mPrizesNum = 0;//入賞数

    private CS_Controller mBigController;//司令塔(大)

 //-----------------------イベントハンドラ-----------------------
    public delegate void Performance(int _performance);

    //登録時に使用
    public static event Performance OnPlayPerformance;
 //-------------------------------------------------------------



    int debugCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        
        mBigController = GameObject.Find("BigController").GetComponent<CS_Controller>();//司令塔大を取得
        
        //ミッション選択オブジェクトを生成
        GameObject instance = Instantiate(mMisstionSelect, Vector3.zero, mMisstionSelect.transform.rotation);
        instance.name = mMisstionSelect.name; // (Clone)が付かないように名前をオリジナルの名前に戻す
    }

    // Update is called once per frame
    void Update()
    {

        //入賞数が3？
        if (mPrizesNum == 3 && mBigController.GetVariationFinish())
        {
            //別物を参照しているのでシーンからMissionSelectを見つけてサイド取得
            mMisstionSelect = GameObject.Find("MissionSelect");
            //プレイヤーがミッションを選択する状態を開始する
            mMisstionSelect.GetComponent<CS_LotMission>().PlaySelectMode();
            RemoveAllHandlers();
            Destroy(this.gameObject);
            return;
        }


        //変動できるかを取得
        bool variationStart = mBigController.CanVariationStart();
        if (!variationStart) { return; }//falseなら終了

        // イベントハンドラはnullなら終了
        if (OnPlayPerformance == null) { return; }

        //保留玉が無いなら終了
        if(mBigController.GetStock() == 0) { return; }

        //変動時間設定
        mBigController.VariationTimer = 3.0f;

        GameObject.Find("BigController").GetComponent<CS_MissionData>().NoDevelpment = false;//無発展フラグをfalse

       //保留玉使用（変動開始）
       mBigController.UseStock(WIN_LOST.LOST);
    
        //テスト
        if (mBigController.GetJuckpot()) { Instantiate(mCuinSE, mCuinSE.transform.position, mCuinSE.transform.rotation); }

        //演出抽選
        int randomNumber = CS_LotteryFunction.LotNormalInt(mMissionStatus.infomation[mPrizesNum].mission.Count -1);

        //ミッション内容保存
        CS_MissionData data = GameObject.Find("BigController").GetComponent<CS_MissionData>();
        data.SaveMissionContents(mPrizesNum, randomNumber);

        mPrizesNum++;//入賞数加算

        if (OnPlayPerformance != null)
        {
            //イベントハンドラ実行
            OnPlayPerformance(randomNumber);
        }
           
    }



    private void CheckLottery()
    {
        if (debugCount < 10000)
        {
            //int randomNumber = CS_LotteryFunction.LotPerformance(mProbabilities);
            //Debug.Log("�����_���ɑI�΂ꂽ���o: " + mProbabilityStatus.performances[randomNumber].name);
            //debugCount++;

            //if (debugCount >= 10000)
            //{
            //    Debug.Log("10000��I��");
            //}
        }

    }

   
    public static void RemoveAllHandlers()
    {
        // OnPlayPerformanceに登録されている関数を消す
        if (OnPlayPerformance != null)
        {
            //登録されているものを取得
            Delegate[] handlers = OnPlayPerformance.GetInvocationList();

            foreach (Delegate handler in handlers)
            {
                OnPlayPerformance -= (Performance)handler;
            }
        }
    }

    

}
