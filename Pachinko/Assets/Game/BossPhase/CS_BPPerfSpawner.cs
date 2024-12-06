using UnityEngine;

public class CS_BPPerfSpawner : MonoBehaviour
{
    public CSO_MissionPhaseTable missionPhaseTable; // �X�N���v�^�u���I�u�W�F�N�g�̎Q��

    // �~�b�V����ID�Ɋ�Â��ĉ��o�v���n�u���擾���郁�\�b�h
    public GameObject GetMissionPerformancePrefab(int missionId)
    {
        // missionId�����X�g�͈͓̔��ł��邱�Ƃ��m�F
        if (missionPhaseTable != null && missionId >= 0 && missionId < missionPhaseTable.infomation.Count)
        {
            return missionPhaseTable.infomation[missionId].performance;
        }

        Debug.LogWarning("�w�肳�ꂽMissionID�������ł�: " + missionId);
        return null;
    }

    // ���o�v���n�u���C���X�^���X�����郁�\�b�h
    public void DisplayMissionPerformance(int missionId)
    {
        GameObject performancePrefab = GetMissionPerformancePrefab(missionId);
        if (performancePrefab != null)
        {
            Instantiate(performancePrefab, transform.position, Quaternion.identity);
        }
    }
}