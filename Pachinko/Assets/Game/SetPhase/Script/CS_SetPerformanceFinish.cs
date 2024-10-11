//---------------------------------
//準備フェーズの演出終了報告
//担当者：中島
//---------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SetPerformanceFinish : MonoBehaviour
{
    [SerializeField, Header("演出終了までの時間")]
    private float mTimer = 1f;

    //終了報告までのタイマー
    public float Timer
    {
        set
        {
            mTimer = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FinishWait());
    }

  

    private IEnumerator FinishWait()
    {
        //指定した秒数待つ
        yield return new WaitForSeconds(mTimer);

        //司令塔を取得して、演出が終了したことを知らせる
        CS_Controller ctrl = GameObject.Find("BigController").GetComponent<CS_Controller>();
        ctrl.PerformanceFinish();
        //CS_SetPheseController spc = CS_SetPheseController.GetCtrl();
        if(ctrl == null) { Debug.LogError("司令塔が見つからない");}

        //spc.PerformanceFinish();
        Destroy(this);
    }
}
