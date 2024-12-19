using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // 逃げる速度
    public float escapeSpeed = 3f;

    // 逃げる動作をオン・オフできるフラグ
    public bool canEscape = true;

    // 逃げる時間（2秒後に開始）
    private bool isEscaping = false;
    private float escapeStartTime;

    void Update()
    {
        // 逃げる動作がオンの場合にのみ実行
        if (canEscape && isEscaping)
        {
            float elapsedTime = Time.time - escapeStartTime;
            // 2秒後に逃げる処理を開始
            if (elapsedTime >= 0)
            {
                // z軸方向に逃げる
                transform.position += Vector3.back * escapeSpeed * Time.deltaTime;  // `Vector3.back`で-1方向に移動
            }
        }
    }

    // 逃げる処理を開始
    public void StartEscape()
    {
        if (canEscape)  // 逃げる動作がオンの場合のみ実行
        {
            isEscaping = true;
            escapeStartTime = Time.time + 2f; // 2秒後に逃げる開始
        }
    }

    // 逃げる動作を停止するためのメソッド（オプション）
    public void StopEscape()
    {
        isEscaping = false;
    }
}
