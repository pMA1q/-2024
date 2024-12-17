using UnityEngine;
using UnityEngine.SceneManagement;

public class ResolutionManager : MonoBehaviour
{
    // 初期解像度
    private Vector2 originalResolution = new Vector2(1920, 1080);  // 通常時の解像度（例: 1920x1080）
    private Vector2 titleResolution = new Vector2(800, 450);       // タイトル画面時の解像度（800x450）

    void OnEnable()
    {
        // シーンが読み込まれたときに解像度を設定
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // イベントを解除
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // シーンが読み込まれる度に呼ばれる
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // タイトルシーンの場合は800x450に設定
        if (scene.name == "TitleScene")  // タイトルシーンの名前を指定
        {
            Screen.SetResolution((int)titleResolution.x, (int)titleResolution.y, false);  // ウィンドウモード
        }
        else
        {
            // その他のシーンでは元の解像度に戻す
            Screen.SetResolution((int)originalResolution.x, (int)originalResolution.y, true);  // フルスクリーン
        }
    }
}
