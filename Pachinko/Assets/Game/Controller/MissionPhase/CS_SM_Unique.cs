//---------------------------------
//�����~�b�V�����̃��j�[�N�ȉ��o��̉��o�����߂�
//�S���ҁF����
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
        mUniquePF_Functions = new Func<int>[] { P11, P12 ,P18_19, P18_19, P20, P21, P22, P23, P26};
    }

    //���o����11��
    private int P11()
    {

        if (!missionData.GetChoiceSuccess()) { return -1; }

        int[] nextMissionNums = new int[] { 4,5,6,7,8,9,10,13,14,15,16,17,18,19,24,25};
        int missionIdx = CS_LotteryFunction.LotNormalInt(nextMissionNums.Length - 1);
        missionData.ChoiceSuccessReset();
        return nextMissionNums[missionIdx] -1;
    }

    //���o����12��
    private int P12()
    {
        int[] nextMissionNums = new int[] { 4, 5, 6, 7, 8, 9, 10, 13, 14, 15, 16, 17, 18, 19, 24, 25 };
        int missionIdx = CS_LotteryFunction.LotNormalInt(nextMissionNums.Length - 1);

        return nextMissionNums[missionIdx] - 1;
    }

    //���o����18&19��
    private int P18_19()
    {
        //�I���Ɏ��s�����H
        if (!missionData.GetChoiceSuccess()) 
        {
            //�P�~�b�V������������0�ɖ߂�
            missionData.SubjugationOneMission = 0;
            return -1;
        }
        CSO_PlayerStatus pStatus = GameObject.Find("BigController").GetComponent<CS_MissionData>().PlayerStatus;
        CharacterStatus cStatus = pStatus.charaStatus;
        float[] status = new float[5] { cStatus.charColorUP, cStatus.preemptiveAttack, cStatus.attack, cStatus.revaival, cStatus.cutIn };
        List<float> choicePercent = new List<float> {cStatus.charColorUpPow.conicePercent, cStatus.preemptiveAttackUpPow.conicePercent,cStatus.attackUpPow.conicePercent,
                                                     cStatus.revivalUpPow.conicePercent,cStatus.cutInUpPow.conicePercent};
        List<float> smallpower = new List<float> {cStatus.charColorUpPow.smallUP, cStatus.preemptiveAttackUpPow.smallUP,cStatus.attackUpPow.smallUP,
                                                     cStatus.revivalUpPow.smallUP,cStatus.cutInUpPow.smallUP};
        List<float> midllepower = new List<float> {cStatus.charColorUpPow.smallUP, cStatus.preemptiveAttackUpPow.smallUP,cStatus.attackUpPow.smallUP,
                                                     cStatus.revivalUpPow.smallUP,cStatus.cutInUpPow.smallUP};
        List<float> bigpower = new List<float> {cStatus.charColorUpPow.bigUP, cStatus.preemptiveAttackUpPow.bigUP,cStatus.attackUpPow.bigUP,
                                                     cStatus.revivalUpPow.bigUP,cStatus.cutInUpPow.bigUP};
        List<float> maxpower = new List<float> {cStatus.charColorUpPow.max, cStatus.preemptiveAttackUpPow.max,cStatus.attackUpPow.max,
                                                     cStatus.revivalUpPow.max,cStatus.cutInUpPow.max};
        int random = CS_LotteryFunction.LotPerformance(choicePercent);

        //1~3�̓|�����H
        if(missionData.SubjugationOneMission >= 1 && missionData.SubjugationOneMission <= 3) { status[random] += smallpower[random]; }
        //4~6�̓|�����H
        else if (missionData.SubjugationOneMission <= 6) { status[random] += midllepower[random]; }
        //7~9�̓|�����H
        else { status[random] += bigpower[random]; }

        //�ő�l�𒴂��Ȃ��悤�ɂ���
        if(status[random] > maxpower[random]) { status[random] = maxpower[random]; }

        cStatus.charColorUP = status[0];
        cStatus.preemptiveAttack = status[1];
        cStatus.attack = status[2];
        cStatus.revaival = status[3];
        cStatus.cutIn = status[4];
        //�v���C���[�X�e�[�^�X���X�V
        pStatus.charaStatus = cStatus;
        return -1;
    }

    //���o����20��
    private int P20()
    {
        missionData.PlayerBuff = CS_MissionData.PLAYER_BUFF.WEAK;
        return -1;
    }

    //���o����20��
    private int P21()
    {
        missionData.PlayerBuff = CS_MissionData.PLAYER_BUFF.STRONG;
        return -1;
    }

    //���o����22��
    private int P22()
    {
        missionData.IsEnemyDeBuff = true;
        return -1;
    }

    //���o����23��
    private int P23()
    {
        missionData.Skill++;
        return -1;
    }

    //���o����26��
    private int P26()
    {
        missionData.HighProbabEnemyMode = true;
        return -1;
    }

    //���o����30��
    private int P30()
    {
        missionData.RewardUp = true;
        return -1;
    }

    //���̉��o�ԍ������߂ĕԂ�
    public override int DesisionMission(int _val)
    {
        return mUniquePF_Functions[_val]();
    }
}
