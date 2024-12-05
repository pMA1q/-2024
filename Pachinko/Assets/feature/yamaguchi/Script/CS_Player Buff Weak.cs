using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PlayerBuffWeak : MonoBehaviour
{
    public float baseAttackPower = 10f;
    private float currentAttackPower;
    private bool isBuffed = false;

    private float buffChance = 0.2f; // バフが当たる確率（20%）

    // Start is called before the first frame update
    void Start()
    {
        currentAttackPower = baseAttackPower;
    }

    public void TryApplyBuffer()
    {
        //抽選を行い、指定の確率でバフ適用
        if (Random.value < buffChance)
        {
            ApplyWeakAttackBuffer();
        }
    }

    public void ApplyWeakAttackBuffer(float buffAmount = 2f)
    {
        if (isBuffed) return; //既にバフがかかっている場合は処理をスキップ

        isBuffed = true;
        currentAttackPower += buffAmount;//攻撃力を少しだけ増加
    }

    private void OnRectTransformRemoved()
    {
        currentAttackPower = baseAttackPower; //攻撃力を元に戻す
        isBuffed = false;
    }

    public void Attack() 
    {
        Debug.Log("Attack with power:" + currentAttackPower);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
