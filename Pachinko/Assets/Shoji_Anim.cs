using UnityEngine;

public class ShojiAnimController : MonoBehaviour
{
    public Animator shojiAnimator; // 障子のAnimator
    public Animator cameraAnimator; // カメラのAnimator
    public Animator brightenAnimator; // 明転用のAnimator
    public GameObject enemy; // 敵オブジェクト
    public Material enemyShadowMaterial; // 影状態のMaterial
    public Material enemyVisibleMaterial; // 表示状態のMaterial

    void Start()
    {
        // 初期化：敵を影状態に設定
        enemy.GetComponent<Renderer>().material = enemyShadowMaterial;
    }

    public void StartScene()
    {
        StartCoroutine(PlayScene());
    }

    private IEnumerator PlayScene()
    {
        // 障子少し開く
        shojiAnimator.SetTrigger("OpenSlightly");
        cameraAnimator.SetTrigger("ZoomIn");
        yield return new WaitForSeconds(1.5f); // 少し開くアニメーションの長さ

        // 障子全開
        shojiAnimator.SetTrigger("OpenFully");
        yield return new WaitForSeconds(1.5f); // 全開アニメーションの長さ

        // 明転
        brightenAnimator.SetTrigger("Brighten");
        yield return new WaitForSeconds(2f);

        // 敵の表示を切り替え
        enemy.GetComponent<Renderer>().material = enemyVisibleMaterial;
    }
}
