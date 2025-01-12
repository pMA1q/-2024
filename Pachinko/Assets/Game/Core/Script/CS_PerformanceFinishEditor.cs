using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PerformanceFinishEditor : MonoBehaviour
{
    [SerializeField]
    float mTimer = 3f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FinishPerf());
    }
     
    private IEnumerator FinishPerf()
    {
        yield return new WaitForSeconds(mTimer);
        GameObject root = transform.root.gameObject;
        root.AddComponent<CS_PerformanceFinish>().DestroyConfig(true, 4);
    }
   
}
