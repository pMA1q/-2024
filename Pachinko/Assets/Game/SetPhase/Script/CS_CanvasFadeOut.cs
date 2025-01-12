using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_CanvasFadeOut : MonoBehaviour
{
    public Canvas canvas; // フェードアウトさせるCanvas
    public float mTimer = 2.0f; // フェードアウトにかける時間

    private Graphic[] graphics;

    private void Start()
    {
        if (canvas == null)
        {
            Debug.LogError("Canvasが設定されていません。");
            return;
        }

        // Canvas内のすべてのGraphicコンポーネントを取得
        graphics = canvas.GetComponentsInChildren<Graphic>();

        // フェードアウト開始
        StartCoroutine(FadeOutAndDisableCanvas());
    }

    private IEnumerator FadeOutAndDisableCanvas()
    {
        yield return new WaitForSeconds(5f);
        float elapsedTime = 0f;

        // 初期のアルファ値を保存
        float[] initialAlphas = new float[graphics.Length];
        for (int i = 0; i < graphics.Length; i++)
        {
            initialAlphas[i] = graphics[i].color.a;
        }

        while (elapsedTime < mTimer)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / mTimer);

            foreach (var graphic in graphics)
            {
                if (graphic != null)
                {
                    Color color = graphic.color;
                    color.a = alpha;
                    graphic.color = color;
                }
            }

            yield return null;
        }

        // 最終的に完全に透明にする
        foreach (var graphic in graphics)
        {
            if (graphic != null)
            {
                Color color = graphic.color;
                color.a = 0f;
                graphic.color = color;
            }
        }

        // Canvasを非アクティブ化
        Destroy(this.transform.root.gameObject);
    }
}
