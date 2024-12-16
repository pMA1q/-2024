using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_LeftAttackCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // �Փ˂����I�u�W�F�N�g�� "PachinkoBall" �̃^�O�������m�F
        if (collision.gameObject.CompareTag("Pachinko Ball"))
        {
            // �Փ˂����I�u�W�F�N�g�̃��W�b�h�{�f�B���擾
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // �����}�e���A���̃o�E���h��0�ɂ���
                Collider collider = collision.gameObject.GetComponent<Collider>();
                if (collider != null && collider.material != null)
                {
                    collider.material.bounciness = 0f; // �����W����0��
                    collider.material.bounceCombine = PhysicMaterialCombine.Minimum; // �����̌v�Z���ŏ��l�ɐݒ�
                }
            }
        }
    }
}
