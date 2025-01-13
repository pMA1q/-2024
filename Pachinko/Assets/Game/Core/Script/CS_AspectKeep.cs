using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CS_AspectKeep : MonoBehaviour
{
    [SerializeField]
    private Camera targetCamera; //対象とするカメラ

    [SerializeField]
    private Vector2 aspectVec = new Vector2(1080f, 1920f); //目的解像度

    int width;
    int height;
    private void Awake()
    {
        targetCamera = GetComponent<Camera>();
        width = Screen.width;
        height = Screen.height;
        Screen.SetResolution(width, height, true);
    }

    void Update()
    {
        if (Screen.width != width || Screen.height != height)
        {
            Screen.SetResolution(width, height, true);
        }
        var screenAspect = Screen.width / (float)Screen.height;
        Debug.Log("width" + Screen.width);
        Debug.Log("height" + Screen.height);
        Debug.Log("screenAspect" + screenAspect);
        var targetAspect = aspectVec.x / aspectVec.y; //目的のアスペクト比

        var magRate = targetAspect / screenAspect; //目的アスペクト比にするための倍率
        var viewportRect = new Rect(0, 0, 1, 1); //Viewport初期値でRectを作成

        if (magRate < 1)
        {
            viewportRect.width = magRate; //使用する横幅を変更
            viewportRect.x = 0.5f - viewportRect.width * 0.5f;//中央寄せ
        }
        else
        {
            viewportRect.height = 1 / magRate; //使用する縦幅を変更
            viewportRect.y = 0.5f - viewportRect.height * 0.5f;//中央余生
        }

        targetCamera.rect = viewportRect; //カメラのViewportに適用
    }
}
