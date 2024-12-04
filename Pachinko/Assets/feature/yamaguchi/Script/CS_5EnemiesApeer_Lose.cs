using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*using System;
using static UnityEngine.EventSystems.EventTrigger;*/

public class CS_5EnemiesApeer_Lose : MonoBehaviour
{

    public CS_Player player; //プレイヤーの参照
    public CS_Enemy enemy; //敵の参照
    private bool isBattleStarted = false;  // 戦闘開始フラグ
    public List<CS_Enemy> enemies = new List<CS_Enemy>(); //複数の敵

    private int currentTurnIndex = 0;

    void Update()
    {
        if(!isBattleStarted && Input.GetKeyDown(KeyCode.Return))
        {
            StartBattle();
        }
    }

    void StartBattle()
    {
        Debug.Log("戦闘開始！");
        NextTurn();
    }

    void NextTurn()
    {
        if (player == null || !player.gameObject.activeSelf)
        {
            Debug.Log("プレイヤーが倒れた... ゲームオーバー");
            return;
        }

        /*if (enemies.TrueForAll(e => !e.IsAlive()))
        {
            Debug.Log("敵を全て倒した！ 勝利！");
            return;
        }*/

        if (currentTurnIndex >= enemies.Count)
        {
            currentTurnIndex = 0;  // ターンをリセット
        }

       

        CS_Enemy currentEnemy = enemies[currentTurnIndex];

        // プレイヤーの攻撃
        if (Input.GetKeyDown(KeyCode.Space)) // スペースキーで攻撃
        {
            if (currentEnemy.IsAlive())
            {
                player.Attack(currentEnemy); // プレイヤーが現在の敵を攻撃
                Debug.Log("プレイヤーが敵を攻撃！");

                if (!currentEnemy.IsAlive()) // 敵が倒れたかチェック
                {
                    Debug.Log("敵を倒した！");
                }
            }
        }

        if (currentEnemy.IsAlive())
        {
            currentEnemy.Attack(player);  // 現在の敵が攻撃
        }

        currentTurnIndex++;
        Invoke("NextTurn", 5f);  // 2秒後に次のターン
    }

}
