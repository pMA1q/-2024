using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CS_TitleFadeBoss : MonoBehaviour
{
    
    public float fadeOutTime = 3.0f;   // �t�F�[�h�A�E�g����
    private CS_Controller bigController; // �R���g���[���[�ւ̎Q�Ɓi�t�F�[�Y�Ǘ��p�j
    public CS_Controller.PACHINKO_PHESE nextPhase; // �J�ڐ�̃t�F�[�Y���w��

    private void OnEnable()
    {
        bigController = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_Controller>();
        // �t�F�[�h�A�E�g�J�n
        StartCoroutine(FadeOutPanelAndTransition());
    }

    private IEnumerator FadeOutPanelAndTransition()
    {
        yield return new WaitForSeconds(fadeOutTime);

        // �t�F�[�Y���w�肳�ꂽ�t�F�[�Y�ɕύX
        if (bigController != null)
        {
            bigController.ChangePhase(CS_Controller.PACHINKO_PHESE.BOSS);
            bigController.CreateController();
            Debug.Log($"{nextPhase} �t�F�[�Y�ֈڍs���܂���");
        }
        else
        {
            Debug.LogWarning("CS_Controller ���ݒ肳��Ă��܂���");
        }
        // �t�F�[�h�A�E�g������������I�u�W�F�N�g������
        GameObject rootObject = transform.root.gameObject;
        Destroy(rootObject);


       
    }
}
