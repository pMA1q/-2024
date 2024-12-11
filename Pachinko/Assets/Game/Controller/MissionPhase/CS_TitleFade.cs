using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CS_TitleFade : MonoBehaviour
{
    public RawImage panelImage; // フェードアウトに使用するパネル画像
    public Text panelText;      // フェードアウトに使用するテキスト
    public float fadeOutTime = 3.0f; // フェードアウト時間
    public CS_Controller bigController; // コントローラーへの参照（フェーズ管理用）

    private void OnEnable()
    {
        // フェードアウト開始
        StartCoroutine(FadeOutPanelAndTransition());
    }

    private IEnumerator FadeOutPanelAndTransition()
    {
        // 色を取得
        Color startColor = panelImage.color;
        Color textColor = panelText.color;

        // アルファ値減少速度を計算
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

        // フェーズをボスフェーズに変更
        if (bigController != null)
        {
            bigController.ChangePhase(CS_Controller.PACHINKO_PHESE.BOSS);
            Debug.Log("ボスフェーズへ移行しました");
        }
        else
        {
            Debug.LogWarning("CS_Controller が設定されていません");
        }
    }
}
