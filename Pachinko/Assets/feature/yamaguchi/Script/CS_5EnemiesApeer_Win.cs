using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CS_5EnemiesApeer_Win : MonoBehaviour
{
    public CS_Player player; //プレイヤーの参照
    private bool isBattleStarted = false;  // 戦闘開始フラグ
    public List<CS_Enemy> enemies = new List<CS_Enemy>(); //複数の敵
    public CS_Ally ally; // 仲間の参照
    private bool isAllyJoined = false;  // 仲間が参戦したかどうかのフラグ
    private int currentTurnIndex = 0;
    private int defeatedEnemyCount = 0;

    void Update()
    {
        if (!isBattleStarted && Input.GetKeyDown(KeyCode.Return))
        {
            StartBattle();
        }
    }

    void StartBattle()
    {
        UnityEngine.Debug.Log("戦闘開始！");
        NextTurn();
    }

    void NextTurn()
    {

        if (enemies.TrueForAll(e => !e.IsAlive()))
        {
            UnityEngine.Debug.Log("敵を全て倒した！ 勝利！");
            return;
        }


        if (currentTurnIndex >= enemies.Count)
        {
            currentTurnIndex = 0;  // ターンをリセット
        }

        CS_Enemy currentEnemy = enemies[currentTurnIndex];
        if (currentEnemy.IsAlive())
        {
            currentEnemy.Attack(player);  // 現在の敵が攻撃
        }
        else
        {
            // 敵が倒されている場合、倒された敵の数をカウントする
            if (currentEnemy.IsAlive() == false)
            {
                defeatedEnemyCount++;
                UnityEngine.Debug.Log("敵を倒した！現在倒した数: " + defeatedEnemyCount);
            }

            // 敵を5体倒した場合、33%の確率で仲間を参戦させる
            if (defeatedEnemyCount >= 5 && !isAllyJoined && UnityEngine.Random.Range(0f, 1f) <= 0.33f)
            {
                JoinAlly();
                UnityEngine.Debug.Log("仲間が参戦した！");
            }
        }

        currentTurnIndex++;
        NextTurn();
    }

    void JoinAlly()
    {
        isAllyJoined = true;
        UnityEngine.Debug.Log("仲間が参戦した！");
        ally.JoinBattle();  // 仲間が戦闘に加わる処理
    }

}


