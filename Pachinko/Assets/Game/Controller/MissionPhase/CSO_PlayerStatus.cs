//---------------------------------
//プレイヤーのステータスクラス
//担当者：野崎
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "PlayerStatus", order = 1)]
public class CSO_PlayerStatus : ScriptableObject
{
    // ステータスのプロパティ
    public int hp;
    public float defense;

    public CharacterStatus charaStatus;
    public Ticket ticket;

    [NonSerialized]
    // バックアップ用のステータス
    public CSO_PlayerStatus backupStatus;

    // コンストラクタ（初期値を設定）
    public CSO_PlayerStatus(int initialHp, int initialAttack, int initialDefense, float initialPreemptiveAttack, float initialRevaival)
    {
        hp = initialHp;
        defense = initialDefense;
        //preemptiveAttack = initialPreemptiveAttack;
        //revaival = initialRevaival;

        // 初期ステータスをバックアップとして保存
        BackupInitialStatus();
    }

    //バックアップ用コンストラクタ
    public CSO_PlayerStatus(CSO_PlayerStatus _backUp)
    {
        hp = _backUp.hp;
        defense = _backUp.defense;
        charaStatus = new CharacterStatus( _backUp.charaStatus);
        ticket = new Ticket(_backUp.ticket);
        Debug.Log("チケット数" + ticket.special);
    }


    // 初期ステータスをバックアップとして保持
    private void BackupInitialStatus()
    {
        backupStatus = new CSO_PlayerStatus(this);

    }

    // ゲーム終了時、ステータスを初期状態に戻す
    public void ResetStatus()
    {
        hp = backupStatus.hp;
        defense = backupStatus.defense;
        charaStatus = backupStatus.charaStatus;
        ticket = backupStatus.ticket;
        Debug.Log("プレイヤーステータスをリセットしました");
    }

    // 抽選結果に基づいてステータスを更新
    public void UpdateStatus(int hpChange, int attackChange, int defenseChange)
    {
        hp += hpChange;
        defense += defenseChange;

    }
}
[System.Serializable] 
public class CharacterStatus
{
    [Header("文字色アップ")]
    public float charColorUP;
    [Header("文字色アップパラメーター")]
    public STATUS_UP_Power charColorUpPow;
    [Header("先制攻撃")]
    public float preemptiveAttack;
    [Header("先制攻撃パラメーター")]
    public STATUS_UP_Power preemptiveAttackUpPow;
    [Header("攻撃")]
    public float attack;
    [Header("攻撃パラメーター")]
    public STATUS_UP_Power attackUpPow;
    [Header("復活")]
    public float revaival;
    [Header("復活パラメーター")]
    public STATUS_UP_Power revivalUpPow;
    [Header("カットイン")]
    public float cutIn;
    [Header("カットインパラメーター")]
    public STATUS_UP_Power cutInUpPow;
    [Header("装備ランク")]
    public int equipmentRank;

    // コピーコンストラクタ
    public CharacterStatus(CharacterStatus original)
    {
        charColorUP = original.charColorUP;
        charColorUpPow = original.charColorUpPow;
        preemptiveAttack = original.preemptiveAttack;
        preemptiveAttackUpPow = original.preemptiveAttackUpPow;
        attack = original.attack;
        attackUpPow = original.attackUpPow;
        revaival = original.revaival;
        revivalUpPow = original.revivalUpPow;
        cutIn = original.cutIn;
        cutInUpPow = original.cutInUpPow;
        equipmentRank = original.equipmentRank;
    }
    public CharacterStatus() { } // デフォルトコンストラクタ

}

[System.Serializable]
public struct STATUS_UP_Power
{
    [Header("小UP")]
    public float smallUP;
    [Header("中UP")]
    public float middleUP;
    [Header("大UP")]
    public float bigUP;
    [Header("上限")]
    public float max;
    [Header("選ばれる確率(%)")]
    public float conicePercent;

   
}

[System.Serializable]
public class Ticket
{
    [Header("奥義")]
    public int special = 0;
    [Header("先制攻撃")]
    public int preemptiveAttack = 0;
    [Header("復活")]
    public int revaival = 0;
    [Header("仲間参戦")]
    public int partner = 0;

    // コピーコンストラクタ
    public Ticket(Ticket original)
    {
        special = original.special;
        preemptiveAttack = original.preemptiveAttack;
        revaival = original.revaival;
        partner = original.partner;
    }

    public Ticket() { } // デフォルトコンストラクタ
}