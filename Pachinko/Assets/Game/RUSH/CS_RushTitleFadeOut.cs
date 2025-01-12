using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_RushTitleFadeOut : MonoBehaviour
{
    public RawImage panelImage;        // フェードアウトに使用するパネル画像
    public Image panelText;             // フェードアウトに使用するテキスト
    public float fadeOutTime = 3.0f;   // フェードアウト時間

    private void OnEnable()
    {
        // フェードアウト開始
        StartCoroutine(FadeOutPanelAndTransition());
    }

    private IEnumerator FadeOutPanelAndTransition()
    {
        yield return new WaitForSeconds(3.0f);
        // 色を取得
        Color startColor = panelImage.color;
        Color textColor = panelText.color;

        // アルファ値減少速度を計算CS_CommonData.BigControllerName
        float alphaPerSecondPanel = startColor.a / fadeOutTime;
        float alphaPerSecondText = textColor.a / fadeOutTime;

        // フェードアウト処理
        while (startColor.a > 0)
        {
            startColor.a -= alphaPerSecondPanel * Time.deltaTime;
            textColor.a -= alphaPerSecondText * Time.deltaTime;

            panelImage.color = startColor;
            panelText.color = textColor;

            yield return null;
        }

        // フェードアウトが完了したらオブジェクトを消去
        GameObject rootObject = transform.root.gameObject;
        Destroy(rootObject);
    }
}
