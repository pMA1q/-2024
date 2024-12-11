using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BP_NodevCameraMotion : MonoBehaviour
{
    [SerializeField]
    private CSO_CameraInterpolation cameraMosion;

    private Camera subCamera;


    private int motionNum = 0; // 現在のモーション番号
    private int currentStep = 1; // 現在のステップ番号

    private bool isMoving = false; // 移動中かどうか

    // Start is called before the first frame update
    void Start()
    {
        subCamera = GameObject.Find("SubCamera")?.GetComponent<Camera>();
        subCamera.transform.position = cameraMosion.inofomations[0].positions[0];
        subCamera.transform.eulerAngles = cameraMosion.inofomations[0].lotations[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving && subCamera != null)
        {
            StartCoroutine(MoveAndRotateCamera());
        }
    }

    private IEnumerator MoveAndRotateCamera()
    {
        isMoving = true;

        var currentMotion = cameraMosion.inofomations[motionNum];

        // ポジションと回転のステップリスト
        List<Vector3> positions = currentMotion.positions;
        List<Vector3> rotations = currentMotion.lotations;

        // 長い方のリストの長さを基準にする
        int stepCount = Mathf.Max(positions.Count, rotations.Count);

        // 現在のステップから最後のステップまで補間
        while (currentStep < stepCount)
        {
            
            // ポジションの取得（短い場合は最後の要素を維持）
            Vector3 startPosition = subCamera.transform.position;
            Vector3 targetPosition = currentStep < positions.Count
                ? positions[currentStep]
                : positions[positions.Count - 1];

            // 回転の取得（短い場合は最後の要素を維持）
            Quaternion startRotation = subCamera.transform.rotation;
            Quaternion targetRotation = currentStep < rotations.Count
                ? Quaternion.Euler(rotations[currentStep])
                : Quaternion.Euler(rotations[rotations.Count - 1]);

            float t = 0f;

            // ポジションと回転を補間
            while (t < 1f)
            {
                Debug.Log("移動中");
                t += Time.deltaTime * currentMotion.moveSpeed;

                // ポジション補間
                subCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

                // 回転補間
                subCamera.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

                yield return null;
            }

            // ステップ完了
            currentStep++;
        }

        // 次のモーションに進む
        currentStep = 1;
        yield return StartCoroutine(NextCameraMotion());

        isMoving = false;
    }

    private IEnumerator NextCameraMotion()
    {
        yield return new WaitForSeconds(1f); // 次のモーション開始までの待機

        motionNum++;

        if (motionNum >= cameraMosion.inofomations.Count)
        {
            motionNum = 0; // 最初のモーションに戻る
        }

        subCamera.transform.position = cameraMosion.inofomations[motionNum].positions[0];
        subCamera.transform.eulerAngles = cameraMosion.inofomations[motionNum].lotations[0];
    }
}
