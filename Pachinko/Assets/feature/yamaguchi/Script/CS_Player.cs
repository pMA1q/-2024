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
            Debug.Log($"攻撃！");
            enemy.TakeDamage(attackPower);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log($"プレイヤーは{damage}のダメージを受けた！ 残りHP: {currentHP}");
        if (currentHP < 0)  // HPが0未満にならないように制限
        {
            currentHP = 0;
        }

        if (currentHP <= 0)
        {
            Debug.Log("プレイヤーは倒れた...");
        }

        //CS_PerformanceFinish spcFinish = this.gameObject.AddComponent<CS_PerformanceFinish>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
