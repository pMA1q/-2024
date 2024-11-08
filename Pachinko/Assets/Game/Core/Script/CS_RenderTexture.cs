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

        //this.transform.localScale = mDefaultScale;
    }
}
