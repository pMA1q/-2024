using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_UniqueMission : MonoBehaviour
{
    protected CS_MissionPhaseData missionData;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        missionData = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_MissionPhaseData>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual public int DesisionMission(int _val) { return -1; }
    
}
