using UnityEngine;

public class CS_BossPhaseHandler : MonoBehaviour
{
    public void Initialize(CS_Controller controller)
    {
        // �{�X�t�F�[�Y������
        Debug.Log("�{�X�t�F�[�Y�Ɉڍs���܂���");
        if (controller != null)
        {
            controller.ChangePhase(CS_Controller.PACHINKO_PHESE.BOSS);
        }
        else
        {
            Debug.LogWarning("CS_Controller ��������܂���");
        }
    }
}
