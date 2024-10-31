//---------------------------------
//�v���C���[�̃X�e�[�^�X�N���X
//�S���ҁF���
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus
{
    // �X�e�[�^�X�̃v���p�e�B
    public int hp;
    public int attack;
    public int defense;

    // �o�b�N�A�b�v�p�̃X�e�[�^�X
    private PlayerStatus backupStatus;

    // �R���X�g���N�^�i�����l��ݒ�j
    public PlayerStatus(int initialHp, int initialAttack, int initialDefense)
    {
        hp = initialHp;
        attack = initialAttack;
        defense = initialDefense;

        // �����X�e�[�^�X���o�b�N�A�b�v�Ƃ��ĕۑ�
        BackupInitialStatus();
    }

    // �����X�e�[�^�X���o�b�N�A�b�v�Ƃ��ĕێ�
    private void BackupInitialStatus()
    {
        backupStatus = new PlayerStatus(hp, attack, defense);
    }

    // �Q�[���I�����A�X�e�[�^�X��������Ԃɖ߂�
    public void ResetStatus()
    {
        hp = backupStatus.hp;
        attack = backupStatus.attack;
        defense = backupStatus.defense;
    }

    // ���I���ʂɊ�Â��ăX�e�[�^�X���X�V
    public void UpdateStatus(int hpChange, int attackChange, int defenseChange)
    {
        hp += hpChange;
        attack += attackChange;
        defense += defenseChange;

        Debug.Log($"�X�e�[�^�X�X�V: HP {hp}, �U���� {attack}, �h��� {defense}");
    }
}