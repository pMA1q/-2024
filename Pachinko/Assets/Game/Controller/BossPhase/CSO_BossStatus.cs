using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "BossStatus", menuName = "BossStatus", order = 1)]
public class CSO_BossStatus : ScriptableObject
{
    public List<BossStatusInfomation> infomations;

    [NonSerialized]
    // 初期値を保存するリスト
    public List<BossStatusInfomation> initialValues;

    // 初期化時に現在のデータを保存
    public void SaveInitialValues()
    {
        initialValues = new List<BossStatusInfomation>();
        foreach (var info in infomations)
        {
            // データをディープコピー
            initialValues.Add(new BossStatusInfomation
            {
                name = info.name,
                hp = info.hp,
                attack = info.attack,
                maxHp = info.maxHp,
                maxAttack = info.maxAttack
            });
        }
    }

    // 初期値にリセットする
    public void ResetToInitialValues()
    {
        if (initialValues == null) return;

        infomations.Clear();
        foreach (var info in initialValues)
        {
            infomations.Add(new BossStatusInfomation
            {
                name = info.name,
                hp = info.hp,
                attack = info.attack,
                maxHp = info.maxHp,
                maxAttack = info.maxAttack
            });
        }
    }
}

[System.Serializable]
public class BossStatusInfomation
{
    [Header("ボス名")]
    public string name;
    [Header("HP")]
    public float hp;
    [Header("攻撃")]
    public float attack;
    [Header("上限値HP")]
    public int maxHp;
    [Header("上限値攻撃")]
    public float maxAttack;
}