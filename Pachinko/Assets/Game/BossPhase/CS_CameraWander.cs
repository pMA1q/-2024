using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_CameraWander : MonoBehaviour
{
    [SerializeField]
    private float wanderSpeed = 2f; // カメラの左右移動速度
    [SerializeField]
    private float wanderRange = 3f; // カメラの左右移動範囲
    [SerializeField]
    private Vector3 offset = new Vector3(0f, 5f, -5f); // 対象物からの初期オフセット位置
    [SerializeField]
    private Transform mPlayer;
    private Transform subCameraTransform;
    private Vector3 initialCameraPosition;
    private float timer = 0f;

   
    public void Init()
    {
        // Scene内の"SubCamera"という名前のカメラを取得
        GameObject subCamera = GameObject.Find(CS_CommonData.Obj3D_RenderCamera);
        if (subCamera != null)
        {
            subCameraTransform = subCamera.transform;

            // 対象物の位置を基準にカメラの初期位置を設定
            initialCameraPosition = mPlayer.position + offset;
            subCameraTransform.position = initialCameraPosition;
            Debug.Log("カメラの位置" + subCameraTransform.position);
            subCameraTransform.LookAt(transform.position); // カメラを対象物に向ける
        }
        else
        {
            Debug.LogError("SubCamera が Scene 内に見つかりませんでした。カメラ名を確認してください。");
        }

    }
    void Update()
    {
        if (subCameraTransform != null)
        {
            // 時間によってカメラの左右移動を計算
            timer += Time.deltaTime * wanderSpeed;
            float offsetX = Mathf.Sin(timer) * wanderRange;

            // カメラの新しい位置を計算（初期位置のX軸方向にスライド）
            Vector3 newPosition = initialCameraPosition + mPlayer.right * offsetX;

            // カメラ位置を更新
            subCameraTransform.position = newPosition;

            // カメラを常に対象オブジェクトに向ける
            subCameraTransform.LookAt(mPlayer.position);
        }
    }
}
