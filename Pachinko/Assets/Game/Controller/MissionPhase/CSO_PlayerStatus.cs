//---------------------------------
//プレイヤーのステータスクラス
//担当者：野崎
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus
{
    // ステータスのプロパティ
    public int hp;
    public int attack;
    public int defense;

    public float preemptiveAttack;  //先制攻撃
    public float revaival;          //復活

    // バックアップ用のステータス
    private PlayerStatus backupStatus;

    // コンストラクタ（初期値を設定）
    public PlayerStatus(int initialHp, int initialAttack, int initialDefense, float initialPreemptiveAttack, float initialRevaival)
    {
        hp = initialHp;
        attack = initialAttack;
        defense = initialDefense;
        preemptiveAttack = initialPreemptiveAttack;
        revaival = initialRevaival;

        // 初期ステータスをバックアップとして保存
       // BackupInitialStatus();
    }

    // 初期ステータスをバックアップとして保持
    private void BackupInitialStatus()
    {
        backupStatus = new PlayerStatus(hp, attack, defense, preemptiveAttack, revaival);
    }

    // ゲーム終了時、ステータスを初期状態に戻す
    public void ResetStatus()
    {
        hp = backupStatus.hp;
        attack = backupStatus.attack;
        defense = backupStatus.defense;
    }

    // 抽選結果に基づいてステータスを更新
    public void UpdateStatus(int hpChange, int attackChange, int defenseChange)
    {
        hp += hpChange;
        attack += attackChange;
        defense += defenseChange;

        Debug.Log($"ステータス更新: HP {hp}, 攻撃力 {attack}, 防御力 {defense}");
    }
}
