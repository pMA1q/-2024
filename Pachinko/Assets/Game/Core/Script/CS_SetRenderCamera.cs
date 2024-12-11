using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SetRenderCamera : MonoBehaviour
{
    private float planeDistance = 1.0f;
    // Start is called before the first frame update
    void Awake()
    {
        // "SubCamera"という名前のカメラを検索
        Camera subCamera = GameObject.Find("SubCamera")?.GetComponent<Camera>();

        // SubCameraが見つかった場合、CanvasのRender Cameraに設定
        if (subCamera != null)
        {
            Canvas canvas = GetComponent<Canvas>();
            if (canvas != null)
            {
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                canvas.worldCamera = subCamera;
                canvas.planeDistance = planeDistance;
            }
            else
            {
                Debug.LogWarning("Canvasが見つかりませんでした。");
            }
        }
        else
        {
            Debug.LogWarning("SubCameraが見つかりませんでした。");
        }

        Destroy(this);
    }

    
}
