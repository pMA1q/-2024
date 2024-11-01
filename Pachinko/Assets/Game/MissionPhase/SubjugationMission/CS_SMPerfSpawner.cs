using UnityEngine;

public class CS_SMPerfSpawner : MonoBehaviour
{
    public CSO_MissionPhaseTable missionPhaseTable; // スクリプタブルオブジェクトの参照

    private void Start()
    {
        CS_MissionManeger.OnPlayPerformance += CS_MissionManeger_OnPlayPerformance;
    }

    private void CS_MissionManeger_OnPlayPerformance(int _performance)
    {
        throw new System.NotImplementedException();
    }

    // ミッションIDに基づいて演出プレハブを取得するメソッド
    public GameObject GetMissionPerformancePrefab(int missionId)
    {
        // missionIdがリストの範囲内であることを確認
        if (missionPhaseTable != null && missionId >= 0 && missionId < missionPhaseTable.infomation.Count)
        {
            return missionPhaseTable.infomation[missionId].performance;
        }

        Debug.LogWarning("指定されたMissionIDが無効です: " + missionId);
        return null;
    }

    // 演出プレハブをインスタンス化するメソッド
    public void DisplayMissionPerformance(int missionId)
    {
        GameObject performancePrefab = GetMissionPerformancePrefab(missionId);
        if (performancePrefab != null)
        {
            Instantiate(performancePrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("演出ID" + missionId);
            Instantiate(missionPhaseTable.infomation[3].performance, transform.position, Quaternion.identity);
        }
    }
}

