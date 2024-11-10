//---------------------------------
//�T�u�J�����ɉf���Ă�����̂����g�ɓ��e���鏈��
//�S���ҁF����
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_RenderTexture : MonoBehaviour
{
    [SerializeField, Header("�T�u�J����")]
    private Camera mSubCamera;

    private Renderer mRenderer;

    private Vector3 mDefaultScale;

    // Start is called before the first frame update
    void Start()
    {
        mDefaultScale = this.transform.localScale;

        mRenderer = GetComponent<Renderer>();

        //RenderTexture�̐ݒ�
        RenderTexture rTexture = new RenderTexture(Screen.width, Screen.height, 24);
        mSubCamera.targetTexture = rTexture;

        // �e���A���̃��C���e�N�X�`����RenderTexture�ɐݒ�
        mRenderer.material.mainTexture = rTexture;
    }

    // Update is called once per frame
    void Update()
    {
        
        //�}�e���A���Ƃ��ăI�u�W�F�N�g�ɓ��e
        mRenderer.material.mainTexture = mSubCamera.targetTexture;

        //this.transform.localScale = mDefaultScale;
    }
}
