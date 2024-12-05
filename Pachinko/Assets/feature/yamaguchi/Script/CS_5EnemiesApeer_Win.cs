using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CS_5EnemiesApeer_Win : MonoBehaviour
{
    public CS_Player player; //�v���C���[�̎Q��
    private bool isBattleStarted = false;  // �퓬�J�n�t���O
    public List<CS_Enemy> enemies = new List<CS_Enemy>(); //�����̓G
    public CS_Ally ally; // ���Ԃ̎Q��
    private bool isAllyJoined = false;  // ���Ԃ��Q�킵�����ǂ����̃t���O
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
        UnityEngine.Debug.Log("�퓬�J�n�I");
        NextTurn();
    }

    void NextTurn()
    {

        if (enemies.TrueForAll(e => !e.IsAlive()))
        {
            UnityEngine.Debug.Log("�G��S�ē|�����I �����I");
            return;
        }


        if (currentTurnIndex >= enemies.Count)
        {
            currentTurnIndex = 0;  // �^�[�������Z�b�g
        }

        CS_Enemy currentEnemy = enemies[currentTurnIndex];
        if (currentEnemy.IsAlive())
        {
            currentEnemy.Attack(player);  // ���݂̓G���U��
        }
        else
        {
            // �G���|����Ă���ꍇ�A�|���ꂽ�G�̐����J�E���g����
            if (currentEnemy.IsAlive() == false)
            {
                defeatedEnemyCount++;
                UnityEngine.Debug.Log("�G��|�����I���ݓ|������: " + defeatedEnemyCount);
            }

            // �G��5�̓|�����ꍇ�A33%�̊m���Œ��Ԃ��Q�킳����
            if (defeatedEnemyCount >= 5 && !isAllyJoined && UnityEngine.Random.Range(0f, 1f) <= 0.33f)
            {
                JoinAlly();
                UnityEngine.Debug.Log("���Ԃ��Q�킵���I");
            }
        }

        currentTurnIndex++;
        NextTurn();
    }

    void JoinAlly()
    {
        isAllyJoined = true;
        UnityEngine.Debug.Log("���Ԃ��Q�킵���I");
        ally.JoinBattle();  // ���Ԃ��퓬�ɉ���鏈��
    }

}


