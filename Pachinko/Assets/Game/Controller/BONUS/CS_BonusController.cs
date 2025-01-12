using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BonusController : MonoBehaviour
{
    private CS_BonusPhaseData mCdata;

    [SerializeField]
    private CS_BP_DrawCount DrawRound;
    // Start is called before the first frame update
    void Start()
    {
        mCdata = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_BonusPhaseData>();
        mCdata.IsBonusFinish = false;
        mCdata.RoundCount = 1;
        Debug.Log("åªç›ÉâÉEÉìÉhêî" + mCdata.RoundCount);
    }

    // Update is called once per frame
    void Update()
    {
        DrawRound.SetCount(mCdata.RoundCount);
        if(mCdata.IsBonusFinish)
        {
            CS_Controller ctrl = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_Controller>();
            ctrl.ChangePhase(CS_Controller.PACHINKO_PHESE.RUSH);
            ctrl.CreateController();
            Destroy(this.gameObject);
        }
    }
}
