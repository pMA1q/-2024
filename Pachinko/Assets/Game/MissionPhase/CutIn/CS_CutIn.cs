using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_CutIn : MonoBehaviour
{
    [SerializeField, Header("���ʉ��I�u�W�F�N�g")]
    private GameObject mSE;

    [SerializeField, Header("�����܂ł̎���")]
    private float mDestroyTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject rootObject = transform.root.gameObject;
        if (rootObject.GetComponent<CS_PerformanceFinish>() == null)
        {
            rootObject.AddComponent<CS_PerformanceFinish>().DestroyConfig(true, mDestroyTime); ;//����2:�v���n�u�������܂ł̎���(�b)
        }
    }

    private void OnEnable()
    {
        Instantiate(mSE, Vector3.zero, Quaternion.identity);
    }
}
