//---------------------------------
//サブカメラに映っているものを自身に投影する処理
//担当者：中島
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_RenderTexture : MonoBehaviour
{
    [SerializeField, Header("サブカメラ")]
    private Camera mSubCamera;

    private Renderer mRenderer;

    private Vector3 mDefaultScale;

    // Start is called before the first frame update
    void Start()
    {
        //mDefaultScale = this.transform.localScale;

        //mRenderer = GetComponent<Renderer>();

        ////RenderTextureの設定
        //RenderTexture rTexture = new RenderTexture(mSubCamera.pixelWidth, mSubCamera.pixelHeight, 24);
        //mSubCamera.targetTexture = rTexture;

        //// テリアルのメインテクスチャをRenderTextureに設定
        //mRenderer.material.mainTexture = rTexture;


        //AdjustTextureScale();

        mRenderer = GetComponent<Renderer>();

        // サブカメラのアスペクト比を取得
        float aspectRatio = (float)mSubCamera.pixelWidth / mSubCamera.pixelHeight;

        // RenderTextureの設定。カメラのアスペクト比を元に正確な解像度で作成
        int renderTextureWidth = mSubCamera.pixelHeight * Mathf.RoundToInt(aspectRatio);
        //RenderTexture rTexture = new RenderTexture(renderTextureWidth, mSubCamera.pixelHeight, 24);
        RenderTexture rTexture = new RenderTexture(720, 720, 24);
        rTexture.depthStencilFormat = UnityEngine.Experimental.Rendering.GraphicsFormat.D16_UNorm;
        mSubCamera.targetTexture = rTexture;

        // マテリアルのメインテクスチャをRenderTextureに設定
        mRenderer.material.mainTexture = rTexture;
    }

    // Update is called once per frame
    void Update()
    {

        //// マテリアルとしてオブジェクトに投影
        //mRenderer.material.mainTexture = mSubCamera.targetTexture;

        
        //// 初回のテクスチャスケール設定
        //AdjustTextureScale();
    }

    private void AdjustTextureScale()
    {
        float aspectRatio = (float)mSubCamera.pixelWidth / mSubCamera.pixelHeight;

        // テクスチャのスケールを調整し、アスペクト比に合わせる
        mRenderer.material.mainTextureScale = new Vector2(aspectRatio, 1.0f);

        // テクスチャのオフセットを中央揃えに調整
        mRenderer.material.mainTextureOffset = new Vector2((1.0f - aspectRatio) / 2.0f, 0.0f);
    }
}
