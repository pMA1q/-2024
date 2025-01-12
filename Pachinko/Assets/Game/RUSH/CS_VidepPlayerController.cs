using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CS_VidepPlayerController : MonoBehaviour
{
    public RawImage rawImage;             // 動画を表示するRawImage
    public VideoPlayer videoPlayer;       // VideoPlayerコンポーネント
    public Material placeholderMaterial;  // 再生準備中に表示するマテリアル
    public RenderTexture renderTexture;   // 動画の出力先RenderTexture

    private void Start()
    {
        if(rawImage.texture != null) { return; }
        // 初期状態でRawImageにマテリアルを設定
        if (placeholderMaterial != null)
        {
            rawImage.material = placeholderMaterial;
        }

        // RenderTextureをVideoPlayerに設定
        videoPlayer.targetTexture = renderTexture;

        // 再生準備を開始
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += OnPrepareCompleted;
    }

    private void OnPrepareCompleted(VideoPlayer vp)
    {
        // RawImageにRenderTextureを適用
        rawImage.texture = renderTexture;
        rawImage.material = null; // Materialをクリアして通常のテクスチャを使用

        // 動画再生開始
        videoPlayer.Play();
    }
}
