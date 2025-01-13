using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CS_AspectKeep : MonoBehaviour
{
    [SerializeField]
    private Camera targetCamera; //�ΏۂƂ���J����

    [SerializeField]
    private Vector2 aspectVec = new Vector2(1080f, 1920f); //�ړI�𑜓x

    int width;
    int height;
    private void Awake()
    {
        targetCamera = GetComponent<Camera>();
        width = Screen.width;
        height = Screen.height;
        Screen.SetResolution(width, height, true);
    }

    void Update()
    {
        if (Screen.width != width || Screen.height != height)
        {
            Screen.SetResolution(width, height, true);
        }
        var screenAspect = Screen.width / (float)Screen.height;
        Debug.Log("width" + Screen.width);
        Debug.Log("height" + Screen.height);
        Debug.Log("screenAspect" + screenAspect);
        var targetAspect = aspectVec.x / aspectVec.y; //�ړI�̃A�X�y�N�g��

        var magRate = targetAspect / screenAspect; //�ړI�A�X�y�N�g��ɂ��邽�߂̔{��
        var viewportRect = new Rect(0, 0, 1, 1); //Viewport�����l��Rect���쐬

        if (magRate < 1)
        {
            viewportRect.width = magRate; //�g�p���鉡����ύX
            viewportRect.x = 0.5f - viewportRect.width * 0.5f;//������
        }
        else
        {
            viewportRect.height = 1 / magRate; //�g�p����c����ύX
            viewportRect.y = 0.5f - viewportRect.height * 0.5f;//�����]��
        }

        targetCamera.rect = viewportRect; //�J������Viewport�ɓK�p
    }
}
