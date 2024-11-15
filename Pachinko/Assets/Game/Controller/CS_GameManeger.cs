//---------------------------------
//�Q�[���}�l�[�W���[
//�S���ҁF���
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_GameManager : MonoBehaviour
{
    private CSO_PlayerStatus playerStatus;

    void Start()
    {
        // �����X�e�[�^�X�ݒ�
       // playerStatus = new PlayerStatus(initialHp: 100, initialAttack: 10, initialDefense: 10);

        // �Q�[���J�n
        StartGame();
    }

    // �Q�[���i�s
    public void StartGame()
    {
        PreparePhase();
        MissionPhase();
        BossPhase();
        EndGame();
    }

    // �����t�F�[�Y�i�A�C�e�����W�A�b�B�A�����~�b�V�����̑I��j
    private void PreparePhase()
    {
        Debug.Log("�����t�F�[�Y�J�n");
        // �~�b�V�����I��Ȃǂ̏���
    }

    // �~�b�V�����t�F�[�Y�i20+����]�ŃX�e�[�^�X��ێ��E�X�V�j
    private void MissionPhase()
    {
        Debug.Log("�~�b�V�����t�F�[�Y�J�n");

        for (int i = 0; i < 20; i++)  // ��]���ɉ����ăX�e�[�^�X��X�V
        {
            // �����_���ɃX�e�[�^�X��X�V�i��Ƃ��ă����_���Ȑ��l��g�p�j
            int randomHpChange = UnityEngine.Random.Range(-5, 10);
            int randomAttackChange = UnityEngine.Random.Range(0, 5);
            int randomDefenseChange = UnityEngine.Random.Range(0, 3);

            playerStatus.UpdateStatus(randomHpChange, randomAttackChange, randomDefenseChange);
        }
    }

    // �{�X�t�F�[�Y�i�m�������X�e�[�^�X��Ăяo���Ē��I�j
    private void BossPhase()
    {
        Debug.Log("�{�X�t�F�[�Y�J�n");

        for (int i = 0; i < 10; i++)  // �{�X�t�F�[�Y�ł�10��]
        {
            // �����_���Ń{�X�Ɛ키����
            int randomBossAttack = UnityEngine.Random.Range(0, 15);
            playerStatus.UpdateStatus(-randomBossAttack, 0, 0);
        }
    }

    // �Q�[���I�����̏����i���Z�b�g�j
    private void EndGame()
    {
        Debug.Log("�Q�[���I���A�X�e�[�^�X����Z�b�g����");
        playerStatus.ResetStatus();  // �X�e�[�^�X�������Ԃɖ߂�
    }
}
