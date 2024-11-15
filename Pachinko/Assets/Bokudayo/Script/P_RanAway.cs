using UnityEngine;

public class P_RanAway : MonoBehaviour
{
    public float slideSpeed = 5f; // スライド速度
    private bool isSliding = false; // スライド中かどうかのフラグ
    private Vector3 targetPosition; // 移動先の位置

    void Start()
    {
        targetPosition = new Vector3(-20, 0, 0); // 移動先の位置を設定
        StartCoroutine(StartSlidingAfterDelay(1f)); // 1秒待機してスライドを開始
        GameObject rootObject = transform.root.gameObject;
        if (rootObject.GetComponent<CS_PerformanceFinish>() == null)
        {
            rootObject.AddComponent<CS_PerformanceFinish>().DestroyConfig(true, 5f); ;//プレハブを消すまでの時間(秒)
        }
    }

    private System.Collections.IEnumerator StartSlidingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 指定した秒数待機
        isSliding = true; // スライドを開始
    }

    void Update()
    {
        // スライド中の場合は移動
        if (isSliding)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, slideSpeed * Time.deltaTime);
            // 移動が完了したらスライドを終了
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isSliding = false;
                // オプション: カプセルを非表示にする
                // gameObject.SetActive(false);
            }
        }
    }
}
