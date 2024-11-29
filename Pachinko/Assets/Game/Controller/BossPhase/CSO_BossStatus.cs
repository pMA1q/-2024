using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossStatus", menuName = "BossStatus", order = 1)]
public class CSO_BossStatus : ScriptableObject
{
    public List<BossStatusInfomation> infomations;
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