using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*using System;
using static UnityEngine.EventSystems.EventTrigger;*/

public class CS_5EnemiesApeer_Lose : MonoBehaviour
{

    public CS_Player player; //�v���C���[�̎Q��
    public CS_Enemy enemy; //�G�̎Q��
    private bool isBattleStarted = false;  // �퓬�J�n�t���O
    public List<CS_Enemy> enemies = new List<CS_Enemy>(); //�����̓G

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
        Debug.Log("�퓬�J�n�I");
        NextTurn();
    }

    void NextTurn()
    {
        if (player == null || !player.gameObject.activeSelf)
        {
            Debug.Log("�v���C���[���|�ꂽ... �Q�[���I�[�o�[");
            return;
        }

        /*if (enemies.TrueForAll(e => !e.IsAlive()))
        {
            Debug.Log("�G��S�ē|�����I �����I");
            return;
        }*/

        if (currentTurnIndex >= enemies.Count)
        {
            currentTurnIndex = 0;  // �^�[�������Z�b�g
        }

       

        CS_Enemy currentEnemy = enemies[currentTurnIndex];

        // �v���C���[�̍U��
        if (Input.GetKeyDown(KeyCode.Space)) // �X�y�[�X�L�[�ōU��
        {
            if (currentEnemy.IsAlive())
            {
                player.Attack(currentEnemy); // �v���C���[�����݂̓G���U��
                Debug.Log("�v���C���[���G���U���I");

                if (!currentEnemy.IsAlive()) // �G���|�ꂽ���`�F�b�N
                {
                    Debug.Log("�G��|�����I");
                }
            }
        }

        if (currentEnemy.IsAlive())
        {
            currentEnemy.Attack(player);  // ���݂̓G���U��
        }

        currentTurnIndex++;
        Invoke("NextTurn", 5f);  // 2�b��Ɏ��̃^�[��
    }

}
