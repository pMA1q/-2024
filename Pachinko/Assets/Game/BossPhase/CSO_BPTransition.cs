using System.Collections; // IEnumerator ���g�p���邽�߂ɕK�v
using UnityEngine;
using UnityEngine.SceneManagement; // �V�[���Ǘ��p

public class PhaseTransition : MonoBehaviour
{
    [SerializeField] private Animator fadeAnimator; // �t�F�[�h���o�p��Animator
    [SerializeField] private float transitionDelay = 1.5f; // �J�ڂ܂ł̒x������
    [SerializeField] private CS_Controller bigController; // �R���g���[���[�ւ̎Q��

    // �{�X�t�F�[�Y�ւ̈ڍs
    public void StartBossPhase()
    {
        Debug.Log("�{�X�t�F�[�Y�ֈڍs");
        StartCoroutine(TransitionToBossPhase());
    }

    private IEnumerator TransitionToBossPhase()
    {
        // �t�F�[�h�A�E�g�J�n
        if (fadeAnimator != null)
        {
            fadeAnimator.SetTrigger("FadeOut");
        }

        // �x��
        yield return new WaitForSeconds(transitionDelay);

        // �s�v�ȃI�u�W�F�N�g�𐮗�
        Destroy(this.gameObject);

        // �t�F�[�Y��ύX
        bigController.ChangePhase(CS_Controller.PACHINKO_PHESE.BOSS);

        // �V�[���J�ځi�K�v�ɉ����āj
        // SceneManager.LoadScene("BossScene");
    }
}
