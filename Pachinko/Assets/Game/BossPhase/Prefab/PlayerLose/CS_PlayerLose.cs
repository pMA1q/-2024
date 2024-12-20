using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PlayerLose : MonoBehaviour
{
    [SerializeField]
    private GameObject mLoseCanvas;

    [SerializeField]
    private GameObject mLoseUI;

    private CS_Controller mController;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Finish());
        mController = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_Controller>();
        mLoseUI.SetActive(false);
    }


    private IEnumerator Finish()
    {
        CS_Controller bigCtrl = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_Controller>();
        //1秒後に勝利UIを表示
        yield return new WaitForSeconds(3f);
        mLoseUI.SetActive(true);

        CS_BossPhaseData bData = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_BossPhaseData>();
        bData.IsPlayerLose = true;

        GameObject rootObject = transform.root.gameObject;
        if (rootObject.GetComponent<CS_PerformanceFinish>() == null)
        {
            rootObject.AddComponent<CS_PerformanceFinish>().DestroyConfig(true, 3f); ;//引数2:プレハブを消すまでの時間(秒)
        }

        yield return new WaitForSeconds(2.9f);

        Destroy(mLoseCanvas);
        while (!bigCtrl.GetPatternVariationFinish() || !bigCtrl.GetPerformanceFinish()) { yield return null; }

       // bigCtrl.ChangePhase(CS_Controller.PACHINKO_PHESE.SET);

    }
}
