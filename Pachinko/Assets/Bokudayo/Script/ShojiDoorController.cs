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
    public GameObject backgroundObject; // ��q�̌��ɂ���I�u�W�F�N�g�i�F�ύX����Ώہj
    public float slideSpeed = 2.0f;  // �X���C�h���x
    public float openWidth = 2.0f;  // �J�����i�����\�j

    private Vector3 leftStartPos, rightStartPos;  // �����ʒu
    private bool isOpening = false;

    // �F�ƃ}�e���A�����܂Ƃ߂��ϐ�
    public MaterialWithColor closedState = new MaterialWithColor { color = Color.black, material = null }; // ���Ă��鎞�̐F�����ɐݒ�
    public MaterialWithColor backgroundClosedState = new MaterialWithColor { color = Color.black, material = null }; // �w�i�I�u�W�F�N�g���������̐F�����ɐݒ�
    public MaterialWithColor openingState = new MaterialWithColor { color = Color.yellow, material = null }; // �J���n�߂̎��̐F�ƃ}�e���A��

    private Renderer[] backgroundRenderers;  // �w�i�I�u�W�F�N�g��Renderer�z��
    private Renderer leftRenderer, rightRenderer;

    // ���̃}�e���A���ƐF��ێ�����ϐ�
    private Material originalLeftMaterial, originalRightMaterial;
    private Color originalLeftColor, originalRightColor;
    private Material originalBackgroundMaterial;
    private Color originalBackgroundColor;

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
            // �e�I�u�W�F�N�g�Ƃ��̎q�I�u�W�F�N�g���ׂĂ�Renderer���擾
            backgroundRenderers = backgroundObject.GetComponentsInChildren<Renderer>();
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

        if (backgroundRenderers != null && backgroundRenderers.Length > 0)
        {
            originalBackgroundMaterial = backgroundRenderers[0].material;
            originalBackgroundColor = backgroundRenderers[0].material.color;
        }

        // ������Ԃ̐F��ݒ�i���Ă��鎞�A���F�j
        SetShojiState(closedState);
        SetBackgroundState(backgroundClosedState);
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
            // �J���n�߂ɐF�������i�J���n�߂��^�C�~���O�Ō��̐F�ɖ߂��j
            if (leftRenderer != null && rightRenderer != null && backgroundRenderers != null)
            {
                // �����ŐF������
                SetShojiState(new MaterialWithColor { material = originalLeftMaterial, color = originalLeftColor });
                SetBackgroundState(new MaterialWithColor { material = originalBackgroundMaterial, color = originalBackgroundColor });
            }

            // ���������ɁA�E�����E�ɃX���C�h������
            leftShoji.transform.position = Vector3.Lerp(leftShoji.transform.position, leftStartPos - new Vector3(openWidth, 0, 0), slideSpeed * Time.deltaTime);
            rightShoji.transform.position = Vector3.Lerp(rightShoji.transform.position, rightStartPos + new Vector3(openWidth, 0, 0), slideSpeed * Time.deltaTime);

            // �J����������F��߂�
            if (Vector3.Distance(leftShoji.transform.position, leftStartPos - new Vector3(openWidth, 0, 0)) < 0.01f)
            {
                // �J�����������Ɍ��̐F�ɖ߂�
                SetShojiState(new MaterialWithColor { material = originalLeftMaterial, color = originalLeftColor });
                SetBackgroundState(new MaterialWithColor { material = originalBackgroundMaterial, color = originalBackgroundColor });
            }
        }
    }

    // �J����{�^����C�x���g�ɕR�Â��ČĂяo��
    public void OpenShoji()
    {
        // �F�����ɖ߂�
        SetShojiState(new MaterialWithColor { material = originalLeftMaterial, color = originalLeftColor });
        SetBackgroundState(new MaterialWithColor { material = originalBackgroundMaterial, color = originalBackgroundColor });

        isOpening = true;
    }

    // ����{�^����C�x���g�ɕR�Â��ČĂяo��
    public void CloseShoji()
    {
        isOpening = false;
        leftShoji.transform.position = leftStartPos;
        rightShoji.transform.position = rightStartPos;

        // ������Ԃ̐F�ƃ}�e���A����ݒ�
        SetShojiState(closedState);
        SetBackgroundState(backgroundClosedState);
    }

    // �J�����𒲐����郁�\�b�h
    public void SetOpenWidth(float width)
    {
        openWidth = width;
    }

    // ��q�̐F�ƃ}�e���A����ݒ肷�郁�\�b�h
    private void SetShojiState(MaterialWithColor state)
    {
        if (leftRenderer != null)
        {
            leftRenderer.material.color = state.color;  // �F��ݒ�
        }
        if (rightRenderer != null)
        {
            rightRenderer.material.color = state.color;  // �F��ݒ�
        }
    }

    // �w�i�I�u�W�F�N�g�̐F�ƃ}�e���A����ݒ肷�郁�\�b�h
    private void SetBackgroundState(MaterialWithColor state)
    {
        // �e�I�u�W�F�N�g�Ƃ��̎q�I�u�W�F�N�g���ׂĂ�Renderer�ɑ΂��ĐF�ƃ}�e���A����ݒ�
        if (backgroundRenderers != null)
        {
            foreach (Renderer renderer in backgroundRenderers)
            {
                if (renderer != null)
                {
                    renderer.material.color = state.color;  // �F��ݒ�
                }
            }
        }
    }
}
