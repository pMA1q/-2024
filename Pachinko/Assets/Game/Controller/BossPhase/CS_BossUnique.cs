using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BossUnique : MonoBehaviour
{
    
    protected CSO_BossStatus mBossStatus;
    protected CSO_PlayerStatus mPlayerStatus;
    protected CS_BossPhaseData mBossData;
    // Start is called before the first frame update
    virtual protected void Start()
    {
        mBossStatus = GameObject.Find("BigController").GetComponent<CS_BossPhaseData>().BossStatus;
        mPlayerStatus = GameObject.Find("BigController").GetComponent<CS_BossPhaseData>().PlayerStatus;
        mBossData = GameObject.Find("BigController").GetComponent<CS_BossPhaseData>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected bool ReLot(float _percentage)
    {
        float randomValue = UnityEngine.Random.Range(0f, 100f);
        return _percentage <= randomValue;
    }

    virtual public int DesisionFlag(int _val) { return -1; }
    virtual public int ReLottery(int _val) { return -1; }
}
