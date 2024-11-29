using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_3Enemies_Flee : MonoBehaviour
{
    public CS_Player player; //プレイヤーの参照
    private bool isBattleStarted = false;  // 戦闘開始フラグ
    public List<CS_Enemy> enemies = new List<CS_Enemy>(); //複数の敵

    //private int currentTurnIndex = 0;

    void Update()
    {
        if (!isBattleStarted && Input.GetKeyDown(KeyCode.Return))
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
        if (enemies.TrueForAll(e => !e.IsAlive()))
        {
            Debug.Log("逃げる！");
            return;
        }
    }

}
