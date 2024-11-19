//---------------------------------
//討伐ミッションのユニークな演出後の演出を決める
//担当者：中島
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CS_SM_Unique : CS_UniqueMission
{
    private Func<int>[] mUniquePF_Functions;

    private bool mCoiceSuccess = false;

    public bool Coice
    {
        set
        {
            Coice = value;
        }
    }

    protected override void Start()
    {
        base.Start();
        mUniquePF_Functions = new Func<int>[] { P11, P12 ,P18_19, P18_19};
    }

    //演出項目11番
    private int P11()
    {

        if (!missionData.GetChoiceSuccess()) { return -1; }

        int[] nextMissionNums = new int[] { 4,5,6,7,8,9,10,13,14,15,16,17,18,19,24,25};
        int missionIdx = CS_LotteryFunction.LotNormalInt(nextMissionNums.Length - 1);
        missionData.ChoiceSuccessReset();
        return nextMissionNums[missionIdx] -1;
    }

    //演出項目12番
    private int P12()
    {
        int[] nextMissionNums = new int[] { 4, 5, 6, 7, 8, 9, 10, 13, 14, 15, 16, 17, 18, 19, 24, 25 };
        int missionIdx = CS_LotteryFunction.LotNormalInt(nextMissionNums.Length - 1);

        return nextMissionNums[missionIdx] - 1;
    }

    //演出項目18&19番
    private int P18_19()
    {
        if (!missionData.GetChoiceSuccess()) { return -1; }
        CSO_PlayerStatus pStatus = GameObject.Find("BigController").GetComponent<CS_MissionData>().PlayerStatus;
        CharacterStatus cStatus = pStatus.charaStatus;
        float[] status = new float[5] { cStatus.charColorUP, cStatus.preemptiveAttack, cStatus.revaival, cStatus.equipmentRank, cStatus.cutIn };
        int random = CS_LotteryFunction.LotNormalInt(4);
        status[random] += 50f;
        cStatus.charColorUP = status[0];
        cStatus.preemptiveAttack = status[1];
        cStatus.revaival = status[2];
        cStatus.equipmentRank = (int)status[3];
        cStatus.cutIn = status[4];
        pStatus.charaStatus = cStatus;
        return -1;
    }

    //演出項目22番
    private void P22()
    {
        missionData.NextBattleFlag = true;
    }

    //次の演出番号を決めて返す
    public override int DesisionMission(int _val)
    {
        return mUniquePF_Functions[_val]();
    }
}
