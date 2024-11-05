using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Windmill : MonoBehaviour
{
    private Rigidbody rb;
   
    void Start()
    {
        // Rigidbody���擾
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Pachinko Ball�Ƃ̏Փ˂��`�F�b�N
        if (collision.gameObject.CompareTag("Pachinko Ball"))
        {
            // �Փ˓_�ɉ����ăg���N�i��]�́j��������
            Vector3 impactPoint = collision.contacts[0].point;
            Vector3 direction = transform.position - impactPoint;
            rb.AddTorque(direction * 100f);
        }
    }
}
