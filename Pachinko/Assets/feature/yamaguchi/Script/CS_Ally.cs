using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Ally : MonoBehaviour
{
    public string allyName; // ���Ԃ̖��O
    public int health; // ���Ԃ̗̑�
    public int attackPower; // ���Ԃ̍U����

    // �퓬�ɉ����Ƃ��̏���
    public void JoinBattle()
    {
        Debug.Log(allyName + " ���Q�킵���I");
    }

    // ���Ԃ��U�����鏈��
    public void Attack(CS_Enemy enemy)
    {
        Debug.Log(allyName + " �� " + enemy.name + " �� " + attackPower + " �̃_���[�W��^�����I");
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
