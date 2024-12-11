using UnityEngine;

public class CS_BossPhaseHandler : MonoBehaviour
{
    public void Initialize(CS_Controller controller)
    {
        // ボスフェーズ初期化
        Debug.Log("ボスフェーズに移行しました");
        if (controller != null)
        {
            controller.ChangePhase(CS_Controller.PACHINKO_PHESE.BOSS);
        }
        else
        {
            Debug.LogWarning("CS_Controller が見つかりません");
        }
    }
}
