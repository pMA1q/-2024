using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTrigger : MonoBehaviour
{
    // �I�u�W�F�N�g���g���K�[�ɓ��������̏���
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�I�u�W�F�N�g��Plane�ɓ�����");
    }

    // �I�u�W�F�N�g���g���K�[����o�����̏���
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("�I�u�W�F�N�g��Plane����o��");
    }
}
