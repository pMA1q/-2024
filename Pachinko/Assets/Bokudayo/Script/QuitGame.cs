using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // �Q�[���I���������s�����\�b�h
    public void QuitApplication()
    {
        // �Q�[�����I��
        Debug.Log("�Q�[�����I�����܂�");
        Application.Quit();

        // �G�f�B�^�Ńe�X�g���Ă���ꍇ�A��~������ǉ�
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
