using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CS_Boss1_Unique : CS_BossUnique
{
    private Func<int>[] mUniquePF_Functions;
    private Func<int>[] mUniquePF_ReLotteryFunctions;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        mUniquePF_Functions = new Func<int>[] { P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21, P22, P23, P24, P25, P26, P27, P28, P29, P30 };
        mUniquePF_ReLotteryFunctions = new Func<int>[] { P4_Relot, P5_Relot, P6_Relot, P7_Relot, P8_Relot, P9_Relot, P10_Relot, P11_Relot, P12_Relot, P13_Relot, P17_Relot, P19_20Relot, P19_20Relot,
            P23_Relot, P24_Relot, P25_Relot, P26_Relot, P29_Relot, P30_Relot };

    }

    // ���ڔԍ�4�̕�V�A�L�^�f�[�^����
    private int P4()
    {
        float bossHp = mBossStatus.infomations[mBossData.BossNumber].hp;
        bossHp -= mBossData.PlayerOneAttackPow;
        if(bossHp <= 0.0f)
        {
            bossHp = 0;
            mBossData.IsSubjugation = true;//�{�X�����t���O��true
        }
        mBossStatus.infomations[mBossData.BossNumber].hp = bossHp;
        return -1;
    }

    //���ڔԍ�4�̍Ē��I����
    private int P4_Relot()
    {
        int next = -1;
       
        float percentage = mPlayerStatus.charaStatus.charColorUP;
        float attack = mPlayerStatus.charaStatus.preemptiveAttack * 0.60f;//�U����(��)
        if(ReLot(percentage))
        {
            attack = mPlayerStatus.charaStatus.preemptiveAttack * 0.80f;//�U����(��)�ɂ���
        }

        mBossData.PlayerOneAttackPow = attack;

        return next;
    }

    // ���ڔԍ�5�̕�V�A�L�^�f�[�^����
    private int P5()
    {
        mPlayerStatus.hp -= mBossData.BossOneAttackPow;

        //�����t���O�������Ă���Ȃ�A���ɖ߂�
        if (mBossData.IsPlayerRevaival)
        {
            mPlayerStatus.hp = mBossData.BackUpHP;
        }
        else if (mPlayerStatus.hp <= 0)
        {
            mBossData.IsPlayerLose = true;
        }
        return -1;
    }

    //���ڔԍ�5�̍Ē��I����
    private int P5_Relot()
    {
        int next = -1;
        mBossData.IsPlayerRevaival = false;
        int[] preemptive = new int[2] { 4, 10 };
        float percentage = mPlayerStatus.charaStatus.preemptiveAttack;
        float playerHp = mPlayerStatus.hp;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * 0.60f;//���̕ϓ����̃A�^�b�N��
        playerHp -= mBossData.BossOneAttackPow;//�{�X�̍U���́i��j��hp�������
        if (playerHp <= 0f)//�̗͂������Ȃ�Ε���������
        {
            percentage = mPlayerStatus.charaStatus.revaival;
            if (!ReLot(percentage))//���I���Ȃ�������s�k
            {
                return -1;

            }
            else
            {
                //���I�����Ȃ畜��
                mBossData.IsPlayerRevaival = true;
            }
        }
        else//�搧�U���̒l�ōĒ��I
        {
            if (ReLot(percentage)) { next = preemptive[CS_LotteryFunction.LotNormalInt(preemptive.Length)] - 1; }//���I����ΐ搧�U���̔ԍ���Ԃ�
        }
        return next;
    }

    // ���ڔԍ�6�̕�V�A�L�^�f�[�^����
    private int P6()
    {
        mPlayerStatus.hp -= mBossData.BossOneAttackPow;

        //�����t���O�������Ă���Ȃ�A���ɖ߂�
        if (mBossData.IsPlayerRevaival)
        {
            mPlayerStatus.hp = mBossData.BackUpHP;
        }
        else if (mPlayerStatus.hp <= 0)
        {
            mBossData.IsPlayerLose = true;
        }
        return -1;
    }

    //���ڔԍ�6�̍Ē��I����
    private int P6_Relot()
    {
        int next = -1;
        mBossData.IsPlayerRevaival = false;
        int[] preemptive = new int[2] { 4, 10 };
        float percentage = mPlayerStatus.charaStatus.preemptiveAttack;
        float playerHp = mPlayerStatus.hp;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * 0.80f;//���̕ϓ����̃A�^�b�N��
        playerHp -= mBossData.BossOneAttackPow;//�{�X�̍U���́i���j��hp�������
        if (playerHp <= 0f)//�̗͂������Ȃ�Ε���������
        {
            percentage = mPlayerStatus.charaStatus.revaival;
            if (!ReLot(percentage))//���I���Ȃ�������s�k
            {
                return -1;
            }
            else
            {
                //���I�����Ȃ畜��
                mBossData.IsPlayerRevaival = true;
            }
        }
        else//�搧�U���̒l�ōĒ��I
        {
            if (ReLot(percentage)) { next = preemptive[CS_LotteryFunction.LotNormalInt(preemptive.Length)] - 1; }//���I����ΐ搧�U���̔ԍ���Ԃ�
        }
        return next;
    }

    // ���ڔԍ�7�̕�V�A�L�^�f�[�^����
    private int P7()
    {
        mPlayerStatus.hp -= mBossData.BossOneAttackPow;

        //�����t���O�������Ă���Ȃ�A���ɖ߂�
        if (mBossData.IsPlayerRevaival)
        {
            mPlayerStatus.hp = mBossData.BackUpHP;
        }
        else if (mPlayerStatus.hp <= 0)
        {
            mBossData.IsPlayerLose = true;
        }
        return -1;
    }


    //���ڔԍ�7�̍Ē��I����
    private int P7_Relot()
    {
        int next = -1;

        float percentage = mPlayerStatus.charaStatus.revaival;
        float playerHp = mPlayerStatus.hp;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * 0.60f;//���̕ϓ����̃A�^�b�N��
        playerHp -= mBossData.BossOneAttackPow;//�{�X�̍U���́i���j��hp�������
        if (playerHp <= 0f)//�̗͂������Ȃ�Ε���������
        {
            percentage = mPlayerStatus.charaStatus.revaival;
            if (ReLot(percentage))
            {
                //���I����Ε���
                mBossData.IsPlayerRevaival = true;
            }
            
        }
        return next;
    }

   
    // ���ڔԍ�8�̕�V�A�L�^�f�[�^����
    private int P8()
    {
        mPlayerStatus.hp -= mBossData.BossOneAttackPow;

        //�����t���O�������Ă���Ȃ�A���ɖ߂�
        if (mBossData.IsPlayerRevaival)
        {
            mPlayerStatus.hp = mBossData.BackUpHP;
        }
        else if (mPlayerStatus.hp <= 0)
        {
            mBossData.IsPlayerLose = true;
        }
        return -1;
    }

    //���ڔԍ�8�̍Ē��I����
    private int P8_Relot()
    {
        int next = -1;

        float percentage = mPlayerStatus.charaStatus.revaival;
        float playerHp = mPlayerStatus.hp;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack;//���̕ϓ����̃A�^�b�N��
        playerHp -= mBossData.BossOneAttackPow;//�{�X�̍U���́i���j��hp�������
        if (playerHp <= 0f)//�̗͂������Ȃ�Ε���������
        {
            percentage = mPlayerStatus.charaStatus.revaival;
            if (ReLot(percentage))
            {
                //���I����Ε���
                mBossData.IsPlayerRevaival = true;
            }

        }
        return next;
    }

    // ���ڔԍ�9�̕�V�A�L�^�f�[�^����
    private int P9()
    {
        mPlayerStatus.hp -= mBossData.BossOneAttackPow;

        //�����t���O�������Ă���Ȃ�A���ɖ߂�
        if (mBossData.IsPlayerRevaival)
        {
            mPlayerStatus.hp = mBossData.BackUpHP;
        }
        else if(mPlayerStatus.hp <= 0)
        {
            mBossData.IsPlayerLose = true;
        }
        return -1;
    }

    //���ڔԍ�9�̍Ē��I����
    private int P9_Relot()
    {
        int next = -1;
        mBossData.IsPlayerRevaival = false;
        int[] preemptive = new int[2] { 4, 10 };
        float percentage = mPlayerStatus.charaStatus.preemptiveAttack;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * 0.60f;//���̕ϓ����̃A�^�b�N��
        
        if (ReLot(percentage)) 
        { 
            next = preemptive[CS_LotteryFunction.LotNormalInt(preemptive.Length)] - 1; //���I����ΐ搧�U���̔ԍ���Ԃ�
        }
        

        return next;
    }

    // ���ڔԍ�10�̕�V�A�L�^�f�[�^����
    private int P10()
    {
        mBossStatus.infomations[mBossData.BossNumber].hp -= mBossData.PlayerOneAttackPow;

        //�����t���O�������Ă���Ȃ�A���ɖ߂�
        if (mBossStatus.infomations[mBossData.BossNumber].hp <= 0)
        {
            mBossData.IsSubjugation = true;
        }
        return -1;
    }

    //���ڔԍ�10�̍Ē��I����
    private int P10_Relot()
    {
        int next = -1;

        float percentage = mPlayerStatus.charaStatus.charColorUP;
        float attack = mPlayerStatus.charaStatus.preemptiveAttack * 0.60f;//�U����(��)
        if (ReLot(percentage))
        {
            attack = mPlayerStatus.charaStatus.preemptiveAttack * 0.80f;//�U����(��)�ɂ���
        }

        mBossData.PlayerOneAttackPow = attack;

        return next;
    }

    // ���ڔԍ�11�̕�V�A�L�^�f�[�^����
    private int P11()
    {
        mBossStatus.infomations[mBossData.BossNumber].hp -= mBossData.PlayerOneAttackPow;

        //�{�X�̗̑͂������Ȃ�Γ�������
        if (mBossStatus.infomations[mBossData.BossNumber].hp <= 0)
        {
            mBossData.IsSubjugation = true;
        }
        return -1;
    }

    //���ڔԍ�11�̍Ē��I����
    private int P11_Relot()
    {
        int next = -1;

        float percentage = mPlayerStatus.charaStatus.charColorUP;
        float attack = mPlayerStatus.charaStatus.attack * 0.60f;//�U����(��)
        if (ReLot(percentage))
        {
            attack = mPlayerStatus.charaStatus.attack * 0.80f;//�U����(��)�ɂ���
        }

        mBossData.PlayerOneAttackPow = attack;

        return next;
    }

    // ���ڔԍ�12�̕�V�A�L�^�f�[�^����
    private int P12()
    {
        return -1;
    }

    //���ڔԍ�12�̍Ē��I����
    private int P12_Relot()
    {
        int next = -1;
        int[] preemptive = new int[2] { 4, 10 };
        float percentage = mPlayerStatus.charaStatus.preemptiveAttack;
        if (ReLot(percentage)) 
        { 
            next = preemptive[CS_LotteryFunction.LotNormalInt(preemptive.Length)] - 1;  //���I����ΐ搧�U���̔ԍ���Ԃ�
            mBossData.PlayerOneAttackPow = mPlayerStatus.charaStatus.preemptiveAttack * 0.60f;
        }

        return next;
    }

    // ���ڔԍ�13�̕�V�A�L�^�f�[�^����
    private int P13()
    {
        mPlayerStatus.hp -= mBossData.BossOneAttackPow;

        //�����t���O�������Ă���Ȃ�A���ɖ߂�
        if (mBossData.IsPlayerRevaival)
        {
            mPlayerStatus.hp = mBossData.BackUpHP;
        }
        else if (mPlayerStatus.hp <= 0)
        {
            mBossData.IsPlayerLose = true;
        }
        return -1;
    }

    //���ڔԍ�13�̍Ē��I����
    private int P13_Relot()
    {
        int next = -1;

        float percentage = mPlayerStatus.charaStatus.revaival;
        float playerHp = mPlayerStatus.hp;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * 0.60f;//���̕ϓ����̃A�^�b�N��
        playerHp -= mBossData.BossOneAttackPow;//�{�X�̍U���́i���j��hp�������
        if (playerHp <= 0f)//�̗͂������Ȃ�Ε���������
        {
            percentage = mPlayerStatus.charaStatus.revaival;
            if (!ReLot(percentage))//���I���Ȃ�������s�k
            {
                return -1;
            }
            else
            {
                //���I����Ε���
                mBossData.IsPlayerRevaival = true;
            }
        }
        return next;
    }

    // ���ڔԍ�14�̕�V�A�L�^�f�[�^����
    private int P14()
    {
        //�A���U���������炷
        mBossStatus.infomations[mBossData.BossNumber].hp -= mPlayerStatus.attack * 0.60f * mBossData.SuccessionNum;

        //�{�X�̗̑͂������Ȃ�Γ�������
        if (mBossStatus.infomations[mBossData.BossNumber].hp <= 0)
        {
            mBossData.IsSubjugation = true;
        }
        return -1;
    }

    // ���ڔԍ�15�̕�V�A�L�^�f�[�^����
    private int P15()
    {
        
        return -1;
    }

    // ���ڔԍ�16�̕�V�A�L�^�f�[�^����
    private int P16()
    {
        return -1;
    }

    // ���ڔԍ�17�̕�V�A�L�^�f�[�^����
    private int P17()
    {
        return -1;
    }

    //���ڔԍ�17�̍Ē��I����
    private int P17_Relot()
    {
        int next = -1;

        float percentage = mPlayerStatus.charaStatus.charColorUP;
        float attack = mPlayerStatus.charaStatus.preemptiveAttack * 0.80f;//�U����(��)
        if (ReLot(percentage))
        {
            attack = mPlayerStatus.charaStatus.preemptiveAttack;//�U����(��)�ɂ���
        }

        mBossData.PlayerOneAttackPow = attack;

        return next;
    }

    // ���ڔԍ�18�̕�V�A�L�^�f�[�^����
    private int P18()
    {
        return -1;
    }

    // ���ڔԍ�19�̕�V�A�L�^�f�[�^����
    private int P19()
    {
        return -1;
    }

    //���ڔԍ�19,20�̍Ē��I����
    private int P19_20Relot()
    {
        int next = -1;
        mBossData.IsPlayerRevaival = false;
        int[] preemptive = new int[3] {11, 17,24 };
        float percentage = mPlayerStatus.charaStatus.preemptiveAttack;


        if (ReLot(percentage)) 
        {
            //���I����ΐ搧�U���̔ԍ���Ԃ�
            int randomval = CS_LotteryFunction.LotNormalInt(preemptive.Length);
            next = preemptive[randomval] - 1;
            float[] attack = new float[3] { mPlayerStatus.attack * 0.60f, mPlayerStatus.attack * 0.80f, mPlayerStatus.attack * 0.80f };
            mBossData.PlayerOneAttackPow = attack[randomval];
        }


        return next;
    }

    // ���ڔԍ�20�̕�V�A�L�^�f�[�^����
    private int P20()
    {
        return -1;
    }

    // ���ڔԍ�21�̕�V�A�L�^�f�[�^����
    private int P21()
    {
        return -1;
    }

    // ���ڔԍ�22�̕�V�A�L�^�f�[�^����
    private int P22()
    {
        return -1;
    }

    // ���ڔԍ�23�̕�V�A�L�^�f�[�^����
    private int P23()
    {
        return -1;
    }

    //���ڔԍ�23�̍Ē��I����
    private int P23_Relot()
    {
        int next = -1;

        float percentage = mPlayerStatus.charaStatus.revaival;
        float playerHp = mPlayerStatus.hp;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * 0.80f;//���̕ϓ����̃A�^�b�N��
        playerHp -= mBossData.BossOneAttackPow;//�{�X�̍U���́i���j��hp�������
        if (playerHp <= 0f)//�̗͂������Ȃ�Ε���������
        {
            percentage = mPlayerStatus.charaStatus.revaival;
            if (!ReLot(percentage))//���I���Ȃ�������s�k
            {
                return -1;
            }
            else
            {
                //���I����Ε���
                mBossData.IsPlayerRevaival = true;
            }
        }
        return next;
    }

    // ���ڔԍ�24�̕�V�A�L�^�f�[�^����
    private int P24()
    {
        return -1;
    }

    //���ڔԍ�24�̍Ē��I����
    private int P24_Relot()
    {
        int next = -1;

        float percentage = mPlayerStatus.charaStatus.charColorUP;
        float attack = mPlayerStatus.charaStatus.preemptiveAttack * 0.80f;//�U����(��)
        if (ReLot(percentage))
        {
            attack = mPlayerStatus.charaStatus.preemptiveAttack;//�U����(��)�ɂ���
        }

        mBossData.PlayerOneAttackPow = attack;

        return next;
    }

    // ���ڔԍ�25�̕�V�A�L�^�f�[�^����
    private int P25()
    {
        return -1;
    }

    private bool success = false;
    //���ڔԍ�25�̍Ē��I����
    private int P25_Relot()
    {
        int next = -1;

        success = false;

        float percentage = mPlayerStatus.charaStatus.cutIn;

        if (ReLot(percentage))
        {
            success = true;
            mBossData.PlayerOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].hp;
        }

        mBossData.IsConfirmationChoice = success;

         return next;
    }

    // ���ڔԍ�26�̕�V�A�L�^�f�[�^����
    private int P26()
    {
        return -1;
    }

    private int P26_Relot()
    {
        int next = -1;

        success = false;

        float percentage = mPlayerStatus.charaStatus.cutIn;

        if (ReLot(percentage))
        {
            success = true;
        }

        mBossData.IsConfirmationChoice = success;

        return next;
    }

    // ���ڔԍ�27�̕�V�A�L�^�f�[�^����
    private int P27()
    {
        return -1;
    }

    // ���ڔԍ�28�̕�V�A�L�^�f�[�^����
    private int P28()
    {
        return -1;
    }

    // ���ڔԍ�29�̕�V�A�L�^�f�[�^����
    private int P29()
    {
        return -1;
    }

    //���ڔԍ�29�̍Ē��I����
    private int P29_Relot()
    {
        int next = -1;

        success = false;

        float percentage = mPlayerStatus.charaStatus.cutIn;

        if (ReLot(percentage))
        {
            success = true;
        }

        mBossData.IsConfirmationChoice = success;

        return next;
    }

    // ���ڔԍ�30�̕�V�A�L�^�f�[�^����
    private int P30()
    {
        return -1;
    }

    //���ڔԍ�30�̍Ē��I����
    private int P30_Relot()
    {
        int next = -1;
        mBossData.IsPlayerRevaival = false;
        int[] preemptive = new int[2] { 4, 10 };
        float percentage = mPlayerStatus.charaStatus.preemptiveAttack;
        float playerHp = mPlayerStatus.hp;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * 0.60f;//���̕ϓ����̃A�^�b�N��
        playerHp -= mBossData.BossOneAttackPow;//�{�X�̍U���́i���j��hp�������
        if (playerHp <= 0f)//�̗͂������Ȃ�Ε���������
        {
            percentage = mPlayerStatus.charaStatus.revaival;
            if (!ReLot(percentage))//���I���Ȃ�������s�k
            {
                return -1;
            }
            else
            {
                //���I�����Ȃ畜��
                mBossData.IsPlayerRevaival = true;
            }
        }
        else//�搧�U���̒l�ōĒ��I
        {
            if (ReLot(percentage)) { next = preemptive[CS_LotteryFunction.LotNormalInt(preemptive.Length)] - 1; }//���I����ΐ搧�U���̔ԍ���Ԃ�
        }

        return next;
    }

    public override int DesisionFlag(int _val)
    {
        return mUniquePF_ReLotteryFunctions[_val]();
    }

    public override int ReLottery(int _val)
    {
        mBossData.BackUpHP = mPlayerStatus.hp;
        return mUniquePF_Functions[_val]();
    }
}
