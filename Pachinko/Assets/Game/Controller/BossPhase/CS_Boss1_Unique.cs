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

    //テスト用
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
            Debug.Log("復活処理開始");
            guage.PlayerHpRevival(); 
        }
    }

    // 項目番号4の報酬、記録データ処理
    private int P4()
    {
        return -1;
    }

    //項目番号4の再抽選処理
    private int P4_Relot()
    {
        int next = -1;
       
        float percentage = mPlayerStatus.charaStatus.charColorUP;
        float attack = mBossData.BossOneBlockHp;//1ゲージ分
        if(ReLot(percentage))
        {
            attackType = ATTACK_TYPE.MIDDLE;
            attack = mBossData.BossOneBlockHp*2;//攻撃力(中)にする
        }

        mBossData.PlayerOneAttackPow = attack;

        CheckPlayerkPowerUp(4);
        playerAttack = true;
        return next;
    }

    // 項目番号5の報酬、記録データ処理
    private int P5()
    {
        return -1;
    }

    //項目番号5の再抽選処理
    private int P5_Relot()
    {
        int next = -1;
        mBossData.IsPlayerRevaival = false;
        float percentage = mPlayerStatus.charaStatus.preemptiveAttack;
        float playerHp = mPlayerStatus.hp;
        attackType = ATTACK_TYPE.WEAK;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * boss_magnification[(int)attackType];//この変動時のアタック量
        CheakBossPowerUp();
        playerHp -= Mathf.Ceil(mBossData.BossOneAttackPow / mBossData.PlayerOneBlockHp);//ボスの攻撃をhpから引く
        Debug.Log("プレイヤーHP" + playerHp);
        if (playerHp <= 0f)//体力が無くなれば復活抽せん
        {
            percentage = mPlayerStatus.charaStatus.revaival;
            mBossData.IsPlayerRevaival = ReLot(percentage);//当選結果を復活フラグに更新
            bossAttack = true;
        }
        else//先制攻撃の値で再抽選
        {
            if (ReLot(percentage))
            {
                //当選すれば先制攻撃の番号を返す
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

    // 項目番号6の報酬、記録データ処理
    private int P6()
    {
        return -1;
    }

    //項目番号6の再抽選処理
    private int P6_Relot()
    {
        int next = -1;
        mBossData.IsPlayerRevaival = false;
        float percentage = mPlayerStatus.charaStatus.preemptiveAttack;
        float playerHp = mPlayerStatus.hp;
        attackType = ATTACK_TYPE.MIDDLE;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * boss_magnification[(int)attackType];//この変動時のアタック量
        CheakBossPowerUp();
        playerHp -= Mathf.Ceil(mBossData.BossOneAttackPow / mBossData.PlayerOneBlockHp);//ボスの攻撃をhpから引く
        
        if (playerHp <= 0f)//体力が無くなれば復活抽せん
        {
            percentage = mPlayerStatus.charaStatus.revaival;
            mBossData.IsPlayerRevaival = ReLot(percentage);//当選結果を復活フラグに更新
            bossAttack = true;
        }
        else//先制攻撃の値で再抽選
        {
            if (ReLot(percentage))
            {
                //当選すれば先制攻撃の番号を返す
                next = 10;
                mBossData.PlayerOneAttackPow = mBossData.BossOneBlockHp;
                CheckPlayerkPowerUp(10);
                playerAttack = true;
            }
            else { bossAttack = true; }
        }
        return next;
    }

    // 項目番号7の報酬、記録データ処理
    private int P7()
    {
        return -1;
    }


    //項目番号7の再抽選処理
    private int P7_Relot()
    {
        int next = -1;
        mBossData.IsPlayerRevaival = false;
        float playerHp = mPlayerStatus.hp;
        attackType = ATTACK_TYPE.WEAK;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * boss_magnification[(int)attackType];//この変動時のアタック量
        CheakBossPowerUp();
        playerHp -= Mathf.Ceil(mBossData.BossOneAttackPow / mBossData.PlayerOneBlockHp);//ボスの攻撃をhpから引く

        if (playerHp <= 0f)//体力が無くなれば復活抽せん
        {
            float percentage = mPlayerStatus.charaStatus.revaival;
            percentage = mPlayerStatus.charaStatus.revaival;
            mBossData.IsPlayerRevaival = ReLot(percentage);//当選結果を復活フラグに更新
        }
        bossAttack = true;
        return next;
    }

   
    // 項目番号8の報酬、記録データ処理
    private int P8()
    {
        return -1;
    }

    //項目番号8の再抽選処理
    private int P8_Relot()
    {
        int next = -1;
        float playerHp = mPlayerStatus.hp;
        attackType = ATTACK_TYPE.STRONG;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * boss_magnification[(int)attackType];//この変動時のアタック量
        playerHp -= Mathf.Ceil(mBossData.BossOneAttackPow / mBossData.PlayerOneBlockHp);//ボスの攻撃をhpから引く
        if (playerHp <= 0f)//体力が無くなれば復活抽せん
        {
            float percentage = mPlayerStatus.charaStatus.revaival;
            percentage = mPlayerStatus.charaStatus.revaival;
            mBossData.IsPlayerRevaival = ReLot(percentage);//当選結果を復活フラグに更新
        }
        bossAttack = true;
        return next;
    }

    // 項目番号9の報酬、記録データ処理
    private int P9()
    {
        return -1;
    }

    //項目番号9の再抽選処理
    private int P9_Relot()
    {
        int next = -1;
        float playerHp = mPlayerStatus.hp;
        attackType = ATTACK_TYPE.WEAK;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * boss_magnification[(int)attackType];//この変動時のアタック量
        CheakBossPowerUp();
        playerHp -= Mathf.Ceil(mBossData.BossOneAttackPow / mBossData.PlayerOneBlockHp);//ボスの攻撃をhpから引く
        if (playerHp <= 0f)//体力が無くなれば復活抽せん
        {
            float percentage = mPlayerStatus.charaStatus.revaival;
            percentage = mPlayerStatus.charaStatus.revaival;
            mBossData.IsPlayerRevaival = ReLot(percentage);//当選結果を復活フラグに更新
        }
        bossAttack = true;
        return next;
    }

    // 項目番号10の報酬、記録データ処理
    private int P10()
    {
        return -1;
    }

    //項目番号10の再抽選処理
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

    // 項目番号11の報酬、記録データ処理
    private int P11()
    {
        return -1;
    }

    //項目番号11の再抽選処理
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

    // 項目番号12の報酬、記録データ処理
    private int P12()
    {
        //特になし
        return -1;
    }

    //項目番号12の再抽選処理
    private int P12_Relot()
    {
        int next = -1;
        float percentage = mPlayerStatus.charaStatus.preemptiveAttack;
        if (ReLot(percentage)) 
        { 
            next = 10;  //当選すれば先制攻撃の番号を返す
            attackType = ATTACK_TYPE.WEAK;
            mBossData.PlayerOneAttackPow = mPlayerStatus.charaStatus.attack * player_magnification[(int)attackType];
            CheckPlayerkPowerUp(12);
            playerAttack = true;
        }

        return next;
    }

    // 項目番号13の報酬、記録データ処理
    private int P13()
    {
        return -1;
    }

    //項目番号13の再抽選処理
    private int P13_Relot()
    {
        int next = -1;
        mBossData.IsPlayerRevaival = false;
        float percentage = mPlayerStatus.charaStatus.preemptiveAttack;
        float playerHp = mPlayerStatus.hp;
        attackType = ATTACK_TYPE.WEAK;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * boss_magnification[(int)attackType];//この変動時のアタック量
        CheakBossPowerUp();
        playerHp -= Mathf.Ceil(mBossData.BossOneAttackPow / mBossData.PlayerOneBlockHp);//ボスの攻撃をhpから引く
        if (playerHp <= 0f)//体力が無くなれば復活抽せん
        {
            percentage = mPlayerStatus.charaStatus.revaival;
            mBossData.IsPlayerRevaival = ReLot(percentage);//当選結果を復活フラグに更新
            bossAttack = true;

        }
        else//先制攻撃の値で再抽選
        {
            if (ReLot(percentage))
            {
                //当選すれば10を返す
                next = 10;
                mBossData.PlayerOneAttackPow = mBossData.BossOneBlockHp;
                CheckPlayerkPowerUp(10);
                playerAttack = true;
            }
            else { bossAttack = true; }
        }

        return next;
    }

    // 項目番号14の報酬、記録データ処理
    private int P14()
    {
        return -1;
    }

    //項目番号13の再抽選処理
    private int P14_AttackPow()
    {
        attackType = ATTACK_TYPE.WEAK;
        mBossData.PlayerOneAttackPow = mPlayerStatus.charaStatus.attack * player_magnification[(int)attackType];
        CheckPlayerkPowerUp(14);
        playerAttack = true;
        return -1;
    }

    // 項目番号15の報酬、記録データ処理
    private int P15()
    {
        //次回攻撃確定
        int[] nextMissionNums = new int[] { 4,　10, 11, 17, 24};
        int missionIdx = CS_LotteryFunction.LotNormalInt(nextMissionNums.Length - 1);
        return nextMissionNums[missionIdx];
    }

    // 項目番号16の報酬、記録データ処理
    private int P16()
    {
        guage.PlayerHpHeal();
        return -1;
    }

    // 項目番号17の報酬、記録データ処理
    private int P17()
    {
        return -1;
    }

    //項目番号17の再抽選処理
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

    // 項目番号18の報酬、記録データ処理
    private int P18()
    {
        int next = -1;
        if(mBossData.BossBuff_Debuff == CS_BossPhaseData.BUFF_DEBUFF.DEBUFF)
        {
            //次回攻撃確定
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

    //項目番号18の再抽選処理ここではバフ、デバフをきめつ
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

    // 項目番号19の報酬、記録データ処理
    private int P19()
    {
        //特になし
        return -1;
    }

    //項目番号19,20の再抽選処理
    private int P19_20Relot()
    {
        int next = -1;
        mBossData.IsPlayerRevaival = false;
        float percentage = mPlayerStatus.charaStatus.preemptiveAttack;


        if (ReLot(percentage)) 
        {
            //当選すれば先制攻撃の番号を返す
            next = 10;
            attackType = ATTACK_TYPE.WEAK;
            mBossData.PlayerOneAttackPow = mPlayerStatus.charaStatus.attack * player_magnification[(int)attackType];
            CheckPlayerkPowerUp(10);
            playerAttack = true;
        }


        return next;
    }

    // 項目番号20の報酬、記録データ処理
    private int P20()
    {
        //特になし
        return -1;
    }

    // 項目番号21の報酬、記録データ処理
    private int P21()
    {
        mBossData.PlayerBuff_Debuff = CS_BossPhaseData.BUFF_DEBUFF.BUFF_SMALL;
        return -1;
    }

    // 項目番号22の報酬、記録データ処理
    private int P22()
    {
        mBossData.PlayerBuff_Debuff = CS_BossPhaseData.BUFF_DEBUFF.BUFF_BIG;
        return -1;
    }

    // 項目番号23の報酬、記録データ処理
    private int P23()
    {
        return -1;
    }

    //項目番号23の再抽選処理
    private int P23_Relot()
    {
        int next = -1;
        float playerHp = mPlayerStatus.hp;
        attackType = ATTACK_TYPE.MIDDLE;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * boss_magnification[(int)attackType];//この変動時のアタック量
        CheakBossPowerUp();
        playerHp -= Mathf.Ceil(mBossData.BossOneAttackPow / mBossData.PlayerOneBlockHp);//ボスの攻撃をhpから引く
        if (playerHp <= 0f)//体力が無くなれば復活抽せん
        {
            float percentage = mPlayerStatus.charaStatus.revaival;
            percentage = mPlayerStatus.charaStatus.revaival;
            mBossData.IsPlayerRevaival = ReLot(percentage);//当選結果を復活フラグに更新
        }
        bossAttack = true;
        return next;
    }

    // 項目番号24の報酬、記録データ処理
    private int P24()
    {
        return -1;
    }

    //項目番号24の再抽選処理
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

    // 項目番号25の報酬、記録データ処理
    private int P25()
    {
        return -1;
    }

    private bool success = false;
    //項目番号25の再抽選処理
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

    // 項目番号26の報酬、記録データ処理
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
        //攻撃力(中)
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

    // 項目番号27の報酬、記録データ処理
    private int P27()
    {
        mBossData.IsSkillStrong = true;
        return -1;
    }

    // 項目番号28の報酬、記録データ処理
    private int P28()
    {
        //次回攻撃確定
        int[] nextMissionNums = new int[] { 4, 10, 11, 17, 24 };
        int missionIdx = CS_LotteryFunction.LotNormalInt(nextMissionNums.Length - 1);
        return nextMissionNums[missionIdx];
    }

    // 項目番号29の報酬、記録データ処理
    private int P29()
    {
        if (mBossData.IsPartnereJoin) 
        {
            mBossData.IsPartnereJoin = false;
            return -1;
        }
        return -1;
    }

    //項目番号29の再抽選処理
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
            Debug.Log("仲間参戦");
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

    // 項目番号30の報酬、記録データ処理
    private int P30()
    {
        return -1;
    }

    //項目番号30の再抽選処理
    private int P30_Relot()
    {
        int next = -1;
        mBossData.IsPlayerRevaival = false;
        float percentage = mPlayerStatus.charaStatus.preemptiveAttack;
        float playerHp = mPlayerStatus.hp;
        attackType = ATTACK_TYPE.WEAK;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * boss_magnification[(int)attackType];//この変動時のアタック量
        CheakBossPowerUp();
        playerHp -= Mathf.Ceil(mBossData.BossOneAttackPow / mBossData.PlayerOneBlockHp);//ボスの攻撃をhpから引く
        if (playerHp <= 0f)//体力が無くなれば復活抽せん
        {
            percentage = mPlayerStatus.charaStatus.revaival;
            mBossData.IsPlayerRevaival = ReLot(percentage);//当選結果を復活フラグに更新
            bossAttack = true;
        }
        else//先制攻撃の値で再抽選
        {
            if (ReLot(percentage))
            {
                //当選すれば10を返す
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
        //Debug.Log("抽選後処理" + _val);
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

    //次回確定フラグなどを変更処理
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
                Debug.Log("ボスパワーアップ中");
                break;
            case ATTACK_TYPE.MIDDLE:
                attackType = ATTACK_TYPE.STRONG;
                Debug.Log("ボスパワーアップ強");
                break;
        }
        if(mBossData.BossBuff_Debuff == CS_BossPhaseData.BUFF_DEBUFF.BUFF_BIG)
        {
            Debug.Log("ボスパワーアップ2段階");
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
            Debug.Log("ダメージ1段階アッププレイヤーパワーアップ");
            return; 
        }
        if (mBossData.IsSkillStrong)
        {
            mBossData.PlayerOneAttackPow *= 2;
            Debug.Log("プレイヤーパワーアップダメージ2倍");
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
            Debug.Log("ボスの消去ブロック数:+" + buffPow );
            return;
        }

        switch (attackType)
        {
            case ATTACK_TYPE.WEAK:
                attackType = ATTACK_TYPE.MIDDLE;
                Debug.Log("プレイヤーパワーアップ中");
                break;
            case ATTACK_TYPE.MIDDLE:
                attackType = ATTACK_TYPE.STRONG;
                Debug.Log("プレイヤーパワーアップ強");
                break;
        }

        if (mBossData.BossBuff_Debuff == CS_BossPhaseData.BUFF_DEBUFF.BUFF_BIG)
        {
            Debug.Log("プレイヤーパワーアップバフ大");
            attackType = ATTACK_TYPE.STRONG;
        }
        mBossData.PlayerOneAttackPow = mBossData.PlayerStatus.charaStatus.attack * player_magnification[(int)attackType];
    }

}
