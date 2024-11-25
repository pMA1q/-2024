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
        mUniquePF_ReLotteryFunctions = new Func<int>[] { P4_Relot, P5_Relot, P6_Relot, P7_Relot, P8_Relot, P9_Relot, P10_11Relot, P10_11Relot, P12_Relot, P13_Relot, P17_Relot, P19_20Relot, P19_20Relot,
            P23_Relot, P24_Relot, P25_26Relot, P25_26Relot, P29_Relot, P30_Relot };

    }

    // ���ڔԍ�4�̕�V�A�L�^�f�[�^����
    private int P4()
    {
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
            if (ReLot(percentage)) { next = CS_LotteryFunction.LotNormalInt(preemptive.Length) - 1; }//���I����ΐ搧�U���̔ԍ���Ԃ�
        }

        return next;
    }

    // ���ڔԍ�6�̕�V�A�L�^�f�[�^����
    private int P6()
    {
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
            if (ReLot(percentage)) { next = CS_LotteryFunction.LotNormalInt(preemptive.Length) - 1; }//���I����ΐ搧�U���̔ԍ���Ԃ�
        }

        //�v���C���[�̗͍X�V
        mPlayerStatus.hp = playerHp;

        return next;
    }

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

    // ���ڔԍ�7�̕�V�A�L�^�f�[�^����
    private int P7()
    {
        return -1;
    }

    // ���ڔԍ�8�̕�V�A�L�^�f�[�^����
    private int P8()
    {
        return -1;
    }

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

    // ���ڔԍ�9�̕�V�A�L�^�f�[�^����
    private int P9()
    {
        return -1;
    }

    private int P9_Relot()
    {
        int next = -1;
        mBossData.IsPlayerRevaival = false;
        int[] preemptive = new int[2] { 4, 10 };
        float percentage = mPlayerStatus.charaStatus.preemptiveAttack;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * 0.60f;//���̕ϓ����̃A�^�b�N��
        
        if (ReLot(percentage)) { next = CS_LotteryFunction.LotNormalInt(preemptive.Length) - 1; }//���I����ΐ搧�U���̔ԍ���Ԃ�
        

        return next;
    }

    // ���ڔԍ�10�̕�V�A�L�^�f�[�^����
    private int P10()
    {
        return -1;
    }

    //���ڔԍ�10,11�̍Ē��I����
    private int P10_11Relot()
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
        return -1;
    }

    // ���ڔԍ�12�̕�V�A�L�^�f�[�^����
    private int P12()
    {
        return -1;
    }
    private int P12_Relot()
    {
        int next = -1;
        mBossData.IsPlayerRevaival = false;
        int[] preemptive = new int[2] { 4, 10 };
        float percentage = mPlayerStatus.charaStatus.preemptiveAttack;
        if (ReLot(percentage)) { next = CS_LotteryFunction.LotNormalInt(preemptive.Length) - 1; }//���I����ΐ搧�U���̔ԍ���Ԃ�

        return next;
    }

    // ���ڔԍ�13�̕�V�A�L�^�f�[�^����
    private int P13()
    {
        return -1;
    }

    private int P13_Relot()
    {
        int next = -1;

        float percentage = mPlayerStatus.charaStatus.revaival;
        float playerHp = mPlayerStatus.hp;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack;//���̕ϓ����̃A�^�b�N��
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

    private int P19_20Relot()
    {
        int next = -1;
        mBossData.IsPlayerRevaival = false;
        int[] preemptive = new int[2] { 4, 10 };
        float percentage = mPlayerStatus.charaStatus.preemptiveAttack;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * 0.60f;//���̕ϓ����̃A�^�b�N��

        if (ReLot(percentage)) { next = CS_LotteryFunction.LotNormalInt(preemptive.Length) - 1; }//���I����ΐ搧�U���̔ԍ���Ԃ�


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

    private int P23_Relot()
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
    //���ڔԍ�25,26�̍Ē��I����
    private int P25_26Relot()
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

    // ���ڔԍ�26�̕�V�A�L�^�f�[�^����
    private int P26()
    {
        return -1;
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

    //���ڔԍ�25,26�̍Ē��I����
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

    private int P30_Relot()
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

    public override int DesisionFlag(int _val)
    {
        return mUniquePF_ReLotteryFunctions[_val]();
    }

    public override int ReLottery(int _val)
    {
        return mUniquePF_Functions[_val]();
    }
}
