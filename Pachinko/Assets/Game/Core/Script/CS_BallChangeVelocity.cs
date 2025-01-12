using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BallChangeVelocity : MonoBehaviour
{
    [SerializeField]
    private float mMin = 0.5f;
    [SerializeField]
    private float mMax = 1.0f;

    // �{�[���̈ړ��������w�肷�邽�߂�enum
    public enum Direction
    {
        NONE,
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    // Inspector�ŕ�����ݒ�
    [SerializeField]
    private Direction mDirection = Direction.NONE;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pachinko Ball")) // �{�[����Tag��ݒ�
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // �����_���Ȋp�x�ŗ͂�������
                float angle = Random.Range(mMin, mMax);

                Vector3 velocity = rb.velocity;

                // �w�肳�ꂽ�����ɉ����đ��x��ύX
                switch (mDirection)
                {
                    case Direction.NONE:
                        velocity *= angle;
                        break;
                    case Direction.UP:
                        velocity = new Vector3(velocity.x, Mathf.Abs(velocity.y) * angle, velocity.z);
                        break;
                    case Direction.DOWN:
                        velocity = new Vector3(velocity.x, -Mathf.Abs(velocity.y) * angle, velocity.z);
                        break;
                    case Direction.LEFT:
                        velocity = new Vector3(-Mathf.Abs(velocity.x) * angle, velocity.y, velocity.z);
                        break;
                    case Direction.RIGHT:
                        velocity = new Vector3(Mathf.Abs(velocity.x) * angle, velocity.y, velocity.z);
                        break;
                        
                }

                // �V�������x��K�p
                rb.velocity = velocity;
            }
        }
    }
}
