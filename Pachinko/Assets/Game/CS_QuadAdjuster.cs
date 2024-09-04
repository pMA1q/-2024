using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_QuadAdjuster : MonoBehaviour
{
    [SerializeField,Header("�T�u�J����")]
    private Camera mSubCamera; 
    // Start is called before the first frame update
    void Start()
    {
        // �J�����̋���
        float distance = mSubCamera.transform.position.z;

        // �J�����̎���p
        float fov = mSubCamera.fieldOfView;
        float aspect = mSubCamera.aspect;

        // �J�����̍����̃n�[�t�T�C�Y
        float height = 2.0f * Mathf.Tan(Mathf.Deg2Rad * fov / 2.0f) * Mathf.Abs(distance);
        // �J�����̕��̃n�[�t�T�C�Y
        float width = height * aspect;

        // Quad�̃T�C�Y��ݒ�
        transform.localScale = new Vector3(width, height, 1.0f);

        // Quad�̈ʒu���J�����̑O���ɐݒ�
        transform.position = mSubCamera.transform.position + mSubCamera.transform.forward * Mathf.Abs(distance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
