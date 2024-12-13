                                                                                                                                                      using UnityEngine;

public class TransparencyController : MonoBehaviour
{
    private Renderer objectRenderer;  // オブジェクトのRenderer
    private Material objectMaterial;  // オブジェクトのマテリアル

    [Range(0f, 1f)]  // スライダーで透明度を調整できるように設定（0: 完全に透明, 1: 完全に不透明）
    public float transparency = 1f;

    void Start()
    {
        // オブジェクトのRendererを取得
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer != null)
        {
            // オブジェクトのマテリアルを取得
            objectMaterial = objectRenderer.material;
        }
    }

    void Update()
    {
        if (objectMaterial != null)
        {
            // 現在の透明度に基づいて色を変更
            Color currentColor = objectMaterial.GetColor("_Color");  // 現在の色を取得
            currentColor.a = transparency;  // 透明度を設定
            objectMaterial.SetColor("_Color", currentColor);  // 新しい色を設定
        }
    }
}
