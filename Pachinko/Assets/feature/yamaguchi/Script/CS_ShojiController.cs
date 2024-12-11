using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ShojiController : MonoBehaviour
{
    public Transform shojiLeft;  // ���̏�q
    public Transform shojiRight; // �E�̏�q

    public float openDistance = 2.0f;  // ��q���J�������iZ�������j
    public float openSpeed = 2.0f;     // �J������
    private bool isOpening = false;   // ���݊J���Ă��邩�ǂ���

    private Vector3 leftStartPos;     // ����q�̏����ʒu
    private Vector3 rightStartPos;    // �E��q�̏����ʒu
    private Vector3 leftOpenPos;      // ����q�̊J�����ʒu
    private Vector3 rightOpenPos;     // �E��q�̊J�����ʒu

    void Start()
    {
        // �����ʒu���L�^
        leftStartPos = shojiLeft.position;
        rightStartPos = shojiRight.position;

        // �J�������̈ʒu���v�Z
        leftOpenPos = leftStartPos + new Vector3(0, 0, openDistance);
        rightOpenPos = rightStartPos + new Vector3(0, 0, -openDistance);
    }

    void Update()
    {
        // �X�y�[�X�L�[�ŊJ��؂�ւ���
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isOpening = !isOpening;  // ��Ԃ�؂�ւ�
        }

        // ��Ԃɉ����Ĉړ�
        if (isOpening)
        {
            // �J������
            shojiLeft.position = Vector3.Lerp(shojiLeft.position, leftOpenPos, Time.deltaTime * openSpeed);
            shojiRight.position = Vector3.Lerp(shojiRight.position, rightOpenPos, Time.deltaTime * openSpeed);
        }
        else
        {
            // ���鏈��
            shojiLeft.position = Vector3.Lerp(shojiLeft.position, leftStartPos, Time.deltaTime * openSpeed);
            shojiRight.position = Vector3.Lerp(shojiRight.position, rightStartPos, Time.deltaTime * openSpeed);
        }
    }
}
