//---------------------------------
//�����t�F�[�Y�i�ߓ�
//�S���ҁF����
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
        //�p�t�H�[�}���X���X�g����m����R�s�[
        //for (int i = 0; i < mProbabilityStatus.performances.Count; i++)
        //{
        //    mProbabilities.Add(mProbabilityStatus.performances[i].value);
        //    Debug.Log(mProbabilityStatus.performances[i].name + "�̊m��" + mProbabilities[i] + "%");
        //}

       

        mBigController = GameObject.Find("BigController").GetComponent<CS_Controller>();//司令塔大を取得
                                                                                        //ミッション選択オブジェクトを生成
        GameObject instance = Instantiate(mMisstionSelect, mMisstionSelect.transform.position, mMisstionSelect.transform.rotation);
        instance.name = mMisstionSelect.name; // (Clone)が付かないように名前をオリジナルの名前に戻す

        Debug.Log("mMisstionSelect" + mMisstionSelect.name);
    }

    // Update is called once per frame
    void Update()
    {

        //CheckLottery();
      

        //変動できるかを取得
        bool variationStart = mBigController.CanVariationStart();
        if (!variationStart) { return; }//falseなら終了


        //入賞数が3？
        if(mPrizesNum == 3)
        {
            //別物を参照しているのでシーンからMissionSelectを見つけてサイド取得
            mMisstionSelect = GameObject.Find("MissionSelect");
            //プレイヤーがミッションを選択する状態を開始する
            mMisstionSelect.GetComponent<CS_LotMission>().PlaySelectMode();
            RemoveAllHandlers();
            Destroy(this.gameObject);
            return;
        }

        // イベントハンドラはnullなら終了
        if (OnPlayPerformance == null) { return; }

        //保留玉が無いなら終了
        if(mBigController.GetStock() == 0) { return; }

        //保留玉使用（変動開始）
        mBigController.UseStock();

        //演出抽選
        int randomNumber = CS_LotteryFunction.LotNormalInt(mMissionStatus.infomation[mPrizesNum].mission.Count -1);

        mPrizesNum++;//入賞数加算


        if (OnPlayPerformance != null)
        {
            //イベントハンドラ実行y
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
