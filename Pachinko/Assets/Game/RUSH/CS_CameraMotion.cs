using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_CameraMotion : MonoBehaviour
{
    [SerializeField] private Vector3 goalPosition; // ゴール地点の位置
    [SerializeField] private Vector3 goalRotation; // ゴール地点での回転値 (Euler角)
    [SerializeField] private float duration = 2.0f; // ゴール地点に行くまでの時間

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Quaternion goalQuaternion;
    private float elapsedTime = 0f;

    void Start()
    {
        // 初期位置と回転を記録
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // ゴール地点の回転値をQuaternionに変換
        goalQuaternion = Quaternion.Euler(goalRotation);
    }

    void Update()
    {
        if (elapsedTime < duration)
        {
            // 経過時間を更新
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            // 位置と回転を補間
            transform.position = Vector3.Lerp(initialPosition, goalPosition, t);
            transform.rotation = Quaternion.Slerp(initialRotation, goalQuaternion, t);
        }
        else
        {
            // ゴール地点に到達したらスクリプトを破棄
            transform.position = goalPosition;
            transform.rotation = goalQuaternion;
            Destroy(this);
        }
    }
}
