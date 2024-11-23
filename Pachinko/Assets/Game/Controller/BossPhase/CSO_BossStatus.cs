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
    [Header("�{�X��")]
    public string name;
    [Header("HP")]
    public float hp;
    [Header("�U��")]
    public float attack;
    [Header("����lHP")]
    public int maxHp;
    [Header("����l�U��")]
    public float maxAttack;
}