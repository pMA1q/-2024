using UnityEngine;

public class BackgroundObjectTransparency : MonoBehaviour
{
    private Renderer[] backgroundRenderers;  // ��q�w�i�I�u�W�F�N�g�̃����_���[�z��

    void Start()
    {
        // �w�i�I�u�W�F�N�g�̃����_���[���擾
        backgroundRenderers = GetComponentsInChildren<Renderer>();

        // �w�i�I�u�W�F�N�g�𓧖��ɐݒ�
        SetObjectTransparency(new Color(1, 1, 1, 0));  // ���S����
    }

    // �w�i�I�u�W�F�N�g�̓����x��ݒ肷�郁�\�b�h
    public void SetObjectTransparency(Color transparentColor)
    {
        if (backgroundRenderers != null)
        {
            foreach (var renderer in backgroundRenderers)
            {
                if (renderer != null)
                {
                    // �����ɂ��邽�߂̐ݒ�
                    var material = renderer.material;
                    material.SetFloat("_Mode", 3);  // �������[�h�ɐݒ�
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.EnableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = 3000;

                    // �F�𓧖��ɐݒ�
                    renderer.material.color = transparentColor;
                }
            }
        }
    }

    // ���̃X�N���v�g���瓧���x��ύX���邽�߂̃��\�b�h
    public void MakeObjectTransparent()
    {
        SetObjectTransparency(new Color(1, 1, 1, 0));  // ���S�����ɂ���
    }

    // ���̃X�N���v�g����w�i�I�u�W�F�N�g�����ɖ߂��i�����x�� 1 �ɐݒ�j
    public void ResetObjectTransparency()
    {
        SetObjectTransparency(new Color(1, 1, 1, 1));  // ���S�s�����ɖ߂�
    }
}
