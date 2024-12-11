using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ShojiController : MonoBehaviour
{
    public Transform shojiLeft;  // ���̏�q
    public Transform shojiRight; // �E�̏�q

    public float openDistance = 2.0f;  // �J������
    public float openSpeed = 2.0f;     // �J������
    private bool isOpening = false;   // �J���Ă��邩�̏��

    private Vector3 leftStartPos;
    private Vector3 rightStartPos;

    void Start()
    {
        // �����ʒu���L�^
        leftStartPos = shojiLeft.position;
        rightStartPos = shojiRight.position;
    }

    void Update()
    {
        // �X�y�[�X�L�[�ŊJ��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isOpening)
            {
                OpenShoji();
            }
        }

        // �J���Ă���Ƃ��̈ړ�����
        if (isOpening)
        {
            shojiLeft.position = Vector3.Lerp(shojiLeft.position, leftStartPos + Vector3.left * openDistance, Time.deltaTime * openSpeed);
            shojiRight.position = Vector3.Lerp(shojiRight.position, rightStartPos + Vector3.right * openDistance, Time.deltaTime * openSpeed);
        }
    }

    // ��q���J�����\�b�h
    public void OpenShoji()
    {
        isOpening = true;
    }
}
