using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CS_Boss1_Unique : CS_BossUnique
{
    private Func<int>[] mUniquePF_Functions;
    private Func<int>[] mUniquePF_ReLotteryFunctions;
    private enum ATTACK_TYPE
    {
        WEAK = 0,
        MIDDLE,
        STRONG
    }
    float[] boss_magnification = new float[] { 0.50f, 0.70f, 0.90f };
    float[] player_magnification = new float[] { 0.60f, 0.60f, 1.0f };
    ATTACK_TYPE attackType;

    bool playerAttack = false;
    bool bossAttack = false;

    //�e�X�g�p
    CS_HpGuage guage;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        mUniquePF_Functions = new Func<int>[] { P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21, P22, P23, P24, P25, P26, P27, P28, P29, P30 };
        mUniquePF_ReLotteryFunctions = new Func<int>[] { P4_Relot, P5_Relot, P6_Relot, P7_Relot, P8_Relot, P9_Relot, P10_Relot, P11_Relot, P12_Relot, P13_Relot, P14_AttackPow,P17_Relot,P18_Relot, P19_20Relot, P19_20Relot,
            P23_Relot, P24_Relot, P25_Relot, P26_Relot, P29_Relot, P30_Relot };

        guage = GameObject.Find("HpGuage").GetComponent<CS_HpGuage>();
    }

    private IEnumerator Revaival()
    {
        while (!guage.HpDownUpdateFinish) { yield return null; }
        if (mBossData.IsPlayerRevaival)
        {
            Debug.Log("���������J�n");
            guage.PlayerHpRevival(); 
        }
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
        float attack = mBossData.BossOneBlockHp;//1�Q�[�W��
        if(ReLot(percentage))
        {
            attackType = ATTACK_TYPE.MIDDLE;
            attack = mBossData.BossOneBlockHp*2;//�U����(��)�ɂ���
        }

        mBossData.PlayerOneAttackPow = attack;

        CheckPlayerkPowerUp(4);
        playerAttack = true;
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
        float percentage = mPlayerStatus.charaStatus.preemptiveAttack;
        float playerHp = mPlayerStatus.hp;
        attackType = ATTACK_TYPE.WEAK;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * boss_magnification[(int)attackType];//���̕ϓ����̃A�^�b�N��
        CheakBossPowerUp();
        playerHp -= Mathf.Ceil(mBossData.BossOneAttackPow / mBossData.PlayerOneBlockHp);//�{�X�̍U����hp�������
        Debug.Log("�v���C���[HP" + playerHp);
        if (playerHp <= 0f)//�̗͂������Ȃ�Ε���������
        {
            percentage = mPlayerStatus.charaStatus.revaival;
            mBossData.IsPlayerRevaival = ReLot(percentage);//���I���ʂ𕜊��t���O�ɍX�V
            bossAttack = true;
        }
        else//�搧�U���̒l�ōĒ��I
        {
            if (ReLot(percentage))
            {
                //���I����ΐ搧�U���̔ԍ���Ԃ�
                next = 10;
                mBossData.PlayerOneAttackPow = mBossData.BossOneBlockHp;
                CheckPlayerkPowerUp(10);
                playerAttack = true;
            }
            else
            { 
                bossAttack = true; 
            }
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
        float percentage = mPlayerStatus.charaStatus.preemptiveAttack;
        float playerHp = mPlayerStatus.hp;
        attackType = ATTACK_TYPE.MIDDLE;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * boss_magnification[(int)attackType];//���̕ϓ����̃A�^�b�N��
        CheakBossPowerUp();
        playerHp -= Mathf.Ceil(mBossData.BossOneAttackPow / mBossData.PlayerOneBlockHp);//�{�X�̍U����hp�������
        
        if (playerHp <= 0f)//�̗͂������Ȃ�Ε���������
        {
            percentage = mPlayerStatus.charaStatus.revaival;
            mBossData.IsPlayerRevaival = ReLot(percentage);//���I���ʂ𕜊��t���O�ɍX�V
            bossAttack = true;
        }
        else//�搧�U���̒l�ōĒ��I
        {
            if (ReLot(percentage))
            {
                //���I����ΐ搧�U���̔ԍ���Ԃ�
                next = 10;
                mBossData.PlayerOneAttackPow = mBossData.BossOneBlockHp;
                CheckPlayerkPowerUp(10);
                playerAttack = true;
            }
            else { bossAttack = true; }
        }
        return next;
    }

    // ���ڔԍ�7�̕�V�A�L�^�f�[�^����
    private int P7()
    {
        return -1;
    }


    //���ڔԍ�7�̍Ē��I����
    private int P7_Relot()
    {
        int next = -1;
        mBossData.IsPlayerRevaival = false;
        float playerHp = mPlayerStatus.hp;
        attackType = ATTACK_TYPE.WEAK;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * boss_magnification[(int)attackType];//���̕ϓ����̃A�^�b�N��
        CheakBossPowerUp();
        playerHp -= Mathf.Ceil(mBossData.BossOneAttackPow / mBossData.PlayerOneBlockHp);//�{�X�̍U����hp�������

        if (playerHp <= 0f)//�̗͂������Ȃ�Ε���������
        {
            float percentage = mPlayerStatus.charaStatus.revaival;
            percentage = mPlayerStatus.charaStatus.revaival;
            mBossData.IsPlayerRevaival = ReLot(percentage);//���I���ʂ𕜊��t���O�ɍX�V
        }
        bossAttack = true;
        return next;
    }

   
    // ���ڔԍ�8�̕�V�A�L�^�f�[�^����
    private int P8()
    {
        return -1;
    }

    //���ڔԍ�8�̍Ē��I����
    private int P8_Relot()
    {
        int next = -1;
        float playerHp = mPlayerStatus.hp;
        attackType = ATTACK_TYPE.STRONG;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * boss_magnification[(int)attackType];//���̕ϓ����̃A�^�b�N��
        playerHp -= Mathf.Ceil(mBossData.BossOneAttackPow / mBossData.PlayerOneBlockHp);//�{�X�̍U����hp�������
        if (playerHp <= 0f)//�̗͂������Ȃ�Ε���������
        {
            float percentage = mPlayerStatus.charaStatus.revaival;
            percentage = mPlayerStatus.charaStatus.revaival;
            mBossData.IsPlayerRevaival = ReLot(percentage);//���I���ʂ𕜊��t���O�ɍX�V
        }
        bossAttack = true;
        return next;
    }

    // ���ڔԍ�9�̕�V�A�L�^�f�[�^����
    private int P9()
    {
        return -1;
    }

    //���ڔԍ�9�̍Ē��I����
    private int P9_Relot()
    {
        int next = -1;
        float playerHp = mPlayerStatus.hp;
        attackType = ATTACK_TYPE.WEAK;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * boss_magnification[(int)attackType];//���̕ϓ����̃A�^�b�N��
        CheakBossPowerUp();
        playerHp -= Mathf.Ceil(mBossData.BossOneAttackPow / mBossData.PlayerOneBlockHp);//�{�X�̍U����hp�������
        if (playerHp <= 0f)//�̗͂������Ȃ�Ε���������
        {
            float percentage = mPlayerStatus.charaStatus.revaival;
            percentage = mPlayerStatus.charaStatus.revaival;
            mBossData.IsPlayerRevaival = ReLot(percentage);//���I���ʂ𕜊��t���O�ɍX�V
        }
        bossAttack = true;
        return next;
    }

    // ���ڔԍ�10�̕�V�A�L�^�f�[�^����
    private int P10()
    {
        return -1;
    }

    //���ڔԍ�10�̍Ē��I����
    private int P10_Relot()
    {
        int next = -1;
        float percentage = mPlayerStatus.charaStatus.charColorUP;
        attackType = ATTACK_TYPE.WEAK;
       
        if (ReLot(percentage))
        {
            attackType = ATTACK_TYPE.MIDDLE;
        }

        mBossData.PlayerOneAttackPow = mPlayerStatus.charaStatus.attack * player_magnification[(int)attackType];
        CheckPlayerkPowerUp(10);
        playerAttack = true;
        return next;
    }

    // ���ڔԍ�11�̕�V�A�L�^�f�[�^����
    private int P11()
    {
        return -1;
    }

    //���ڔԍ�11�̍Ē��I����
    private int P11_Relot()
    {
        int next = -1;
        float percentage = mPlayerStatus.charaStatus.charColorUP;
        attackType = ATTACK_TYPE.WEAK;

        if (ReLot(percentage))
        {
            attackType = ATTACK_TYPE.MIDDLE;
        }

        mBossData.PlayerOneAttackPow = mPlayerStatus.charaStatus.attack * player_magnification[(int)attackType];
        CheckPlayerkPowerUp(11);
        playerAttack = true;
        return next;
    }

    // ���ڔԍ�12�̕�V�A�L�^�f�[�^����
    private int P12()
    {
        //���ɂȂ�
        return -1;
    }

    //���ڔԍ�12�̍Ē��I����
    private int P12_Relot()
    {
        int next = -1;
        float percentage = mPlayerStatus.charaStatus.preemptiveAttack;
        if (ReLot(percentage)) 
        { 
            next = 10;  //���I����ΐ搧�U���̔ԍ���Ԃ�
            attackType = ATTACK_TYPE.WEAK;
            mBossData.PlayerOneAttackPow = mPlayerStatus.charaStatus.attack * player_magnification[(int)attackType];
            CheckPlayerkPowerUp(12);
            playerAttack = true;
        }

        return next;
    }

    // ���ڔԍ�13�̕�V�A�L�^�f�[�^����
    private int P13()
    {
        return -1;
    }

    //���ڔԍ�13�̍Ē��I����
    private int P13_Relot()
    {
        int next = -1;
        mBossData.IsPlayerRevaival = false;
        float percentage = mPlayerStatus.charaStatus.preemptiveAttack;
        float playerHp = mPlayerStatus.hp;
        attackType = ATTACK_TYPE.WEAK;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * boss_magnification[(int)attackType];//���̕ϓ����̃A�^�b�N��
        CheakBossPowerUp();
        playerHp -= Mathf.Ceil(mBossData.BossOneAttackPow / mBossData.PlayerOneBlockHp);//�{�X�̍U����hp�������
        if (playerHp <= 0f)//�̗͂������Ȃ�Ε���������
        {
            percentage = mPlayerStatus.charaStatus.revaival;
            mBossData.IsPlayerRevaival = ReLot(percentage);//���I���ʂ𕜊��t���O�ɍX�V
            bossAttack = true;

        }
        else//�搧�U���̒l�ōĒ��I
        {
            if (ReLot(percentage))
            {
                //���I�����10��Ԃ�
                next = 10;
                mBossData.PlayerOneAttackPow = mBossData.BossOneBlockHp;
                CheckPlayerkPowerUp(10);
                playerAttack = true;
            }
            else { bossAttack = true; }
        }

        return next;
    }

    // ���ڔԍ�14�̕�V�A�L�^�f�[�^����
    private int P14()
    {
        return -1;
    }

    //���ڔԍ�13�̍Ē��I����
    private int P14_AttackPow()
    {
        attackType = ATTACK_TYPE.WEAK;
        mBossData.PlayerOneAttackPow = mPlayerStatus.charaStatus.attack * player_magnification[(int)attackType];
        CheckPlayerkPowerUp(14);
        playerAttack = true;
        return -1;
    }

    // ���ڔԍ�15�̕�V�A�L�^�f�[�^����
    private int P15()
    {
        //����U���m��
        int[] nextMissionNums = new int[] { 4,�@10, 11, 17, 24};
        int missionIdx = CS_LotteryFunction.LotNormalInt(nextMissionNums.Length - 1);
        return nextMissionNums[missionIdx];
    }

    // ���ڔԍ�16�̕�V�A�L�^�f�[�^����
    private int P16()
    {
        guage.PlayerHpHeal();
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
        attackType = ATTACK_TYPE.MIDDLE;

        if (ReLot(percentage))
        {
            attackType = ATTACK_TYPE.STRONG;
        }

        mBossData.PlayerOneAttackPow = mPlayerStatus.charaStatus.attack * player_magnification[(int)attackType];
        CheckPlayerkPowerUp(17);
        playerAttack = true;
        return next;
    }

    // ���ڔԍ�18�̕�V�A�L�^�f�[�^����
    private int P18()
    {
        int next = -1;
        if(mBossData.BossBuff_Debuff == CS_BossPhaseData.BUFF_DEBUFF.DEBUFF)
        {
            //����U���m��
            int[] nextMissionNums = new int[] { 4, 10, 11, 17, 24 };
            int missionIdx = CS_LotteryFunction.LotNormalInt(nextMissionNums.Length - 1);
            next = nextMissionNums[missionIdx];
        }
        if(mBossData.BossBuff_Debuff == CS_BossPhaseData.BUFF_DEBUFF.BUFF_SMALL || mBossData.BossBuff_Debuff == CS_BossPhaseData.BUFF_DEBUFF.BUFF_BIG)
        {
            next = 5;
        }
        return next;
    }

    //���ڔԍ�18�̍Ē��I���������ł̓o�t�A�f�o�t�����߂�
    private int P18_Relot()
    {
        int next = -1;
        int val = CS_LotteryFunction.LotNormalInt(3);
        if (val == 0) { mBossData.BossBuff_Debuff = CS_BossPhaseData.BUFF_DEBUFF.BUFF_SMALL; }
        else if (val == 1) { mBossData.BossBuff_Debuff = CS_BossPhaseData.BUFF_DEBUFF.BUFF_BIG; }
        else
        { 
           mBossData.BossBuff_Debuff = CS_BossPhaseData.BUFF_DEBUFF.DEBUFF;  
        }
        
        return next;
    }

    // ���ڔԍ�19�̕�V�A�L�^�f�[�^����
    private int P19()
    {
        //���ɂȂ�
        return -1;
    }

    //���ڔԍ�19,20�̍Ē��I����
    private int P19_20Relot()
    {
        int next = -1;
        mBossData.IsPlayerRevaival = false;
        float percentage = mPlayerStatus.charaStatus.preemptiveAttack;


        if (ReLot(percentage)) 
        {
            //���I����ΐ搧�U���̔ԍ���Ԃ�
            next = 10;
            attackType = ATTACK_TYPE.WEAK;
            mBossData.PlayerOneAttackPow = mPlayerStatus.charaStatus.attack * player_magnification[(int)attackType];
            CheckPlayerkPowerUp(10);
            playerAttack = true;
        }


        return next;
    }

    // ���ڔԍ�20�̕�V�A�L�^�f�[�^����
    private int P20()
    {
        //���ɂȂ�
        return -1;
    }

    // ���ڔԍ�21�̕�V�A�L�^�f�[�^����
    private int P21()
    {
        mBossData.PlayerBuff_Debuff = CS_BossPhaseData.BUFF_DEBUFF.BUFF_SMALL;
        return -1;
    }

    // ���ڔԍ�22�̕�V�A�L�^�f�[�^����
    private int P22()
    {
        mBossData.PlayerBuff_Debuff = CS_BossPhaseData.BUFF_DEBUFF.BUFF_BIG;
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
        float playerHp = mPlayerStatus.hp;
        attackType = ATTACK_TYPE.MIDDLE;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * boss_magnification[(int)attackType];//���̕ϓ����̃A�^�b�N��
        CheakBossPowerUp();
        playerHp -= Mathf.Ceil(mBossData.BossOneAttackPow / mBossData.PlayerOneBlockHp);//�{�X�̍U����hp�������
        if (playerHp <= 0f)//�̗͂������Ȃ�Ε���������
        {
            float percentage = mPlayerStatus.charaStatus.revaival;
            percentage = mPlayerStatus.charaStatus.revaival;
            mBossData.IsPlayerRevaival = ReLot(percentage);//���I���ʂ𕜊��t���O�ɍX�V
        }
        bossAttack = true;
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
        attackType = ATTACK_TYPE.MIDDLE;

        if (ReLot(percentage))
        {
            attackType = ATTACK_TYPE.STRONG;
        }

        mBossData.PlayerOneAttackPow = mPlayerStatus.charaStatus.attack * player_magnification[(int)attackType];
        CheckPlayerkPowerUp(17);
        playerAttack = true;
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
            playerAttack = true;
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
            playerAttack = true;
            // next = 24;
        }
        else
        {
            bossAttack = true;
            //next = 23;
        }

        attackType = ATTACK_TYPE.MIDDLE;
        //�U����(��)
        if (success)
        { 
            mBossData.PlayerOneAttackPow = mPlayerStatus.charaStatus.attack * player_magnification[(int)attackType];
            CheckPlayerkPowerUp(26);
        }
        else
        { 
            mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * boss_magnification[(int)attackType];
            CheakBossPowerUp();
        }

        return next;
    }

    // ���ڔԍ�27�̕�V�A�L�^�f�[�^����
    private int P27()
    {
        mBossData.IsSkillStrong = true;
        return -1;
    }

    // ���ڔԍ�28�̕�V�A�L�^�f�[�^����
    private int P28()
    {
        //����U���m��
        int[] nextMissionNums = new int[] { 4, 10, 11, 17, 24 };
        int missionIdx = CS_LotteryFunction.LotNormalInt(nextMissionNums.Length - 1);
        return nextMissionNums[missionIdx];
    }

    // ���ڔԍ�29�̕�V�A�L�^�f�[�^����
    private int P29()
    {
        if (mBossData.IsPartnereJoin) 
        {
            mBossData.IsPartnereJoin = false;
            return -1;
        }
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

        if(success)
        {
            mBossData.IsPartnereJoin = CS_LotteryFunction.LotJackpot(3);
            Debug.Log("���ԎQ��");
        }
        else
        {
            attackType = ATTACK_TYPE.WEAK;
            mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * boss_magnification[(int)attackType];
            CheakBossPowerUp();
            bossAttack = true;
        }
       

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
        float percentage = mPlayerStatus.charaStatus.preemptiveAttack;
        float playerHp = mPlayerStatus.hp;
        attackType = ATTACK_TYPE.WEAK;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * boss_magnification[(int)attackType];//���̕ϓ����̃A�^�b�N��
        CheakBossPowerUp();
        playerHp -= Mathf.Ceil(mBossData.BossOneAttackPow / mBossData.PlayerOneBlockHp);//�{�X�̍U����hp�������
        if (playerHp <= 0f)//�̗͂������Ȃ�Ε���������
        {
            percentage = mPlayerStatus.charaStatus.revaival;
            mBossData.IsPlayerRevaival = ReLot(percentage);//���I���ʂ𕜊��t���O�ɍX�V
            bossAttack = true;
        }
        else//�搧�U���̒l�ōĒ��I
        {
            if (ReLot(percentage))
            {
                //���I�����10��Ԃ�
                next = 10;
                mBossData.PlayerOneAttackPow = mBossData.BossOneBlockHp;
                CheckPlayerkPowerUp(10);
                playerAttack = true;
            }
            else { bossAttack = true; }
        }

        return next;
    }

    public override int DesisionFlag(int _val)
    {
        if (mBossData == null) { mBossData = GameObject.Find("BigController").GetComponent<CS_BossPhaseData>(); }
        if (bossAttack)
        {
            guage.PlayerHpDown();
            StartCoroutine(Revaival());   
        }
        if(playerAttack)
        {
            guage.BossHpDown();
        }
        //Debug.Log("���I�㏈��" + _val);
        int next = mUniquePF_Functions[_val]() - 1;
        FlagChange();
        return next;
    }

    public override int ReLottery(int _val)
    {
        if (mBossData == null) { mBossData = GameObject.Find("BigController").GetComponent<CS_BossPhaseData>(); }
        mBossData.BackUpHP = mPlayerStatus.hp;
        playerAttack = false;
        bossAttack = false;
        int next = mUniquePF_ReLotteryFunctions[_val]();
        mBossData.IsPlayerAttack = playerAttack;
        mBossData.IsBossAttack = bossAttack;
        return next -1;
    }

    //����m��t���O�Ȃǂ�ύX����
    private void FlagChange()
    {
        if(playerAttack)
        {
            playerAttack = false;
            if (mBossData.IsDamageOneRankUp) { mBossData.IsDamageOneRankUp = false; }
            if (mBossData.IsSkillStrong) { mBossData.IsSkillStrong = false; }
            if (mBossData.BossBuff_Debuff == CS_BossPhaseData.BUFF_DEBUFF.DEBUFF) { mBossData.BossBuff_Debuff = CS_BossPhaseData.BUFF_DEBUFF.NONE; }
            if (mBossData.PlayerBuff_Debuff == CS_BossPhaseData.BUFF_DEBUFF.BUFF_SMALL || mBossData.PlayerBuff_Debuff == CS_BossPhaseData.BUFF_DEBUFF.BUFF_BIG)
            { mBossData.PlayerBuff_Debuff = CS_BossPhaseData.BUFF_DEBUFF.NONE; }
           
            return;
        }
        else if(bossAttack)
        {
            bossAttack = false;
            if (mBossData.BossBuff_Debuff == CS_BossPhaseData.BUFF_DEBUFF.BUFF_SMALL || mBossData.BossBuff_Debuff == CS_BossPhaseData.BUFF_DEBUFF.BUFF_BIG)
            { mBossData.BossBuff_Debuff = CS_BossPhaseData.BUFF_DEBUFF.NONE; }
            if (mBossData.PlayerBuff_Debuff == CS_BossPhaseData.BUFF_DEBUFF.DEBUFF) { mBossData.BossBuff_Debuff = CS_BossPhaseData.BUFF_DEBUFF.NONE; }
        }
        mBossData.IsPlayerAttack = playerAttack;
        mBossData.IsBossAttack = bossAttack;

    }

    private void CheakBossPowerUp()
    {
       if(mBossData .BossBuff_Debuff == CS_BossPhaseData.BUFF_DEBUFF.DEBUFF || mBossData.BossBuff_Debuff == CS_BossPhaseData.BUFF_DEBUFF.NONE) { return; }
        
       switch(attackType)
        {
            case ATTACK_TYPE.WEAK:
                attackType = ATTACK_TYPE.MIDDLE;
                Debug.Log("�{�X�p���[�A�b�v��");
                break;
            case ATTACK_TYPE.MIDDLE:
                attackType = ATTACK_TYPE.STRONG;
                Debug.Log("�{�X�p���[�A�b�v��");
                break;
        }
        if(mBossData.BossBuff_Debuff == CS_BossPhaseData.BUFF_DEBUFF.BUFF_BIG)
        {
            Debug.Log("�{�X�p���[�A�b�v2�i�K");
            attackType = ATTACK_TYPE.STRONG; 
        }
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * boss_magnification[(int)attackType];
       // mBossData.Buff_Debuff = CS_BossPhaseData.BUFF_DEBUFF.NONE;
    }

    private void CheckPlayerkPowerUp(int _perf)
    {
        if(mBossData.IsDamageOneRankUp )
        { 
            DamegeRankUp(_perf);
            Debug.Log("�_���[�W1�i�K�A�b�v�v���C���[�p���[�A�b�v");
            return; 
        }
        if (mBossData.IsSkillStrong)
        {
            mBossData.PlayerOneAttackPow *= 2;
            Debug.Log("�v���C���[�p���[�A�b�v�_���[�W2�{");
            return;
        }
        if (mBossData.PlayerBuff_Debuff == CS_BossPhaseData.BUFF_DEBUFF.BUFF_SMALL || mBossData.PlayerBuff_Debuff == CS_BossPhaseData.BUFF_DEBUFF.BUFF_BIG)
        { DamegeBuffUp(_perf); }
    }

    private void DamegeRankUp(int _perf)
    {
        if(_perf == 4)
        {
            mBossData.PlayerOneAttackPow += mBossData.BossOneBlockHp;
            return;
        }

        switch (attackType)
        {
            case ATTACK_TYPE.WEAK:
                attackType = ATTACK_TYPE.MIDDLE;
                break;
            case ATTACK_TYPE.MIDDLE:
                attackType = ATTACK_TYPE.STRONG;
                break;
        }

        mBossData.PlayerOneAttackPow = mBossData.PlayerStatus.charaStatus.attack * player_magnification[(int)attackType];
        //mBossData.Buff_Debuff = CS_BossPhaseData.BUFF_DEBUFF.NONE;
    }
    private void DamegeBuffUp(int _perf)
    {
        int buffPow = (int)mBossData.PlayerBuff_Debuff;
        if (_perf == 4)
        {
            mBossData.PlayerOneAttackPow += mBossData.BossOneBlockHp * buffPow;
            Debug.Log("�{�X�̏����u���b�N��:+" + buffPow );
            return;
        }

        switch (attackType)
        {
            case ATTACK_TYPE.WEAK:
                attackType = ATTACK_TYPE.MIDDLE;
                Debug.Log("�v���C���[�p���[�A�b�v��");
                break;
            case ATTACK_TYPE.MIDDLE:
                attackType = ATTACK_TYPE.STRONG;
                Debug.Log("�v���C���[�p���[�A�b�v��");
                break;
        }

        if (mBossData.BossBuff_Debuff == CS_BossPhaseData.BUFF_DEBUFF.BUFF_BIG)
        {
            Debug.Log("�v���C���[�p���[�A�b�v�o�t��");
            attackType = ATTACK_TYPE.STRONG;
        }
        mBossData.PlayerOneAttackPow = mBossData.PlayerStatus.charaStatus.attack * player_magnification[(int)attackType];
    }

}
