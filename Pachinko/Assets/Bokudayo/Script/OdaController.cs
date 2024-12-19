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

    // 移動中かどうかのフラグ
    private bool isMoving = true;

    // 複数の敵キャラクターのスクリプト（EnemyControllerの配列）
    public EnemyController[] enemyControllers;

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
        // 移動中の場合のみz座標を補間
        if (isMoving)
        {
            // ゲームが始まってからの経過時間を計算
            float elapsedTime = Time.time - startTime;

            // 経過時間に基づいてz座標を補間
            float newZ = Mathf.Lerp(startZ, targetZ, elapsedTime * moveSpeed);

            // 新しいz座標を設定
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);

            // z座標が目標に達したら、移動を停止
            if (Mathf.Approximately(newZ, targetZ))
            {
                isMoving = false; // 移動終了

                // 複数の敵キャラクターが順番に逃げる動作を開始
                foreach (var enemyController in enemyControllers)
                {
                    if (enemyController != null)
                    {
                        enemyController.StartEscape();  // EnemyControllerのStartEscapeメソッドを呼び出し
                    }
                }
            }
        }

        // スペースキーが押されたか確認
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // SpacePressed トリガーをセットして遷移を発生させる
            animator.SetTrigger("SpacePressed");
        }
    }
}
