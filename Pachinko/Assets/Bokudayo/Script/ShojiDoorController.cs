using UnityEngine;

[System.Serializable]
public struct MaterialWithColor
{
    public Material material;  // マテリアル
    public Color color;       // 色
}

public class ShojiDoorController : MonoBehaviour
{
    public GameObject leftShoji;  // 左側の障子オブジェクト
    public GameObject rightShoji; // 右側の障子オブジェクト
    public GameObject backgroundObject; // 障子の後ろにあるオブジェクト（色変更する対象）
    public float slideSpeed = 2.0f;  // スライド速度
    public float openWidth = 2.0f;  // 開く幅（調整可能）

    private Vector3 leftStartPos, rightStartPos;  // 初期位置
    private bool isOpening = false;

    // 色とマテリアルをまとめた変数
    public MaterialWithColor closedState = new MaterialWithColor { color = Color.black, material = null }; // 閉じている時の色を黒に設定
    public MaterialWithColor backgroundClosedState = new MaterialWithColor { color = Color.black, material = null }; // 背景オブジェクトが閉じた時の色を黒に設定
    public MaterialWithColor openingState = new MaterialWithColor { color = Color.yellow, material = null }; // 開き始めの時の色とマテリアル

    private Renderer[] backgroundRenderers;  // 背景オブジェクトのRenderer配列
    private Renderer leftRenderer, rightRenderer;

    // 元のマテリアルと色を保持する変数
    private Material originalLeftMaterial, originalRightMaterial;
    private Color originalLeftColor, originalRightColor;
    private Material originalBackgroundMaterial;
    private Color originalBackgroundColor;

    void Start()
    {
        // 初期位置を保存
        leftStartPos = leftShoji.transform.position;
        rightStartPos = rightShoji.transform.position;

        // Rendererコンポーネントを取得
        leftRenderer = leftShoji.GetComponent<Renderer>();
        rightRenderer = rightShoji.GetComponent<Renderer>();

        // 背景オブジェクトのRendererを取得
        if (backgroundObject != null)
        {
            // 親オブジェクトとその子オブジェクトすべてのRendererを取得
            backgroundRenderers = backgroundObject.GetComponentsInChildren<Renderer>();
        }

        // 元の色とマテリアルを保持
        if (leftRenderer != null)
        {
            originalLeftMaterial = leftRenderer.material;
            originalLeftColor = leftRenderer.material.color;
        }
        if (rightRenderer != null)
        {
            originalRightMaterial = rightRenderer.material;
            originalRightColor = rightRenderer.material.color;
        }

        if (backgroundRenderers != null && backgroundRenderers.Length > 0)
        {
            originalBackgroundMaterial = backgroundRenderers[0].material;
            originalBackgroundColor = backgroundRenderers[0].material.color;
        }

        // 初期状態の色を設定（閉じている時、黒色）
        SetShojiState(closedState);
        SetBackgroundState(backgroundClosedState);
    }

    void Update()
    {
        // スペースキーが押された場合に障子を開く
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OpenShoji();
        }

        // 左右にスライドして開ける処理
        if (isOpening)
        {
            // 開き始めに色を解除（開き始めたタイミングで元の色に戻す）
            if (leftRenderer != null && rightRenderer != null && backgroundRenderers != null)
            {
                // ここで色を解除
                SetShojiState(new MaterialWithColor { material = originalLeftMaterial, color = originalLeftColor });
                SetBackgroundState(new MaterialWithColor { material = originalBackgroundMaterial, color = originalBackgroundColor });
            }

            // 左側を左に、右側を右にスライドさせる
            leftShoji.transform.position = Vector3.Lerp(leftShoji.transform.position, leftStartPos - new Vector3(openWidth, 0, 0), slideSpeed * Time.deltaTime);
            rightShoji.transform.position = Vector3.Lerp(rightShoji.transform.position, rightStartPos + new Vector3(openWidth, 0, 0), slideSpeed * Time.deltaTime);

            // 開ききったら色を戻す
            if (Vector3.Distance(leftShoji.transform.position, leftStartPos - new Vector3(openWidth, 0, 0)) < 0.01f)
            {
                // 開ききった時に元の色に戻す
                SetShojiState(new MaterialWithColor { material = originalLeftMaterial, color = originalLeftColor });
                SetBackgroundState(new MaterialWithColor { material = originalBackgroundMaterial, color = originalBackgroundColor });
            }
        }
    }

    // 開けるボタンやイベントに紐づけて呼び出す
    public void OpenShoji()
    {
        // 色を元に戻す
        SetShojiState(new MaterialWithColor { material = originalLeftMaterial, color = originalLeftColor });
        SetBackgroundState(new MaterialWithColor { material = originalBackgroundMaterial, color = originalBackgroundColor });

        isOpening = true;
    }

    // 閉じるボタンやイベントに紐づけて呼び出す
    public void CloseShoji()
    {
        isOpening = false;
        leftShoji.transform.position = leftStartPos;
        rightShoji.transform.position = rightStartPos;

        // 閉じた状態の色とマテリアルを設定
        SetShojiState(closedState);
        SetBackgroundState(backgroundClosedState);
    }

    // 開く幅を調整するメソッド
    public void SetOpenWidth(float width)
    {
        openWidth = width;
    }

    // 障子の色とマテリアルを設定するメソッド
    private void SetShojiState(MaterialWithColor state)
    {
        if (leftRenderer != null)
        {
            leftRenderer.material.color = state.color;  // 色を設定
        }
        if (rightRenderer != null)
        {
            rightRenderer.material.color = state.color;  // 色を設定
        }
    }

    // 背景オブジェクトの色とマテリアルを設定するメソッド
    private void SetBackgroundState(MaterialWithColor state)
    {
        // 親オブジェクトとその子オブジェクトすべてのRendererに対して色とマテリアルを設定
        if (backgroundRenderers != null)
        {
            foreach (Renderer renderer in backgroundRenderers)
            {
                if (renderer != null)
                {
                    renderer.material.color = state.color;  // 色を設定
                }
            }
        }
    }
}
