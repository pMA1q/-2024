using System.Collections; // IEnumerator を使用するために必要
using UnityEngine;
using UnityEngine.SceneManagement; // シーン管理用

public class PhaseTransition : MonoBehaviour
{
    [SerializeField] private Animator fadeAnimator; // フェード演出用のAnimator
    [SerializeField] private float transitionDelay = 1.5f; // 遷移までの遅延時間
    [SerializeField] private CS_Controller bigController; // コントローラーへの参照

    // ボスフェーズへの移行
    public void StartBossPhase()
    {
        Debug.Log("ボスフェーズへ移行");
        StartCoroutine(TransitionToBossPhase());
    }

    private IEnumerator TransitionToBossPhase()
    {
        // フェードアウト開始
        if (fadeAnimator != null)
        {
            fadeAnimator.SetTrigger("FadeOut");
        }

        // 遅延
        yield return new WaitForSeconds(transitionDelay);

        // 不要なオブジェクトを整理
        Destroy(this.gameObject);

        // フェーズを変更
        bigController.ChangePhase(CS_Controller.PACHINKO_PHESE.BOSS);

        // シーン遷移（必要に応じて）
        // SceneManager.LoadScene("BossScene");
    }
}
