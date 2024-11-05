using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SetPsitionPerfPos : MonoBehaviour
{
    [SerializeField, Header("サブカメラの位置から+αの値")]
    private Vector3 mOffsetPos;

    void Start()
    {
        // "SubCamera"という名前のカメラを検索
        Camera subCamera = GameObject.Find("SubCamera")?.GetComponent<Camera>();

        // SubCameraが見つかった場合、CanvasのRender Cameraに設定
        if (subCamera != null)
        {
            Vector3 newPos = this.transform.position;
            newPos = subCamera.transform.position + mOffsetPos;
            this.transform.position = newPos;
        }
        else
        {
            Debug.LogWarning("SubCameraが見つかりませんでした。");
        }

       // Destroy(this);
    }
}

