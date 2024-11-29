using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SetPositionPerfPos : MonoBehaviour
{
    [Header("座標(10000,0,0)を原点としたときのポジション")]
    [SerializeField, Header("オブジェクトの初期位置")]
    private Vector3 mObjectPos= new Vector3(10000f, 0f, 10f);

    [SerializeField, Header("カメラのポジション")]
    private Vector3 mCameraPos = new Vector3(10000f, 0f, 0f);
    [SerializeField, Header("カメラの回転")]
    private Vector3 mCameraRot;

    Camera mSubCamera;

    void Start()
    {
        // "SubCamera"という名前のカメラを検索
        mSubCamera = GameObject.Find("SubCamera")?.GetComponent<Camera>();

        // SubCameraが見つかった場合、CanvasのRender Cameraに設定
        if (mSubCamera != null)
        {
            Vector3 newPos = mObjectPos;
            this.transform.position = newPos;
            mSubCamera.transform.position = mCameraPos;
            mSubCamera.transform.eulerAngles = mCameraRot;
        }
        else
        {
            Debug.LogError("SubCameraが見つかりませんでした。");
        }

       // Destroy(this);
    }
}

