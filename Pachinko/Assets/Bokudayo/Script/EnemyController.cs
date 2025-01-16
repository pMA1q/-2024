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

    // 倒れるための回転パラメータ
    private bool isKnockedDown = false;
    private float knockdownRotationTime = 0f;
    public float knockdownDuration = 3f;  // 倒れる動作の時間を調整（大きくするほど遅く倒れる）
    public float knockdownDelay = 1f;    // 倒れるまでの遅延時間（例えば1秒遅らせる）

    // 倒れる動作開始の時間
    private float knockdownStartTime;

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

        // 倒れるアニメーション（回転を適用）
        if (isKnockedDown)
        {
            // 倒れるまでの遅延が経過した後、倒れ始める
            if (Time.time - knockdownStartTime >= knockdownDelay)
            {
                // 倒れる動作の進行
                knockdownRotationTime += Time.deltaTime / knockdownDuration;  // knockdownDurationで倒れる時間を調整
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-90f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z), knockdownRotationTime);

                // 回転が完了したら倒れた状態に保持
                if (knockdownRotationTime >= 1f)
                {
                    isKnockedDown = false; // これで倒れる動作が終了
                }
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
            targetRotation = Quaternion.Euler(startRotation.x, startRotation.y + 180.0f, startRotation.z);  // 目標回転を設定（y軸で180度回転）

            rotationTime = 0f;  // 回転開始時に進行状況をリセット

            // 逃げる動作を開始する前に回転を開始
            isEscaping = true;
            escapeStartTime = Time.time + 2f; // 2秒後に逃げる開始
        }
    }

    // 攻撃を受けて倒れる処理
    public void KnockDown()
    {
        isKnockedDown = true;  // 倒れる処理を開始
        knockdownRotationTime = 0f; // 初期化
        knockdownStartTime = Time.time;  // 倒れる動作を開始した時間を記録
        Debug.Log("ノックダウン開始");
    }

    // 逃げる動作を停止するためのメソッド（オプション）
    public void StopEscape()
    {
        isEscaping = false;
    }
}
