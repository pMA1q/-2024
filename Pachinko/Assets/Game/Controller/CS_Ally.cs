using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Ally : MonoBehaviour
{
    public string allyName; // 仲間の名前
    public int health; // 仲間の体力
    public int attackPower; // 仲間の攻撃力

    // 戦闘に加わるときの処理
    public void JoinBattle()
    {
        Debug.Log(allyName + " が参戦した！");
    }

    // 仲間が攻撃する処理
    public void Attack(CS_Enemy enemy)
    {
        Debug.Log(allyName + " が " + enemy.name + " に " + attackPower + " のダメージを与えた！");
        enemy.TakeDamage(attackPower);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
