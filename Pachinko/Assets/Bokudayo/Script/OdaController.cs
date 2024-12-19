using UnityEngine;

public class OdaController : MonoBehaviour
{
    // Animator コンポーネント
    private Animator animator;

    // 最終的なz座標
    public float targetZ = -10f;
    // 移動速度（どれくらいの速さで移動するか）
    public float moveSpeed = 1f;

    // 最初の位置を保存
    private float startZ;
    // 移動開始時刻
    private float startTime;

    // 逃げる動作を開始するためのフラグ
    private bool isStopped = false;
    private float stopTime;

    // 立ち止まってからアニメーション遷移を開始するためのフラグ
    private bool animationTriggered = false;
    private float animationDelay = 2f;  // 2秒後にアニメーション遷移
    private float animationStartTime;

    // 複数の敵を格納する配列
    public EnemyController[] enemyControllers;

    // 攻撃可能かどうかのフラグ（外部から制御可能）
    public bool canAttack = true;

    void Start()
    {
        // Animator コンポーネントの取得
        animator = GetComponent<Animator>();

        // 初期のz座標を保存
        startZ = transform.position.z;
        // ゲーム開始時に移動を開始
        startTime = Time.time;
    }

    void Update()
    {
        // ゲームが始まってからの経過時間を計算
        float elapsedTime = Time.time - startTime;

        // 経過時間に基づいてz座標を補間
        float newZ = Mathf.Lerp(startZ, targetZ, elapsedTime * moveSpeed);

        // 新しいz座標を設定
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);

        // z座標が目標に達したら、移動を停止
        if (Mathf.Approximately(newZ, targetZ) && !isStopped)
        {
            // 停止フラグを設定して、停止後の2秒待機を開始
            isStopped = true;
            stopTime = Time.time;
            animationStartTime = Time.time;  // アニメーションのカウント開始
        }

        // 停止後2秒待機したら、攻撃アニメーション遷移を開始
        if (isStopped && !animationTriggered && Time.time - animationStartTime >= animationDelay && canAttack)
        {
            // アニメーションを遷移させる
            animator.SetTrigger("SpacePressed");

            // アニメーション遷移が発生したことを記録
            animationTriggered = true;

            // 攻撃が発生したので、敵が倒れるようにする
            foreach (var enemyController in enemyControllers)
            {
                if (enemyController != null)
                {
                    enemyController.KnockDown(); // 敵を倒す
                }
            }
        }

        // 停止後2秒待機したら、敵の逃げる動作を開始
        if (isStopped && Time.time - stopTime >= 2f)
        {
            // 2秒後に全ての敵を逃げさせる
            foreach (var enemyController in enemyControllers)
            {
                if (enemyController != null)
                {
                    enemyController.StartEscape();
                }
            }
            isStopped = false; // 逃げる処理が開始されたら、停止フラグをリセット
        }

        // スペースキーが押されたか確認
        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            // SpacePressed トリガーをセットして遷移を発生させる
            animator.SetTrigger("SpacePressed");
        }
    }

    // 外部から攻撃のオンオフを制御するメソッド
    public void ToggleAttack(bool isEnabled)
    {
        canAttack = isEnabled;
    }
}
