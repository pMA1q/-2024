using UnityEngine;

public class CS_BossPhaseHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject mBossPhaseTitle;
    public void Initialize(CS_Controller controller)
    {
        // �{�X�t�F�[�Y������
        Debug.Log("�{�X�t�F�[�Y�Ɉڍs���܂���");
        if (controller != null)
        {
            controller.ChangePhase(CS_Controller.PACHINKO_PHESE.BOSS);
            Instantiate(mBossPhaseTitle, mBossPhaseTitle.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else
        {
            Debug.LogWarning("CS_Controller ��������܂���");
        }
    }
}
