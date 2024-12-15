using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_QuadEffectAnimation : MonoBehaviour
{
    [SerializeField,Header("�A�j���[�V��������摜�f�[�^")]
    private Sprite[] sprites; // �A�j���[�V�����p�X�v���C�g�̃��X�g

    [SerializeField,Header("�t���[���Ԃ̎��ԁi�b�j")]
    private float frameRate = 0.05f; // �t���[���Ԃ̎��ԁi�b�j

    private Material[] quadMaterial; // Quad�̃}�e���A��
    private int currentFrame = 0; // ���݂̃t���[���ԍ�
    private float timer = 0f; // �o�ߎ���

    void Start()
    {
        // Quad��Renderer�R���|�[�l���g����}�e���A�����擾
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            quadMaterial = renderer.materials;
        }
        else
        {
            Debug.LogError("Renderer��������܂���I");
        }
    }

    private void OnEnable()
    {
        currentFrame = 0;
    }

    void Update()
    {
        if (quadMaterial == null || sprites == null || sprites.Length == 0) return;

        // �A�j���[�V�����^�C�~���O
        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            // ���̃t���[���ɐi��
            currentFrame = (currentFrame + 1) % sprites.Length;

            // �X�v���C�g���e�N�X�`���Ƃ��ă}�e���A���ɐݒ�
            quadMaterial[0].mainTexture = sprites[currentFrame].texture;

            timer = 0f; // �^�C�}�[�����Z�b�g
        }
    }
}
