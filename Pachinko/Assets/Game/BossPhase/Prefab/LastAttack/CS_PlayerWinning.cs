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

    private float mNowTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Finish());
        mController = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_Controller>();
        winUI.SetActive(false);
    }

    private void Update()
    {
        if(mNowTime >= 0.5f) { return; }
        mNowTime += Time.deltaTime;


        Vector3 pos = this.transform.position;
        pos += this.transform.forward * 0.5f * Time.deltaTime;
        this.transform.position = pos;
    }

    private IEnumerator Finish()
    {
        CS_Controller bigCtrl = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_Controller>();
        //1�b��ɏ���UI��\��
        yield return new WaitForSeconds(2f);
        winUI.SetActive(true);

        // 3�b��ɐ}����傫���f��
        yield return new WaitForSeconds(3f);
        bigCtrl.NumberRailBigger();

        // 3�b���777�Ő}���ϓ����~�߂�
        yield return new WaitForSeconds(2f);
        bigCtrl.Set777();

        
        GameObject rootObject = transform.root.gameObject;
        if (rootObject.GetComponent<CS_PerformanceFinish>() == null)
        {
            rootObject.AddComponent<CS_PerformanceFinish>().DestroyConfig(true, 1f); ;//����2:�v���n�u�������܂ł̎���(�b)
        }
        CS_BossPhaseData bData = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_BossPhaseData>();
        bData.IsSubjugation = true;
        while (!bigCtrl.GetPatternVariationFinish() || !bigCtrl.GetPerformanceFinish()) { yield return null; }

        bigCtrl.ChangePhase(CS_Controller.PACHINKO_PHESE.SET);

        Destroy(winCanvas);
    }
    
}
