using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharaBonus : MonoBehaviour
{
    // 移動先の座標
    public Transform targetPos;
    // 移動にかける時間（秒）
    public float moveDuration = 2f;

    // 開始処理
    void Start()
    {
        // コルーチンを開始
        StartCoroutine(MoveToPosition(targetPos.position, moveDuration));
    }

    // 指定位置にn秒かけて移動するコルーチン
    private IEnumerator MoveToPosition(Vector3 target, float duration)
    {
        // 現在の位置
        Vector3 start = transform.position;
        // 経過時間
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // 経過時間の割合
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            // 線形補間で位置を更新
            transform.position = Vector3.Lerp(start, target, t);

            // 次のフレームまで待機
            yield return null;
        }

        // 最終的に位置を確定
        transform.position = target;
    }
}
