//---------------------------------
//�v���C���[�̃X�e�[�^�X�N���X
//�S���ҁF���
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSO_PlayerStatus
{
    // �X�e�[�^�X�̃v���p�e�B
    public int hp;
    public int attack;
    public int defense;

    public float preemptiveAttack;  //�搧�U��
    public float revaival;          //����

    // �o�b�N�A�b�v�p�̃X�e�[�^�X
    private CSO_PlayerStatus backupStatus;

    // �R���X�g���N�^�i�����l��ݒ�j
    public CSO_PlayerStatus(int initialHp, int initialAttack, int initialDefense, float initialPreemptiveAttack, float initialRevaival)
    {
        hp = initialHp;
        attack = initialAttack;
        defense = initialDefense;
        preemptiveAttack = initialPreemptiveAttack;
        revaival = initialRevaival;

        // �����X�e�[�^�X��o�b�N�A�b�v�Ƃ��ĕۑ�
       // BackupInitialStatus();
    }

    // �����X�e�[�^�X��o�b�N�A�b�v�Ƃ��ĕێ�
    private void BackupInitialStatus()
    {
        backupStatus = new CSO_PlayerStatus(hp, attack, defense, preemptiveAttack, revaival);
    }

    // �Q�[���I�����A�X�e�[�^�X�������Ԃɖ߂�
    public void ResetStatus()
    {
        hp = backupStatus.hp;
        attack = backupStatus.attack;
        defense = backupStatus.defense;
    }

    // ���I���ʂɊ�Â��ăX�e�[�^�X��X�V
    public void UpdateStatus(int hpChange, int attackChange, int defenseChange)
    {
        hp += hpChange;
        attack += attackChange;
        defense += defenseChange;

        Debug.Log($"�X�e�[�^�X�X�V: HP {hp}, �U���� {attack}, �h��� {defense}");
    }
}
