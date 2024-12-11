using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ShojiController : MonoBehaviour
{
    public Transform shojiLeft;  // 左の障子
    public Transform shojiRight; // 右の障子

    public float openDistance = 2.0f;  // 障子が開く距離（Z軸方向）
    public float openSpeed = 2.0f;     // 開く速さ
    private bool isOpening = false;   // 現在開いているかどうか

    private Vector3 leftStartPos;     // 左障子の初期位置
    private Vector3 rightStartPos;    // 右障子の初期位置
    private Vector3 leftOpenPos;      // 左障子の開いた位置
    private Vector3 rightOpenPos;     // 右障子の開いた位置

    void Start()
    {
        // 初期位置を記録
        leftStartPos = shojiLeft.position;
        rightStartPos = shojiRight.position;

        // 開いた時の位置を計算
        leftOpenPos = leftStartPos + new Vector3(0, 0, openDistance);
        rightOpenPos = rightStartPos + new Vector3(0, 0, -openDistance);
    }

    void Update()
    {
        // スペースキーで開閉を切り替える
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isOpening = !isOpening;  // 状態を切り替え
        }

        // 状態に応じて移動
        if (isOpening)
        {
            // 開く処理
            shojiLeft.position = Vector3.Lerp(shojiLeft.position, leftOpenPos, Time.deltaTime * openSpeed);
            shojiRight.position = Vector3.Lerp(shojiRight.position, rightOpenPos, Time.deltaTime * openSpeed);
        }
        else
        {
            // 閉じる処理
            shojiLeft.position = Vector3.Lerp(shojiLeft.position, leftStartPos, Time.deltaTime * openSpeed);
            shojiRight.position = Vector3.Lerp(shojiRight.position, rightStartPos, Time.deltaTime * openSpeed);
        }
    }
}
