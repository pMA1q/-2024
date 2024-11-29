using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BossUnique : MonoBehaviour
{

    protected CSO_BossStatus mBossStatus;
    // Start is called before the first frame update
    virtual protected void Start()
    {
        mBossStatus = GameObject.Find("BigController").GetComponent<CS_BossPhaseData>().BossStatus;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual public int DesisionFlag(int _val) { return -1; }
}
