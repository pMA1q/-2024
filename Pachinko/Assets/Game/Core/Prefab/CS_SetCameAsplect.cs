using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SetCameAsplect : MonoBehaviour
{
    [SerializeField, Header("â°î‰ó¶")]
    private float mWidthRatio = 16.0f;

    [SerializeField, Header("ècî‰ó¶")]
    private float mHeightRatio = 9.0f;

    [SerializeField, Header("éãñÏäp")]
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

        // éãñÏäpÇçƒê›íËÇµÇƒämé¿Ç…îΩâf
        camera.fieldOfView = mFieldView;
    }
}
