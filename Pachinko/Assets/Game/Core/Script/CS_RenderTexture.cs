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
        mDefaultScale = this.transform.localScale;

        mRenderer = GetComponent<Renderer>();

        //カメラのアスペクト比を取得
        float cameraAspect = mSubCamera.aspect;

        //オブジェクトのスケールをカメラのアスペクト比に合わせて調整
        Vector3 screenScale = transform.localScale;
        screenScale.y = screenScale.x / cameraAspect; // アスペクト比に基づいて高さを調整
        transform.localScale = screenScale;

        //RenderTextureの設定
        RenderTexture rTexture = new RenderTexture(Screen.width, Screen.height, 24);
        mSubCamera.targetTexture = rTexture;

        // テリアルのメインテクスチャをRenderTextureに設定
        mRenderer.material.mainTexture = rTexture;
    }

    // Update is called once per frame
    void Update()
    {
        
        //マテリアルとしてオブジェクトに投影
        mRenderer.material.mainTexture = mSubCamera.targetTexture;

        this.transform.localScale = mDefaultScale;
    }
}
