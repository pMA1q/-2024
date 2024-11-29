using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "BossStatus", menuName = "BossStatus", order = 1)]
public class CSO_BossStatus : ScriptableObject
{
    public List<BossStatusInfomation> infomations;

    [NonSerialized]
    // �����l��ۑ����郊�X�g
    public List<BossStatusInfomation> initialValues;

    // ���������Ɍ��݂̃f�[�^��ۑ�
    public void SaveInitialValues()
    {
        initialValues = new List<BossStatusInfomation>();
        foreach (var info in infomations)
        {
            // �f�[�^���f�B�[�v�R�s�[
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

    // �����l�Ƀ��Z�b�g����
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