using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_RewardUp : MonoBehaviour
{
    public float baseReward = 100.0f; // 基本報酬
    public bool isRewardUp = false;   // 報酬アップステータス
    public bool isEventActive = false; // イベント状態
    public int ticketCount = 0;        // 現在のチケット数

    void Start()
    {
        float finalReward = CalculateReward();
        int finalTicketCount = AddTicketBonus();

        Debug.Log("最終報酬: " + finalReward);
        Debug.Log("最終チケット数: " + finalTicketCount);
    }

    float CalculateReward()
    {
        float reward = baseReward;

        // ステータスによる1.5倍の報酬アップ
        if (isRewardUp)
        {
            reward *= 1.5f;
            Debug.Log("ステータスが1.5倍になった！");
        }

        // イベントによる1段階アップ（例: +10％）
        if (isEventActive)
        {
            reward *= 1.1f; // 1段階UPとして10％増加
            Debug.Log("1段階UPした!");
        }

        return reward;
    }

    int AddTicketBonus()
    {
        // チケットの50%確率で+1
        if (Random.value < 0.5f)
        {
            ticketCount += 1;
            Debug.Log("チケットが+1された！");
        }

        return ticketCount;
    }
}
