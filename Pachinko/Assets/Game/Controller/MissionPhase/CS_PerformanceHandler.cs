using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PerformanceHandler : MonoBehaviour
{
    // CS_SMPerfSpawner �ւ̎Q��
    public CS_SMPerfSpawner perfSpawner;

    void OnEnable()
    {
        // �C�x���g�Ƀ��\�b�h��o�^
        SubjugSastinMission.OnPlayPerformance += HandlePerformance;
    }

    void OnDisable()
    {
        // �C�x���g���烁�\�b�h�������i���������[�N�h�~�j
        SubjugSastinMission.OnPlayPerformance -= HandlePerformance;
    }

    // �C�x���g�����������Ƃ��Ɏ��s����郁�\�b�h
    void HandlePerformance(int performanceId)
    {
        Debug.Log("�p�t�H�[�}���XID����M����܂���: " + performanceId);

        // perfSpawner ���ݒ肳��Ă���ꍇ�A�w�肳�ꂽ�p�t�H�[�}���XID�Ɋ�Â��ĉ��o��\��
        if (perfSpawner != null)
        {
            perfSpawner.DisplayMissionPerformance(performanceId);
        }
        else
        {
            Debug.LogWarning("CS_SMPerfSpawner���ݒ肳��Ă��܂���");
        }
    }
}
