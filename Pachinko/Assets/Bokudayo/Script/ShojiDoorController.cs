using System.Collections;
using UnityEngine;

[System.Serializable]
public struct MaterialWithColor
{
    public Material material;  // �}�e���A��
    public Color color;       // �F
}

public class ShojiDoorController : MonoBehaviour
{
    public GameObject leftShoji;  // �����̏�q�I�u�W�F�N�g
    public GameObject rightShoji; // �E���̏�q�I�u�W�F�N�g
    public GameObject[] backgroundModels; // �����̔w�i�I�u�W�F�N�g���i�[����z��
    public GameObject backgroundObject; // ��q�̌��ɂ���I�u�W�F�N�g�i�F�ύX����Ώہj

    [Header("�X���C�h�ݒ�")]
    public float firstSlideSpeed = 2.0f;  // 1��ڂ̃X���C�h���x�i�����j
    public float secondSlideSpeed = 2.0f; // 2��ڂ̃X���C�h���x�i�����j
    public float firstOpenWidth = 1.0f;    // �ŏ��ɊJ����
    public float openWidth = 2.0f;         // ���S�ɊJ����
    public float firstOpenDuration = 1.0f; // 1��ڂ̃X���C�h�ɂ����鎞��

    [Header("�F�ݒ�")]
    public MaterialWithColor closedState = new MaterialWithColor { color = Color.black, material = null }; // ���Ă��鎞�̐F
    public MaterialWithColor openingState = new MaterialWithColor { color = Color.yellow, material = null }; // �J���n�߂̎��̐F
    public MaterialWithColor transparentState = new MaterialWithColor { color = new Color(1, 1, 1, 0), material = null }; // �����ȏ�Ԃ̐F

    private Vector3 leftStartPos, rightStartPos;  // �����ʒu
    private bool isOpening = false; // �J���Ă��邩�̃t���O
    private bool firstSlideComplete = false;  // �ŏ��̃X���C�h�������������ǂ����̃t���O
    private float firstSlideStartTime; // �ŏ��̃X���C�h�J�n����
    private float secondSlideStartTime; // 2��ڂ̃X���C�h�J�n����

    // �w�i�I�u�W�F�N�g��Renderer�z��
    private Renderer[] backgroundRenderers;
    private Renderer leftRenderer, rightRenderer;
    private Renderer[][] backgroundModelRenderers; // �����̔w�iModel��Renderer�z��

    // ���̃}�e���A���ƐF��ێ�����ϐ�
    private Material originalLeftMaterial, originalRightMaterial;
    private Color originalLeftColor, originalRightColor;
    private Material[] originalBackgroundModelMaterials;
    private Color[] originalBackgroundModelColors;
    private Material originalBackgroundObjectMaterial;
    private Color originalBackgroundObjectColor;

    // �J�����p�̕ϐ�
    private Vector3 originalCameraPosition;
    public float cameraCloseDistance = -50.0f; // ��q���J�����Ƃ��ɃJ�������߂Â�����
    public float cameraMoveSpeed = 4.0f; // �J�����ړ��̃X�s�[�h

    void Start()
    {
        // �����ʒu��ۑ�
        leftStartPos = leftShoji.transform.position;
        rightStartPos = rightShoji.transform.position;

        // Renderer�R���|�[�l���g���擾
        leftRenderer = leftShoji.GetComponent<Renderer>();
        rightRenderer = rightShoji.GetComponent<Renderer>();

        // �w�i�I�u�W�F�N�g��Renderer���擾
        if (backgroundObject != null)
        {
            backgroundRenderers = backgroundObject.GetComponentsInChildren<Renderer>();
        }

        if (backgroundModels != null && backgroundModels.Length > 0)
        {
            // �e�w�i���f����Renderer���擾
            backgroundModelRenderers = new Renderer[backgroundModels.Length][];
            for (int i = 0; i < backgroundModels.Length; i++)
            {
                backgroundModelRenderers[i] = backgroundModels[i].GetComponentsInChildren<Renderer>();
            }
        }

        // ���̐F�ƃ}�e���A����ێ�
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

        // ������Ԃ̐F��ݒ�i���Ă��鎞�A���F�j
        SetShojiState(closedState);
        SetBackgroundObjectState(new MaterialWithColor { color = Color.black, material = null }); // �w�iObject�����ɐݒ�
        SetBackgroundModelState(new MaterialWithColor { color = new Color(0, 0, 0, 0), material = null }); // �w�iModel�𓧖��ɐݒ�

        // �J�����̌��̈ʒu��ۑ�
        originalCameraPosition = Camera.main.transform.position;
    }

    void Update()
    {
        // �X�y�[�X�L�[�������ꂽ�ꍇ�ɏ�q���J��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OpenShoji();
        }

