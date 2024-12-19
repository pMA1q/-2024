using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PlayerWinning : MonoBehaviour
{
    [SerializeField]
    private GameObject winUI;

    private CS_Controller mController;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Finish());
        mController = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_Controller>();
    }


    private IEnumerator Finish()
    {
        CS_Controller bigCtrl = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_Controller>();
        yield return new WaitForSeconds(3f);
        bigCtrl.NumberRailBigger();

        yield return new WaitForSeconds(4f);

        
        bigCtrl.Set777();

        
        GameObject rootObject = transform.root.gameObject;
        if (rootObject.GetComponent<CS_PerformanceFinish>() == null)
        {
            rootObject.AddComponent<CS_PerformanceFinish>().DestroyConfig(true, 1f); ;//à¯êî2:ÉvÉåÉnÉuÇè¡Ç∑Ç‹Ç≈ÇÃéûä‘(ïb)
        }
        CS_BossPhaseData bData = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_BossPhaseData>();
        bData.IsSubjugation = true;
        while (!bigCtrl.GetPatternVariationFinish() || !bigCtrl.GetPerformanceFinish()) { yield return null; }

        bigCtrl.ChangePhase(CS_Controller.PACHINKO_PHESE.SET);

        Destroy(winUI);

    }
    
}
