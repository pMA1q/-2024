using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_TitleFadeMission : MonoBehaviour
{
    public RawImage panelImage;        // �t�F�[�h�A�E�g�Ɏg�p����p�l���摜
    public Text panelText;             // �t�F�[�h�A�E�g�Ɏg�p����e�L�X�g
    public float fadeOutTime = 3.0f;   // �t�F�[�h�A�E�g����
    public CS_Controller bigController; // �R���g���[���[�ւ̎Q�Ɓi�t�F�[�Y�Ǘ��p�jCS_CommonData.BigControllerName
    public CS_Controller.PACHINKO_PHESE nextPhase; // �J�ڐ�̃t�F�[�Y���w��

    private void OnEnable()
    {
        bigController = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_Controller>();
        // �t�F�[�h�A�E�g�J�n
        StartCoroutine(FadeOutPanelAndTransition());
    }

    private IEnumerator FadeOutPanelAndTransition()
    {
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