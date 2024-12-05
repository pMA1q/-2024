using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Debuff : MonoBehaviour
{
    //���̍U���͂�ێ�����ϐ�
    public float baseAttackPower;
    //���݂̍U����(�f�o�t��o�t���K�p���ꂽ���)
    public float currentAttackPower;

    // Start is called before the first frame update
    void Start()
    {
        //������ �U���͂͌��̒l����n�܂�
        currentAttackPower = baseAttackPower;
    }

    //�U���͂Ƀf�o�t��K�p    
    public void ApplyDebuff(float debuffPercentage)
    {
        //�f�o�t�̊����ɉ����čU���͂�����������
        float debuffAmount = baseAttackPower * (debuffPercentage / 100);
        currentAttackPower = baseAttackPower - debuffAmount;

        Debug.Log("�f�o�t���K�p����܂����B���݂̍U����:" + currentAttackPower);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
