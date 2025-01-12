using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteAlways]
public class CS_AspectKeep : MonoBehaviour
{
    [SerializeField]
    private Camera targetCamera; //対象とするカメラ

    [SerializeField]
    private Vector2 aspectVec = new Vector2( 1080f,1920f ); //目的解像度

    private void OnEnable()
    {
        targetCamera = GetComponent<Camera>();
    }

    void Update()
    {
        var screenAspect = Display.main.systemWidth / (float)Display.main.systemHeight; //画面のアスペクト比
        Debug.Log("width" + Display.main.systemWidth);
        Debug.Log("height" + Display.main.systemHeight);
        Debug.Log("screenAspect" + screenAspect);
        var targetAspect = aspectVec.x / aspectVec.y; //目的のアスペクト比
        Debug.Log("targetAspect" + targetAspect);

        var magRate = targetAspect / screenAspect; //目的アスペクト比にするための倍率
        var viewportRect = new Rect(0, 0, 1, 1); //Viewport初期値でRectを作成

        Debug.Log("倍率" + magRate);

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
        Destroy(this);
    }
}
