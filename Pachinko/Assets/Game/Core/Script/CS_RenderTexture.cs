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
        //mDefaultScale = this.transform.localScale;

        //mRenderer = GetComponent<Renderer>();

        ////RenderTexture�̐ݒ�
        //RenderTexture rTexture = new RenderTexture(mSubCamera.pixelWidth, mSubCamera.pixelHeight, 24);
        //mSubCamera.targetTexture = rTexture;

        //// �e���A���̃��C���e�N�X�`����RenderTexture�ɐݒ�
        //mRenderer.material.mainTexture = rTexture;


        //AdjustTextureScale();

        mRenderer = GetComponent<Renderer>();

        // �T�u�J�����̃A�X�y�N�g����擾
        float aspectRatio = (float)mSubCamera.pixelWidth / mSubCamera.pixelHeight;

        // RenderTexture�̐ݒ�B�J�����̃A�X�y�N�g������ɐ��m�ȉ𑜓x�ō쐬
        int renderTextureWidth = mSubCamera.pixelHeight * Mathf.RoundToInt(aspectRatio);
        //RenderTexture rTexture = new RenderTexture(renderTextureWidth, mSubCamera.pixelHeight, 24);
        RenderTexture rTexture = new RenderTexture(720, 720, 24);
        rTexture.depthStencilFormat = UnityEngine.Experimental.Rendering.GraphicsFormat.D16_UNorm;
        mSubCamera.targetTexture = rTexture;

        // �}�e���A���̃��C���e�N�X�`����RenderTexture�ɐݒ�
        mRenderer.material.mainTexture = rTexture;
    }

    // Update is called once per frame
    void Update()
    {

        //// �}�e���A���Ƃ��ăI�u�W�F�N�g�ɓ��e
        //mRenderer.material.mainTexture = mSubCamera.targetTexture;

        
        //// ����̃e�N�X�`���X�P�[���ݒ�
        //AdjustTextureScale();
    }

    private void AdjustTextureScale()
    {
        float aspectRatio = (float)mSubCamera.pixelWidth / mSubCamera.pixelHeight;

        // �e�N�X�`���̃X�P�[���𒲐����A�A�X�y�N�g��ɍ��킹��
        mRenderer.material.mainTextureScale = new Vector2(aspectRatio, 1.0f);

        // �e�N�X�`���̃I�t�Z�b�g�𒆉������ɒ���
        mRenderer.material.mainTextureOffset = new Vector2((1.0f - aspectRatio) / 2.0f, 0.0f);
    }
}
