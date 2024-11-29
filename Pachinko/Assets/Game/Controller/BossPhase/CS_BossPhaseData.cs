using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BossPhaseData : MonoBehaviour
{
    [SerializeField, Header("ボスステータス")]
    private CSO_BossStatus mBossList;
    public CSO_BossStatus BossStatus { get { return mBossList; } }

    private CSO_PlayerStatus mPlayerStatus;
    public CSO_PlayerStatus PlayerStatus { get { return mPlayerStatus; } }


    // Start is called before the first frame update
    void Start()
    {
        mPlayerStatus = this.GetComponent<CS_MissionPhaseData>().PlayerStatus;   
    }

    //無発展フラグ
    private bool IsNoDevelopment = false;
    public bool NoDevelpment
    {
        set { IsNoDevelopment = value; }
        get { return IsNoDevelopment; }
    }

    public void ResetData()
    {

    }

}
