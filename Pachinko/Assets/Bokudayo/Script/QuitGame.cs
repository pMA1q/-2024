using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // ゲーム終了処理を行うメソッド
    public void QuitApplication()
    {
        // ゲームを終了
        Debug.Log("ゲームを終了します");
        Application.Quit();

        // エディタでテストしている場合、停止処理を追加
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
