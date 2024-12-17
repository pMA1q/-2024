using UnityEngine;
using UnityEngine.SceneManagement;

public class ResolutionManager : MonoBehaviour
{
    // �����𑜓x
    private Vector2 originalResolution = new Vector2(1920, 1080);  // �ʏ펞�̉𑜓x�i��: 1920x1080�j
    private Vector2 titleResolution = new Vector2(800, 450);       // �^�C�g����ʎ��̉𑜓x�i800x450�j

    void OnEnable()
    {
        // �V�[�����ǂݍ��܂ꂽ�Ƃ��ɉ𑜓x��ݒ�
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // �C�x���g������
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // �V�[�����ǂݍ��܂��x�ɌĂ΂��
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �^�C�g���V�[���̏ꍇ��800x450�ɐݒ�
        if (scene.name == "TitleScene")  // �^�C�g���V�[���̖��O���w��
        {
            Screen.SetResolution((int)titleResolution.x, (int)titleResolution.y, false);  // �E�B���h�E���[�h
        }
        else
        {
            // ���̑��̃V�[���ł͌��̉𑜓x�ɖ߂�
            Screen.SetResolution((int)originalResolution.x, (int)originalResolution.y, true);  // �t���X�N���[��
        }
    }
}
