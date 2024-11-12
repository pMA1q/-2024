using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CS_MissionTitleFade : MonoBehaviour
{
    public Image panelImage;
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
        float elapsedTime = 0;

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
        panelImage.color = new Color(startColor.r, startColor.g, startColor.b, 0);
        //GameObject rootObject = transform.root.gameObject;
        //Destroy(rootObject);
    }
}