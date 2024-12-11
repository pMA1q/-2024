//---------------------------------
//準備フェーズの演出終了報告
//担当者：中島
//---------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PerformanceFinish : MonoBehaviour
{
    [SerializeField, Header("演出終了までの時間")]
    private float mTimer = 0f;

    public bool IsDestroyObject = true;

    //終了報告までのタイマー
    public float DestroyTimer
    {
        set
        {
            mTimer = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CS_Controller ctrl = GameObject.Find("BigController").GetComponent<CS_Controller>();
        ctrl.PerformanceSemiFinish = true;//演出終了仮フラグをtrue
        
    }

    public void DestroyConfig(bool _destroy, float _destroyTimer)
    {
        IsDestroyObject = _destroy;
        DestroyTimer = _destroyTimer;

        StartCoroutine(FinishWait());
    }

    private IEnumerator FinishWait()
    {
        //司令塔を取得して、演出が終了したことを知らせる
        CS_Controller ctrl = GameObject.Find("BigController").GetComponent<CS_Controller>();
        ctrl.PerformanceSemiFinish = true ;
        //指定した秒数待つ
        yield return new WaitForSeconds(mTimer);

        
        ctrl.PerformanceFinish();
        //CS_SetPheseController spc = CS_SetPheseController.GetCtrl();
        if(ctrl == null) { Debug.LogError("司令塔が見つからない");}

        if (!IsDestroyObject) { Destroy(this); yield return null; }

        //spc.PerformanceFinish();
        // このオブジェクトが属する最上位の親オブジェクトを取得
        GameObject rootObject = transform.root.gameObject;

        // 最上位のオブジェクト（親プレハブ）を削除
        Destroy(rootObject);
    }
}
