using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ShojiController : MonoBehaviour
{
    public Transform shojiLeft;  // 左の障子
    public Transform shojiRight; // 右の障子

    public float openDistance = 2.0f;  // 開く距離
    public float openSpeed = 2.0f;     // 開く速さ
    private bool isOpening = false;   // 開いているかの状態

    private Vector3 leftStartPos;
    private Vector3 rightStartPos;

    void Start()
    {
        // 初期位置を記録
        leftStartPos = shojiLeft.position;
        rightStartPos = shojiRight.position;
    }

    void Update()
    {
        // スペースキーで開閉
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isOpening)
            {
                OpenShoji();
            }
        }

        // 開いているときの移動処理
        if (isOpening)
        {
            shojiLeft.position = Vector3.Lerp(shojiLeft.position, leftStartPos + Vector3.left * openDistance, Time.deltaTime * openSpeed);
            shojiRight.position = Vector3.Lerp(shojiRight.position, rightStartPos + Vector3.right * openDistance, Time.deltaTime * openSpeed);
        }
    }

    // 障子を開くメソッド
    public void OpenShoji()
    {
        isOpening = true;
    }
}
