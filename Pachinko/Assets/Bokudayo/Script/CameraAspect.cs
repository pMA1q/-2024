using UnityEngine;

public class CameraAspect : MonoBehaviour
{
    void Start()
    {
        // �A�X�y�N�g���ݒ�i16:9�̗�j
        Camera.main.aspect = 5.0f / 9.0f;  // ����16, ����9�̔䗦
    }
}
