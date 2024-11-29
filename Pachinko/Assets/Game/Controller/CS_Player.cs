using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Player : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;
    public int attackPower = 10;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }

    public bool IsAlive()
    {
        return currentHP > 0;
    }

    public void Attack(CS_Enemy enemy)
    {
        if (IsAlive())
        {
            Debug.Log($"�U���I");
            enemy.TakeDamage(attackPower);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log($"�v���C���[��{damage}�̃_���[�W���󂯂��I �c��HP: {currentHP}");
        if (currentHP < 0)  // HP��0�����ɂȂ�Ȃ��悤�ɐ���
        {
            currentHP = 0;
        }

        if (currentHP <= 0)
        {
            Debug.Log("�v���C���[�͓|�ꂽ...");
        }

        //CS_PerformanceFinish spcFinish = this.gameObject.AddComponent<CS_PerformanceFinish>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
