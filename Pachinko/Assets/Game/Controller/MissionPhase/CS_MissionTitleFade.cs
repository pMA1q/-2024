using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CS_MissionTitleFade : MonoBehaviour
{
    public RawImage panelImage;
    public Text panelText;
    public float fadeOutTime = 3.0f;

    private void OnEnable()
    {
        // フェードアウト開始
        StartCoroutine(FadeOutPanel());
    }

    private IEnumerator FadeOutPanel() 
    {
        Color startColor = panelImage.color;
        Color TextColor = panelText.color;

        float alphaperSecond = startColor.a / fadeOutTime;
        float alphaperText = TextColor.a / fadeOutTime;
        while (startColor.a > 0)
        {
            startColor.a -= alphaperSecond * Time.deltaTime;
            panelImage.color = startColor;
            TextColor.a -= alphaperText * Time.deltaTime;
            panelText.color = TextColor;
            yield return null;
        }

        GameObject rootObject = transform.root.gameObject;
        Destroy(rootObject);
    }
}