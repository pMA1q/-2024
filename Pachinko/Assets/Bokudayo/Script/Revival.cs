using UnityEngine;

public class Revival : MonoBehaviour
{
    public GameObject leftCapsule;  // 左側のカプセル
    public GameObject rightCapsule1; // 右側のカプセル
    public GameObject rightCapsule2; // 右側のカプセル
    public GameObject rightCapsule3; // 右側のカプセル

    private bool battleStarted = false;
    private int battleCount = 0; // 戦った回数
    private Vector3 leftStartPosition;  // 左カプセルの初期位置
    private Vector3 rightStartPosition1; // 右カプセルの初期位置
    private Vector3 rightStartPosition2; 
    private Vector3 rightStartPosition3; 
    private Quaternion leftStartRotation; // 左カプセルの初期回転
    private Quaternion rightStartRotation1; // 右カプセルの初期回転
    private Quaternion rightStartRotation2; 
    private Quaternion rightStartRotation3; 
    private bool battleEnded = false; // バトルが終了したかどうか

    void Start()
    {
        // 初期位置と回転を保存
        leftStartPosition = leftCapsule.transform.position;
        rightStartPosition1 = rightCapsule1.transform.position;
        rightStartPosition2 = rightCapsule2.transform.position;
        rightStartPosition3 = rightCapsule3.transform.position;
        leftStartRotation = leftCapsule.transform.rotation;
        rightStartRotation1 = rightCapsule1.transform.rotation;
        rightStartRotation2 = rightCapsule2.transform.rotation;
        rightStartRotation3 = rightCapsule3.transform.rotation;
    }

    void Update()
    {
        if (!battleStarted && !battleEnded)
        {
            // バトルを開始
            StartBattle();
        }
    }

    void StartBattle()
    {
        battleStarted = true;

        // カプセルが戦う
        StartCoroutine(BattleCoroutine());
    }

    System.Collections.IEnumerator BattleCoroutine()
    {
        yield return new WaitForSeconds(1); // 戦う前の待機時間

        // 勝者を決定
        bool leftWins = battleCount % 2 == 1; // 奇数回は左が負け、偶数回は右が負け

        if (leftWins)
        {
            // 右側のカプセルが倒れる
            rightCapsule1.GetComponent<Rigidbody>().isKinematic = false;
            rightCapsule1.GetComponent<Rigidbody>().AddForce(Vector3.right * 500);
            rightCapsule2.GetComponent<Rigidbody>().isKinematic = false;
            rightCapsule2.GetComponent<Rigidbody>().AddForce(Vector3.right * 500);
            rightCapsule3.GetComponent<Rigidbody>().isKinematic = false;
            rightCapsule3.GetComponent<Rigidbody>().AddForce(Vector3.right * 500);
            yield return new WaitForSeconds(2); // 倒れた後の待機時間

            // バトル終了処理
            //Debug.Log("右側のカプセルが倒れました。バトル終了！");
            battleEnded = true; // バトル終了フラグを設定
        }
        else
        {
            // 左側のカプセルが倒れる
            leftCapsule.GetComponent<Rigidbody>().isKinematic = false;
            leftCapsule.GetComponent<Rigidbody>().AddForce(Vector3.left * 500);
            yield return new WaitForSeconds(2); // 倒れた後の待機時間
            RespawnCapsule(leftCapsule, leftStartPosition, leftStartRotation);
        }

        battleCount++; // 戦った回数をカウント
    }

    void RespawnCapsule(GameObject capsule, Vector3 startPosition, Quaternion startRotation)
    {
        // カプセルを元の位置と回転に復活させる
        capsule.transform.position = startPosition;
        capsule.transform.rotation = startRotation; // 回転を元に戻す
        capsule.GetComponent<Rigidbody>().isKinematic = true; // 再度静止状態に戻す
        battleStarted = false; // バトルを再スタート可能にする
    }
}
