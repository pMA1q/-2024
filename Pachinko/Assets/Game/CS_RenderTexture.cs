using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_RenderTexture : MonoBehaviour
{
    [SerializeField, Header("�T�u�J����")]
    private Camera mSubCamera;

    private Renderer mRenderer;

    // Start is called before the first frame update
    void Start()
    {
        mRenderer = GetComponent<Renderer>();

        // �J�����̃A�X�y�N�g����擾
        float cameraAspect = mSubCamera.aspect;

        // �I�u�W�F�N�g�̃X�P�[�����J�����̃A�X�y�N�g��ɍ��킹�Ē���
        Vector3 screenScale = transform.localScale;
        screenScale.y = screenScale.x / cameraAspect; // �A�X�y�N�g��Ɋ�Â��č����𒲐�
        transform.localScale = screenScale;

        // RenderTexture�̐ݒ�
        RenderTexture rTexture = new RenderTexture(Screen.width, Screen.height, 24);
        mSubCamera.targetTexture = rTexture;

        // �}�e���A���̃��C���e�N�X�`����RenderTexture�ɐݒ�
        mRenderer.material.mainTexture = rTexture;
    }

    // Update is called once per frame
    void Update()
    {
        
        //RenderTexture rTexture = new RenderTexture(Screen.width, Screen.height, 24);
        //mSubCamera.targetTexture = rTexture;

        //�}�e���A���Ƃ��ăI�u�W�F�N�g�ɓ��e
        mRenderer.material.mainTexture = mSubCamera.targetTexture;
    }
}
