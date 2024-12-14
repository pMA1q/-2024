using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BP_EnemyAvoidance : MonoBehaviour
{
    [SerializeField, Header("������܂ł̎���")]
    private float mAvoidTime = 1f;
    [SerializeField, Header("�����̈ʒu")]
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

        //���o�I����m�点��
        GameObject rootObject = transform.root.gameObject;
        if (rootObject.GetComponent<CS_PerformanceFinish>() == null)
        {
            //3�b��ɉ��o������
            rootObject.AddComponent<CS_PerformanceFinish>().DestroyConfig(true, 4f);
        }
    }
  
}
