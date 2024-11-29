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
        Debug.Log($"{damage}のダメージを受けた！ 残りHP : {currentHP}");
        if (currentHP < 0)  // HPが0未満にならないように制限
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
            Debug.Log($"攻撃！");
            player.TakeDamage(attackPower);
        }
    }

    private void Die()
    {
        Debug.Log($"倒れた！");
        gameObject.SetActive(false);  // 敵を非表示に
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
}
