using UnityEngine;
using UnityEngine.SceneManagement;  // �V�[���J�ڂ��g�����߂ɕK�v

public class SceneLoader : MonoBehaviour
{
    // ���̃V�[���ɑJ�ڂ��郁�\�b�h
    public void LoadNextScene()
    {
        // ���̃V�[���i�V�[������ύX���Ă��������j
        SceneManager.LoadScene("PachinkoScene");  // PachinkoScene�ɑJ��
    }
}
