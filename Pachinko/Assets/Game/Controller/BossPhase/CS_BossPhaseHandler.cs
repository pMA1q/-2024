using UnityEngine;

public class CS_BossPhaseHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject mBossPhaseTitle;
    public void Initialize(CS_Controller controller)
    {
        // ボスフェーズ初期化
        Debug.Log("ボスフェーズに移行しました");
        if (controller != null)
        {
            controller.ChangePhase(CS_Controller.PACHINKO_PHESE.BOSS);
            Instantiate(mBossPhaseTitle, mBossPhaseTitle.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else
        {
            Debug.LogWarning("CS_Controller が見つかりません");
        }
    }
}
