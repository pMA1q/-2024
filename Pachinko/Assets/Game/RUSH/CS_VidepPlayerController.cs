using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CS_VidepPlayerController : MonoBehaviour
{
    public RawImage rawImage;             // �����\������RawImage
    public VideoPlayer videoPlayer;       // VideoPlayer�R���|�[�l���g
    public Material placeholderMaterial;  // �Đ��������ɕ\������}�e���A��
    public RenderTexture renderTexture;   // ����̏o�͐�RenderTexture

    private void Start()
    {
        if(rawImage.texture != null) { return; }
        // ������Ԃ�RawImage�Ƀ}�e���A����ݒ�
        if (placeholderMaterial != null)
        {
            rawImage.material = placeholderMaterial;
        }

        // RenderTexture��VideoPlayer�ɐݒ�
        videoPlayer.targetTexture = renderTexture;

        // �Đ��������J�n
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += OnPrepareCompleted;
    }

    private void OnPrepareCompleted(VideoPlayer vp)
    {
        // RawImage��RenderTexture��K�p
        rawImage.texture = renderTexture;
        rawImage.material = null; // Material���N���A���Ēʏ�̃e�N�X�`�����g�p

        // ����Đ��J�n
        videoPlayer.Play();
    }
}
