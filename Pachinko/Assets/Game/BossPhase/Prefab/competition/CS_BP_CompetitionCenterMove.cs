using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BP_CompetitionCenterMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 100f;
    [SerializeField]
    // 移動範囲
    private float moveRange = 200f;

    // 内部変数
    private RectTransform rectTransform;
    private float direction = 1f; // 1: 右, -1: 左
    private float initialX;

    void Start()
    {
        // RectTransformの参照を取得
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("RectTransformがアタッチされていません！");
            enabled = false;
            return;
        }
        // 初期のX座標を保存
        initialX = rectTransform.anchoredPosition.x;
    }

    void Update()
    {
        // 現在の位置を取得
        Vector2 pos = rectTransform.anchoredPosition;

        // X座標を更新
        pos.x += speed * direction * Time.deltaTime;

        // 範囲を超えた場合、移動方向を反転
        if (pos.x > initialX + moveRange)
        {
            pos.x = initialX + moveRange;
            direction = -1f;
        }
        else if (pos.x < initialX - moveRange)
        {
            pos.x = initialX - moveRange;
            direction = 1f;
        }

        // 更新された位置を適用
        rectTransform.anchoredPosition = pos;
    }
}
