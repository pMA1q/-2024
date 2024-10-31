using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PerformanceHandler : MonoBehaviour
{
    // CS_SMPerfSpawner への参照
    public CS_SMPerfSpawner perfSpawner;

    void OnEnable()
    {
        // イベントにメソッドを登録
        SubjugSastinMission.OnPlayPerformance += HandlePerformance;
    }

    void OnDisable()
    {
        // イベントからメソッドを解除（メモリリーク防止）
        SubjugSastinMission.OnPlayPerformance -= HandlePerformance;
    }

    // イベントが発生したときに実行されるメソッド
    void HandlePerformance(int performanceId)
    {
        Debug.Log("パフォーマンスIDが受信されました: " + performanceId);

        // perfSpawner が設定されている場合、指定されたパフォーマンスIDに基づいて演出を表示
        if (perfSpawner != null)
        {
            perfSpawner.DisplayMissionPerformance(performanceId);
        }
        else
        {
            Debug.LogWarning("CS_SMPerfSpawnerが設定されていません");
        }
    }
}
