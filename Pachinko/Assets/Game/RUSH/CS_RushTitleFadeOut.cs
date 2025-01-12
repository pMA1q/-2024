using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_RushTitleFadeOut : MonoBehaviour
{
    public RawImage panelImage;        // �t�F�[�h�A�E�g�Ɏg�p����p�l���摜
    public Image panelText;             // �t�F�[�h�A�E�g�Ɏg�p����e�L�X�g
    public float fadeOutTime = 3.0f;   // �t�F�[�h�A�E�g����

    private void OnEnable()
    {
        // �t�F�[�h�A�E�g�J�n
        StartCoroutine(FadeOutPanelAndTransition());
    }

    private IEnumerator FadeOutPanelAndTransition()
    {
        yield return new WaitForSeconds(3.0f);
        // �F���擾
        Color startColor = panelImage.color;
        Color textColor = panelText.color;

        // �A���t�@�l�������x���v�ZCS_CommonData.BigControllerName
        float alphaPerSecondPanel = startColor.a / fadeOutTime;
        float alphaPerSecondText = textColor.a / fadeOutTime;

        // �t�F�[�h�A�E�g����
        while (startColor.a > 0)
        {
            startColor.a -= alphaPerSecondPanel * Time.deltaTime;
            textColor.a -= alphaPerSecondText * Time.deltaTime;

            panelImage.color = startColor;
            panelText.color = textColor;

            yield return null;
        }

        // �t�F�[�h�A�E�g������������I�u�W�F�N�g������
        GameObject rootObject = transform.root.gameObject;
        Destroy(rootObject);
    }
}
