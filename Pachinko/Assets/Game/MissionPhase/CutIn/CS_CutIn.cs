using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_CutIn : MonoBehaviour
{
    [SerializeField, Header("効果音オブジェクト")]
    private GameObject mSE;

    [SerializeField, Header("消去までの時間")]
    private float mDestroyTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject rootObject = transform.root.gameObject;
        if (rootObject.GetComponent<CS_PerformanceFinish>() == null)
        {
            rootObject.AddComponent<CS_PerformanceFinish>().DestroyConfig(true, mDestroyTime); ;//引数2:プレハブを消すまでの時間(秒)
        }
    }

    private void OnEnable()
    {
        Instantiate(mSE, Vector3.zero, Quaternion.identity);
    }
}
