using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BallChangeVelocity : MonoBehaviour
{
    [SerializeField]
    private float mMin = 0.5f;
    [SerializeField]
    private float mMax = 1.0f;

    // ボールの移動方向を指定するためのenum
    public enum Direction
    {
        NONE,
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    // Inspectorで方向を設定
    [SerializeField]
    private Direction mDirection = Direction.NONE;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pachinko Ball")) // ボールにTagを設定
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // ランダムな角度で力を加える
                float angle = Random.Range(mMin, mMax);

                Vector3 velocity = rb.velocity;

                // 指定された方向に応じて速度を変更
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

                // 新しい速度を適用
                rb.velocity = velocity;
            }
        }
    }
}
