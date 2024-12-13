using UnityEngine;

public class BackgroundObjectTransparency : MonoBehaviour
{
    private Renderer[] backgroundRenderers;  // 障子背景オブジェクトのレンダラー配列

    void Start()
    {
        // 背景オブジェクトのレンダラーを取得
        backgroundRenderers = GetComponentsInChildren<Renderer>();

        // 背景オブジェクトを透明に設定
        SetObjectTransparency(new Color(1, 1, 1, 0));  // 完全透明
    }

    // 背景オブジェクトの透明度を設定するメソッド
    public void SetObjectTransparency(Color transparentColor)
    {
        if (backgroundRenderers != null)
        {
            foreach (var renderer in backgroundRenderers)
            {
                if (renderer != null)
                {
                    // 透明にするための設定
                    var material = renderer.material;
                    material.SetFloat("_Mode", 3);  // 透明モードに設定
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.EnableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = 3000;

                    // 色を透明に設定
                    renderer.material.color = transparentColor;
                }
            }
        }
    }

    // 他のスクリプトから透明度を変更するためのメソッド
    public void MakeObjectTransparent()
    {
        SetObjectTransparency(new Color(1, 1, 1, 0));  // 完全透明にする
    }

    // 他のスクリプトから背景オブジェクトを元に戻す（透明度を 1 に設定）
    public void ResetObjectTransparency()
    {
        SetObjectTransparency(new Color(1, 1, 1, 1));  // 完全不透明に戻す
    }
}
