using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_QuadEffectAnimation : MonoBehaviour
{
    [SerializeField,Header("アニメーションする画像データ")]
    private Sprite[] sprites; // アニメーション用スプライトのリスト

    [SerializeField,Header("フレーム間の時間（秒）")]
    private float frameRate = 0.05f; // フレーム間の時間（秒）

    private Material[] quadMaterial; // Quadのマテリアル
    private int currentFrame = 0; // 現在のフレーム番号
    private float timer = 0f; // 経過時間

    void Start()
    {
        // QuadのRendererコンポーネントからマテリアルを取得
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            quadMaterial = renderer.materials;
        }
        else
        {
            Debug.LogError("Rendererが見つかりません！");
        }
    }

    private void OnEnable()
    {
        currentFrame = 0;
    }

    void Update()
    {
        if (quadMaterial == null || sprites == null || sprites.Length == 0) return;

        // アニメーションタイミング
        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            // 次のフレームに進む
            currentFrame = (currentFrame + 1) % sprites.Length;

            // スプライトをテクスチャとしてマテリアルに設定
            quadMaterial[0].mainTexture = sprites[currentFrame].texture;

            timer = 0f; // タイマーをリセット
        }
    }
}
