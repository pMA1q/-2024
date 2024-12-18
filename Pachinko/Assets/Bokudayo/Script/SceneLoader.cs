using UnityEngine;
using UnityEngine.SceneManagement;  // シーン遷移を使うために必要

public class SceneLoader : MonoBehaviour
{
    // 次のシーンに遷移するメソッド
    public void LoadNextScene()
    {
        // 次のシーン（シーン名を変更してください）
        SceneManager.LoadScene("PachinkoScene");  // PachinkoSceneに遷移
    }
}
