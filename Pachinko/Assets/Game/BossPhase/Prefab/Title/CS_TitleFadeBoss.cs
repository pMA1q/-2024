using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CS_TitleFadeBoss : MonoBehaviour
{
    
    public float fadeOutTime = 3.0f;   // フェードアウト時間
    private CS_Controller bigController; // コントローラーへの参照（フェーズ管理用）
    public CS_Controller.PACHINKO_PHESE nextPhase; // 遷移先のフェーズを指定

    private void OnEnable()
    {
        bigController = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_Controller>();
        // フェードアウト開始
        StartCoroutine(FadeOutPanelAndTransition());
    }

    private IEnumerator FadeOutPanelAndTransition()
    {
        yield return new WaitForSeconds(fadeOutTime);

        // フェーズを指定されたフェーズに変更
        if (bigController != null)
        {
            bigController.ChangePhase(CS_Controller.PACHINKO_PHESE.BOSS);
            bigController.CreateController();
            Debug.Log($"{nextPhase} フェーズへ移行しました");
        }
        else
        {
            Debug.LogWarning("CS_Controller が設定されていません");
        }
        // フェードアウトが完了したらオブジェクトを消去
        GameObject rootObject = transform.root.gameObject;
        Destroy(rootObject);


       
    }
}
