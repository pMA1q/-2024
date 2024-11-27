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

    // 項目番号4の報酬、記録データ処理
    private int P4()
    {
        float bossHp = mBossStatus.infomations[mBossData.BossNumber].hp;
        bossHp -= mBossData.PlayerOneAttackPow;
        if(bossHp <= 0.0f)
        {
            bossHp = 0;
            mBossData.IsSubjugation = true;//ボス討伐フラグをtrue
        }
        mBossStatus.infomations[mBossData.BossNumber].hp = bossHp;
        return -1;
    }

    //項目番号4の再抽選処理
    private int P4_Relot()
    {
        int next = -1;
       
        float percentage = mPlayerStatus.charaStatus.charColorUP;
        float attack = mPlayerStatus.charaStatus.preemptiveAttack * 0.60f;//攻撃力(弱)
        if(ReLot(percentage))
        {
            attack = mPlayerStatus.charaStatus.preemptiveAttack * 0.80f;//攻撃力(中)にする
        }

        mBossData.PlayerOneAttackPow = attack;

        return next;
    }

    // 項目番号5の報酬、記録データ処理
    private int P5()
    {
        mPlayerStatus.hp -= mBossData.BossOneAttackPow;

        //復活フラグが立っているなら、元に戻す
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

    //項目番号5の再抽選処理
    private int P5_Relot()
    {
        int next = -1;
        mBossData.IsPlayerRevaival = false;
        int[] preemptive = new int[2] { 4, 10 };
        float percentage = mPlayerStatus.charaStatus.preemptiveAttack;
        float playerHp = mPlayerStatus.hp;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * 0.60f;//この変動時のアタック量
        playerHp -= mBossData.BossOneAttackPow;//ボスの攻撃力（弱）をhpから引く
        if (playerHp <= 0f)//体力が無くなれば復活抽せん
        {
            percentage = mPlayerStatus.charaStatus.revaival;
            if (!ReLot(percentage))//当選しなかったら敗北
            {
                return -1;

            }
            else
            {
                //当選したなら復活
                mBossData.IsPlayerRevaival = true;
            }
        }
        else//先制攻撃の値で再抽選
        {
            if (ReLot(percentage)) { next = preemptive[CS_LotteryFunction.LotNormalInt(preemptive.Length)] - 1; }//当選すれば先制攻撃の番号を返す
        }
        return next;
    }

    // 項目番号6の報酬、記録データ処理
    private int P6()
    {
        mPlayerStatus.hp -= mBossData.BossOneAttackPow;

        //復活フラグが立っているなら、元に戻す
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

    //項目番号6の再抽選処理
    private int P6_Relot()
    {
        int next = -1;
        mBossData.IsPlayerRevaival = false;
        int[] preemptive = new int[2] { 4, 10 };
        float percentage = mPlayerStatus.charaStatus.preemptiveAttack;
        float playerHp = mPlayerStatus.hp;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * 0.80f;//この変動時のアタック量
        playerHp -= mBossData.BossOneAttackPow;//ボスの攻撃力（中）をhpから引く
        if (playerHp <= 0f)//体力が無くなれば復活抽せん
        {
            percentage = mPlayerStatus.charaStatus.revaival;
            if (!ReLot(percentage))//当選しなかったら敗北
            {
                return -1;
            }
            else
            {
                //当選したなら復活
                mBossData.IsPlayerRevaival = true;
            }
        }
        else//先制攻撃の値で再抽選
        {
            if (ReLot(percentage)) { next = preemptive[CS_LotteryFunction.LotNormalInt(preemptive.Length)] - 1; }//当選すれば先制攻撃の番号を返す
        }
        return next;
    }

    // 項目番号7の報酬、記録データ処理
    private int P7()
    {
        mPlayerStatus.hp -= mBossData.BossOneAttackPow;

        //復活フラグが立っているなら、元に戻す
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


    //項目番号7の再抽選処理
    private int P7_Relot()
    {
        int next = -1;

        float percentage = mPlayerStatus.charaStatus.revaival;
        float playerHp = mPlayerStatus.hp;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * 0.60f;//この変動時のアタック量
        playerHp -= mBossData.BossOneAttackPow;//ボスの攻撃力（中）をhpから引く
        if (playerHp <= 0f)//体力が無くなれば復活抽せん
        {
            percentage = mPlayerStatus.charaStatus.revaival;
            if (ReLot(percentage))
            {
                //当選すれば復活
                mBossData.IsPlayerRevaival = true;
            }
            
        }
        return next;
    }

   
    // 項目番号8の報酬、記録データ処理
    private int P8()
    {
        mPlayerStatus.hp -= mBossData.BossOneAttackPow;

        //復活フラグが立っているなら、元に戻す
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

    //項目番号8の再抽選処理
    private int P8_Relot()
    {
        int next = -1;

        float percentage = mPlayerStatus.charaStatus.revaival;
        float playerHp = mPlayerStatus.hp;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack;//この変動時のアタック量
        playerHp -= mBossData.BossOneAttackPow;//ボスの攻撃力（中）をhpから引く
        if (playerHp <= 0f)//体力が無くなれば復活抽せん
        {
            percentage = mPlayerStatus.charaStatus.revaival;
            if (ReLot(percentage))
            {
                //当選すれば復活
                mBossData.IsPlayerRevaival = true;
            }

        }
        return next;
    }

    // 項目番号9の報酬、記録データ処理
    private int P9()
    {
        mPlayerStatus.hp -= mBossData.BossOneAttackPow;

        //復活フラグが立っているなら、元に戻す
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

    //項目番号9の再抽選処理
    private int P9_Relot()
    {
        int next = -1;
        mBossData.IsPlayerRevaival = false;
        int[] preemptive = new int[2] { 4, 10 };
        float percentage = mPlayerStatus.charaStatus.preemptiveAttack;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * 0.60f;//この変動時のアタック量
        
        if (ReLot(percentage)) 
        { 
            next = preemptive[CS_LotteryFunction.LotNormalInt(preemptive.Length)] - 1; //当選すれば先制攻撃の番号を返す
        }
        

        return next;
    }

    // 項目番号10の報酬、記録データ処理
    private int P10()
    {
        mBossStatus.infomations[mBossData.BossNumber].hp -= mBossData.PlayerOneAttackPow;

        //復活フラグが立っているなら、元に戻す
        if (mBossStatus.infomations[mBossData.BossNumber].hp <= 0)
        {
            mBossData.IsSubjugation = true;
        }
        return -1;
    }

    //項目番号10の再抽選処理
    private int P10_Relot()
    {
        int next = -1;

        float percentage = mPlayerStatus.charaStatus.charColorUP;
        float attack = mPlayerStatus.charaStatus.preemptiveAttack * 0.60f;//攻撃力(弱)
        if (ReLot(percentage))
        {
            attack = mPlayerStatus.charaStatus.preemptiveAttack * 0.80f;//攻撃力(中)にする
        }

        mBossData.PlayerOneAttackPow = attack;

        return next;
    }

    // 項目番号11の報酬、記録データ処理
    private int P11()
    {
        mBossStatus.infomations[mBossData.BossNumber].hp -= mBossData.PlayerOneAttackPow;

        //ボスの体力が無くなれば討伐成功
        if (mBossStatus.infomations[mBossData.BossNumber].hp <= 0)
        {
            mBossData.IsSubjugation = true;
        }
        return -1;
    }

    //項目番号11の再抽選処理
    private int P11_Relot()
    {
        int next = -1;

        float percentage = mPlayerStatus.charaStatus.charColorUP;
        float attack = mPlayerStatus.charaStatus.attack * 0.60f;//攻撃力(弱)
        if (ReLot(percentage))
        {
            attack = mPlayerStatus.charaStatus.attack * 0.80f;//攻撃力(中)にする
        }

        mBossData.PlayerOneAttackPow = attack;

        return next;
    }

    // 項目番号12の報酬、記録データ処理
    private int P12()
    {
        return -1;
    }

    //項目番号12の再抽選処理
    private int P12_Relot()
    {
        int next = -1;
        int[] preemptive = new int[2] { 4, 10 };
        float percentage = mPlayerStatus.charaStatus.preemptiveAttack;
        if (ReLot(percentage)) 
        { 
            next = preemptive[CS_LotteryFunction.LotNormalInt(preemptive.Length)] - 1;  //当選すれば先制攻撃の番号を返す
            mBossData.PlayerOneAttackPow = mPlayerStatus.charaStatus.preemptiveAttack * 0.60f;
        }

        return next;
    }

    // 項目番号13の報酬、記録データ処理
    private int P13()
    {
        mPlayerStatus.hp -= mBossData.BossOneAttackPow;

        //復活フラグが立っているなら、元に戻す
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

    //項目番号13の再抽選処理
    private int P13_Relot()
    {
        int next = -1;

        float percentage = mPlayerStatus.charaStatus.revaival;
        float playerHp = mPlayerStatus.hp;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * 0.60f;//この変動時のアタック量
        playerHp -= mBossData.BossOneAttackPow;//ボスの攻撃力（中）をhpから引く
        if (playerHp <= 0f)//体力が無くなれば復活抽せん
        {
            percentage = mPlayerStatus.charaStatus.revaival;
            if (!ReLot(percentage))//当選しなかったら敗北
            {
                return -1;
            }
            else
            {
                //当選すれば復活
                mBossData.IsPlayerRevaival = true;
            }
        }
        return next;
    }

    // 項目番号14の報酬、記録データ処理
    private int P14()
    {
        //連続攻撃数分減らす
        mBossStatus.infomations[mBossData.BossNumber].hp -= mPlayerStatus.attack * 0.60f * mBossData.SuccessionNum;

        //ボスの体力が無くなれば討伐成功
        if (mBossStatus.infomations[mBossData.BossNumber].hp <= 0)
        {
            mBossData.IsSubjugation = true;
        }
        return -1;
    }

    // 項目番号15の報酬、記録データ処理
    private int P15()
    {
        
        return -1;
    }

    // 項目番号16の報酬、記録データ処理
    private int P16()
    {
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
        float attack = mPlayerStatus.charaStatus.preemptiveAttack * 0.80f;//攻撃力(中)
        if (ReLot(percentage))
        {
            attack = mPlayerStatus.charaStatus.preemptiveAttack;//攻撃力(強)にする
        }

        mBossData.PlayerOneAttackPow = attack;

        return next;
    }

    // 項目番号18の報酬、記録データ処理
    private int P18()
    {
        return -1;
    }

    // 項目番号19の報酬、記録データ処理
    private int P19()
    {
        return -1;
    }

    //項目番号19,20の再抽選処理
    private int P19_20Relot()
    {
        int next = -1;
        mBossData.IsPlayerRevaival = false;
        int[] preemptive = new int[3] {11, 17,24 };
        float percentage = mPlayerStatus.charaStatus.preemptiveAttack;


        if (ReLot(percentage)) 
        {
            //当選すれば先制攻撃の番号を返す
            int randomval = CS_LotteryFunction.LotNormalInt(preemptive.Length);
            next = preemptive[randomval] - 1;
            float[] attack = new float[3] { mPlayerStatus.attack * 0.60f, mPlayerStatus.attack * 0.80f, mPlayerStatus.attack * 0.80f };
            mBossData.PlayerOneAttackPow = attack[randomval];
        }


        return next;
    }

    // 項目番号20の報酬、記録データ処理
    private int P20()
    {
        return -1;
    }

    // 項目番号21の報酬、記録データ処理
    private int P21()
    {
        return -1;
    }

    // 項目番号22の報酬、記録データ処理
    private int P22()
    {
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

        float percentage = mPlayerStatus.charaStatus.revaival;
        float playerHp = mPlayerStatus.hp;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * 0.80f;//この変動時のアタック量
        playerHp -= mBossData.BossOneAttackPow;//ボスの攻撃力（中）をhpから引く
        if (playerHp <= 0f)//体力が無くなれば復活抽せん
        {
            percentage = mPlayerStatus.charaStatus.revaival;
            if (!ReLot(percentage))//当選しなかったら敗北
            {
                return -1;
            }
            else
            {
                //当選すれば復活
                mBossData.IsPlayerRevaival = true;
            }
        }
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
        float attack = mPlayerStatus.charaStatus.preemptiveAttack * 0.80f;//攻撃力(中)
        if (ReLot(percentage))
        {
            attack = mPlayerStatus.charaStatus.preemptiveAttack;//攻撃力(強)にする
        }

        mBossData.PlayerOneAttackPow = attack;

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
        }

        mBossData.IsConfirmationChoice = success;

        return next;
    }

    // 項目番号27の報酬、記録データ処理
    private int P27()
    {
        return -1;
    }

    // 項目番号28の報酬、記録データ処理
    private int P28()
    {
        return -1;
    }

    // 項目番号29の報酬、記録データ処理
    private int P29()
    {
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

        mBossData.IsConfirmationChoice = success;

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
        int[] preemptive = new int[2] { 4, 10 };
        float percentage = mPlayerStatus.charaStatus.preemptiveAttack;
        float playerHp = mPlayerStatus.hp;
        mBossData.BossOneAttackPow = mBossStatus.infomations[mBossData.BossNumber].attack * 0.60f;//この変動時のアタック量
        playerHp -= mBossData.BossOneAttackPow;//ボスの攻撃力（中）をhpから引く
        if (playerHp <= 0f)//体力が無くなれば復活抽せん
        {
            percentage = mPlayerStatus.charaStatus.revaival;
            if (!ReLot(percentage))//当選しなかったら敗北
            {
                return -1;
            }
            else
            {
                //当選したなら復活
                mBossData.IsPlayerRevaival = true;
            }
        }
        else//先制攻撃の値で再抽選
        {
            if (ReLot(percentage)) { next = preemptive[CS_LotteryFunction.LotNormalInt(preemptive.Length)] - 1; }//当選すれば先制攻撃の番号を返す
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
