//---------------------------------
//OnCollision�Ńp�`���R�{�[�����Փˎ��̏���
//�S���ҁF����
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_LeftAttackTrigger : MonoBehaviour
{
    // �͂̑傫����ݒ肷��
    [SerializeField]
    private float forceAmount = 10f;

    // �͂�������������
    public enum ForceDirection
    {
        Forward,   // ���s���� (Z+)
        Backward,  // ��O���� (Z-)
        Left,      // ������ (X-)
        Right,     // �E���� (X+)
        Up         // ����� (Y+)
    }

    // �͂����������
    [SerializeField]
    private ForceDirection forceDirection = ForceDirection.Forward;

    // �g���K�[�ɉ������G�ꂽ�Ƃ��ɌĂ΂�郁�\�b�h
    private void OnTriggerStay (Collider other)
    {
        // �G�ꂽ�I�u�W�F�N�g�̃^�O��"A"�̏ꍇ
        if (other.CompareTag("Pachinko Ball"))
        {
            // Rigidbody�R���|�[�l���g���擾����
            Rigidbody rb = other.GetComponent<Rigidbody>();

            // Rigidbody�����݂���ꍇ�̂ݗ͂�������
            if (rb != null)
            {
                // �͂̕��������肷��
                Vector3 force = Vector3.zero;

                switch (forceDirection)
                {
                    case ForceDirection.Forward:
                       
                        force = Vector3.forward * forceAmount;
                        
                        break;
                    case ForceDirection.Backward:
                        force = Vector3.back * forceAmount;
                        break;
                    case ForceDirection.Left:
                        force = Vector3.left * forceAmount;
                        break;
                    case ForceDirection.Right:
                        force = Vector3.right * forceAmount;
                        break;
                    case ForceDirection.Up:
                        force = Vector3.up * forceAmount;
                        break;
                }
                
                // �w�肵�������ɗ͂�������
                //rb.AddForce(force, ForceMode.Impulse);
                rb.velocity = force;
            }
        }
    }
}

