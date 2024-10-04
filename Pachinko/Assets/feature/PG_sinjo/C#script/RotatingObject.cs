using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject windmill; // Windmill�I�u�W�F�N�g
    public GameObject cylinder; // Cylinder�I�u�W�F�N�g

    void Start()
    {
        // Windmill��Cylinder��Collider���擾
        Collider windmillCollider = windmill.GetComponent<Collider>();
        Collider cylinderCollider = cylinder.GetComponent<Collider>();

        // 2��Collider�Ԃ̏Փ˂𖳎�����
        Physics.IgnoreCollision(windmillCollider, cylinderCollider);
        rb = GetComponent<Rigidbody>();
    }

    // �Փ˂������������ɌĂ΂��֐�
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pachinko Ball"))
        {
            // �Փ˓_�ɉ����ăg���N�i��]�́j��������
            Vector3 impactPoint = collision.contacts[0].point;
            Vector3 direction = transform.position - impactPoint; // �Փ˓_����I�u�W�F�N�g���S�ւ̃x�N�g��
            rb.AddTorque(direction * 100f); // ��]�͂�������
            //Debug.Log("���ԏՓ�");
        }
    }
}
