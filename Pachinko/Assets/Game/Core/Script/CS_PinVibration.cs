using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PinVibration : MonoBehaviour
{
    private Collider pinCollider; // ������ Collider�iMesh �Ƃ͕ʂ̃I�u�W�F�N�g�ł�OK�j
    private float vibrationStrength = 0.2f; // �U���̋���
    private float vibrationSpeed = 5; // �U���̑���

    private Vector3 initialPosition; // Collider �̏����ʒu

    void Start()
    {
        if (pinCollider == null)
        {
            pinCollider = GetComponent<Collider>();
        }

        // Collider �̏����ʒu��ۑ�
        initialPosition = pinCollider.transform.position;
    }

    void FixedUpdate()
    {
        // �U�����v�Z
        float offset = Mathf.Cos(Time.time * vibrationSpeed) * vibrationStrength;

        // Collider �̈ʒu���X�V
        pinCollider.transform.position = initialPosition + new Vector3(0, offset, 0);
    }
}
