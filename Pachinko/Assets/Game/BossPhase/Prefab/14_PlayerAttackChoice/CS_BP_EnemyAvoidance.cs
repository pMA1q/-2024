using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BP_EnemyAvoidance : MonoBehaviour
{
    [SerializeField, Header("避けるまでの時間")]
    private float mAvoidTime = 1f;
    [SerializeField, Header("回避後の位置")]
    private Transform mTargetTrans;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Avoid());
    }
    private IEnumerator Avoid()
    {
        yield return new WaitForSeconds(mAvoidTime);

        this.transform.position = mTargetTrans.position;

        yield return new WaitForSeconds(2f);

        //演出終了を知らせる
        GameObject rootObject = transform.root.gameObject;
        if (rootObject.GetComponent<CS_PerformanceFinish>() == null)
        {
            //3秒後に演出を消す
            rootObject.AddComponent<CS_PerformanceFinish>().DestroyConfig(true, 4f);
        }
    }
  
}
