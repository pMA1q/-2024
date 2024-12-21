using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_MS_AddSubCount : MonoBehaviour
{
    [SerializeField, Header("Ç±ÇÃââèoÇ…Ç®ÇØÇÈìGì¢î∞êî")]
    private int mEnemySubCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        CS_MissionPhaseData mData = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_MissionPhaseData>();
        mData.SubjugationOneMission = mEnemySubCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
