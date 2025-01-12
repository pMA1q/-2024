using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteAlways]
public class CS_AspectKeep : MonoBehaviour
{
    [SerializeField]
    private Camera targetCamera; //�ΏۂƂ���J����

    [SerializeField]
    private Vector2 aspectVec = new Vector2( 1080f,1920f ); //�ړI�𑜓x

    private void OnEnable()
    {
        targetCamera = GetComponent<Camera>();
    }

    void Update()
    {
        var screenAspect = Display.main.systemWidth / (float)Display.main.systemHeight; //��ʂ̃A�X�y�N�g��
        Debug.Log("width" + Display.main.systemWidth);
        Debug.Log("height" + Display.main.systemHeight);
        Debug.Log("screenAspect" + screenAspect);
        var targetAspect = aspectVec.x / aspectVec.y; //�ړI�̃A�X�y�N�g��
        Debug.Log("targetAspect" + targetAspect);

        var magRate = targetAspect / screenAspect; //�ړI�A�X�y�N�g��ɂ��邽�߂̔{��
        var viewportRect = new Rect(0, 0, 1, 1); //Viewport�����l��Rect���쐬

        Debug.Log("�{��" + magRate);

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
        Destroy(this);
    }
}
