using UnityEngine;

public class CameraAspect : MonoBehaviour
{
    void Start()
    {
        // アスペクト比を設定（16:9の例）
        Camera.main.aspect = 5.0f / 9.0f;  // 横幅16, 高さ9の比率
    }
}
