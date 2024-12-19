using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PlayerWinning : MonoBehaviour
{
    [SerializeField]
    private GameObject winCanvas;

    [SerializeField]
    private GameObject winUI;

    private CS_Controller mController;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Finish());
        mController = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_Controller>();
        winUI.SetActive(false);
    }


    private IEnumerator Finish()
    {
        CS_Controller bigCtrl = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_Controller>();
        //1秒後に勝利UIを表示
        yield return new WaitForSeconds(2f);
        winUI.SetActive(true);

        // 3秒後に図柄を大きく映す
        yield return new WaitForSeconds(3f);
        bigCtrl.NumberRailBigger();

        // 3秒後に777で図柄変動を止める
        yield return new WaitForSeconds(2f);
        bigCtrl.Set777();

        
        GameObject rootObject = transform.root.gameObject;
        if (rootObject.GetComponent<CS_PerformanceFinish>() == null)
        {
            rootObject.AddComponent<CS_PerformanceFinish>().DestroyConfig(true, 1f); ;//引数2:プレハブを消すまでの時間(秒)
        }
        CS_BossPhaseData bData = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_BossPhaseData>();
        bData.IsSubjugation = true;
        while (!bigCtrl.GetPatternVariationFinish() || !bigCtrl.GetPerformanceFinish()) { yield return null; }

        bigCtrl.ChangePhase(CS_Controller.PACHINKO_PHESE.SET);

        Destroy(winCanvas);
    }
    
}
