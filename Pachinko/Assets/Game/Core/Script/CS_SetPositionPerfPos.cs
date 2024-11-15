using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SetPositionPerfPos : MonoBehaviour
{
    [SerializeField, Header("サブカメラの位置から+αの値")]
    private Vector3 mOffsetPos= new Vector3(0f, 0f, 10f);

    [SerializeField, Header("合計値+αの値")]
    private Vector3 mSumPos;

    Camera mSubCamera;

    void Start()
    {
        // "SubCamera"という名前のカメラを検索
        mSubCamera = GameObject.Find("SubCamera")?.GetComponent<Camera>();

        // SubCameraが見つかった場合、CanvasのRender Cameraに設定
        if (mSubCamera != null)
        {
            Vector3 newPos = this.transform.position;
            newPos = mSubCamera.transform.position + mOffsetPos;
            this.transform.position = newPos;
        }
        else
        {
            Debug.LogWarning("SubCameraが見つかりませんでした。");
        }

       // Destroy(this);
    }

    private void Update()
    {
        this.transform.position = mSubCamera.transform.position + mOffsetPos;
    }
}

