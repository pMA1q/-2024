using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BossUnique : MonoBehaviour
{
    
    protected CSO_BossStatus mBossStatus;
    protected CSO_PlayerStatus mPlayerStatus;
    protected CS_BossPhaseData mBossData;
    protected CS_BossPhaseData.BUFF_DEBUFF mPrevBuffInfo;
    protected float mMaxPlayerHp;
    // Start is called before the first frame update
    virtual protected void Start()
    {
        mBossStatus = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_BossPhaseData>().BossStatus;
        mPlayerStatus = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_MissionPhaseData>().PlayerStatus;
        mBossData = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_BossPhaseData>();
        mMaxPlayerHp = mBossData.PlayerStatus.hp;

        mPrevBuffInfo = mBossData.BossBuff_Debuff;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    protected bool ReLot(float _percentage)
    {
        float randomValue = UnityEngine.Random.Range(0f, 100f);
        return randomValue <= _percentage;
    }

    virtual public int DesisionFlag(int _val) { return -1; }
    virtual public int ReLottery(int _val) { return -1; }
}
