using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Enemy : MonoBehaviour
{

    public int maxHP = 100;
    private int currentHP;
    public int attackPower = 20;


    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }

    public bool IsAlive()
    {
        return currentHP > 0;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log($"{damage}�̃_���[�W���󂯂��I �c��HP : {currentHP}");
        if (currentHP < 0)  // HP��0�����ɂȂ�Ȃ��悤�ɐ���
        {
            currentHP = 0;
        }


        if ( currentHP <= 0 ) 
        {
            Die();
        }
    }

    public void Attack(CS_Player player)
    {
        if (IsAlive())
        {
            Debug.Log($"�U���I");
            player.TakeDamage(attackPower);
        }
    }

    private void Die()
    {
        Debug.Log($"�|�ꂽ�I");
        gameObject.SetActive(false);  // �G���\����
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
}
