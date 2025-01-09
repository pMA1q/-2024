using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_CameraMotion : MonoBehaviour
{
    [SerializeField] private Vector3 goalPosition; // �S�[���n�_�̈ʒu
    [SerializeField] private Vector3 goalRotation; // �S�[���n�_�ł̉�]�l (Euler�p)
    [SerializeField] private float duration = 2.0f; // �S�[���n�_�ɍs���܂ł̎���

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Quaternion goalQuaternion;
    private float elapsedTime = 0f;

    void Start()
    {
        // �����ʒu�Ɖ�]���L�^
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // �S�[���n�_�̉�]�l��Quaternion�ɕϊ�
        goalQuaternion = Quaternion.Euler(goalRotation);
    }

    void Update()
    {
        if (elapsedTime < duration)
        {
            // �o�ߎ��Ԃ��X�V
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            // �ʒu�Ɖ�]����
            transform.position = Vector3.Lerp(initialPosition, goalPosition, t);
            transform.rotation = Quaternion.Slerp(initialRotation, goalQuaternion, t);
        }
        else
        {
            // �S�[���n�_�ɓ��B������X�N���v�g��j��
            transform.position = goalPosition;
            transform.rotation = goalQuaternion;
            Destroy(this);
        }
    }
}
