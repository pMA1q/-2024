using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SetCameAsplect : MonoBehaviour
{
    [SerializeField, Header("���䗦")]
    private float mWidthRatio = 16.0f;

    [SerializeField, Header("�c�䗦")]
    private float mHeightRatio = 9.0f;

    [SerializeField, Header("����p")]
    private float mFieldView = 96.5f;

    private Camera camera;

    void Awake()
    {
        camera = GetComponent<Camera>();
       // SetCameraAspectAndFOV();
    }

    void Update()
    {
        SetCameraAspectAndFOV();
    }

   
    private void SetCameraAspectAndFOV()
    {
        float targetAspect = mWidthRatio / mHeightRatio;
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        Rect rect = camera.rect;

        if (scaleHeight < 1.0f)
        {
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
        }

        camera.rect = rect;

        // ����p���Đݒ肵�Ċm���ɔ��f
        camera.fieldOfView = mFieldView;
    }
}
