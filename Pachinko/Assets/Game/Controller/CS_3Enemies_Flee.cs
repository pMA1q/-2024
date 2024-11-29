using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_3Enemies_Flee : MonoBehaviour
{
    public CS_Player player; //�v���C���[�̎Q��
    private bool isBattleStarted = false;  // �퓬�J�n�t���O
    public List<CS_Enemy> enemies = new List<CS_Enemy>(); //�����̓G

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
        Debug.Log("�퓬�J�n�I");
        NextTurn();
    }

    void NextTurn()
    {
        if (enemies.TrueForAll(e => !e.IsAlive()))
        {
            Debug.Log("������I");
            return;
        }
    }

}
