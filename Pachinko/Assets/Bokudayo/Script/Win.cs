using UnityEngine;

public class Win : MonoBehaviour // クラス名を変更
{
    public GameObject leftCapsule;  // 左側のカプセル
    public GameObject rightCapsule1; // 右側のカプセル
    public GameObject rightCapsule2; // 右側のカプセル
    public GameObject rightCapsule3; // 右側のカプセル

    private bool battleStarted = false;

    void Update()
    {
        if (!battleStarted)
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

        // ランダムに勝者を決定
        //bool leftWins = Random.value > 0.5f;

        //右側のカプセルが倒れる
        bool leftWins = true;

        if (leftWins)
        {
            // 右側のカプセルが倒れる
            rightCapsule1.GetComponent<Rigidbody>().isKinematic = false;
            rightCapsule1.GetComponent<Rigidbody>().AddForce(Vector3.right * 500); // 後ろに倒れる力を加える
            rightCapsule2.GetComponent<Rigidbody>().isKinematic = false;
            rightCapsule2.GetComponent<Rigidbody>().AddForce(Vector3.right * 500); // 後ろに倒れる力を加える
            rightCapsule3.GetComponent<Rigidbody>().isKinematic = false;
            rightCapsule3.GetComponent<Rigidbody>().AddForce(Vector3.right * 500); // 後ろに倒れる力を加える
        }
        else
        {
            // 左側のカプセルが倒れる
            leftCapsule.GetComponent<Rigidbody>().isKinematic = false;
            leftCapsule.GetComponent<Rigidbody>().AddForce(Vector3.back * 500);
        }
    }
}