        // ���E�ɃX���C�h���ĊJ���鏈��
        if (isOpening)
        {
            if (!firstSlideComplete)
            {
                // 1��ڂ̃X���C�h����
                float elapsedTime = Time.time - firstSlideStartTime;
                float lerpFactor = Mathf.Clamp01(elapsedTime / firstOpenDuration);  // 1��ڂ̃X���C�h�ɂ����鎞�Ԃŕ��

                // 1��ڂ̃X���C�h���x��K�p
                leftShoji.transform.position = Vector3.Lerp(leftShoji.transform.position, leftStartPos - new Vector3(firstOpenWidth, 0, 0), lerpFactor * firstSlideSpeed);
                rightShoji.transform.position = Vector3.Lerp(rightShoji.transform.position, rightStartPos + new Vector3(firstOpenWidth, 0, 0), lerpFactor * firstSlideSpeed);

                // �ŏ��̃X���C�h���I�������A�F�����ɖ߂��A2��ڂ̃X���C�h�J�n
                if (lerpFactor >= 1.0f)
                {
                    // �ŏ��̃X���C�h�������ɔw�i�𔒂��ς��鏈�����J�n
                    SetBackgroundModelState(new MaterialWithColor { color = Color.white, material = null });
                    SetBackgroundObjectState(new MaterialWithColor { color = new Color(1, 1, 1, 0.5f), material = null }); // �������ɐݒ�

                    firstSlideComplete = true;
                    secondSlideStartTime = Time.time; // 2��ڂ̃X���C�h�̊J�n���Ԃ��L�^

                    // �J�������߂Â���
                    StartCoroutine(MoveCameraCloser());
                }
            }
            else
            {
                // 2��ڂ̃X���C�h����
                float elapsedTime = Time.time - secondSlideStartTime;
                float lerpFactor = Mathf.Clamp01(elapsedTime / firstOpenDuration);  // 2��ڂ̃X���C�h�ɂ����鎞�Ԃŕ��

                // 2��ڂ̃X���C�h���x��K�p
                leftShoji.transform.position = Vector3.Lerp(leftShoji.transform.position, leftStartPos - new Vector3(openWidth, 0, 0), lerpFactor * secondSlideSpeed);
                rightShoji.transform.position = Vector3.Lerp(rightShoji.transform.position, rightStartPos + new Vector3(openWidth, 0, 0), lerpFactor * secondSlideSpeed);

                // ���S�ɊJ������F�����ɖ߂�
                if (lerpFactor >= 1.0f)
                {
                    SetShojiState(new MaterialWithColor { material = originalLeftMaterial, color = originalLeftColor });
                    SetBackgroundModelState(new MaterialWithColor { color = Color.white, material = null }); // �w�iModel�����S�ɔ���
                    SetBackgroundObjectState(new MaterialWithColor { color = originalBackgroundObjectColor, material = originalBackgroundObjectMaterial }); // �w�iObject�����ɖ߂�

                    // �J���������̈ʒu�ɖ߂�
                    StartCoroutine(MoveCameraBack());
                }
            }
        }
    }

    // �J�������߂Â���
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

        Camera.main.transform.position = targetPosition;  // ���m�Ȉʒu�ɓ��B
    }

    // �J���������̈ʒu�ɖ߂�
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

        Camera.main.transform.position = originalCameraPosition;  // ���m�Ȉʒu�ɓ��B
    }

    // �J����{�^����C�x���g�ɕR�Â��ČĂяo��
    public void OpenShoji()
    {
        // �F��ύX���ĊJ����
        SetShojiState(openingState);
        SetBackgroundObjectState(openingState);

        // �J��������J�n
        isOpening = true;
        firstSlideComplete = false;
        firstSlideStartTime = Time.time; // �ŏ��̃X���C�h�J�n���Ԃ��L�^
    }

    // ��q�̐F�ƃ}�e���A����ݒ肷�郁�\�b�h
    private void SetShojiState(MaterialWithColor state)
    {
        if (leftRenderer != null)
        {
            leftRenderer.material.color = state.color;
            leftRenderer.material.SetFloat("_Mode", 3); // Transparent ���[�h�ɐݒ�
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
            rightRenderer.material.SetFloat("_Mode", 3); // Transparent ���[�h�ɐݒ�
            rightRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            rightRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            rightRenderer.material.SetInt("_ZWrite", 0);
            rightRenderer.material.DisableKeyword("_ALPHATEST_ON");
            rightRenderer.material.EnableKeyword("_ALPHABLEND_ON");
            rightRenderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        }
    }

    // �w�iModel�̐F�ƃ}�e���A����ݒ肷�郁�\�b�h
    private void SetBackgroundModelState(MaterialWithColor state)
    {
        if (backgroundModelRenderers != null)
        {
            for (int i = 0; i < backgroundModelRenderers.Length; i++)
            {
                for (int j = 0; j < backgroundModelRenderers[i].Length; j++)
                {
                    backgroundModelRenderers[i][j].material.color = state.color;
                    backgroundModelRenderers[i][j].material.SetFloat("_Mode", 3); // Transparent ���[�h�ɐݒ�
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

    // �w�iObject�̐F�ƃ}�e���A����ݒ肷�郁�\�b�h
    private void SetBackgroundObjectState(MaterialWithColor state)
    {
        if (backgroundRenderers != null)
        {
            foreach (var renderer in backgroundRenderers)
            {
                if (renderer != null)
                {
                    renderer.material.color = state.color;
                    renderer.material.SetFloat("_Mode", 3); // Transparent ���[�h�ɐݒ�
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
