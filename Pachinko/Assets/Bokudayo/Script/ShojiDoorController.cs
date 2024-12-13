using System.Collections;
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
    public GameObject[] backgroundModels; // 複数の背景オブジェクトを格納する配列
    public GameObject backgroundObject; // 障子の後ろにあるオブジェクト（色変更する対象）

    [Header("スライド設定")]
    public float firstSlideSpeed = 2.0f;  // 1回目のスライド速度（速さ）
    public float secondSlideSpeed = 2.0f; // 2回目のスライド速度（速さ）
    public float firstOpenWidth = 1.0f;    // 最初に開く幅
    public float openWidth = 2.0f;         // 完全に開く幅
    public float firstOpenDuration = 1.0f; // 1回目のスライドにかける時間

    [Header("色設定")]
    public MaterialWithColor closedState = new MaterialWithColor { color = Color.black, material = null }; // 閉じている時の色
    public MaterialWithColor openingState = new MaterialWithColor { color = Color.yellow, material = null }; // 開き始めの時の色
    public MaterialWithColor transparentState = new MaterialWithColor { color = new Color(1, 1, 1, 0), material = null }; // 透明な状態の色

    private Vector3 leftStartPos, rightStartPos;  // 初期位置
    private bool isOpening = false; // 開いているかのフラグ
    private bool firstSlideComplete = false;  // 最初のスライドが完了したかどうかのフラグ
    private float firstSlideStartTime; // 最初のスライド開始時間
    private float secondSlideStartTime; // 2回目のスライド開始時間

    // 背景オブジェクトのRenderer配列
    private Renderer[] backgroundRenderers;
    private Renderer leftRenderer, rightRenderer;
    private Renderer[][] backgroundModelRenderers; // 複数の背景ModelのRenderer配列

    // 元のマテリアルと色を保持する変数
    private Material originalLeftMaterial, originalRightMaterial;
    private Color originalLeftColor, originalRightColor;
    private Material[] originalBackgroundModelMaterials;
    private Color[] originalBackgroundModelColors;
    private Material originalBackgroundObjectMaterial;
    private Color originalBackgroundObjectColor;

    // カメラ用の変数
    private Vector3 originalCameraPosition;
    public float cameraCloseDistance = -50.0f; // 障子が開いたときにカメラが近づく距離
    public float cameraMoveSpeed = 4.0f; // カメラ移動のスピード

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
            backgroundRenderers = backgroundObject.GetComponentsInChildren<Renderer>();
        }

        if (backgroundModels != null && backgroundModels.Length > 0)
        {
            // 各背景モデルのRendererを取得
            backgroundModelRenderers = new Renderer[backgroundModels.Length][];
            for (int i = 0; i < backgroundModels.Length; i++)
            {
                backgroundModelRenderers[i] = backgroundModels[i].GetComponentsInChildren<Renderer>();
            }
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

        if (backgroundModelRenderers != null)
        {
            originalBackgroundModelMaterials = new Material[backgroundModelRenderers.Length];
            originalBackgroundModelColors = new Color[backgroundModelRenderers.Length];

            for (int i = 0; i < backgroundModelRenderers.Length; i++)
            {
                for (int j = 0; j < backgroundModelRenderers[i].Length; j++)
                {
                    originalBackgroundModelMaterials[i] = backgroundModelRenderers[i][j].material;
                    originalBackgroundModelColors[i] = backgroundModelRenderers[i][j].material.color;
                }
            }
        }

        if (backgroundRenderers != null && backgroundRenderers.Length > 0)
        {
            originalBackgroundObjectMaterial = backgroundRenderers[0].material;
            originalBackgroundObjectColor = backgroundRenderers[0].material.color;
        }

        // 初期状態の色を設定（閉じている時、黒色）
        SetShojiState(closedState);
        SetBackgroundObjectState(new MaterialWithColor { color = Color.black, material = null }); // 背景Objectを黒に設定
        SetBackgroundModelState(new MaterialWithColor { color = new Color(0, 0, 0, 0), material = null }); // 背景Modelを透明に設定

        // カメラの元の位置を保存
        originalCameraPosition = Camera.main.transform.position;
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
            if (!firstSlideComplete)
            {
                // 1回目のスライド処理
                float elapsedTime = Time.time - firstSlideStartTime;
                float lerpFactor = Mathf.Clamp01(elapsedTime / firstOpenDuration);  // 1回目のスライドにかける時間で補間

                // 1回目のスライド速度を適用
                leftShoji.transform.position = Vector3.Lerp(leftShoji.transform.position, leftStartPos - new Vector3(firstOpenWidth, 0, 0), lerpFactor * firstSlideSpeed);
                rightShoji.transform.position = Vector3.Lerp(rightShoji.transform.position, rightStartPos + new Vector3(firstOpenWidth, 0, 0), lerpFactor * firstSlideSpeed);

                // 最初のスライドが終わったら、色を元に戻し、2回目のスライド開始
                if (lerpFactor >= 1.0f)
                {
                    // 最初のスライド完了時に背景を白く変える処理を開始
                    SetBackgroundModelState(new MaterialWithColor { color = Color.white, material = null });
                    SetBackgroundObjectState(new MaterialWithColor { color = new Color(1, 1, 1, 0.5f), material = null }); // 半透明に設定

                    firstSlideComplete = true;
                    secondSlideStartTime = Time.time; // 2回目のスライドの開始時間を記録

                    // カメラを近づける
                    StartCoroutine(MoveCameraCloser());
                }
            }
            else
            {
                // 2回目のスライド処理
                float elapsedTime = Time.time - secondSlideStartTime;
                float lerpFactor = Mathf.Clamp01(elapsedTime / firstOpenDuration);  // 2回目のスライドにかける時間で補間

                // 2回目のスライド速度を適用
                leftShoji.transform.position = Vector3.Lerp(leftShoji.transform.position, leftStartPos - new Vector3(openWidth, 0, 0), lerpFactor * secondSlideSpeed);
                rightShoji.transform.position = Vector3.Lerp(rightShoji.transform.position, rightStartPos + new Vector3(openWidth, 0, 0), lerpFactor * secondSlideSpeed);

                // 完全に開いたら色を元に戻す
                if (lerpFactor >= 1.0f)
                {
                    SetShojiState(new MaterialWithColor { material = originalLeftMaterial, color = originalLeftColor });
                    SetBackgroundModelState(new MaterialWithColor { color = Color.white, material = null }); // 背景Modelを完全に白に
                    SetBackgroundObjectState(new MaterialWithColor { color = originalBackgroundObjectColor, material = originalBackgroundObjectMaterial }); // 背景Objectを元に戻す

                    // カメラを元の位置に戻す
                    StartCoroutine(MoveCameraBack());
                }
            }
        }
    }

    // カメラを近づける
    private IEnumerator MoveCameraCloser()
    {
        Vector3 targetPosition = originalCameraPosition - Camera.main.transform.forward * cameraCloseDistance;
        float journeyLength = Vector3.Distance(Camera.main.transform.position, targetPosition);
        float startTime = Time.time;

        while (Vector3.Distance(Camera.main.transform.position, targetPosition) > 0.1f)
        {
            float distanceCovered = (Time.time - startTime) * cameraMoveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPosition, fractionOfJourney);
            yield return null;
        }

        Camera.main.transform.position = targetPosition;  // 正確な位置に到達
    }

    // カメラを元の位置に戻す
    private IEnumerator MoveCameraBack()
    {
        float journeyLength = Vector3.Distance(Camera.main.transform.position, originalCameraPosition);
        float startTime = Time.time;

        while (Vector3.Distance(Camera.main.transform.position, originalCameraPosition) > 0.1f)
        {
            float distanceCovered = (Time.time - startTime) * cameraMoveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, originalCameraPosition, fractionOfJourney);
            yield return null;
        }

        Camera.main.transform.position = originalCameraPosition;  // 正確な位置に到達
    }

    // 開けるボタンやイベントに紐づけて呼び出す
    public void OpenShoji()
    {
        // 色を変更して開ける
        SetShojiState(openingState);
        SetBackgroundObjectState(openingState);

        // 開く動作を開始
        isOpening = true;
        firstSlideComplete = false;
        firstSlideStartTime = Time.time; // 最初のスライド開始時間を記録
    }

    // 障子の色とマテリアルを設定するメソッド
    private void SetShojiState(MaterialWithColor state)
    {
        if (leftRenderer != null)
        {
            leftRenderer.material.color = state.color;
            leftRenderer.material.SetFloat("_Mode", 3); // Transparent モードに設定
            leftRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            leftRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            leftRenderer.material.SetInt("_ZWrite", 0);
            leftRenderer.material.DisableKeyword("_ALPHATEST_ON");
            leftRenderer.material.EnableKeyword("_ALPHABLEND_ON");
            leftRenderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        }

        if (rightRenderer != null)
        {
            rightRenderer.material.color = state.color;
            rightRenderer.material.SetFloat("_Mode", 3); // Transparent モードに設定
            rightRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            rightRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            rightRenderer.material.SetInt("_ZWrite", 0);
            rightRenderer.material.DisableKeyword("_ALPHATEST_ON");
            rightRenderer.material.EnableKeyword("_ALPHABLEND_ON");
            rightRenderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        }
    }

    // 背景Modelの色とマテリアルを設定するメソッド
    private void SetBackgroundModelState(MaterialWithColor state)
    {
        if (backgroundModelRenderers != null)
        {
            for (int i = 0; i < backgroundModelRenderers.Length; i++)
            {
                for (int j = 0; j < backgroundModelRenderers[i].Length; j++)
                {
                    backgroundModelRenderers[i][j].material.color = state.color;
                    backgroundModelRenderers[i][j].material.SetFloat("_Mode", 3); // Transparent モードに設定
                    backgroundModelRenderers[i][j].material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    backgroundModelRenderers[i][j].material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    backgroundModelRenderers[i][j].material.SetInt("_ZWrite", 0);
                    backgroundModelRenderers[i][j].material.DisableKeyword("_ALPHATEST_ON");
                    backgroundModelRenderers[i][j].material.EnableKeyword("_ALPHABLEND_ON");
                    backgroundModelRenderers[i][j].material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                }
            }
        }
    }

    // 背景Objectの色とマテリアルを設定するメソッド
    private void SetBackgroundObjectState(MaterialWithColor state)
    {
        if (backgroundRenderers != null)
        {
            foreach (var renderer in backgroundRenderers)
            {
                if (renderer != null)
                {
                    renderer.material.color = state.color;
                    renderer.material.SetFloat("_Mode", 3); // Transparent モードに設定
                    renderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    renderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    renderer.material.SetInt("_ZWrite", 0);
                    renderer.material.DisableKeyword("_ALPHATEST_ON");
                    renderer.material.EnableKeyword("_ALPHABLEND_ON");
                    renderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                }
            }
        }
    }
}
