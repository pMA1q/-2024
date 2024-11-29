using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_RewardUp : MonoBehaviour
{
    public float baseReward = 100.0f; // ��{��V
    public bool isRewardUp = false;   // ��V�A�b�v�X�e�[�^�X
    public bool isEventActive = false; // �C�x���g���
    public int ticketCount = 0;        // ���݂̃`�P�b�g��

    void Start()
    {
        float finalReward = CalculateReward();
        int finalTicketCount = AddTicketBonus();

        Debug.Log("�ŏI��V: " + finalReward);
        Debug.Log("�ŏI�`�P�b�g��: " + finalTicketCount);
    }

    float CalculateReward()
    {
        float reward = baseReward;

        // �X�e�[�^�X�ɂ��1.5�{�̕�V�A�b�v
        if (isRewardUp)
        {
            reward *= 1.5f;
            Debug.Log("�X�e�[�^�X��1.5�{�ɂȂ����I");
        }

        // �C�x���g�ɂ��1�i�K�A�b�v�i��: +10���j
        if (isEventActive)
        {
            reward *= 1.1f; // 1�i�KUP�Ƃ���10������
            Debug.Log("1�i�KUP����!");
        }

        return reward;
    }

    int AddTicketBonus()
    {
        // �`�P�b�g��50%�m����+1
        if (Random.value < 0.5f)
        {
            ticketCount += 1;
            Debug.Log("�`�P�b�g��+1���ꂽ�I");
        }

        return ticketCount;
    }
}
