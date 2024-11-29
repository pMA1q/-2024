using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SetPositionPerfPos : MonoBehaviour
{
    [Header("座標(10000,0,0)を原点としたときのポジション")]
    [SerializeField, Header("サブカメラの位置から+αの値")]
    private Vector3 mObjectPos= new Vector3(0f, 0f, 10f);

    [SerializeField, Header("カメラのポジション")]
    private Vector3 mCameraPos;

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
        }
        else
        {
            Debug.LogError("SubCameraが見つかりませんでした。");
        }

       // Destroy(this);
    }

    private void Update()
    {
        this.transform.position = mSubCamera.transform.position + mOffsetPos;
    }
}

