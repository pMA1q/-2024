using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // 逃げる速度
    public float escapeSpeed = 3f;

    // 逃げる動作をオン・オフできるフラグ
    public bool canEscape = true;

    // 回転の速さ
    public float rotationSpeed = 2f;  // どれくらいの速さで回転させるか

    // 逃げる時間（2秒後に開始）
    private bool isEscaping = false;
    private float escapeStartTime;

    // 回転のための補間パラメータ
    private Quaternion startRotation;
    private Quaternion targetRotation;
    private float rotationTime;

    void Update()
    {
        // 逃げる動作がオンの場合にのみ実行
        if (canEscape && isEscaping)
        {
            // 回転の補間（徐々に回転させる）
            if (rotationTime < 1f)
            {
                rotationTime += Time.deltaTime * rotationSpeed;  // 回転速度に合わせて進行状況を増加
                transform.rotation = Quaternion.Lerp(startRotation, targetRotation, rotationTime);  // 徐々に回転
            }

            // 回転が完了した後に逃げる動作を開始
            if (rotationTime >= 1f)
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
            // 回転を開始する直前に回転の初期設定を行う
            startRotation = transform.rotation;  // 現在の回転を保存
            targetRotation = Quaternion.Euler(0f, 180f, 0f);  // 目標回転を設定（y軸で180度回転）

            rotationTime = 0f;  // 回転開始時に進行状況をリセット

            // 逃げる動作を開始する前に回転を開始
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
