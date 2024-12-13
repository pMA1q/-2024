                                                                                                                                                      using UnityEngine;

public class TransparencyController : MonoBehaviour
{
    private Renderer objectRenderer;  // �I�u�W�F�N�g��Renderer
    private Material objectMaterial;  // �I�u�W�F�N�g�̃}�e���A��

    [Range(0f, 1f)]  // �X���C�_�[�œ����x�𒲐��ł���悤�ɐݒ�i0: ���S�ɓ���, 1: ���S�ɕs�����j
    public float transparency = 1f;

    void Start()
    {
        // �I�u�W�F�N�g��Renderer���擾
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer != null)
        {
            // �I�u�W�F�N�g�̃}�e���A�����擾
            objectMaterial = objectRenderer.material;
        }
    }

    void Update()
    {
        if (objectMaterial != null)
        {
            // ���݂̓����x�Ɋ�Â��ĐF��ύX
            Color currentColor = objectMaterial.GetColor("_Color");  // ���݂̐F���擾
            currentColor.a = transparency;  // �����x��ݒ�
            objectMaterial.SetColor("_Color", currentColor);  // �V�����F��ݒ�
        }
    }
}
