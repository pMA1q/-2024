using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SetRenderCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // "SubCamera"�Ƃ������O�̃J����������
        Camera subCamera = GameObject.Find("SubCamera")?.GetComponent<Camera>();

        // SubCamera�����������ꍇ�ACanvas��Render Camera�ɐݒ�
        if (subCamera != null)
        {
            Canvas canvas = GetComponent<Canvas>();
            if (canvas != null)
            {
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                canvas.worldCamera = subCamera;
            }
            else
            {
                Debug.LogWarning("Canvas��������܂���ł����B");
            }
        }
        else
        {
            Debug.LogWarning("SubCamera��������܂���ł����B");
        }

        Destroy(this);
    }

    
}
