using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PinVibration : MonoBehaviour
{
    private Collider pinCollider; // 動かす Collider（Mesh とは別のオブジェクトでもOK）
    private float vibrationStrength = 0.2f; // 振動の強さ
    private float vibrationSpeed = 5; // 振動の速さ

    private Vector3 initialPosition; // Collider の初期位置

    void Start()
    {
        if (pinCollider == null)
        {
            pinCollider = GetComponent<Collider>();
        }

        // Collider の初期位置を保存
        initialPosition = pinCollider.transform.position;
    }

    void FixedUpdate()
    {
        // 振動を計算
        float offset = Mathf.Cos(Time.time * vibrationSpeed) * vibrationStrength;

        // Collider の位置を更新
        pinCollider.transform.position = initialPosition + new Vector3(0, offset, 0);
    }
}
