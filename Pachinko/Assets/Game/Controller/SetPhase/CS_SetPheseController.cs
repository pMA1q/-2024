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

    [SerializeField,Header("準備フェーズのテーブル")]
    private CSO_SetPhaseTable mMissionStatus;

    [SerializeField, Header("ミッションselectのプレハブ")]
    private GameObject mMisstionSelect;

    [SerializeField]
    private GameObject mLeftShootCanvas;

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
        
        mBigController = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_Controller>();//司令塔大を取得
        
        //ミッション選択オブジェクトを生成
        GameObject instance = Instantiate(mMisstionSelect, Vector3.zero, mMisstionSelect.transform.rotation);
        instance.name = mMisstionSelect.name; // (Clone)が付かないように名前をオリジナルの名前に戻す

        if(mBigController.GetPrevPhese() == CS_Controller.PACHINKO_PHESE.RUSH)
        {
            Instantiate(mLeftShootCanvas, Vector3.zero, mLeftShootCanvas.transform.rotation);
        }
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

        GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_CommonData>().NoDevelpment = false;//無発展フラグをfalse

       //保留玉使用（変動開始）
       mBigController.UseStock(WIN_LOST.LOST);
   
        //演出抽選
        int randomNumber = CS_LotteryFunction.LotNormalInt(mMissionStatus.infomation[mPrizesNum].mission.Count -1);

        //ミッション内容保存
        CS_MissionPhaseData data = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_MissionPhaseData>();
        data.SaveMissionContents(mPrizesNum, randomNumber);

        mPrizesNum++;//入賞数加算

        if (OnPlayPerformance != null)
        {
            //イベントハンドラ実行
            OnPlayPerformance(randomNumber);
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
