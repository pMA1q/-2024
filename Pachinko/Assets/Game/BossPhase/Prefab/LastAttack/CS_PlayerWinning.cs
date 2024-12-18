using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PlayerWinning : MonoBehaviour
{
    [SerializeField]
    //private GameObject winUI;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Finish());
    }


    private IEnumerator Finish()
    {
        yield return new WaitForSeconds(2f);
        ///winUI.SetActive(true);

        yield return new WaitForSeconds(5f);

        
        CS_Controller bigCtrl = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_Controller>();
        bigCtrl.Set777();

        GameObject rootObject = transform.root.gameObject;
        if (rootObject.GetComponent<CS_PerformanceFinish>() == null)
        {
            rootObject.AddComponent<CS_PerformanceFinish>().DestroyConfig(true, 0.1f); ;//à¯êî2:ÉvÉåÉnÉuÇè¡Ç∑Ç‹Ç≈ÇÃéûä‘(ïb)
        }

        CS_BossPhaseData bData = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_BossPhaseData>();
        bData.IsSubjugation = true;
       
       
    }
    
}
