using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PlayerBuffWeak : MonoBehaviour
{
    public float baseAttackPower = 10f;
    private float currentAttackPower;
    private bool isBuffed = false;

    private float buffChance = 0.2f; // �o�t��������m���i20%�j

    // Start is called before the first frame update
    void Start()
    {
        currentAttackPower = baseAttackPower;
    }

    public void TryApplyBuffer()
    {
        //���I���s���A�w��̊m���Ńo�t�K�p
        if (Random.value < buffChance)
        {
            ApplyWeakAttackBuffer();
        }
    }

    public void ApplyWeakAttackBuffer(float buffAmount = 2f)
    {
        if (isBuffed) return; //���Ƀo�t���������Ă���ꍇ�͏������X�L�b�v

        isBuffed = true;
        currentAttackPower += buffAmount;//�U���͂�������������
    }

    private void OnRectTransformRemoved()
    {
        currentAttackPower = baseAttackPower; //�U���͂����ɖ߂�
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
