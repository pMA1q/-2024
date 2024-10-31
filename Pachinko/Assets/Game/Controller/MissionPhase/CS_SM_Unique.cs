//---------------------------------
//�����~�b�V�����̃��j�[�N�ȉ��o��̉��o�����߂�
//�S���ҁF����
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CS_SM_Unique : MonoBehaviour
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

    private void Start()
    {
        mUniquePF_Functions = new Func<int>[] { P11, P12 };
    }

    //���o����11��
    public int P11()
    {
        if (!mCoiceSuccess) { return -1; }


        int[] nextMissionNums = new int[] { 4,5,6,7,8,9,10,13,14,15,16,17,18,19,24,25};
        int missionIdx = CS_LotteryFunction.LotNormalInt(nextMissionNums.Length - 1);

        return nextMissionNums[missionIdx] -1;
    }

    //���o����11��
    public int P12()
    {
        int[] nextMissionNums = new int[] { 4, 5, 6, 7, 8, 9, 10, 13, 14, 15, 16, 17, 18, 19, 24, 25 };
        int missionIdx = CS_LotteryFunction.LotNormalInt(nextMissionNums.Length - 1);

        return nextMissionNums[missionIdx] - 1;
    }


    //���̉��o�ԍ������߂ĕԂ�
    public int DesisionMission(int _val)
    {
        return mUniquePF_Functions[_val]();
    }
}